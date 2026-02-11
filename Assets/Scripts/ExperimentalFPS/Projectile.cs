using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string projName = "";
    private float minDamage = 14f;
    private float maxDamage = 20f;

    public float MaxDamage { get => maxDamage; set => maxDamage = value; }
    public float MinDamage { get => minDamage; set => minDamage = value; }
}
