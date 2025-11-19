using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardPost : MonoBehaviour
{
    public string nickname = "";
    public string postDescription = "";
    public Image image;
    [SerializeField] TextMeshProUGUI dateText;
    DateTime currentUtcDateTime;

    // Start is called before the first frame update
    void Start()
    {
        Publish();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Publish()
    {
        currentUtcDateTime = DateTime.Now;
        gameObject.SetActive(true);
        dateText.text = currentUtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public void Delete()
    {

    }

    public void Like()
    {

    }
}
