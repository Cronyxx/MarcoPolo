using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class SkillHunter : MonoBehaviour
{
    
    public Button HunterButton;
    public Image imageCooldown;
    public Text imageCooldownText;
    bool isCooldown;
    private float cooldownTime, nextFireTime;
    
    void Start()
    {
        isCooldown = false;
        cooldownTime = MarcoPoloGame.HUNTER_REVEAL_CD;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCooldown)
        {
            var val = 1 / cooldownTime * Time.deltaTime;
            imageCooldown.fillAmount += val;

            imageCooldownText.text = ((int)(cooldownTime - imageCooldown.fillAmount * cooldownTime) + 1).ToString();

            if(imageCooldown.fillAmount >= 1)
            {
                isCooldown = false;
                imageCooldown.fillAmount = 0;

                imageCooldownText.text = "";
            }
        }
    }

    public void OnHunterButtonClick()
    {

        Debug.Log("Player props: " + PhotonNetwork.LocalPlayer.CustomProperties);       
        if(!isCooldown)
        {
            MakeAllEcho();
        }
        
    }

    void MakeAllEcho()
    {
        ParticleSystem[] PSArray = GameObject.FindObjectsOfType<ParticleSystem>();

        foreach(ParticleSystem PS in PSArray)
        {
            PS.Emit(300);
        }

        imageCooldownText.text = ((int) MarcoPoloGame.HUNTER_REVEAL_CD).ToString();
        isCooldown = true;
    }
}
