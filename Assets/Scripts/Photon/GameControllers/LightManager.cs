using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private Light _light;
    public Renderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        InitLight();
    }

    public void InitLight()
    {
        _light.transform.position = new Vector3(transform.position.x, transform.position.y, -4.0f);
        _light.type = LightType.Directional;
        _light.intensity = MarcoPoloGame.LIGHT_INT_NOT_PLAYING;
    }

    public void SetLightAll()
    {
        _light.type = LightType.Directional;
        _light.intensity = MarcoPoloGame.LIGHT_INT_NOT_PLAYING;
    }

    public void SetLightHunted()
    {
        // _light.type = LightType.Spot;
        // _light.intensity = MarcoPoloGame.LIGHT_INT_HUNTED;
        // _light.spotAngle = 90.0f;

        _light.type = LightType.Directional;
        _light.intensity = MarcoPoloGame.LIGHT_INT_NOT_PLAYING;
    }

    public void SetLightHunter()
    {
        _light.intensity = MarcoPoloGame.LIGHT_INT_HUNTER;
    }
}
