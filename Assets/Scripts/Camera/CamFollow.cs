using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject camPos;

    public float camSpeed = 20;
    public float shiftSpeed = 12;
    
    public float zOffset = 20;

    public float playerOffset = 5;

    public bool cameraUp = false;
    public bool cameraDown = false;

    void Start()
    {
        camPos = GameObject.FindGameObjectWithTag("Camera");
        transform.position = camPos.transform.position;
    }


    private void Update()
    {
        TrackPlayer();
    }


    public void TrackPlayer()
    {
        if (!cameraUp && !cameraDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(camPos.transform.position.x, camPos.transform.position.y, zOffset), camSpeed * Time.deltaTime);
        }
        if (cameraUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(camPos.transform.position.x, (camPos.transform.position.y + playerOffset), zOffset), camSpeed * Time.deltaTime);
        }
        if (cameraDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(camPos.transform.position.x, (camPos.transform.position.y - playerOffset), zOffset), camSpeed * Time.deltaTime);
        }
    }


    public void CamReturnUp()
    {
        cameraUp = false;
        print("return up");
    }

    public void CamReturnDown()
    {
        cameraDown = false;
        print("return down");
    }

}
