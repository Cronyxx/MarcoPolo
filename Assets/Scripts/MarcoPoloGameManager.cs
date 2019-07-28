using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MarcoPoloGameManager : MonoBehaviourPunCallbacks
{
    public Text infoText, roundTimerText, masterClientText, hunterText;
    private PhotonView PV;
    private ScoreManager SM;
    private LightManager LM;
    private CharacterInit CI;

    private UnityEngine.Object[] SkillPickupsPrefabs;

    public bool gameInProgress, roundInProgress;

    public GameObject playArea;

    public float roundTimer;

    int hunterId;

    int playersAlive;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        SM = GetComponent<ScoreManager>();

        if(PhotonNetwork.IsMasterClient) 
        {
            InitRoom();
            masterClientText.text = "MASTER";
            hunterId = -1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.PlayerList.Length >= MarcoPoloGame.PLAYERS_IN_MATCH 
                    && !roundInProgress
                    && !gameInProgress)
            {
                gameInProgress = true;
                PV.RPC("RPC_StartPreRound", RpcTarget.All);
            }
        }

    }

    #region Game Logic Functions
    // (MASTER ONLY) This function will initialise the room settings, such as the rounds progress
    void InitRoom() 
    {
        Hashtable roomProps = new Hashtable() 
        {
            { MarcoPoloGame.ROUNDS_PROGRESS, 0 }
        };

        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProps);

        // get gameobjects for skills
        SkillPickupsPrefabs = new UnityEngine.Object[MarcoPoloGame.SKILL_COUNT];
        SkillPickupsPrefabs = Resources.LoadAll("PhotonPrefabs/Skills/SkillPickupPrefabs");

        StartCoroutine(SpawnPowerUps());
    }

    // This function initiate the pre round (which leads into the round). It gives a 10s timer before each round begins for players to prepare.
    // maybe should destroy cooroutine after preround
    private IEnumerator StartPreRound()
    {
        LM = ((GameObject)PhotonNetwork.LocalPlayer.TagObject).GetComponent<PhotonPlayer>().myAvatar.GetComponentInChildren<LightManager>();
        LM.SetLightAll();
        
        Debug.Log("Conditions for a game are met! Starting round...");
        float timer = (float) MarcoPoloGame.PRE_ROUND_TIME;
        
        while(timer >= 0.0f) 
        {
            infoText.text = string.Format("Game begins in {0}", (int) timer);

            yield return new WaitForEndOfFrame();

            timer -= Time.deltaTime;
        }
        
        // Once the pre-game timer has reached 0, our master client will initiate the game
        if(PhotonNetwork.IsMasterClient && timer <= 0.0f) 
        {
            StartRound();
        }

    }
    
    // (MASTER ONLY) This function starts each round, initialising the local player & also selecting the hunter
    void StartRound()
    {
        Debug.Log("Round is starting!");

        int roundsProgress = (int) PhotonNetwork.CurrentRoom.CustomProperties[MarcoPoloGame.ROUNDS_PROGRESS];
        roundsProgress ++;
        Hashtable roomProps = new Hashtable() 
        {
            { MarcoPoloGame.ROUNDS_PROGRESS, roundsProgress }
        };

        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProps);
        
        roundInProgress = true;

        PV.RPC("RPC_SetInfoText", RpcTarget.All, string.Format("Round {0} of {1}", roundsProgress, MarcoPoloGame.ROUNDS_PER_GAME));

        playersAlive = MarcoPoloGame.PLAYERS_IN_MATCH - 1;

        SelectHunter();

        PV.RPC("RPC_StartRoundTimer", RpcTarget.All);
    }

    // This function ends each round, and checks if the game is over. If not, it continues with the next pre-round and the game continues.
    void EndRound()
    {
        // TODO: End round settings like display round end screen, check if the game has ended etc.
        Debug.Log("Round is ending!");
        StopAllCoroutines();
        roundInProgress = false;

        SM.CalcScore(PhotonNetwork.PlayerList[hunterId], PhotonNetwork.LocalPlayer, true);

        if(IsGameOver()) 
        {
            Debug.Log("Game is over! Please get out.");
            LM.SetLightAll();
            
            PV.RPC("RPC_SetInfoText", RpcTarget.All, "Game over! Leave now.");
        } 
        else 
        {
            
            Debug.Log("Game is not over! We'll proceed with the next round.");
            StartCoroutine(StartPreRound());
        }
        
    }

    // Function to start the round timer
    private IEnumerator StartRoundTimer() 
    {   
        roundTimer = (float) MarcoPoloGame.ROUND_TIME;

        while(roundTimer >= 0) 
        {
            roundTimerText.text = ((int) roundTimer).ToString();
            yield return new WaitForEndOfFrame();

            roundTimer -= Time.deltaTime;
        }

        if(roundTimer <= 0.0f) 
        {
            EndRound();

        }
    }

    // Function that returns a boolean; checks if the round is over
    bool IsRoundOver() {
        // TODO: must check if the round has ended 
        
        if(playersAlive == 0) 
        {
            return true;
        } else 
        {
            return false;    
        }
    }

    // Function to check if the game is over (hit max no. of rounds)
    bool IsGameOver() {
        int roundsProgress = (int) PhotonNetwork.CurrentRoom.CustomProperties[MarcoPoloGame.ROUNDS_PROGRESS];
        if(roundsProgress == MarcoPoloGame.ROUNDS_PER_GAME) 
        {
            return true;
        }
        
        return false;
    }

    #endregion

    #region Player Related Functions
    

    // Function to kill the player: just sets the player's alive settings to false
    void KillPlayer() 
    {
        Hashtable deadProps = new Hashtable
        {
            { MarcoPoloGame.IS_ALIVE, false }
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(deadProps);
        playersAlive--;
    }

    // Function to handle the case where the current player has come into contact with a hunter
    public void HunterTouchEvent()
    {
        if(roundInProgress) 
        {
            Debug.Log(PhotonNetwork.LocalPlayer + " touched the hunter!");

            // If the player is alive, kill him
            if ((bool)PhotonNetwork.LocalPlayer.CustomProperties[MarcoPoloGame.IS_ALIVE])
            {
                Debug.Log("Killing the player since he's alive...");
                KillPlayer();
            }

            if (IsRoundOver())
            {
                Debug.Log("Round is over!");
                PV.RPC("RPC_EndRound", RpcTarget.All);
            }
        }
    }

    // Randomly selects the hunter from the list of available players.
    // TODO: Add in pseudo-random selection AKA make sure previously selected ppl haven't been selected again
    void SelectHunter() 
    {
        if(PhotonNetwork.IsMasterClient) {
            hunterId += 1;
            
            PV.RPC("RPC_SetHunterId", RpcTarget.All, hunterId);
        }
    }

    

    #endregion

    IEnumerator SpawnPowerUps()
    {
        float x = playArea.transform.position.x;
        float y = playArea.transform.position.y;

        while(true)
        {
            yield return new WaitForSeconds(MarcoPoloGame.SKILL_SPAWN_INTERVAL);
            PV.RPC("RPC_SpawnPowerUp", RpcTarget.All, x + UnityEngine.Random.Range(-MarcoPoloGame.PLAY_AREA_WIDTH / 2, MarcoPoloGame.PLAY_AREA_WIDTH / 2), 
                y + UnityEngine.Random.Range(-MarcoPoloGame.PLAY_AREA_HEIGHT / 4, MarcoPoloGame.PLAY_AREA_HEIGHT / 2));

            
        }
        
    }
    #region RPC Functions
    [PunRPC]
    void RPC_SetInfoText(string text)
    {
        infoText.text = text;
    }

    [PunRPC]
    void RPC_SetHunterId(int newHunterId)
    {
        this.hunterId = newHunterId;
        hunterText.text = "Hunter is " + PhotonNetwork.PlayerList[newHunterId].NickName;
        CI = ((GameObject)PhotonNetwork.LocalPlayer.TagObject).GetComponentInChildren<CharacterInit>();
        CI.SetCharacter(newHunterId);
    }

    [PunRPC]
    void RPC_StartRoundTimer() 
    {
        StartCoroutine(StartRoundTimer());
    }

    [PunRPC]
    void RPC_StartPreRound() 
    {
        StartCoroutine(StartPreRound());
    }

    [PunRPC]
    void RPC_EndRound() 
    {
        EndRound();
    }
    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
    {
        // on hunter update init the timer
        if(changedProps.ContainsKey(MarcoPoloGame.IS_HUNTER)) {
            
        }
    }

    [PunRPC]
    void RPC_SpawnPowerUp(float x, float y) 
    {
        if(!(bool) PhotonNetwork.LocalPlayer.CustomProperties[MarcoPoloGame.IS_HUNTER])
        {
            GameObject temp = (GameObject) Instantiate(
                SkillPickupsPrefabs[UnityEngine.Random.Range(0, SkillPickupsPrefabs.Length)], 
                new Vector3(x, 
                            y, 
                            playArea.transform.position.z), 
                Quaternion.identity, 
                playArea.transform);
        }
    }

    #endregion
}
