using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Linq;
using System.Drawing;
using Color = UnityEngine.Color;

public class MessagesUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] textUI;
    [SerializeField] TextMeshProUGUI tip;
    bool firstTime = true;
    [SerializeField] string pickupMes = "You picked up ";
    [SerializeField] string inventoryFull = "Inventory is full";
    [SerializeField] string useMes = "You used ";
    [SerializeField] TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        tip.DOFade(0f, 0f);
        foreach (TextMeshProUGUI t in textUI)
        {
            t.color = new Color(1f, 1f, 1f, 0f);
        }
    }
    public void ShowPickup(string itemName)
    {
        if (firstTime)
        {
            tip.DOFade(1f, 2f).OnComplete(() => {
                tip.DOFade(0f, 1f).SetDelay(5f);
            });
        }
        firstTime = false;
        TextMeshProUGUI textmesh = FindFreeText();
        textmesh.color = Color.white;
        textmesh.text = pickupMes + " <color=green>" + itemName + "</color>";

        textmesh.DOFade(1f, 0.5f).OnComplete(() =>
        {
        DOVirtual.DelayedCall(2f, () => {
            textmesh.DOFade(0f, 1f).OnComplete(() =>
            {
                //textmesh.gameObject.SetActive(false);
            });
        });
        });
    }

    public void ShowUsage(string itemName)
    {
        firstTime = false;
        TextMeshProUGUI textmesh = FindFreeText();
        textmesh.color = Color.white;
        textmesh.text = useMes + " <color=green>" + itemName + "</color>";

        textmesh.DOFade(1f, 0.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f, () => {
                textmesh.DOFade(0f, 1f).OnComplete(() =>
                {
                    //textmesh.gameObject.SetActive(false);
                });
            });
        });
    }

    public void ShowMessage(string mesText)
    {
        TextMeshProUGUI textmesh = FindFreeText();
        textmesh.color = Color.white;
        textmesh.text = mesText;

        textmesh.DOFade(1f, 0.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f, () => {
                textmesh.DOFade(0f, 1f).OnComplete(() =>
                {
                    //textmesh.gameObject.SetActive(false);
                });
            });
        });
    }

    private TextMeshProUGUI FindFreeText()
    {
        TextMeshProUGUI text = textUI[0].transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>();

        text.transform.SetAsLastSibling();
        text.DOKill();
        text.DOFade(1f, 0f);
        return text;
        /*
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
        */
    }

    public void FullInventory()
    {
        TextMeshProUGUI textmesh = FindFreeText();
        textmesh.color = Color.red;
        textmesh.text = inventoryFull;
        textmesh.DOFade(1f, 0.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2f, () => {
                textmesh.DOFade(0f, 1f).OnComplete(() =>
                {
                    //textmesh.gameObject.SetActive(false);
                });
            });
        });
    }
}
