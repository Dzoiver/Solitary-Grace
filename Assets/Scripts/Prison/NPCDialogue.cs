using GM;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

[System.Serializable]
public class StringArray
{
    public string[] messages;
}

public class NPCDialogue : MonoBehaviour
{
    static public bool DialoguePlaying = false;
    public List<StringArray> messageGroups = new List<StringArray>();
    int groupIndex = 0;
    //[SerializeField] string[] messageText;
    int textIndex = 0;
    TextShow textObject;
    TextMeshProUGUI text;
    public UnityEvent onTrigger;
    public UnityEvent onLeave;
    public UnityEvent onFinish;
    [SerializeField] bool triggerOnce = false;
    bool trigger = true;

    float currentTime = 0f;
    [SerializeField] float printNextTime = 0.05f;
    int characterIndex = 0;

    bool printing = false;
    RaycastHit hit;
    Ray ray;

    private void Awake()
    {
        textObject = FindAnyObjectByType<TextShow>();
        text = textObject.GetComponent<TextMeshProUGUI>();
    }

    private void OnMouseOver() // Start Dialogue
    {
        if (Input.GetKeyDown(KeyCode.E) && GameFuncs.PlayerScript.IsControl() && !DialoguePlaying)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1.8f))
            {
                if (hit.distance >= 1.8f)
                {
                    return;
                }

                text.horizontalAlignment = HorizontalAlignmentOptions.Left;
                text.text = "";
                PrintDialogue();
            }
        }
    }

    private void Update() // Skip dialogue
    {
        if (Vector3.Distance(GameFuncs.PlayerScript.gameObject.transform.position, transform.position) >= 4f)
        {
            onLeave.Invoke();
            CloseText();
        }
        else if (Input.GetKeyDown(KeyCode.E) && GameFuncs.PlayerScript.IsControl() && characterIndex > 0) 
        {
            PrintDialogue();
        }
    }

    private void FixedUpdate()
    {
        if (printing == false)
            return;

        currentTime += Time.deltaTime;

        if (currentTime > printNextTime)
        {
            currentTime = 0f;
            characterIndex++;
            text.text = messageGroups[groupIndex].messages[textIndex][0..characterIndex];

            if (characterIndex == messageGroups[groupIndex].messages[textIndex].Length)
            {
                textIndex++;
                printing = false;
                return;
            }
        }
    }

    private void PrintDialogue()
    {
        DialoguePlaying = true;
        if (trigger)
            onTrigger.Invoke();

        if (triggerOnce)
            trigger = false;


        enabled = true;
        print(textIndex);
        if (textIndex == messageGroups[groupIndex].messages.Length)
        {
            if (groupIndex < messageGroups.Count - 1)
            groupIndex++;

            onFinish.Invoke();
            CloseText();
            return;
        }
        characterIndex = 0;
        if (printing)
        {
            characterIndex = messageGroups[groupIndex].messages[textIndex].Length - 1;
        }
        printing = true;
        text.enabled = true;
    }

    private void CloseText()
    {
        text.text = "";
        textIndex = 0;
        text.enabled = false;
        enabled = false;
        DialoguePlaying = false;
    }
}
