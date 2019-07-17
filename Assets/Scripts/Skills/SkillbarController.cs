using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

public class SkillbarController : MonoBehaviour
{
    public Image dashCooldownImage;
    public Text dashText;
    public bool dashIsCooldown = false;
    private float dashCooldown = MarcoPoloGame.DASH_CD;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dashIsCooldown)
        {
            DashHandler();
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
}
