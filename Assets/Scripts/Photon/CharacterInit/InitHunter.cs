using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class InitHunter : MonoBehaviour
{
    public GameObject HunterPulse;
    private LightManager LM;
    // Start is called before the first frame update
    void Start()
    {
        LM = GameObject.Find("GameManager").GetComponent<LightManager>();
        Instantiate(HunterPulse, transform.position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetHunter()
    {
        Hashtable hunterProps = new Hashtable
        {
            { MarcoPoloGame.IS_ALIVE, true },
            { MarcoPoloGame.IS_HUNTER, true }
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(hunterProps);
        LM.SetLightHunter();
    }
}
