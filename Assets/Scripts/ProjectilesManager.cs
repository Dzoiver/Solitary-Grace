using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesManager : MonoBehaviour
{
    [SerializeField] GunBullet[] bullets;
    int bulletIndex = -1;

    public GunBullet GetNewBullet()
    {
        if (bulletIndex < bullets.Length - 1)
            bulletIndex++;
        else
            bulletIndex = 0;
        return bullets[bulletIndex];
    }
}
