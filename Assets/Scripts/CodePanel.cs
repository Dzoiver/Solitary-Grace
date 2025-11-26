using GM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CodePanel : MonoBehaviour
{
    public string code = "7320";
    TMP_InputField inputField;
    private bool solved = false;
    private bool wrong = false;
    private bool activated = false;
    public UnityEvent onSolve;
    Color solvedColor = new Color(0.5f, 1f, 0.5f, 1f);
    Color wrongColor = new Color(1f, 0.5f, 0.5f, 1f);
    Color normalColor = new Color();
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        image = GetComponent<Image>();
        normalColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Try enter code");
            if (wrong)
            {
                Clear();
                return;
            }

            EnterCode();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Exit");
            Deactivate();
        }
    }

    public void Activate()
    {
        activated = true;
        GameFuncs.PlayerScript.SetControl(false);
        inputField.ActivateInputField();
    }

    public void Deactivate()
    {
        activated = false;
        GameFuncs.PlayerScript.SetControl(true);
        inputField.DeactivateInputField();
    }

    public bool EnterCode()
    {
        if (inputField.text == code)
        {
            Debug.Log("Correct");
            Solved();
            return true;
        }
        else
        {
            Debug.Log("Incorrect");
            wrong = true;
            image.color = wrongColor;
            return false;
        }
    }

    public void Solved()
    {
        onSolve.Invoke();
        Deactivate();
        enabled = false;
        solved = true;
        image.color = solvedColor;
    }

    public void OnValueChange()
    {
        // sound
    }

    public void Clear()
    {
        wrong = false;
        inputField.text = "";
        image.color = normalColor;
    }
}
