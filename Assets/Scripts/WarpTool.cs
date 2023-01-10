using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTool : MonoBehaviour
{
    [SerializeField] GameObject radiant;
    [SerializeField] GameObject dire;
    [SerializeField] ParticleSystem particles;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            particles.Play();
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
