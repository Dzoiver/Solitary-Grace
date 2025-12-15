using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorKnocking : MonoBehaviour
{
    float interval = 0f;
    float currentTime = 0f;
    float randomTime = 0f;
    bool knocking = false;
    bool knockingStarted = false;
    [SerializeField] GameObject getsleep;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knocking && !audio.isPlaying)
            StartCoroutine(CoroutineRandomDelay());

    }

    public void StartKnocking()
    {
        knockingStarted = true;
        randomTime = Random.Range(0f, 5f);
        knocking = true;
    }

    IEnumerator CoroutineRandomDelay()
    {
        audio.PlayOneShot(audio.clip);
        knocking = false;
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        knocking = true;
    }

    public void GetSleep()
    {
        if (knockingStarted)
            getsleep.SetActive(true);
    }
}
