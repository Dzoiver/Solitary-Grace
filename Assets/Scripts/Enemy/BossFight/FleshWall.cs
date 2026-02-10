using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshWall : MonoBehaviour
{
    int count = 0;

    [SerializeField] BossEye[] eyes;
    DOTweenAnimation anim;
    [SerializeField] Boss boss;
    AudioSource audio;
    [SerializeField] AudioClip deathSound;

    public int Count { get => count; set
        {
            count = value;
            if (count == 0)
            {
                anim.DOPlayBackwards();

            } else if (count >= eyes.Length)
            {
                anim.DOPlayForward();
                boss.FleshWallCount++;
                audio.PlayOneShot(deathSound);
            }
        } }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<DOTweenAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
