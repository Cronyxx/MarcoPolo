using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSetting : MonoBehaviour
{
    public static MultiplayerSetting multiplayerSetting;

    public bool delayStart; //whether want delay start or instant start
    public int maxPlayers;

    public int MenuScene;
    public int LobbyScene;
    public int multiplayerScene;

    private void Awake()
    {
        if (MultiplayerSetting.multiplayerSetting == null)
        {
            MultiplayerSetting.multiplayerSetting = this;
        }
        else
        {
            if (MultiplayerSetting.multiplayerSetting != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Screen.fullScreen = false;
    }

}
