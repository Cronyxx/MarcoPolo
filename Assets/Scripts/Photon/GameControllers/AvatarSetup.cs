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
    private LightManager LM;

    // Start is called before the first frame update
    void Start()
    {
        if (this.photonView.IsMine)
        {
            LM = GetComponent<LightManager>();
            LM.InitLight();
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

        myCharacter.GetComponent<Renderer>().material.shader = Resources.Load<Material>("Materials/Sprite_Material").shader;
    }
}