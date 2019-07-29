using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SkillFast : Skill
{
    bool isFast;

    GameObject obj;
    PlayerMovement PM;
    MonoBehaviour MB;

    SpriteRenderer SR;
    GameObject statusBar, statusPrefab, status;

    public SkillFast()
    {
        this.skillId = 2;
        this.skillName = "Fast";
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
            statusPrefab = Resources.Load("PhotonPrefabs/Skills/StatusFast") as GameObject;
        }
    
        this.MB.StopAllCoroutines();
        this.MB.StartCoroutine(Effect());
    }

    public override IEnumerator Effect()
    {
        Affect();
        yield return new WaitForSeconds(MarcoPoloGame.SKILL_FAST_DUR);
        Unaffect();
    }
    public override void Affect()
    {
        Debug.Log("Fasted!");
        PM.movementSpeed = MarcoPoloGame.SKILL_FAST_SPD;
        isFast = true;

        // SR.color = Color.green;

        AddStatusEffect(MarcoPoloGame.SKILL_FAST_DUR);
    }

    public override void Unaffect()
    {
        Debug.Log("UnFasted!");
        PM.movementSpeed = MarcoPoloGame.PLAYER_SPEED;
        isFast = false;

        // SR.color = Color.white;
    }

    public override void AddStatusEffect(float timeout)
    {
        status = MonoBehaviour.Instantiate(statusPrefab);
        status.transform.SetParent(statusBar.transform);

        MonoBehaviour.Destroy(status, timeout);
    }
}
