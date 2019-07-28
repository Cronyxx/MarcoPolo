using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvases : MonoBehaviour
{
    public GameObject MainMenuCanvas, InstructionCanvas, CreditsCanvas;
    public GameObject MainMenuBG;
    void Start()
    {
        Initialise();
    }

    void Initialise()
    {
        HideCanvases();
        MainMenuCanvas.SetActive(true);
    }
    void HideCanvases()
    {
        MainMenuCanvas.SetActive(false);
        InstructionCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
    }

    public void OnClickExitButton()
    {
        HideCanvases();
        MainMenuCanvas.SetActive(true);
    }

    public void OnClickInstructionsButton()
    {
        HideCanvases();
        InstructionCanvas.SetActive(true);
    }

    public void OnClickCreditsButton()
    {
        HideCanvases();
        CreditsCanvas.SetActive(true);
    }
}
