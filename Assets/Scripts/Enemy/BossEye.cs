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
        meshRenderer.material = eyeDeadMat;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!opened)
            return;
        if (other.CompareTag("Bullet"))
        {
            opened = false;
            CloseEye();
        }
    }

    public void OpenEye()
    {
        if (!opened && !eyeAnim.tween.IsPlaying())
        {
            meshRenderer.material = eyeActiveMat;
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
