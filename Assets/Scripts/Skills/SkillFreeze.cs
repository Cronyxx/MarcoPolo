using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SkillFreeze : Skill
{    bool isFrozen;

    GameObject obj;
    PlayerMovement PM;
    MonoBehaviour MB;

    SpriteRenderer SR;
    GameObject statusBar, statusPrefab, status;

    public SkillFreeze()
    {
        this.skillId = 0;
        this.skillName = "Freeze";
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
            statusPrefab = Resources.Load("PhotonPrefabs/Skills/StatusFreeze") as GameObject;
        }
    
        this.MB.StopAllCoroutines();
        this.MB.StartCoroutine(Effect());
    }

    public override IEnumerator Effect()
    {
        Affect();
        yield return new WaitForSeconds(MarcoPoloGame.SKILL_FREEZE_DUR);
        Unaffect();
    }
    public override void Affect()
    {
        Debug.Log("Frozen!");
        PM.movementSpeed = 0;
        isFrozen = true;

        SR.color = Color.blue;

        AddStatusEffect(MarcoPoloGame.SKILL_FREEZE_DUR);
    }

    public override void Unaffect()
    {
        Debug.Log("Unfrozen!");
        PM.movementSpeed = MarcoPoloGame.PLAYER_SPEED;
        isFrozen = false;

        SR.color = Color.white;
    }

    public override void AddStatusEffect(float timeout)
    {
        status = Instantiate(statusPrefab);
        status.transform.SetParent(statusBar.transform);

        Destroy(status, timeout);
    }
}
