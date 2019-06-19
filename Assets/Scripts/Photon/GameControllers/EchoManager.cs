using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EchoManager : MonoBehaviour
{
    private PhotonView PV;
    private ParticleSystem PS;
    private PlayerMovement PM;
    private float currTime;
    public float nextEcho;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        PS = GetComponent<ParticleSystem>();
        PM = GetComponent<PlayerMovement>();
        currTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PM.isMoving && currTime > nextEcho && !(bool) PhotonNetwork.LocalPlayer.CustomProperties[MarcoPoloGame.IS_HUNTER])
        {
            PV.RPC("RPC_EmitSoundWave", RpcTarget.All);
            currTime = 0;
        }
        currTime += Time.deltaTime;
    }

    [PunRPC]
    private void RPC_EmitSoundWave()
    {
        PS.Emit(300);
        // might want to add some sound effects
    }

}
