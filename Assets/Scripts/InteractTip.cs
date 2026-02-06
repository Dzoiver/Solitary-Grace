using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class InteractTip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tipText;
    // Start is called before the first frame update
    void Start()
    {
        tipText.DOFade(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            tipText.DOFade(1f, 2f).OnComplete(() => {
                tipText.DOFade(0f, 1f).SetDelay(5f);
            });
        }
    }
}
