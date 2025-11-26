using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEye : MonoBehaviour
{
    DOTweenAnimation eyeAnim;
    bool opened = false;
    BossHealer healer;
    MeshRenderer meshRenderer;
    [SerializeField] Material eyeActiveMat;
    [SerializeField] Material eyeDeadMat;
    void Start()
    {
        eyeAnim = GetComponent<DOTweenAnimation>();
        healer = FindObjectOfType<BossHealer>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && opened == true)
        {
            CloseEye();
        }
    }

    public void OpenEye()
    {
        if (!opened)
        {
            healer.AddEye();
            opened = true;
            eyeAnim.DOPlay();
        }
    }

    public void CloseEye()
    {
        eyeAnim.DOPlayBackwards();
        opened = false;
        meshRenderer.material = eyeDeadMat;
        healer.DeleteEye();
    }
}
