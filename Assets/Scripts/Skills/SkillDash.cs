using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SkillDash : Skill
{

    GameObject obj;
    PlayerMovement PM;
    MonoBehaviour MB;

    SpriteRenderer SR;
    EchoManager EM;
    GameObject statusBar, statusPrefab, status;

    public SkillDash()
    {
        this.skillId = 4;
        this.skillName = "Dash";
    }

    public override void Init(GameObject obj, PlayerMovement PM, MonoBehaviour MB, GameObject statusBar)
    {
        if(this.obj == null) 
        {
            this.obj = obj;
            this.PM = PM;
            this.MB = MB;
            this.statusBar = statusBar;
            this.EM = obj.GetComponent<EchoManager>();

            SR = obj.transform.GetChild(2).GetComponent<SpriteRenderer>();
            statusPrefab = Resources.Load("PhotonPrefabs/Skills/StatusDash") as GameObject;
        }
    
        this.MB.StopAllCoroutines();
        Affect();
    }

    public override void Affect()
    {        
        PM.isDashing = true;
        
        // Rigidbody2D RB = obj.GetComponent<Rigidbody2D>();
        // Vector2 velocity = PM.velocity;
        // RB.AddForce(Vector2.right * 1000f);
    }

    public override void AddStatusEffect(float timeout)
    {
        status = Instantiate(statusPrefab);
        status.transform.SetParent(statusBar.transform);

        Destroy(status, timeout);
    }
}
