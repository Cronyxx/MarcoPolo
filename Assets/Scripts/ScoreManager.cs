using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class ScoreManager : MonoBehaviour
{
    private MarcoPoloGameManager GM;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<MarcoPoloGameManager>();
        
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            player.SetScore(0);
        }
    }

    void AddScore(Player player, int score)
    {
        int currScore = player.GetScore();

        player.SetScore(currScore + score);
    }

    public void CalcScore(Player hunter, Player hunted = null, bool roundEnded = false)
    {
        Debug.Log("Score calculating!");
        
        if(GM.roundInProgress && !roundEnded)
        {
            Debug.Log("Game is in progress!");
            int hunterScore = (int) (GM.roundTimer) + 30;
            int huntedScore = (int) (MarcoPoloGame.ROUND_TIME - GM.roundTimer);

            AddScore(hunter, hunterScore);
            AddScore(hunted, huntedScore);
        }
        else if(roundEnded)
        {
            Debug.Log("Round has ended!");
            bool hunterWin = true;
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                if(!(bool) player.CustomProperties[MarcoPoloGame.IS_HUNTER]
                    && (bool) player.CustomProperties[MarcoPoloGame.IS_ALIVE])
                {
                    hunterWin = false;
                    break;
                }
            }
            
            if(hunterWin)
            {
                Debug.Log("The hunter wins! Adding 200 points.");
                AddScore(hunter, 200);
            }
            else 
            {
                Debug.Log("Hunter loses! Players alive receive 100 points each.");
                foreach(Player player in PhotonNetwork.PlayerList)
                {
                    if(!(bool) player.CustomProperties[MarcoPoloGame.IS_HUNTER]
                        && (bool) player.CustomProperties[MarcoPoloGame.IS_ALIVE])
                    {
                        AddScore(player, 100);
                    }
                }
            }
        }
    }


}
