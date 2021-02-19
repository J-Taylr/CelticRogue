using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public GameObject statUpgradePrefab;

    void Start()
    {

    }

    public void SetupSpawn()
    {
        GameObject stat = Instantiate(statUpgradePrefab, transform.position, Quaternion.identity);
       StatPickup pickup = stat.GetComponent<StatPickup>();
    }


}
