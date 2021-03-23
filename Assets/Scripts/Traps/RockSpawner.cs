using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public float spawndelay;
    public GameObject rock;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRock());
    }

   


    IEnumerator SpawnRock()
    {
        while (true)
        {


            GameObject go = Instantiate(rock, gameObject.transform.position, Quaternion.identity);
            go.transform.SetParent(gameObject.transform);

            yield return new WaitForSeconds(spawndelay);
        }
    }
}
