using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo PI;

    //Used for character selection
    public int mySelectedCharacter;
    public GameObject[] allCharacters;

    private void OnEnable()
    {
        if (PlayerInfo.PI == null)
        {
            PlayerInfo.PI = this;
        }
        else
        {
            if (PlayerInfo.PI != this)
            {
                Destroy(PlayerInfo.PI.gameObject);
                PlayerInfo.PI = this;
            }
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MyCharacter"))
        {
            mySelectedCharacter = PlayerPrefs.GetInt("MyCharacter");
        }
        else
        {
            mySelectedCharacter = 0;
            PlayerPrefs.SetInt("MyCharacter", mySelectedCharacter);
        }
    }
}