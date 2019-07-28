using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PhotonMenu : MonoBehaviourPunCallbacks
{
    public static PhotonMenu menu;

    public GameObject playButton, cancelButton, offlineButton;

    public InputField NickNameInput;

    private void Awake()
    {
        menu = this; //creates the singleton, lives within the Main Menu scene
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings(); //Connects to Master photon server
        } else
        {
            offlineButton.SetActive(false);
            playButton.SetActive(true);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon Master server.");
        PhotonNetwork.AutomaticallySyncScene = true;
        offlineButton.SetActive(false);
        playButton.SetActive(true);
    }

    public void OnPlayButtonClicked()
    {
        Debug.Log("Play Button was clicked.");
        playButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinLobby();
        Debug.Log("Joining lobby.");

        PhotonNetwork.NickName = NickNameInput.text;
    }

    public void OnCancelButtonClicked()
    {
        Debug.Log("Cancel Button was clicked.");
        cancelButton.SetActive(false);
        playButton.SetActive(true);
        PhotonNetwork.LeaveLobby();
    }

    public void OnInstructionsButtonClicked()
    {

    }

    public void OnCreditsButtonClicked()
    {
        
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        PhotonNetwork.LoadLevel(MultiplayerSetting.multiplayerSetting.LobbyScene);
    }
}
