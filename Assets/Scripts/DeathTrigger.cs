using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] GameOver gameover;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            source.Play();
            // Gameover Screen
            gameover.gameObject.SetActive(true);
            gameover.ShowGameOver();
        }
    }
}
