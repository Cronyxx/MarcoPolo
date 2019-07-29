using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntedPulse : MonoBehaviour
{
    private ParticleSystem PS;
    private PlayerMovement PM;

    void Start()
    {
        PS = GetComponent<ParticleSystem>();
        PM = transform.parent.GetComponent<PlayerMovement>();
    }
    void Update()
    {
        PS.Emit(300);
    }
}
