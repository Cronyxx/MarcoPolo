using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private Light myLight;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        InitLight();
    }

    public void InitLight()
    {
        myLight.transform.position = new Vector3(transform.position.x, transform.position.y, -4.0f);
        myLight.type = LightType.Directional;
        myLight.intensity = MarcoPoloGame.LIGHT_INT_NOT_PLAYING;
    }

    public void SetLightAll()
    {
        myLight.type = LightType.Directional;
        myLight.intensity = MarcoPoloGame.LIGHT_INT_NOT_PLAYING;
    }

    public void SetLightHunted()
    {
        myLight.type = LightType.Spot;
        myLight.intensity = MarcoPoloGame.LIGHT_INT_HUNTED;
        myLight.spotAngle = 90.0f;
    }

    public void SetLightHunter()
    {
        myLight.intensity = MarcoPoloGame.LIGHT_INT_HUNTER;
    }
}
