using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LeaveLobbyMenu : MonoBehaviourPunCallbacks
{
    public void OnClick_LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public override void OnLeftLobby()
    {
        base.OnLeftLobby();
        PhotonNetwork.LoadLevel(MultiplayerSetting.multiplayerSetting.MenuScene);
    }
}
