using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private PlayerMovement PM;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        PM = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isMoving", PM.isMoving);
    }
}
