using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MenuController : MonoBehaviour
{
    public Text nickNamePlaceHolder;
    void Start()
    {
        if(PhotonNetwork.LocalPlayer.NickName != null)
        {

        }
    }
    
    public void OnClickCharacterPick(int whichCharacter)
    {
        if (PlayerInfo.PI != null)
        {
            PlayerInfo.PI.mySelectedCharacter = whichCharacter;
            PlayerPrefs.SetInt("MyCharacter", whichCharacter);
        }
    }

    
}
