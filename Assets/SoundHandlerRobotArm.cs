﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandlerRobotArm : MonoBehaviour
{

    FMOD.Studio.EventInstance soundEvent;
    


    public void PlayServos()
    {
        soundEvent = FMODUnity.RuntimeManager.CreateInstance("event:/RobotArmServo");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
        
    }

    public void StopServos()
    {
        soundEvent.release();
    }

}
