using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusic : MonoBehaviour
{
    public static BgMusic bgMusic;
	private void Awake()
	{
		if (BgMusic.bgMusic == null)
		{
			BgMusic.bgMusic = this;
		}
		else
		{
			if (BgMusic.bgMusic != this)
			{
				Destroy(this.gameObject);
			}
		}
		DontDestroyOnLoad(this.gameObject);
	}

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }


    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Multiplayer Scene"))
        {
            Destroy(gameObject);
        }
    }
}
