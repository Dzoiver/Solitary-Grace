using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageOpener : MonoBehaviour
{
    [SerializeField] DOTweenAnimation[] cages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        foreach (DOTweenAnimation anim in cages)
        {
            anim.DOPlay();
        }
    }
}
