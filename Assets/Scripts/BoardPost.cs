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
    [SerializeField] ImageBoard imageBoard;

    private void Start()
    {
        //Publish();
    }

    // Update is called once per frame
    void Update()
    {
        //Publish();
    }

    public void Publish()
    {
        print("published");
        imageBoard.NotificationCount++;
        Computer.notificationBell.SetActive(true);
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
