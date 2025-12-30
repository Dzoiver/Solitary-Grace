using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WakeUperCamera : MonoBehaviour
{
    CameraReturnControls wakeupObject;
    Animator anim;
    GameOver gameover;
    // Start is called before the first frame update
    void Start()
    {
        gameover = FindObjectOfType<GameOver>();
        anim = GetComponent<Animator>();
        wakeupObject = FindObjectOfType<CameraReturnControls>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WakeUp()
    {
        anim.enabled = false;
        wakeupObject.PlayWakeUp();
        //gameover.DieFromMonster();
    }

    public void FadeIn()
    {
        GameFuncs.FadeIn(2f);
    }
}
