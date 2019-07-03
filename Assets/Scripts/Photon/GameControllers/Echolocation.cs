using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Echolocation : MonoBehaviour
{
    public float coolDown;
    private float currTime;
    private ParticleSystem PS;
    private PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PS = GetComponent<ParticleSystem>();
        PV = GetComponent<PhotonView>();
        currTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
