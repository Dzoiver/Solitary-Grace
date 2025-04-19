using UnityEngine;
using SolitaryAudio;

public class Switch : MonoBehaviour
{
    [SerializeField] Light livingRoomLight;
    [SerializeField] GameObject lampOff;
    [SerializeField] GameObject lampOn;

    private void OnTriggerEnter(Collider other)
    {
        if (livingRoomLight.enabled)
        {
            LightOff();
        }
        else
        {
            LightOn();
        }
    }

    public void LightOff()
    {
        if (lampOn != null)
            lampOn.SetActive(false);
        if (lampOff != null)
            lampOff.SetActive(true);
        AudioController.Play("switch");
        livingRoomLight.enabled = false;
    }

    public void LightOn()
    {
        if (lampOn != null)
            lampOn.SetActive(true);
        if (lampOff != null)
            lampOff.SetActive(false);
        AudioController.Play("switch");
        livingRoomLight.enabled = true;
    }
}
