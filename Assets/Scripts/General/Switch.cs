using UnityEngine;
using SolitaryAudio;
using UnityEngine.Rendering;
using GM;

public class Switch : MonoBehaviour
{
    [SerializeField] Light livingRoomLight;
    [SerializeField] GameObject lampOff;
    [SerializeField] GameObject lampOn;

    RaycastHit hit;
    Ray ray;

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameFuncs.PlayerScript.IsControl())
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1.8f))
            {
                if (hit.distance >= 1.8f)
                {
                    return;
                }

                if (livingRoomLight.enabled)
                {
                    LightOff();
                }
                else
                {
                    LightOn();
                }

            }
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
