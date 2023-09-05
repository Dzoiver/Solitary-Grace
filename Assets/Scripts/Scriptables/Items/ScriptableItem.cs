using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 2)]
public class ScriptableItem : ScriptableObject
{
    public int id;
    public string name;
    public int maxQuantity;
    public int quantity;
}
