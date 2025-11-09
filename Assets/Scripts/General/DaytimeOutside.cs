using GM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DaytimeOutside : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private bool _dayTime;
    [SerializeField] private Material skybox;
    public bool dayTime
    {
        get { return _dayTime; }
        set
        {
            _dayTime = value;
            SetDay(_dayTime);
        }
    }

    private void Start()
    {
        SetDay(_dayTime);
    }
    // Start is called before the first frame update
    public void SetDay(bool interactable)
    {
        if (interactable)
        {
            Camera.main.clearFlags = CameraClearFlags.Skybox;
            RenderSettings.skybox = skybox;
            directionalLight.gameObject.SetActive(true);
            directionalLight.intensity = 1;
            RenderSettings.ambientLight = new Color(0.55f, 0.55f, 0.55f);
        }
        if (!interactable)
        {
            Camera.main.clearFlags = CameraClearFlags.SolidColor;
            RenderSettings.skybox = null;
            directionalLight.intensity = 0;
            RenderSettings.ambientLight = new Color(0f, 0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
