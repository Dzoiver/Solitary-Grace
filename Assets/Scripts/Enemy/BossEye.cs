using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEye : MonoBehaviour
{
    DOTweenAnimation eyeAnim;
    bool opened = false;
    bool killed = false;
    BossHealer healer;
    MeshRenderer meshRenderer;
    [SerializeField] FleshWall fleshwall;
    [SerializeField] Material eyeActiveMat;
    [SerializeField] Material eyeDeadMat;
    [SerializeField] AudioClip[] glassSounds;
    AudioSource audio;

    public bool Opened { get => opened; set => opened = value; }
    public bool Killed { get => killed; set => killed = value; }

    void Start()
    {
        eyeAnim = GetComponent<DOTweenAnimation>();
        healer = FindObjectOfType<BossHealer>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = eyeDeadMat;
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Opened)
            return;
        if (other.CompareTag("Bullet"))
        {
            KillEye();
        }
    }

    public void OpenEye()
    {
        if (!Opened)
        {
            meshRenderer.material = eyeActiveMat;
            Opened = true;
            healer.closedEyes.Remove(this);
            eyeAnim.DOPlayForward();
        }
    }

    public void KillEye()
    {
        if (!Opened)
            return;

        int rng = Random.Range(0, 2);
        audio.PlayOneShot(glassSounds[rng]);
        eyeAnim.DOPlayBackwards();
        Opened = false;
        Killed = true;
        meshRenderer.material = eyeDeadMat;
        healer.aliveEyes.Remove(this);
        healer.closedEyes.Remove(this);
        fleshwall.Count++;
    }

    public void HideEye()
    {
        if (!Opened)
            return;

        healer.closedEyes.Add(this);
        eyeAnim.DOPlayBackwards();
        Opened = false;
        meshRenderer.material = eyeDeadMat;
    }
}
