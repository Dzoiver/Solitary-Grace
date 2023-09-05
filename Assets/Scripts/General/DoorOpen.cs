using UnityEngine;
using DG.Tweening;
using GM;
using SolitaryAudio;
using Zenject;

public class DoorOpen : MonoBehaviour
{
    [Inject] Inventory inventory;
    [SerializeField] GameObject destinationPoint;
    public bool Closed = false;
    [SerializeField] ScriptableItem key;

    private bool PlayerHasKey()
    {
        foreach (InventoryItem item in inventory.ItemsList)
        {
            if (item.Name == "Camera Key")
            {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UseCube") // If a player presses E button on the door
        {
            if (Closed)
            {
                if (!PlayerHasKey())
                {
                    AudioController.Play("doorOpen");
                    return;
                }
            }

            GameFuncs.PlayerScript.SetControl(false);
            AudioController.Play("doorOpen");
            GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 1), 1f).onComplete = () => // Fadeout
            {
                AudioController.Play("doorClose");
                GameFuncs.TeleportPlayer(destinationPoint);
                GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 0), 1f); // Fadein
                GameFuncs.PlayerScript.SetControl(true);
            };
        }
    }
}
