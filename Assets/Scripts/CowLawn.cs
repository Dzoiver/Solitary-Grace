using GM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CowLawn : MonoBehaviour
{
    [SerializeField] GameObject computer;
    [SerializeField] GameObject osCanvas;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(DelayExit());
        }
    }

    IEnumerator DelayExit()
    {
        osCanvas.SetActive(true);
        computer.SetActive(true);
        gameObject.SetActive(false);
        GameFuncs.PlayerScript.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
    }
}
