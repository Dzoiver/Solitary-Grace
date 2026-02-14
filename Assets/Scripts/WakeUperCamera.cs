using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WakeUperCamera : MonoBehaviour
{
    CameraReturnControls wakeupObject;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        wakeupObject = FindObjectOfType<CameraReturnControls>();
    }

    public void WakeUp()
    {
        anim.enabled = false;
        wakeupObject.PlayWakeUp();
    }

    public void FadeIn()
    {
        GameFuncs.FadeIn(2f);
    }
}
