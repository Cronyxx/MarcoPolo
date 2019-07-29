using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MapSelector : MonoBehaviour
{
    public GameObject[] mapArray;
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
        }
    }
}
