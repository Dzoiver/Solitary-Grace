using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ImageBoard : MonoBehaviour
{
    [SerializeField] GameObject notificationIcon;
    ImageBoard[] posts;
    [SerializeField] TextMeshProUGUI notificationsCountText;
    private int notificationCount = 0;

    public int NotificationCount { get => notificationCount;
        set 
        {
            if (value > 0)
            {
                notificationsCountText.text = value.ToString();
                notificationIcon.SetActive(true);
            }
            else
                notificationIcon.SetActive(false);
            notificationCount = value;
        }
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //notificationIcon.SetActive(false);
    }

    private void OnEnable()
    {
        notificationIcon.SetActive(false);
        notificationCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
