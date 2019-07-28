using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MenuController : MonoBehaviour
{
    public InputField NickNameInput;
    void Start()
    {
        if(PhotonNetwork.NickName != "")
        {
            NickNameInput.text = PhotonNetwork.NickName;
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
