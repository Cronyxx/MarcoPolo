using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    private ParticleSystem PS;
    void Start()
    {
        PS = GetComponent<ParticleSystem>();
        PS.Emit(300);
        Destroy(this.gameObject, 3.0f);
    }
}
