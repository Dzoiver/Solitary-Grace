using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimations : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Sit", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakSit()
    {
        animator.SetBool("CameraLook", true);
    }

    public void EscapeCamera()
    {
        animator.SetBool("Escape", true);
    }
}
