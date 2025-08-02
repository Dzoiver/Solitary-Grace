using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConfirmationBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textObject;
    private Menu menu;
    private void Awake()
    {
        menu = GetComponent<Menu>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeItemName(string itemname)
    {
        textObject.text = "Do you wish to pick up the " + itemname + "?";
    }

    public void ConfirmYes()
    {
        // menu.add
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
