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
    private PhotonView PV;

    public GameObject statusBar;
    void Start()
    {
        skills = new Skill[MarcoPoloGame.SKILL_COUNT];
        PM = GetComponent<PlayerMovement>();
        PV = GetComponent<PhotonView>();
        MB = GameObject.FindObjectOfType<MonoBehaviour>();

        statusBar = GameObject.Find("Canvas/Status Bar");

        skills[0] = new SkillFreeze();
        skills[1] = new SkillSlow();
        skills[2] = new SkillFast();
        skills[3] = new SkillEcho();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            // freeze
            PV.RPC("RPC_UsedSkill", RpcTarget.AllViaServer, 0, UnityEngine.Random.Range(0, PhotonNetwork.PlayerList.Length), MarcoPoloGame.SKILL_TYPE_HUNTER);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // slow
            PV.RPC("RPC_UsedSkill", RpcTarget.AllViaServer, 1, UnityEngine.Random.Range(0, PhotonNetwork.PlayerList.Length), MarcoPoloGame.SKILL_TYPE_HUNTER);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            // fast
            PV.RPC("RPC_UsedSkill", RpcTarget.AllViaServer, 2, UnityEngine.Random.Range(0, PhotonNetwork.PlayerList.Length), MarcoPoloGame.SKILL_TYPE_HUNTER);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            // fast
            PV.RPC("RPC_UsedSkill", RpcTarget.AllViaServer, 3, UnityEngine.Random.Range(0, PhotonNetwork.PlayerList.Length), MarcoPoloGame.SKILL_TYPE_HUNTER);
        }
    }

    [PunRPC]
    void RPC_UsedSkill(int skillId, int playerId, int skillType)
    {
        Debug.Log("skillId: " + skillId + " , playerSelected: " + playerId);
        Debug.Log(skills[skillId].skillName + " affecting me!");

        // DEBUG here must change to false
        
        bool affectingMe = true;

        // if(skillType == MarcoPoloGame.SKILL_TYPE_HUNTED)
        // {
        //     if(!(bool) PhotonNetwork.LocalPlayer.CustomProperties[MarcoPoloGame.IS_HUNTER]) 
        //     {
        //         affectingMe = true;
        //     }
        // } 
        // else if(skillType == MarcoPoloGame.SKILL_TYPE_HUNTER) 
        // {
        //     if((bool) PhotonNetwork.LocalPlayer.CustomProperties[MarcoPoloGame.IS_HUNTER]) 
        //     {
        //         affectingMe = true;
        //     }
        // } 
        // else if(skillType == MarcoPoloGame.SKILL_TYPE_GLOBAL) 
        // {
            
        // }

        if(affectingMe)
        {
            skills[skillId].Init(this.gameObject, PM, MB, statusBar);
        }
        
    }

}
