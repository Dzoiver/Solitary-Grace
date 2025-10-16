using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WakeUp()
    {
        wakeupObject.PlayWakeUp();
    }

    public void FadeIn()
    {
        GameFuncs.FadeIn(2f);
    }
}
