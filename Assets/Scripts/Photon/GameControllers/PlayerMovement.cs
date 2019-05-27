using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView PV;
    private Rigidbody2D RB;
    public float movementSpeed;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            BasicMovement();
            BasicRotation();
        }
    }

    void BasicMovement()
    {
        Vector3 endPosition = Vector3.zero;
        if (Input.GetAxis("Vertical") > 0)
        {
            endPosition += transform.up;
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            endPosition -= transform.up;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            endPosition += transform.right;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            endPosition -= transform.right;
        }
        RB.MovePosition(transform.position + endPosition * Time.deltaTime * movementSpeed);
    }

    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit a wall");
    }
}
