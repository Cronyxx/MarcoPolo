using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class AvatarSetup : MonoBehaviourPun
{
    public Camera myCamera;
    public AudioListener myAL;
    public GameObject myCharacter;
    public int characterValue;
    public GameObject playerLightPrefab;
    private GameObject myLight;

    public Animator animator;

    private bool isMine;

    // Start is called before the first frame update
    void Start()
    {
        if (this.photonView.IsMine)
        {
            isMine = true;
            myLight = Instantiate(playerLightPrefab, transform.position, transform.rotation, transform);
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
            transform.rotation, transform.GetChild(1));
        myCharacter.tag = "Player";

        if(!isMine)
        {
        myCharacter.GetComponent<Renderer>().material.shader = Resources.Load<Material>("Materials/Sprite_Material").shader;
        }
    }
}