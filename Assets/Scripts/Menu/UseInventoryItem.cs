using GM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseInventoryItem : MonoBehaviour
{
    ContextMenuItem context;
    private void Awake()
    {
        context = FindObjectOfType<ContextMenuItem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TryUse(Inventory item)
    {
        switch(item.name)
        {
            case "Health Drink":
                GameFuncs.PlayerScript.GiveHP(30f);
                context.gameObject.SetActive(false);
                break;
            case "Tom":
                Console.WriteLine("Ваше имя - Tom");
                break;
            case "Sam":
                Console.WriteLine("Ваше имя - Sam");
                break;
            default:
                break;
            }
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
