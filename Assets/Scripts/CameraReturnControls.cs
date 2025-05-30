using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReturnControls : MonoBehaviour
{
    [SerializeField] private GameObject player;
    Animator anim;
    Vector3 playerStartPos;
    PlayerScript playerScript;
    Vector3 cameraAngle;

    // Start is called before the first frame update
    void Start()
    {
        playerStartPos = player.transform.position;
        anim = GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerScript>();
        cameraAngle = playerScript.GetCamera();
    }

    public void PlayWakeUp()
    {
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
        playerScript.SetCamera(cameraAngle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
