using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEye : MonoBehaviour
{
    DOTweenAnimation eyeAnim;
    bool opened = false;
    // Start is called before the first frame update
    void Start()
    {
        eyeAnim = GetComponent<DOTweenAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet" && opened == true)
        {
            eyeAnim.DORewind();
            opened = false;
        }
    }

    public void OpenEye()
    {
        opened = true;
        eyeAnim.DOPlay();
    }
}
