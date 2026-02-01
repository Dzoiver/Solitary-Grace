using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOnFire : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject newRespawn;
    CameraReturnControls cameracontrols;
    [SerializeField] GameObject room;
    [SerializeField] GameObject deloadWhileInRoom;
    [SerializeField] GameObject fire;
    [SerializeField] GetSomeSleep getsomesleep;
    [SerializeField] GameObject wakeupdest;
    GameOver gameover;

    private void Awake()
    {
        gameover = FindObjectOfType<GameOver>();
        CameraReturnControls[] scripts = FindObjectsOfType<CameraReturnControls>();
        cameracontrols = FindObjectOfType<CameraReturnControls>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckBothItems()
    {
        if (inventory.Has(11) && inventory.Has(12))
        {
            //wakeupdest.transform.position = newRespawn.transform.position;
            //Debug.Log(Checkpoint.onTeleportGlobal);
            //Checkpoint.onTeleportGlobal.AddListener(SetUpFireAndRoom);
            cameracontrols.ChangeWakeUpStart(newRespawn.transform);
            gameover.onRespawn.AddListener(SetUpFireAndRoom);
            
        }
    }

    public void SetUpFireAndRoom()
    {
        fire.SetActive(true);
        room.SetActive(true);
        deloadWhileInRoom.SetActive(false);
    }

    public void UnsetUpFire()
    {
        if (!room.activeSelf)
            return;
        gameover.onRespawn.RemoveListener(SetUpFireAndRoom);
        //Checkpoint.onTeleportGlobal.RemoveListener(SetUpFireAndRoom);
    }
}
