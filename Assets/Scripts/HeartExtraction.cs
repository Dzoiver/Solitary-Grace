using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartExtraction : MonoBehaviour
{
    [SerializeField] GameObject heartModel;
    float timeToFade = 0.3f;
    BoxCollider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExtractHeart()
    {
        StartCoroutine(ExtactRoutine());

    }

    IEnumerator ExtactRoutine()
    {
        GameFuncs.PlayerScript.SetControl(false);
        GameFuncs.FadeIn(timeToFade);
        yield return new WaitForSeconds(0.5f);
        heartModel.SetActive(true);
        collider.enabled = true;
        GameFuncs.FadeOut(timeToFade);
        GameFuncs.PlayerScript.SetControl(true);
    }
}
