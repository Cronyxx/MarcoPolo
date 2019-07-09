using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class InitHunted : MonoBehaviour
{
    public GameObject HuntedPulse;
    private LightManager LM;
    // Start is called before the first frame update
    void Start()
    {
        LM = GameObject.Find("GameManager").GetComponent<LightManager>();
        Instantiate(HuntedPulse, transform.position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetHunted()
    {
        Hashtable huntedProps = new Hashtable
        {
            { MarcoPoloGame.IS_ALIVE, true },
            { MarcoPoloGame.IS_HUNTER, false }
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(huntedProps);
        GameObject playerObj = (GameObject)PhotonNetwork.LocalPlayer.TagObject;
        LM.SetLightHunted();
    }
}
