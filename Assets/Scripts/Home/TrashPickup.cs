using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    [SerializeField] GameObject outTrigger;
    [SerializeField] GameObject doorMessage;
    AudioSource grabSFX;

    private void Start()
    {
        grabSFX = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        grabSFX.Play();
        outTrigger.SetActive(true);
        doorMessage.SetActive(false);
        gameObject.transform.Translate(0f, 5f, 0f);
        // gameObject.SetActive(false);
    }
}
