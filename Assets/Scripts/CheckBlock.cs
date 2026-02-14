using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBlock : MonoBehaviour
{
    [SerializeField] GameObject checkItem;
    [SerializeField] DOTweenAnimation anim;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == checkItem)
        {
            anim.enabled = true;
            enabled = false;
        }
    }

    public void TryTrigger()
    {
        Debug.Log("try trigger: " + anim.enabled);
        if (anim.enabled)
            anim.DOPlay();
    }
}
