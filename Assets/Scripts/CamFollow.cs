using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject camPos;

    public float camSpeed = 20;
    public float zOffset = 20;
    void Start()
    {
        camPos = GameObject.FindGameObjectWithTag("Camera");
        transform.position = camPos.transform.position;
    }


    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(camPos.transform.position.x, camPos.transform.position.y, zOffset), camSpeed* Time.deltaTime);

    }




}
