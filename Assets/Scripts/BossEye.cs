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
    // Start is called before the first frame update
    void Start()
    {
        eyeAnim = GetComponent<DOTweenAnimation>();
        healer = FindObjectOfType<BossHealer>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Bullet") && opened == true)
        {
            eyeAnim.DOPlayBackwards();
            opened = false;
            meshRenderer.material = eyeDeadMat;
            healer.DeleteEye();
        }
    }

    public void OpenEye()
    {
        healer.AddEye();
        opened = true;
        eyeAnim.DOPlay();
        meshRenderer.material = eyeActiveMat;
    }
}
