using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SensitivityController : MonoBehaviour
{
    MouseLook mouseScript;
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    private void Awake()
    {
        mouseScript = FindObjectOfType<MouseLook>();
        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            slider.value = PlayerPrefs.GetFloat("MouseSensitivity");
            SetSensitivity();
        }
    }

    public void SetSensitivity()
    {
        if (mouseScript != null)
            mouseScript.preferenceSens = slider.value;
        PlayerPrefs.SetFloat("MouseSensitivity", slider.value);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
