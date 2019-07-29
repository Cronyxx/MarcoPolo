using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CharacterInit : MonoBehaviour
{
    public GameObject HuntedPulse;
    public GameObject HunterPulse;
    private LightManager LM;
    // Start is called before the first frame update
    void OnEnable()
    {
        LM = GetComponentInChildren<LightManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //separate the hunter and hunted
    public void SetCharacter(int newHunterId)
    {
        InitHunted();
        if (PhotonNetwork.PlayerList[newHunterId] == PhotonNetwork.LocalPlayer)
        {
            InitHunter();
        }
        
        Debug.Log("Initialised HUNTER: " + PhotonNetwork.LocalPlayer.NickName + ", " + PhotonNetwork.LocalPlayer.CustomProperties);
    }

    private void InitHunter()
    {
        Hashtable charProps = new Hashtable
            {
                { MarcoPoloGame.IS_ALIVE, false },
                { MarcoPoloGame.IS_HUNTER, true }
            };
        LM.SetLightHunter();
        // GameObject tempPulse = Instantiate(HunterPulse, transform.position, Quaternion.identity, transform);
        PhotonNetwork.LocalPlayer.SetCustomProperties(charProps);
    }

    public void InitHunted()
    {
        Hashtable charProps = new Hashtable
            {
                { MarcoPoloGame.IS_ALIVE, true },
                { MarcoPoloGame.IS_HUNTER, false }
            };
        LM.SetLightHunted();
        // GameObject tempPulse = Instantiate(HuntedPulse, transform.position, Quaternion.identity, transform);
        PhotonNetwork.LocalPlayer.SetCustomProperties(charProps);
    }
}
