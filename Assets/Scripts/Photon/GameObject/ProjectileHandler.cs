using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class ProjectileHandler : MonoBehaviour
{

    public GameObject ProjectilePrefab;
    private PhotonView PV;
    private Rigidbody2D RB;
    private float projectileDelay = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine && !(bool)PhotonNetwork.LocalPlayer.CustomProperties[MarcoPoloGame.IS_HUNTER])
        {
            if (Input.GetButton("Jump") && projectileDelay <= 0.0)
            {
                projectileDelay = 0.1f;

                PV.RPC("RPC_Fire", RpcTarget.All, RB.position);
            }

            if (projectileDelay > 0.0f)
            {
                projectileDelay -= Time.deltaTime;
            }
        }
        
    }

    [PunRPC]
    public void RPC_Fire(Vector2 position, PhotonMessageInfo info)
    {
        Debug.Log("Firing!");
        float lag = (float)(PhotonNetwork.Time - info.SentServerTime);
        GameObject projectile;

        projectile = Instantiate(ProjectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().InitProjectile(PV.Owner, new Vector2(1, 0), Mathf.Abs(lag));
    }
}
