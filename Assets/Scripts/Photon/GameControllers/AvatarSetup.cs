using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AvatarSetup : MonoBehaviourPun
{
    public Camera myCamera;
    public AudioListener myAL;
    public GameObject myCharacter;
    public int characterValue;

    // Start is called before the first frame update
    void Start()
    {
        if (this.photonView.IsMine)
        {
            this.photonView.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedCharacter);
        }
        else
        {
            Destroy(myCamera);
            Destroy(myAL);
        }
    }

    [PunRPC]
    void RPC_AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;
        myCharacter = Instantiate(PlayerInfo.PI.allCharacters[whichCharacter], transform.position,
            transform.rotation, transform); 
        myCharacter.tag = "Player";
    }
}