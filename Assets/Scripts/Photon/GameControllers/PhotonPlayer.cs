using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using System.IO;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonPlayer : MonoBehaviour, IPunInstantiateMagicCallback
{
    private PhotonView PV;
    public GameObject myAvatar;
    private GameObject PlayerLight;
    private Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawnerPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
        if (PV.IsMine)
        {

            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), GameSetup.GS.spawnPoints[spawnerPicker].position,
                GameSetup.GS.spawnPoints[spawnerPicker].rotation, 0);

            myAvatar.tag = "Player";
        
            InitLight();
            InitPlayer();
        }
    }

    // This function initialises the player's settings with it being alive and not the hunter
    void InitPlayer()
    {
        Hashtable initProps = new Hashtable
        {
            { MarcoPoloGame.IS_ALIVE, true },
            { MarcoPoloGame.IS_HUNTER, false }
        };
        
        PhotonNetwork.LocalPlayer.SetCustomProperties(initProps);
    }

    public void InitLight() 
    {
        PlayerLight = new GameObject("Player Light");
        PlayerLight.transform.position = new Vector3(myAvatar.transform.position.x, myAvatar.transform.position.y, -4.0f);
        PlayerLight.transform.SetParent(myAvatar.transform);
        myLight = PlayerLight.AddComponent<Light>();
        myLight.type = LightType.Directional;
        myLight.intensity = MarcoPoloGame.LIGHT_INT_NOT_PLAYING;    
    }

    public void SetLightAll()
    {
        if (PV.IsMine) 
        {
            myLight.type = LightType.Directional;
            myLight.intensity = MarcoPoloGame.LIGHT_INT_NOT_PLAYING;    
        }
    }

    public void SetLightHunted() 
    {
        if (PV.IsMine) 
        {
            myLight.type = LightType.Spot;
            myLight.intensity = MarcoPoloGame.LIGHT_INT_HUNTED;
            myLight.spotAngle = 90.0f;
        }
    }

    public void SetLightHunter()
    {
        if (PV.IsMine) 
        {
            myLight.intensity = MarcoPoloGame.LIGHT_INT_HUNTER;
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info) 
    {
        info.Sender.TagObject = this.gameObject;
    }
}
