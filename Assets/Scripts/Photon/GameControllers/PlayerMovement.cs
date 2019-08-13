using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

using Hashtable = ExitGames.Client.Photon.Hashtable;


public class PlayerMovement : MonoBehaviourPunCallbacks
{
    private PhotonView PV;
    private MarcoPoloGameManager GM;
    private ScoreManager SM;
    private SkillbarController SC;
    private Rigidbody2D RB;
    public bool isMoving;
    public float movementSpeed;
    public float rotationSpeed;
    private RaycastHit2D[] m_Contacts = new RaycastHit2D[100];

    private float dashTimer;
    public bool isDashing;

    public Vector2 velocity = Vector2.zero;

    //joystick stuff
    private VariableJoystick variableJoystick;
    private JoyButton joyButton;
    private Transform charSprite;

    private Vector2 currPosition;
    private Vector2 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        RB = GetComponent<Rigidbody2D>();
        RB.collisionDetectionMode =  CollisionDetectionMode2D.Continuous;

        GM = GameObject.FindObjectOfType<MarcoPoloGameManager>();
        SM = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        SC = GameObject.Find("Canvas/Skillbar").GetComponent<SkillbarController>();
        isMoving = false;

        dashTimer = 0.0f;
        isDashing = false;

        if (PV.IsMine)
        {
            variableJoystick = FindObjectOfType<VariableJoystick>();
            joyButton = FindObjectOfType<JoyButton>();
            charSprite = transform.GetChild(1);
        }
        currPosition = Vector2.zero;
        lastPosition = Vector2.zero;

    }
    
    void FixedUpdate()
    {
        if (PV.IsMine)
        {
            //BasicMovement();
            //BasicRotation();
            JoystickMovement();
            Dash();
        } else
        {
            currPosition = transform.position;
            if (currPosition.Equals(lastPosition))
            {
                isMoving = false;
            } else
            {
                isMoving = true;
            }
            lastPosition = currPosition;
        }
    }

    private void JoystickMovement()
    {
        Vector2 direction = Vector2.up * variableJoystick.Vertical + Vector2.right * variableJoystick.Horizontal;
        RB.velocity = direction * movementSpeed;
        if (direction.x > 0 || direction.x < 0 || direction.y < 0 || direction.y > 0)
        {
            charSprite.transform.rotation = Quaternion.Euler(0, 0, (float)((Math.Atan2(direction.y, direction.x) * 180) / Math.PI));
            isMoving = true;
        } else
        {
            isMoving = false;
        }

    }

    private void BasicMovement()
    {
        // Set initial velocity as zero.

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
    void Dash()
    {
            if (isDashing && isMoving)
            {
                RB.AddForce(new Vector2(velocity.x * 500f, velocity.y * 500f), ForceMode2D.Impulse);
                isDashing = false;
            }
    }
    private void OnCollisionStay2D (Collision2D collision)
    {
        if(PV.IsMine && collision.collider.gameObject.GetPhotonView() != null) {
            bool isOtherHunter = (bool) collision.collider.gameObject
                                .GetPhotonView()
                                .Owner
                                .CustomProperties[MarcoPoloGame.IS_HUNTER];

            if(isOtherHunter) 
            {
                GM.HunterTouchEvent();
            }
        }
    }
    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
    {
        if(PhotonNetwork.LocalPlayer == target  && changedProps.ContainsKey(MarcoPoloGame.IS_ALIVE))
        {
            bool isPlayerAlive = (bool) changedProps[MarcoPoloGame.IS_ALIVE];
            bool isHunter = (bool) changedProps[MarcoPoloGame.IS_HUNTER];
            if(!isPlayerAlive && !isHunter)
            {
                Debug.Log("I'm dead! Not able to move...");
                this.movementSpeed = 0;    
            } else 
            {
                Debug.Log("I'm alive! Able to move...");
                this.movementSpeed = MarcoPoloGame.PLAYER_SPEED;
            }
        }
    }
}
