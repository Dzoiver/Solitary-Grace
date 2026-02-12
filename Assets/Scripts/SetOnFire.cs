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
    [SerializeField] GameObject loadOnLeavingTheRoom;
    [SerializeField] DoorOpen door;
    GameObject startDestination;
    [SerializeField] SimpleTrigger blockSleep;
    bool item11Taken = false;
    bool item12Taken = false;
    [SerializeField] GameOver gameover;
    [SerializeField] GameObject objectsAfterFire;

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
        if (inventory.Has(11))
            item11Taken = true;

        if (inventory.Has(12))
            item12Taken = true;

        if (item11Taken && item12Taken)
        {
            //wakeupdest.transform.position = newRespawn.transform.position;
            //Debug.Log(Checkpoint.onTeleportGlobal);
            //Checkpoint.onTeleportGlobal.AddListener(SetUpFireAndRoom);
            //cameracontrols.ChangeWakeUpStart(newRespawn.transform);
            //gameover.onRespawn.AddListener(SetUpFireAndRoom);
            door.destinationPoint = prisonDestination;
            door.onEnter.AddListener(SetUpFireAndRoom);
            blockSleep.SetActiveTrigger(true);
            getsomesleep.gameObject.SetActive(false);
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
        getsomesleep.gameObject.SetActive(true);
        blockSleep.SetActiveTrigger(false);
        door.destinationPoint = startDestination;
        door.onEnter.RemoveListener(SetUpFireAndRoom);
        gameover.onRespawn.AddListener(DeloadFireThing);
        //gameover.onRespawn.RemoveListener(SetUpFireAndRoom);
    }

    public void ReturnObjects()
    {
        room.SetActive(false);
        objectsAfterFire.SetActive(false);
        loadOnLeavingTheRoom.SetActive(true);
    }

    public void DeloadFireThing()
    {
        fire.SetActive(false);
        room.SetActive(false);
        deloadWhileInRoom.SetActive(true);
    }
}
