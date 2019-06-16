using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView PV;
    private MarcoPoloGameManager GM;
    private Rigidbody2D RB;
    private ParticleSystem PS;
    private bool isMoving;
    private float currTime;
    public float nextEcho;
    public float movementSpeed;
    public float rotationSpeed;
    private RaycastHit2D[] m_Contacts = new RaycastHit2D[100];

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        RB = GetComponent<Rigidbody2D>();
        PS = GetComponent<ParticleSystem>();
        GM = GameObject.Find("GameManager").GetComponent<MarcoPoloGameManager>();
        isMoving = false;
        currTime = 0;
    }
    
    void FixedUpdate()
    {
        if (PV.IsMine)
        {
            BasicMovement();
            BasicRotation();
        }

        if (isMoving && currTime > nextEcho)
        {
            PS.Emit(300);
            currTime = 0;
        }
        currTime += Time.deltaTime;
    }

    private void BasicMovement()
    {
        // Set initial velocity as zero.
        var velocity = Vector2.zero;

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            velocity.y = movementSpeed;
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            velocity.y = -movementSpeed;
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            velocity.x = movementSpeed;
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            velocity.x = -movementSpeed;
        }

        if ((velocity.x > 0 || velocity.x < 0) || (velocity.y > 0 || velocity.y < 0))
        {
            isMoving = true;
        } else
        {
            isMoving = false;
        }

        velocity *= Time.deltaTime;
        var direction = velocity.normalized;

        // Find contacts along our movement direction.
        var resultCount = RB.Cast(direction.normalized, m_Contacts, velocity.magnitude);

        // We need to find a hit where we're actually moving.
        for (var i = 0; i < resultCount; ++i)
        {
            var contact = m_Contacts[i];
            var distance = contact.distance;

            // Are we actually moving?
            if (distance > Mathf.Epsilon)
            {
                // Yes, so schedule the move.
                RB.MovePosition(RB.position + (direction * distance));
                return;
            }
            // If we're moving into a contact then finish as we cannot move.
            else if (Vector2.Dot(contact.normal, direction) < 0)
                return;
        }

        // No contact was found so move the full velocity.
        RB.MovePosition(RB.position + velocity); 
    }

    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetPhotonView() != null) {
            bool isOtherHunter = (bool) collision.gameObject
                                .GetPhotonView()
                                .Owner
                                .CustomProperties[MarcoPoloGame.IS_HUNTER];

            if(isOtherHunter) 
            {
                GM.HunterTouchEvent();
            }
        }
    }
}
