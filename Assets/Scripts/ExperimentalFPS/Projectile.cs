using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string projName = "";
    private float damage = 17f;

    public float Damage { get => damage; set => damage = value; }
}
