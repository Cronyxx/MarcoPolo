using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Photon.Pun;

public class PlayerOverviewPanel : MonoBehaviourPunCallbacks
{
    public GameObject PlayerOverviewEntryPrefab;
    private Dictionary<int, GameObject> playerListEntries;


    void Awake() 
    {
        playerListEntries = new Dictionary<int, GameObject>();
        
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject entry = Instantiate(PlayerOverviewEntryPrefab);
            entry.transform.SetParent(gameObject.transform);

            entry.transform.position = gameObject.transform.position;
            entry.transform.GetChild(0).GetComponent<Text>().text = p.NickName;
            entry.transform.GetChild(1).GetComponent<Text>().text = p.GetScore().ToString();

            playerListEntries.Add(p.ActorNumber, entry);
        }
    }

    #region PUN CALLBACKS

        public override void OnPlayerEnteredRoom(Player newPlayer) 
        {
            GameObject entry = Instantiate(PlayerOverviewEntryPrefab);
            entry.transform.SetParent(gameObject.transform);

            entry.transform.position = gameObject.transform.position;
            entry.transform.GetChild(0).GetComponent<Text>().text = newPlayer.NickName;
            entry.transform.GetChild(1).GetComponent<Text>().text = newPlayer.GetScore().ToString();

            playerListEntries.Add(newPlayer.ActorNumber, entry);
        }
        
        
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Destroy(playerListEntries[otherPlayer.ActorNumber].gameObject);
            playerListEntries.Remove(otherPlayer.ActorNumber);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            GameObject entry;
            if (playerListEntries.TryGetValue(targetPlayer.ActorNumber, out entry))
            {
                entry.transform.GetChild(1).GetComponent<Text>().text = targetPlayer.GetScore().ToString();
            }
        }

        #endregion
}
