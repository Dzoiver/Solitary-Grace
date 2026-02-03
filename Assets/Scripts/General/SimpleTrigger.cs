using DG.Tweening;
using GM;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent onEnter;
    [SerializeField] UnityEvent onPress;
    [SerializeField] UnityEvent onFinish;
    [SerializeField] UnityEvent onExit;

    [SerializeField] float startDelay = 0f;
    [SerializeField] float finishDelay = 0f;
    [SerializeField] private bool triggerOnce = false;
    [SerializeField] bool disableRendering = true;

    [SerializeField] bool checkItem = false;
    [SerializeField] ScriptableItem checkItemScriptable;
    [SerializeField] string noItemMessage = "It seems like something is missing";
    Inventory inventory;
    MessagesUI messageUI;
    public bool active = true;
    BoxCollider collider;
    private float bufferVolume = 1f;
    RaycastHit hit;
    Ray ray;

    public float BufferVolume { get => bufferVolume; set => bufferVolume = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (!active)
        {
            gameObject.layer = 2;
        }

        if (checkItem)
        {
            messageUI = FindObjectOfType<MessagesUI>();
            inventory = FindObjectOfType<Inventory>();
        }

        collider = GetComponent<BoxCollider>();
        if (disableRendering)
        {
            MeshRenderer mesh = GetComponent<MeshRenderer>();
            if (mesh != null)
                mesh.enabled = false;
        }
            
    }

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

                if (gameObject.layer != 2)
                {
                    if (checkItemScriptable != null)
                    {
                        if (!inventory.Has(checkItemScriptable.id, out var slot))
                        {
                            messageUI.ShowMessage(noItemMessage);
                            return;
                        }
                        else
                        {
                            inventory.DeleteItem(slot, 1);
                        }
                    }

                    if (triggerOnce)
                    {
                        collider.enabled = false;
                    }
                    StartCoroutine(OpeningCoroutine(onPress, startDelay));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
            return;

        if (other.CompareTag("Player") && onEnter.GetPersistentEventCount() > 0)
        {
            if (checkItemScriptable != null)
            {
                if (!inventory.Has(checkItemScriptable.id, out var slot))
                {
                    messageUI.ShowMessage(noItemMessage);
                    return;
                }
                else
                {
                    inventory.DeleteItem(slot, 1);
                }

            }

            if (triggerOnce)
            {
                collider.enabled = false;
            }
            Debug.Log("enter");
            StartCoroutine(OpeningCoroutine(onEnter, startDelay));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!active)
            return;
        if (other.CompareTag("Player") && onExit.GetPersistentEventCount() > 0)
        {
            Debug.Log("exit");
            onExit.Invoke();
        }
    }

    public void SetActiveTrigger(bool value = true)
    {
        active = value;
        if (active)
            gameObject.layer = 3;
        else
            gameObject.layer = 2;
        collider.enabled = value;
    }

    public void DisablePlayer() => GameFuncs.PlayerScript.SetControl(false);

    public void PlayerFaint()
    {
        Camera.main.GetComponent<Animator>().enabled = true;
        Camera.main.GetComponent<Animator>().Play("FallAnimation");
    }

    public void SilenceAudio(AudioSource source)
    {
        source.DOFade(0f, 6f);
    }

    IEnumerator OpeningCoroutine(UnityEvent startEvent, float delay)
    {
        yield return new WaitForSeconds(delay);
        startEvent.Invoke();
        StartCoroutine(FinishCoroutine(onFinish, finishDelay));
    }

    IEnumerator FinishCoroutine(UnityEvent finishEvent, float delay)
    {
        yield return new WaitForSeconds(delay);
        finishEvent.Invoke();
    }

    public void FadeOutAudio(AudioSource audio)
    {
        if (audio.isPlaying)
        {
            audio.DOFade(0f, 1f).OnComplete(audio.Stop);
        }
    }

    public void FadeInAudio(AudioSource audio)
    {
        audio.Play();
        audio.DOFade(BufferVolume, 1f);
    }
}
