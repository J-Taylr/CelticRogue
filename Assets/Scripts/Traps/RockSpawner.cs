using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public float time;
    public GameObject rock;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRock", time,time);
    }

    void SpawnRock() {
        Instantiate(rock, gameObject.transform.position, Quaternion.identity);
    }
}
