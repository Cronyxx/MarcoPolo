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
            // For when the hunter catches the player
            Debug.Log("Game is in progress!");
            int hunterScore = (int) (GM.roundTimer);
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
                AddScore(hunter, PhotonNetwork.PlayerList.Length * 10);
            }
            else 
            {
                foreach(Player player in PhotonNetwork.PlayerList)
                {
                    if(!(bool) player.CustomProperties[MarcoPoloGame.IS_HUNTER]
                        && (bool) player.CustomProperties[MarcoPoloGame.IS_ALIVE])
                    {
                        AddScore(player, MarcoPoloGame.ROUND_TIME + 10);
                    }
                }
            }
        }
    }


}
