using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorsController : MonoBehaviour
{
    [SerializeField] DOTweenAnimation door1;
    [SerializeField] DOTweenAnimation door2;
    bool opening = false;
    bool closing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoors()
    {
        if (opening)
            return;
        door1.DOPlay();
        door2.DOPlay();
        opening = true;
    }

    public void CloseDoors()
    {
        if (closing)
            return;
        door1.DOPlayBackwards();
        door2.DOPlayBackwards();
    }

    public void ResetDoors()
    {
        opening = false;
        closing = false;
        door1.DOPlayForward();
        door2.DOPlayForward();
    }
}
