using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public int skillId;
    public string skillName;
    GameObject obj;
    PlayerMovement PM;
    MonoBehaviour MB;
    GameObject statusBar;
    
    public virtual void Init(GameObject obj, PlayerMovement PM, MonoBehaviour MB, GameObject statusBar) 
    {
        this.obj = obj;
        this.PM = PM;
        this.MB = MB;
        this.statusBar = statusBar;
    }
    public virtual IEnumerator Effect() 
    {
        yield return new WaitForSeconds(1);
    }
    public virtual void Affect() 
    {

    }
    public virtual void Unaffect()
    {

    }

    public virtual void AddStatusEffect(float timeout)
    {
        
    }
}
