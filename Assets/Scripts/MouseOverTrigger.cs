using DG.Tweening;
using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseOverTrigger : MonoBehaviour
{
    public UnityEvent onClick;
    private bool open = false;
    DOTweenAnimation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<DOTweenAnimation>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        float dist = Vector3.Distance(GameFuncs.PlayerScript.gameObject.transform.position, transform.position);
        if (dist < 5)
        {
            print("object name");
            if (open == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    open = true;
                    anim.DOPlayForward();
                }
            }
            else
            {
                if (open == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        open = false;
                        anim.DOPlayBackwards();
                    }
                }

            }

        }
    }
}