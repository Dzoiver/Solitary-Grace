using UnityEngine;
using DG.Tweening;
using GM;
using SolitaryAudio;
using Zenject;
using UnityEngine.Events;

public class DoorOpen : MonoBehaviour
{
    Inventory inventory;
    [Inject] DialogueManager dManager;
    [SerializeField] GameObject destinationPoint;
    public bool Closed = false;
    [SerializeField] ScriptableItem key;
    [SerializeField] ScriptableMes lines;
    [SerializeField] bool playLockedSound = true;

    [SerializeField] GameObject destinationLeft;
    [SerializeField] GameObject destinationRight;
    public string lockedMessage = "The door is jammed";
    public UnityEvent onEnter;

    private string doorSound;
    private string defaultOpen = "Sounds/door-14-open";
    private string unlockOpen = "Sounds/key-lock-soundFixed";

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

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
                doorSound = unlockOpen;
                Closed = false;
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        doorSound = defaultOpen;
        if (other.gameObject.name == "UseCube") // If a player presses E button on the door
        {
            //other.gameObject.SetActive(false);
            if (Closed)
            {
                if (!PlayerHasKey())
                {
                    if (playLockedSound)
                        AudioController.Play("doorOpen");
                    dManager.SetDialogue(lines);
                    if (lines == null)
                        dManager.SetDialogue(lockedMessage);
                    dManager.PlayDialogue(0);
                    return;
                }
            }
            GameFuncs.PlayerScript.SetControl(false);
            AudioController.PlayOneShot(Resources.Load<AudioClip>(doorSound));
            GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 1), 1f).onComplete = () => // Fadeout
            {
                onEnter.Invoke();
                AudioController.Play("doorClose");
                GameFuncs.TeleportPlayer(destinationPoint);
                GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 0), 1f); // Fadein
                GameFuncs.PlayerScript.SetControl(true);
            };
        }
    }

    public void Unlock()
    {
        Closed = false;
    }
}
