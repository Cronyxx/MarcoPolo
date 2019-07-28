using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonPlayer : MonoBehaviour, IPunInstantiateMagicCallback
{
    private PhotonView PV;
    public GameObject myAvatar;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawnerPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), GameSetup.GS.spawnPoints[spawnerPicker].position,
                GameSetup.GS.spawnPoints[spawnerPicker].rotation, 0);
            myAvatar.transform.parent = this.gameObject.transform;

            myAvatar.tag = "Player";
            InitPlayer();
        }
    }

    // This function initialises the player's settings with it being alive and not the hunter
    void InitPlayer()
    {
        Hashtable initProps = new Hashtable
        {
            { MarcoPoloGame.IS_ALIVE, null },
            { MarcoPoloGame.IS_HUNTER, null }
        };
        
        PhotonNetwork.LocalPlayer.SetCustomProperties(initProps);
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info) 
    {
        info.Sender.TagObject = this.gameObject;
    }
}
