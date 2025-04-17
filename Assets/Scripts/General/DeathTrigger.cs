using UnityEngine;
using Zenject;

public class DeathTrigger : MonoBehaviour
{
    [Inject] GameOver gameover;
    [SerializeField] AudioSource source;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            source.Play(); // Scream sound
            // gameover.gameObject.SetActive(true);
            gameover.NormalDeath(other.gameObject.GetComponent<PlayerScript>());
        }
    }
}
