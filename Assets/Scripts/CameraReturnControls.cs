using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraReturnControls : MonoBehaviour
{
    [SerializeField] private bool playWakeUp = true;
    [SerializeField] private bool returnToPlayer = false;
    Animator anim;
    Camera thisCamera;
    Vector3 playerStartPos = new Vector3(17f, 6.51f, 2.542f); // Position in room after waking up
    Vector3 cameraAngle;
    public UnityEvent onWakeup;

    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
        anim = GetComponent<Animator>();
        cameraAngle = GameFuncs.PlayerScript.GetCamera();
        thisCamera.enabled = false;
        if (playWakeUp)
        {
            anim.Play("WakeupAnim");
            GameFuncs.FadeOut(1f);
            GameFuncs.PlayerScript.SetControl(false);
            anim.enabled = true;
        }
    }

    public void SwitchToPlayer()
    {
        anim.enabled = false;
        thisCamera.enabled = false;
        //GameFuncs.PlayerScript.gameObject.SetActive(true);
        GameFuncs.PlayerScript.GiveHP(100f);
        GameFuncs.PlayerScript.CameraRestore();
        GameFuncs.PlayerScript.SetControl(true);
        GameFuncs.PlayerScript.SetCamera(cameraAngle);
        
    }

    public void PlayWakeUp()
    {
        onWakeup.Invoke();
        // Fade out
        GameFuncs.TeleportPlayer(playerStartPos, Quaternion.Euler(0f, -90f, 0f));
        anim.enabled = true;
        thisCamera.enabled = true;
        GameFuncs.FadeOut(1f);
        //GameFuncs.PlayerScript.gameObject.SetActive(false);
        anim.Play("WakeupAnim");
    }
}
