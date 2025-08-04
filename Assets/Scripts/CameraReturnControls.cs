using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReturnControls : MonoBehaviour
{
    [SerializeField] private bool playWakeUp = true;
    [SerializeField] private bool returnToPlayer = false;
    Animator anim;
    Vector3 playerStartPos;
    Vector3 cameraAngle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cameraAngle = GameFuncs.PlayerScript.GetCamera();
        playerStartPos = GameFuncs.PlayerScript.transform.position;
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
        }
    }

    public void PlayWakeUp()
    {
        if (!playWakeUp)
            return;
        // Fade out
        gameObject.SetActive(true);
        GameFuncs.PlayerScript.gameObject.SetActive(false);
        anim.Play("WakeupAnim");
    }

    /// <summary>
    /// Switches animated camera to player camera
    /// </summary>
    /// <param name="leaveOn">Should it deactivate the animated camera</param>
    public void SwitchToPlayer(bool leaveOn = false)
    {
        anim.enabled = false;
        gameObject.SetActive(leaveOn);
        GameFuncs.PlayerScript.gameObject.transform.position = playerStartPos;
        GameFuncs.PlayerScript.gameObject.SetActive(true);
        GameFuncs.PlayerScript.SetControl(true);
        GameFuncs.PlayerScript.SetCamera(cameraAngle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
