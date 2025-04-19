using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReturnControls : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SwitchToPlayer()
    {
        gameObject.SetActive(false);
        player.SetActive(true);
        GameFuncs.PlayerScript.SetControl(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
