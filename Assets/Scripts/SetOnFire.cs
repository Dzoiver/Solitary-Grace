using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class SetOnFire : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject newRespawn;
    [SerializeField] GameObject room;
    [SerializeField] GameObject deloadWhileInRoom;
    [SerializeField] GameObject fire;
    [SerializeField] GetSomeSleep getsomesleep;
    [SerializeField] GameObject prisonDestination;
    [SerializeField] DoorOpen door;
    GameObject startDestination;
    [SerializeField] GameObject blockSleep;

    private void Awake()
    {
        CameraReturnControls[] scripts = FindObjectsOfType<CameraReturnControls>();
    }
    // Start is called before the first frame update
    void Start()
    {
        startDestination = door.destinationPoint;
    }

    public void CheckBothItems()
    {
        if (inventory.Has(11) && inventory.Has(12))
        {
            //wakeupdest.transform.position = newRespawn.transform.position;
            //Debug.Log(Checkpoint.onTeleportGlobal);
            //Checkpoint.onTeleportGlobal.AddListener(SetUpFireAndRoom);
            //cameracontrols.ChangeWakeUpStart(newRespawn.transform);
            //gameover.onRespawn.AddListener(SetUpFireAndRoom);
            Debug.Log("getting ready");
            door.destinationPoint = prisonDestination;
            door.onEnter.AddListener(SetUpFireAndRoom);
            blockSleep.SetActive(true);
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
        blockSleep.SetActive(false);
        door.destinationPoint = startDestination;
        door.onEnter.RemoveListener(SetUpFireAndRoom);
        //gameover.onRespawn.RemoveListener(SetUpFireAndRoom);
    }

    public void DeloadFireThing()
    {
        fire.SetActive(true);
        room.SetActive(true);
        deloadWhileInRoom.SetActive(false);
    }
}
