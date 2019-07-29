using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SkillSlow : Skill
{
    bool isSlowed;

    GameObject obj;
    PlayerMovement PM;
    MonoBehaviour MB;

    SpriteRenderer SR;
    GameObject statusBar, statusPrefab, status;

    public SkillSlow()
    {
        this.skillId = 1;
        this.skillName = "Slow";
    }

    public override void Init(GameObject obj, PlayerMovement PM, MonoBehaviour MB, GameObject statusBar)
    {
        if(this.obj == null) 
        {
            this.obj = obj;
            this.PM = PM;
            this.MB = MB;
            this.statusBar = statusBar;

            SR = obj.transform.GetChild(2).GetComponent<SpriteRenderer>();
            statusPrefab = Resources.Load("PhotonPrefabs/Skills/StatusSlow") as GameObject;
        }
    
        this.MB.StopAllCoroutines();
        this.MB.StartCoroutine(Effect());
    }

    public override IEnumerator Effect()
    {
        Affect();
        yield return new WaitForSeconds(MarcoPoloGame.SKILL_SLOW_DUR);
        Unaffect();
    }
    public override void Affect()
    {
        Debug.Log("Slowed!");
        PM.movementSpeed = MarcoPoloGame.SKILL_SLOW_SPD;
        isSlowed = true;

        // SR.color = new Color(1, 0.63f, 0, 1.0f);

        AddStatusEffect(MarcoPoloGame.SKILL_SLOW_DUR);
    }

    public override void Unaffect()
    {
        Debug.Log("Unslowed!");
        PM.movementSpeed = MarcoPoloGame.PLAYER_SPEED;
        isSlowed = false;

        // SR.color = Color.white;
    }

    public override void AddStatusEffect(float timeout)
    {
        status = MonoBehaviour.Instantiate(statusPrefab);
        status.transform.SetParent(statusBar.transform);

        MonoBehaviour.Destroy(status, timeout);
    }
}
