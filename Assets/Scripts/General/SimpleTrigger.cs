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
            GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
            return;

        if (other.CompareTag("Player") && onEnter.GetPersistentEventCount() > 0)
        {
            if (checkItemScriptable != null)
            {
                if (!inventory.Has(checkItemScriptable.id))
                {
                    messageUI.ShowMessage(noItemMessage);
                    return;
                }
            }

            if (triggerOnce)
            {
                collider.enabled = false;
            }
            StartCoroutine(OpeningCoroutine(onEnter, startDelay));
        }

        if (other.gameObject.name == "UseCube" && gameObject.layer != 2)
        {
            other.gameObject.SetActive(false);
            if (checkItemScriptable != null)
            {
                if (!inventory.Has(checkItemScriptable.id))
                {
                    messageUI.ShowMessage(noItemMessage);
                    return;
                }
            }

            if (triggerOnce)
            {
                collider.enabled = false;
            }
            StartCoroutine(OpeningCoroutine(onPress, startDelay));
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
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
}
