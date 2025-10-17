using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class MessagesUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] textUI;
    Vector3 defaultTextPos;
    float elevateAmount = 50f;
    int freeText = 0;

    int textIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        defaultTextPos = textUI[0].gameObject.transform.position;
        foreach (TextMeshProUGUI t in textUI)
        {
            t.gameObject.SetActive(false);
        }
    }

    public void ShowPickup(string itemName)
    {
        TextMeshProUGUI textmesh = FindFreeText();
        textmesh.color = Color.white;
        textmesh.text = "You picked up " + itemName;

        textmesh.DOFade(1f, 0.5f).OnComplete(() =>
        {
        DOVirtual.DelayedCall(2f, () => {
            textmesh.DOFade(0f, 1f).OnComplete(() =>
            {
                textmesh.gameObject.SetActive(false);
            });
        });
        });
    }

    private TextMeshProUGUI FindFreeText()
    {
        Vector3 newPosition = new Vector3();
        float maxY = 0f;
        TextMeshProUGUI freeTextObj = null;

        foreach (TextMeshProUGUI t in textUI)
        {
            newPosition = t.gameObject.transform.position;
            newPosition.y += elevateAmount;
            t.transform.position = newPosition;
        }

        foreach (TextMeshProUGUI t in textUI)
        {
            if (t.transform.position.y > maxY)
            {
                maxY = t.transform.position.y;
                freeTextObj = t;
            }
        }

        freeTextObj.transform.position = defaultTextPos;
        freeTextObj.color = new Color(1f, 1f, 1f, 0f);
        freeTextObj.gameObject.SetActive(true);
        return freeTextObj;

    }

    public void FullInventory()
    {
        TextMeshProUGUI textmesh = FindFreeText();
        textmesh.color = Color.red;
        textmesh.text = "Inventory is full";

        textmesh.DOFade(1f, 0.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f, () => {
                textmesh.DOFade(0f, 1f).OnComplete(() =>
                {
                    textmesh.gameObject.SetActive(false);
                });
            });
        });
    }
}
