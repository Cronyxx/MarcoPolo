using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoom room;
    public PhotonView PV;

    public bool IsGameLoaded;
    public int currentScene;

    //Player info
    Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;

    public int playerInGame;

    //Delayed start
    private bool readyToWaitForPlayers;
    private bool readyToStart;
    public float startingTime;
    private float currWaitTimeForPlayers;
    private float currWaitTimeToStartGameWhenRoomFull;
    private float timeToStart;

    private void Awake()
    {
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if (PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        readyToWaitForPlayers = false;
        readyToStart = false;
        currWaitTimeForPlayers = startingTime;
        currWaitTimeToStartGameWhenRoomFull = 4;
        timeToStart = startingTime;
    }

    void Update()
    {
        if (MultiplayerSetting.multiplayerSetting.delayStart)
        {
            if (playersInRoom == 1)
            {
                RestartTimer();
            }
            if (!IsGameLoaded)
            {
                if (readyToStart)
                {
                    currWaitTimeToStartGameWhenRoomFull -= Time.deltaTime;
                    currWaitTimeForPlayers = currWaitTimeToStartGameWhenRoomFull;
                    timeToStart = currWaitTimeToStartGameWhenRoomFull;
                }
                else if (readyToWaitForPlayers)
                {
                    currWaitTimeForPlayers -= Time.deltaTime;
                    timeToStart = currWaitTimeForPlayers;
                }
                Debug.Log("Display time to start to the players:" + timeToStart);
                if (timeToStart <= 0)
                {
                    StartGame();
                }

            }
        }
    }


    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == MultiplayerSetting.multiplayerSetting.multiplayerScene)
        {
            IsGameLoaded = true;

            if (MultiplayerSetting.multiplayerSetting.delayStart)
            {
                PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
            else
            {
                RPC_CreatePlayer();
            }
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined room");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;

        if (MultiplayerSetting.multiplayerSetting.delayStart)
        {
            delayStartSetup();
        }
        else
        {
            StartGame();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new player entered room.");
        playersInRoom++;
        if (MultiplayerSetting.multiplayerSetting.delayStart)
        {
            delayStartSetup();
        }
    }

    private void delayStartSetup()
    {
        Debug.Log("Number of playes in room out of max players possible (" + playersInRoom + ":"
                + MultiplayerSetting.multiplayerSetting.maxPlayers + ")"); //can replay with UI elements
        if (playersInRoom > 1)
        {
            readyToWaitForPlayers = true;
        }
        if (playersInRoom == MultiplayerSetting.multiplayerSetting.maxPlayers)
        {
            readyToStart = true;
            if (!PhotonNetwork.IsMasterClient)
                return;
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    void StartGame()
    {
        IsGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        if (MultiplayerSetting.multiplayerSetting.delayStart)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        PhotonNetwork.LoadLevel(MultiplayerSetting.multiplayerSetting.multiplayerScene);
    } 

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + " has left the game");
        playersInRoom--;
    }

    private void RestartTimer()
    {
        currWaitTimeForPlayers = startingTime;
        timeToStart = startingTime;
        currWaitTimeToStartGameWhenRoomFull = 4;
        readyToWaitForPlayers = false;
        readyToStart = false;
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        playerInGame++;
        if (playerInGame == PhotonNetwork.PlayerList.Length)
        {
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        GameObject player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
        player.tag = "Player";
    }
}
