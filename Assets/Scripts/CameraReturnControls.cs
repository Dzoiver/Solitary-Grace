using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReturnControls : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private bool playWakeUp = true;
    Animator anim;
    Vector3 playerStartPos;
    Vector3 cameraAngle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (!playWakeUp)
        {
            anim.enabled = false;
            SwitchToPlayer();
            return;
        }
        playerStartPos = player.transform.position;
        cameraAngle = GameFuncs.PlayerScript.GetCamera();
    }

    public void PlayWakeUp()
    {
        if (!playWakeUp)
            return;
        // Fade out
        gameObject.SetActive(true);
        player.SetActive(false);
        anim.Play("WakeupAnim");
    }

    public void SwitchToPlayer()
    {
        gameObject.SetActive(false);
        player.transform.position = playerStartPos;
        player.SetActive(true);
        GameFuncs.PlayerScript.SetControl(true);
        GameFuncs.PlayerScript.SetCamera(cameraAngle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
