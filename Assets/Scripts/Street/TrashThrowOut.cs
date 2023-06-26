using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using DG.Tweening;

public class TrashThrowOut : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] GameObject house;
    [SerializeField] Light[] lamps;
    [SerializeField] ZombieAction zombie;
    static private bool triggeredOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggeredOnce)
            return;
        triggeredOnce = true;
        source.DOFade(0f, 1f);
        house.SetActive(false);
        GameFuncs.LampsChangeColor(lamps, Color.red);
        zombie.HeadMoveScary();
    }
}
