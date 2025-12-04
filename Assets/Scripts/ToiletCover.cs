using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletCover : MonoBehaviour
{
    private DOTweenAnimation anim;
    private bool opened = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<DOTweenAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenOrClose()
    {
        if (opened)
            anim.DOPlayBackwards();
        else
            anim.DOPlayForward();
        opened = !opened;
    }
}
