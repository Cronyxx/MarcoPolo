using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEmitter : MonoBehaviour
{
    private ParticleSystem PS;
    // Start is called before the first frame update
    void Start()
    {
        PS = GetComponent<ParticleSystem>();
        PS.Emit(300);

        Destroy(this.gameObject, 3.0f);
    }
}
