using DG.Tweening;
using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MouseOverTrigger : MonoBehaviour
{
    public UnityEvent onClick;
    private bool open = false;
    DOTweenAnimation anim;
    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip closeSound;
    [SerializeField] AudioSource audio;
    public float distance = 5f;
    public bool repeat = true;
    private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<DOTweenAnimation>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        if (!repeat && triggered)
            return;

        float dist = Vector3.Distance(GameFuncs.PlayerScript.gameObject.transform.position, transform.position);
        if (dist < distance)
        {
            if (open == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    triggered = true;
                    audio.clip = openSound;
                    audio.Play();
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
                        audio.clip = closeSound;
                        audio.Play();
                        open = false;
                        anim.DOPlayBackwards();
                    }
                }

            }

        }
    }
}