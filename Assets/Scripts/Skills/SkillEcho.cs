using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SkillEcho : Skill
{

    GameObject obj;
    PlayerMovement PM;
    MonoBehaviour MB;

    SpriteRenderer SR;
    EchoManager EM;
    GameObject statusBar, statusPrefab, status;

    public SkillEcho()
    {
        this.skillId = 3;
        this.skillName = "Echo Off";
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
            statusPrefab = Resources.Load("PhotonPrefabs/Skills/StatusEcho") as GameObject;
        }
    
        this.MB.StopAllCoroutines();
        this.MB.StartCoroutine(Effect());
    }

    public override IEnumerator Effect()
    {
        Affect();
        yield return new WaitForSeconds(MarcoPoloGame.SKILL_ECHO_DUR);
        Unaffect();
    }
    public override void Affect()
    {
        Debug.Log("Echo off!");
        EM.isEchoOn = false;

        SR.color = new Color(0.95f, 0, 1.0f, 1.0f);

        AddStatusEffect(MarcoPoloGame.SKILL_ECHO_DUR);
    }

    public override void Unaffect()
    {
        Debug.Log("Echo back on!");
        EM.isEchoOn = true;

        SR.color = Color.white;
    }

    public override void AddStatusEffect(float timeout)
    {
        status = Instantiate(statusPrefab);
        status.transform.SetParent(statusBar.transform);

        Destroy(status, timeout);
    }
}
