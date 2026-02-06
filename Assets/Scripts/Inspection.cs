using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class Inspection : MonoBehaviour
{
    [SerializeField] ContextMenuItem item;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image image;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnEnable()
    {
        //UpdateDescription();
    }

    public void UpdateDescription()
    {
        gameObject.SetActive(true);
        if (item.currentItem != null)
        {
            text.text = item.currentItem.Description;
            image.sprite = item.currentItem.ItemSprite;
        }
        else
        {
            text.text = "No description for this item";
            image.sprite = null;
        }
    }
}
