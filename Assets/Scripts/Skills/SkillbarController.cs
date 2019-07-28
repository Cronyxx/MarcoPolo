using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class SkillbarController : MonoBehaviour
{
    public Image dashCooldownImage;
    public Text dashText;
    public bool dashIsCooldown = false;
    private float dashCooldown = 2.0f;

    public SkillsHandler SH;

    public GameObject Skill1;

    private int loadedSkillId;
    private GameObject loadedSkill;

    public GameObject SkillButton, HunterButton;
    
    // Start is called before the first frame update
    void Start()
    {
        loadedSkillId = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.LocalPlayer.CustomProperties[MarcoPoloGame.IS_HUNTER] != null) 
        {
            if((bool) PhotonNetwork.LocalPlayer.CustomProperties[MarcoPoloGame.IS_HUNTER])
            {
                HunterButton.SetActive(true);
                SkillButton.SetActive(false);
            } 
            else 
            {
                HunterButton.SetActive(false);
                SkillButton.SetActive(true);
            }
        }
    }

    public void DashHandler()
    {
        var cooldown = dashCooldown;
        
        if(dashIsCooldown)
        {
            var val = 1 / cooldown * Time.deltaTime;
            dashCooldownImage.fillAmount += val;

            dashText.text = ((int)(cooldown - dashCooldownImage.fillAmount * cooldown) + 1).ToString();

            if(dashCooldownImage.fillAmount >= 1)
            {
                dashIsCooldown = false;
                dashCooldownImage.fillAmount = 0;

                dashText.text = "Dash!";
            }
        }
    }

    public void OnSkillPickup(GameObject skillPickup, int skillId)
    {
        Destroy(loadedSkill);
        if(Skill1.transform.childCount > 1)
        {
            Destroy(Skill1.transform.GetChild(0).gameObject);
        }

        loadedSkillId = skillId;
        loadedSkill = skillPickup;
    }

    public void OnSkillButtonPressed()
    {
        Debug.Log("Used skill, skill id: " + loadedSkillId);
        if(loadedSkillId != -1)
        {   
            SH.UseSkill(loadedSkillId);

            Destroy(loadedSkill);
            loadedSkillId = -1;
        }
    }


}
