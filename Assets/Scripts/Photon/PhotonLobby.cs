using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    public GameObject playButton;
    public GameObject cancelButton;

    private void Awake()
    {
        lobby = this; //creates the singleton, lives within the Main Menu scene
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings(); //Connects to Master photon server
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon Master server.");
        PhotonNetwork.AutomaticallySyncScene = true;
        playButton.SetActive(true);
    }

    public void OnPlayButtonClicked()
    {
        Debug.Log("Play Button was clicked.");
        playButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Joining random room.");
    }

    public void OnCancelButtonClicked()
    {
        Debug.Log("Cancel Button was clicked.");
        cancelButton.SetActive(false);
        playButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left the room.");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a random game but failed. There are no open room available.");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Trying to create a new room.");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSetting.multiplayerSetting.maxPlayers};
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed, there must already be a room with the same name.");
        CreateRoom();
    }
}
