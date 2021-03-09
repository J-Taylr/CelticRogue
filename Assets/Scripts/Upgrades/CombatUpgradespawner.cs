using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUpgradespawner : MonoBehaviour
{
    [Tooltip("This will auto fill on runtime")]
    public List<GameObject> spawnPoints = new List<GameObject>();

    [Tooltip("Fill this with every Major upgrade")]
    public List<GameObject> upgradeSelection = new List<GameObject>();

    public List<int> usedNumbers = new List<int>();

    void Start()
    {
        foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("UpgradeSpawn"))
        {
            spawnPoints.Add(spawn);
            SpawnUpgrades(spawn);

        }

        

    }

    public void SpawnUpgrades(GameObject spawner)
    {
        int rndm = ChooseNumber();
            


      GameObject obj = Instantiate(upgradeSelection[rndm], spawner.transform.position, Quaternion.identity);
       
       

    }

    public int ChooseNumber()
    {
        int rndm = Random.Range(1, upgradeSelection.Count);

        while (usedNumbers.Contains(rndm))
        {
            rndm = Random.Range(1, upgradeSelection.Count);
            print(rndm + "already in list");
        }

        print(rndm + "not in list");
        usedNumbers.Add(rndm);
        return rndm;



    }

   

}
