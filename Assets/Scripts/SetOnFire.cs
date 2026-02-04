using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class SetOnFire : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject newRespawn;
    CameraReturnControls cameracontrols;
    [SerializeField] GameObject room;
    [SerializeField] GameObject deloadWhileInRoom;
    [SerializeField] GameObject fire;
    [SerializeField] GetSomeSleep getsomesleep;
    [SerializeField] GameObject prisonDestination;
    [SerializeField] DoorOpen door;
    GameObject startDestination;
    [SerializeField] GameObject blockSleep;
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
        startDestination = door.destinationPoint;
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
            //cameracontrols.ChangeWakeUpStart(newRespawn.transform);
            //gameover.onRespawn.AddListener(SetUpFireAndRoom);
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
}
