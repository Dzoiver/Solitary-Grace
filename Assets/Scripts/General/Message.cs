using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;
using GM;
using Unity.VisualScripting;

public class Message : MonoBehaviour
{
    static private Message oldMessageScript;
    TextMeshProUGUI text;
    [SerializeField] string[] messageText;
    public UnityEvent onTrigger;
    public UnityEvent onFinish;
    [SerializeField] bool centerText = false;
    AudioSource audio;
    [SerializeField] AudioClip[] clips;

    TextShow textObject;
    private int textIndex = 0;
    RaycastHit hit;
    Ray ray;
    float currentIconTime = 0f;
    float iconTime = 0.5f;

    private void Awake()
    {
        textObject = FindAnyObjectByType<TextShow>();
        if (centerText)
            text = textObject.centerText;
        else
            text = textObject.GetComponent<TextMeshProUGUI>();
        enabled = false;

        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!text.enabled)
            return;
        if (textIndex < messageText.Length)
        {
            currentIconTime += Time.deltaTime;
            if (currentIconTime > iconTime)
            {
                if (text.text.Contains("<sprite=1>"))
                    text.text = text.text.Replace("<sprite=1>", "<sprite=0>");
                else
                {
                    text.text = text.text.Replace("<sprite=0>", "<sprite=1>");
                }
                currentIconTime = 0f;
            }
        }

        if (Vector3.Distance(hit.point, GameFuncs.PlayerScript.transform.position) >= 1.8f)
        {
            CloseText();
        }
    }

    private void OnMouseExit()
    {
        CloseText();
    }

    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E) && !NPCDialogue.DialoguePlaying)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            text.horizontalAlignment = HorizontalAlignmentOptions.Center;
            // If you switched to different text in the middle of the text progression, resets the progression
            if (!oldMessageScript)
            {
                oldMessageScript = this;
                if (Physics.Raycast(ray, out hit, 1.8f))
                {
                    
                    if (hit.distance < 1.8f)
                    {
                        ShowText();
                        return;
                    }
                    return;
                }
            }

            if (oldMessageScript != this)
                textIndex = 0;
            oldMessageScript = this;
            if (Physics.Raycast(ray, out hit, 1.8f))
            {
                if (hit.distance <= 1.8f)
                {
                    ShowText();
                }
            }
        }
    }

    private void CloseText()
    {
        textIndex = 0;
        text.enabled = false;
        enabled = false;
    }

    private void ShowText()
    {
        if (clips.Length > 0)
            PlayRandomClip();
        enabled = true;
        if (textIndex == messageText.Length)
        {
            onFinish.Invoke();
            CloseText();
            return;
        }

        text.text = messageText[textIndex] + "<sprite=1>";
        textIndex++;
        text.enabled = true;
        onTrigger.Invoke();
    }

    private void PlayRandomClip()
    {
        audio.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
