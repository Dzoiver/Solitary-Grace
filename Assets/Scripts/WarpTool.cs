using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTool : MonoBehaviour
{
    [SerializeField] GameObject radiant;
    [SerializeField] GameObject dire;
    [SerializeField] ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            particles.Play();
            Debug.Log(particles.isPlaying);
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
