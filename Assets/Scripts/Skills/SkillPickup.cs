using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SkillPickup : MonoBehaviour
{
    bool pickedUp;
    public int skillId;
    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3.0f);
        if(!pickedUp)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D (Collision2D collision)
    {
        if(collision.gameObject.GetPhotonView() != null) 
        {
            bool isOtherHunter = (bool) collision.gameObject
                                .GetPhotonView()
                                .Owner
                                .CustomProperties[MarcoPoloGame.IS_HUNTER];
        
            if(!isOtherHunter)
            {
                pickedUp = true;
                SkillbarController SC = GameObject.FindObjectOfType<SkillbarController>();
                SC.OnSkillPickup(this.gameObject, skillId);

                this.gameObject.transform.parent = SC.Skill1.transform;
                this.gameObject.transform.position = SC.Skill1.transform.position;
                this.gameObject.transform.localScale = new Vector3(30, 30, 1);
            }
            
        }
    }
}
