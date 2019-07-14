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
    public GameObject playerLight;

    // Start is called before the first frame update
    void Start()
    {
        if (this.photonView.IsMine)
        {
            Instantiate(playerLight, transform.position, transform.rotation, transform);
            this.photonView.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedCharacter);
        }
        else
        {
            Destroy(myCamera);
            Destroy(myAL);
        }
        GameObject.Find("GameManager").GetComponent<MarcoPoloGameManager>().enabled = true;
        GetComponent<CharacterInit>().enabled = true;
    }

    [PunRPC]
    void RPC_AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;
        myCharacter = Instantiate(PlayerInfo.PI.allCharacters[whichCharacter], transform.position,
            transform.rotation, transform); 
        myCharacter.tag = "Player";

        myCharacter.GetComponent<Renderer>().material.shader = Resources.Load<Material>("Materials/Sprite_Material").shader;
    }
}