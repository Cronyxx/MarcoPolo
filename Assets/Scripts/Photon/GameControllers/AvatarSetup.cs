using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AvatarSetup : MonoBehaviourPun
{
    
    public Camera myCamera;
    public AudioListener myAL;

    // Start is called before the first frame update
    void Start()
    {
        if (this.photonView.IsMine)
        {

        }
        else
        {
            Destroy(myCamera);
            Destroy(myAL);
        }
    }
}