using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorUpgrades : MonoBehaviour
{
    public List<int> upgradeOrder;
    int x;
    public int noOfUpgrades;
    public GameObject[] upgradesPickups;

    // Start is called before the first frame update
    void Start()
    {
        upgradesPickups = GameObject.FindGameObjectsWithTag("MajorUpgrade");
        for (int i = 0; i < noOfUpgrades; i++)
        {
            NewNumber();

        }
        for (int j = 0; j < upgradesPickups.Length; j++)
        {
            upgradesPickups[j].GetComponent<MajorPickup>().pickupPower = upgradeOrder[j];
        }
    }

    void NewNumber() {
        x = Random.Range(1,noOfUpgrades+1);
        ApproveNumber();
    }
    void ApproveNumber() {
        if (upgradeOrder.Contains(x))
        {
            NewNumber();
        }
        else
        {
            upgradeOrder.Add(x);
        }
    }
}
