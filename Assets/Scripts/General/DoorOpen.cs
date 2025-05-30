using UnityEngine;
using DG.Tweening;
using GM;
using SolitaryAudio;
using Zenject;

public class DoorOpen : MonoBehaviour
{
    [Inject] Inventory inventory;
    [Inject] DialogueManager dManager;
    [SerializeField] GameObject destinationPoint;
    public bool Closed = false;
    [SerializeField] ScriptableItem key;
    [SerializeField] ScriptableMes lines;
    [SerializeField] bool playLockedSound = true;

    public void DoorCanBeOpened(bool canOpen)
    {
        if (canOpen)
            Closed = false;
        else
            Closed = true;
    }
    private bool PlayerHasKey()
    {
        if (key == null)
            return false;

        foreach (InventoryItem item in inventory.ItemsList)
        {
            if (item.Name == key.name)
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
                    if (playLockedSound)
                        AudioController.Play("doorOpen");
                    dManager.SetDialogue(lines);
                    dManager.PlayDialogue(0);
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
