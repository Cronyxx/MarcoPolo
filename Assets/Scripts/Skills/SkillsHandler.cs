using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SkillsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Skill[] skills;
    private PlayerMovement PM;
    private MonoBehaviour MB;
    public PhotonView PV;

    public GameObject statusBar;
    void Start()
    {
        skills = new Skill[MarcoPoloGame.SKILL_COUNT];
        PM = GetComponent<PlayerMovement>();
        PV = GetComponent<PhotonView>();
        MB = GameObject.FindObjectOfType<MonoBehaviour>();

        SkillbarController SB = GameObject.FindObjectOfType<SkillbarController>();
        SB.SH = this;

        statusBar = GameObject.Find("Canvas/Status Bar");

        skills[0] = new SkillFreeze();
        skills[1] = new SkillSlow();
        skills[2] = new SkillFast();
        skills[3] = new SkillEcho();
    }

    void Update()
    {
        
    }

    public void UseSkill(int skillId)
    {
        if(skillId == 0 || skillId == 1)
        {
            // freeze or slow
            PV.RPC("RPC_UsedSkill", RpcTarget.Others, skillId, -1);
        } 
        else if(skillId == 2 || skillId == 3)
        {
            RPC_UsedSkill(skillId, -1);
        }
    }

    [PunRPC]
    public void RPC_UsedSkill(int skillId, int playerId)
    {
        Debug.Log("skillId: " + skillId + " , playerSelected: " + playerId);
        Debug.Log(skills[skillId].skillName + " affecting me!");
        
        bool affectingMe = true;

        if(affectingMe)
        {
            skills[skillId].Init(this.gameObject, PM, MB, statusBar);
        }
        
    }

}
