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
    public GameObject playerLight;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (this.photonView.IsMine)
        {
            Instantiate(playerLight, transform.position, transform.rotation, transform);
            AddCharacter(PlayerInfo.PI.mySelectedCharacter);
        }
        else
        {
            Destroy(myCamera);
            Destroy(myAL);
        }
        GameObject.Find("GameManager").GetComponent<MarcoPoloGameManager>().enabled = true;
        GetComponent<CharacterInit>().enabled = true;
    }

    void AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;
        myCharacter = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Characters", PlayerInfo.PI.allCharacters[whichCharacter]), transform.position,
            transform.rotation);
        this.photonView.RPC("RPC_SetParent", RpcTarget.AllBuffered, myCharacter.GetPhotonView().ViewID);
        myCharacter.tag = "Player";

        myCharacter.GetComponent<Renderer>().material.shader = Resources.Load<Material>("Materials/Sprite_Material").shader;
    }

    [PunRPC]
    void RPC_SetParent(int ID)
    {
        PhotonNetwork.GetPhotonView(ID).transform.SetParent(transform.GetChild(1));
    }
}