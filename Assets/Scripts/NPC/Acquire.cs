using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acquire : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Wave(bool activateWaving = true)
    {
        animator.SetBool("IsWave", activateWaving);
    }
}
