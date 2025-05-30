using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    [SerializeField] GameObject outTrigger;
    [SerializeField] GameObject doorMessage;
    [SerializeField] DoorOpen exitDoor;
    AudioSource grabSFX;

    private void Start()
    {
        grabSFX = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        grabSFX.Play();
        outTrigger.SetActive(true);
        exitDoor.DoorCanBeOpened(true);
        gameObject.transform.Translate(0f, 5f, 0f);
        // gameObject.SetActive(false);
    }
}
