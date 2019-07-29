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
    private float nextEcho = MarcoPoloGame.ECHO_DELAY;

    public GameObject PulsePrefab;
    private AudioSource AD;

    public bool isEchoOn;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        PS = GetComponent<ParticleSystem>();
        PM = GetComponent<PlayerMovement>();
        AD = GameObject.Find("HuntedSound").GetComponent<AudioSource>();
        currTime = 0;

        isEchoOn = true;
    }

    void Update()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties[MarcoPoloGame.IS_HUNTER] == null)
            return;
        if (PV.IsMine && PM.isMoving && currTime > nextEcho && !(bool) PhotonNetwork.LocalPlayer.CustomProperties[MarcoPoloGame.IS_HUNTER] && isEchoOn)
        {
            PV.RPC("RPC_CreatePulse", RpcTarget.AllViaServer, transform.position.x, transform.position.y);
            currTime = 0;
        }
        currTime += Time.deltaTime;
    }

    [PunRPC]
    private void RPC_CreatePulse(float x, float y)
    {
        Instantiate(PulsePrefab, new Vector3(x, y, transform.position.z), Quaternion.identity, transform);
        AD.Play();      
    }

}
