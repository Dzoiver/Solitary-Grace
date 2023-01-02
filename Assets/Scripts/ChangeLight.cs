using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLight : MonoBehaviour
{
    [SerializeField] Material skyboxDefault;
    [SerializeField] PlayerScript player;
    Color ambientColor;
    // Start is called before the first frame update
    void Start()
    {
        ambientColor = new Color(0.36f, 0.34f, 0.36f);
    }

    private void OnTriggerEnter(Collider other)
    {
        RenderSettings.ambientLight = Color.white;
        RenderSettings.skybox = skyboxDefault;
        gameObject.SetActive(false);
        player.Warping(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
