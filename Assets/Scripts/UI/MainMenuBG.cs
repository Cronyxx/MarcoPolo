using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBG : MonoBehaviour
{
    public GameObject ParticlePrefab;
    void Start()
    {
        StartCoroutine(SpawnParticles());
    }

    IEnumerator SpawnParticles()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        while(true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 5.0f));
            GameObject temp = (GameObject) 
                Instantiate(ParticlePrefab, new Vector3(x + Random.Range(-9, 9), y + Random.Range(-5, 5), 0), Quaternion.identity, transform);
        }
    }
}
