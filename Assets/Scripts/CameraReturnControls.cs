using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReturnControls : MonoBehaviour
{
    [SerializeField] private bool playWakeUp = true;
    [SerializeField] private bool returnToPlayer = false;
    Animator anim;
    Camera thisCamera;
    Vector3 playerStartPos = new Vector3(17f, 6.51f, 2.542f);
    Vector3 cameraAngle;

    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
        anim = GetComponent<Animator>();
        cameraAngle = GameFuncs.PlayerScript.GetCamera();

        if (playWakeUp)
        {
            anim.Play("WakeupAnim");
            GameFuncs.FadeOut(1f);
            GameFuncs.PlayerScript.SetControl(false);
            anim.enabled = true;
        }

        /*
        if (returnToPlayer)
        {
            anim.enabled = false;
            return;
        }
        if (!playWakeUp)
        {
            gameObject.SetActive(false);
            anim.enabled = false;
            SwitchToPlayer();
            return;
        */
    }

    public void PlayWakeUp()
    {
        if (!playWakeUp)
            return;
        // Fade out
        anim.enabled = true;
        thisCamera.enabled = true;
        GameFuncs.FadeOut(1f);
        //GameFuncs.PlayerScript.gameObject.SetActive(false);
        anim.Play("WakeupAnim");
    }

    /// <summary>
    /// Switches animated camera to player camera
    /// </summary>
    /// <param name="leaveOn">Should it deactivate the animated camera</param>
    public void SwitchToPlayer()
    {
        anim.enabled = false;
        thisCamera.enabled = false;
        GameFuncs.TeleportPlayer(playerStartPos, Quaternion.Euler(0f, -90f, 0f));
        //GameFuncs.PlayerScript.gameObject.SetActive(true);
        GameFuncs.PlayerScript.SetControl(true);
        GameFuncs.PlayerScript.SetCamera(cameraAngle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
