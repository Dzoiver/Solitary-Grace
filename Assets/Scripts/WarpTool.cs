using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTool : MonoBehaviour
{
    [SerializeField] GameObject radiant;
    [SerializeField] GameObject dire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (radiant.activeSelf)
            {
                radiant.SetActive(false);
                dire.SetActive(true);
            }    
            else
            {
                radiant.SetActive(true);
                dire.SetActive(false);
            }
        }
    }
}
