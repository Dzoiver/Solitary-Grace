using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorsController : MonoBehaviour
{
    [SerializeField] DOTweenAnimation door1;
    [SerializeField] DOTweenAnimation door2;
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
        door1.DOPlay();
        door2.DOPlay();
    }

    public void CloseDoors()
    {
        door1.DORewind();
        door2.DORewind();
    }
}
