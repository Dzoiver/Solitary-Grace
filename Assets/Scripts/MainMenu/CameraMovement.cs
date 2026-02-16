using GM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Camera cameraBeginning;
    [SerializeField] Camera cameraPrison;
    enum CameraStage
    {
        Beginning,
        Prison,
    }
    [SerializeField] CameraStage camera;
    private Vector3 movement;
    private float speed = 0.2f;
    private float timeToChange = 60f;
    private float currentTime = 0f;
    Vector3 prisonStart;
    Vector3 houseStart;

    // Start is called before the first frame update
    void Start()
    {
        prisonStart = cameraPrison.transform.position;
        houseStart = cameraBeginning.transform.position;
        if (camera == CameraStage.Beginning)
        {
            cameraBeginning.gameObject.SetActive(true);
        }
        if (camera == CameraStage.Prison)
        {
            speed = 0.1f;
            cameraPrison.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (camera == CameraStage.Beginning)
        {
            movement = cameraBeginning.transform.position;
            movement.x += Time.deltaTime * speed;
            movement.z += Time.deltaTime * speed;
            cameraBeginning.transform.position = movement;
        }
        if (camera == CameraStage.Prison)
        {
            movement = cameraPrison.transform.position;
            movement.y -= Time.deltaTime * speed;
            cameraPrison.transform.position = movement;
        }

        currentTime += Time.deltaTime;
        if (currentTime > timeToChange)
        {
            currentTime = 0f;
            GameFuncs.FadeIn(2f);
            StartCoroutine(DelayTransition());
        }
    }

    IEnumerator DelayTransition()
    {
        yield return new WaitForSeconds(2f);
        SwitchCamera();
        GameFuncs.FadeOut(1f);
    }

    private void SwitchCamera()
    {
        cameraBeginning.transform.position = houseStart;
        cameraPrison.transform.position = prisonStart;
        if (camera == CameraStage.Prison)
        {
            cameraPrison.gameObject.SetActive(false);
            cameraBeginning.gameObject.SetActive(true);
            camera = CameraStage.Beginning;
        }
        else
        {
            cameraBeginning.gameObject.SetActive(false);
            cameraPrison.gameObject.SetActive(true);
            camera = CameraStage.Prison;
        }
    }
}
