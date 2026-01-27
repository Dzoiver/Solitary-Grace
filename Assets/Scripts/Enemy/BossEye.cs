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
    [SerializeField] AudioClip[] glassSounds;
    AudioSource audio;
    void Start()
    {
        eyeAnim = GetComponent<DOTweenAnimation>();
        healer = FindObjectOfType<BossHealer>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = eyeDeadMat;
        audio = GetComponent<AudioSource>();
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
        if (!opened)
            return;
        int rng = Random.Range(0, 1);

        audio.PlayOneShot(glassSounds[rng]);
        eyeAnim.DOPlayBackwards();
        opened = false;
        meshRenderer.material = eyeDeadMat;
        healer.DeleteEye();
    }
}
