using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Echolocation : MonoBehaviour
{
    public float coolDown;
    private AudioSource AD;
    private float currTime;
    private ParticleSystem PS;
    private PhotonView PV;
    
    // Start is called before the first frame update
    void Start()
    {
        PS = GetComponent<ParticleSystem>();
        PV = GetComponent<PhotonView>();
        AD = GameObject.Find("EcholocationSound").GetComponent<AudioSource>();
        currTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties[MarcoPoloGame.IS_HUNTER] == null)
            return;
        if (PV.IsMine 
            && (bool)PhotonNetwork.LocalPlayer.CustomProperties[MarcoPoloGame.IS_HUNTER] 
            && Input.GetButton("Jump") 
            && currTime > coolDown)
        {
            PV.RPC("RPC_Echolocate", RpcTarget.AllViaServer);
            currTime = 0;
        }
        currTime += Time.deltaTime;
    }

    [PunRPC]
    private void RPC_Echolocate()
    {
        PS.Emit(300);
        AD.Play();
    }
}
