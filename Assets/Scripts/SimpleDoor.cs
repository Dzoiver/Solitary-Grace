using UnityEngine;
using DG.Tweening;
using GM;
using SolitaryAudio;
using Zenject;
using UnityEngine.Events;

public class SimpleDoor : MonoBehaviour
{
    Inventory inventory;
    [Inject] DialogueManager dManager;
    GameObject destinationPoint;
    public bool ClosedRed = false;
    public bool ClosedBlue = false;
    [SerializeField] ScriptableItem key;
    [SerializeField] ScriptableMes lines;
    [SerializeField] bool playLockedSound = true;
    [SerializeField] GameObject destinationBlue;
    [SerializeField] GameObject destinationRed;
    [SerializeField] UnityEvent onOpenBlue;
    [SerializeField] UnityEvent onOpenRed;
    UnityEvent onOpen;
    public string stringText = "";
    private string defaultOpen = "Sounds/door-14-open";
    private string unlockOpen = "Sounds/key-lock-soundFixed";
    private string doorSound;
    private string ladderSoundName = "Sounds/metal-footsteps";
    public bool ladder = false;
    // Start is called before the first frame update
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        doorSound = defaultOpen;
    }

    private bool PlayerHasKey()
    {
        if (key == null)
            return false;

        int slot = 0;

        if (inventory.Has(key.id, out slot))
        {
            doorSound = unlockOpen;

            inventory.DeleteItem(slot, 1);
            ClosedBlue = false;
            ClosedRed = false;
            return true;
        }

        return false;
    }

    public void Unlock()
    {
        ClosedBlue = false;
        ClosedRed = false;
    }

    public GameObject GetFurtherDestination()
    {
        if (Vector3.Distance(GameFuncs.PlayerScript.gameObject.transform.position, destinationRed.transform.position) >
            Vector3.Distance(GameFuncs.PlayerScript.gameObject.transform.position, destinationBlue.transform.position))
        {
            onOpen = onOpenBlue;
            return destinationRed;
        }
        else
        {
            onOpen = onOpenRed;
            return destinationBlue;
        }
    }

    public bool CanOpen()
    {
        bool canOpen = true;
        if (GetFurtherDestination() == destinationBlue && ClosedRed && !PlayerHasKey())
        {
            canOpen = false;
        }
        if (GetFurtherDestination() == destinationRed && ClosedBlue && !PlayerHasKey())
        {
            canOpen = false;
        }

        return canOpen;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UseCube") // If a player presses E button on the door
        {
            //other.gameObject.SetActive(false);
            TryOpen();
        }
    }

    public void TryOpen()
    {
        doorSound = defaultOpen;
        if (ladder)
            doorSound = ladderSoundName;

        if (!CanOpen())
        {
            if (playLockedSound)
                AudioController.Play("doorOpen");
            if (lines != null)
                dManager.SetDialogue(lines);
            else
                dManager.SetDialogue(stringText);
            dManager.PlayDialogue(0);
            return;
        }

        destinationPoint = GetFurtherDestination();
        GameFuncs.PlayerScript.SetControl(false);
        AudioController.PlayOneShot(Resources.Load<AudioClip>(doorSound));
        GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 1), 1f).onComplete = () => // Fadeout
        {
            onOpen.Invoke();
            if (!ladder)
                AudioController.Play("doorClose");
            GameFuncs.TeleportPlayer(destinationPoint);
            GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 0), 1f); // Fadein
            GameFuncs.PlayerScript.SetControl(true);
        };
    }
}
