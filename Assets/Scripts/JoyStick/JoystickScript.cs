using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickScript : MonoBehaviour
{

    //joystick stuff
    protected Joystick joystick;
    protected JoyButton joyButton;

    // Start is called before the first frame update
    void Start()
    {
        //joystick stuff
        joystick = FindObjectOfType<Joystick>();
        joyButton = FindObjectOfType<JoyButton>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
