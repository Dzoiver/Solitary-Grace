using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 2)]
public class ScriptableItem : ScriptableObject
{
    public int id;
    public LocalizedString localizedName;
    public string name
    {
        get
        {
            return localizedName.GetLocalizedString();
        }
    }
    public int maxQuantity;
    public int quantity;
    public Sprite sprite;
    public bool keyitem;
}
