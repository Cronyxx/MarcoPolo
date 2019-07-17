using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Projectile : MonoBehaviour
{
    public GameObject HuntedPulse;
    public Player Owner { get; private set; }
    void Start()
    {
        Destroy(gameObject, 2.0f);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetPhotonView() == null) 
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy() 
    {
        CreatePulse();
    }

    void CreatePulse()
    {
        GameObject pulse = Instantiate(HuntedPulse, transform.position, Quaternion.identity);
        pulse.GetComponent<ParticleSystem>().Emit(300);
        
        Destroy(pulse, MarcoPoloGame.PROJECTILE_PULSE_DUR);
    }

    public void InitProjectile(Player owner, Vector2 direction, float lag)
    {
        Owner = owner;
        transform.right = direction;

        Rigidbody2D RB = GetComponent<Rigidbody2D>();
        RB.velocity = direction * MarcoPoloGame.PROJECTILE_SPD;
        RB.position += RB.velocity * lag;
    }
}
