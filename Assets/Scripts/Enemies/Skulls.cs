using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skulls : MonoBehaviour
{
    private EnemyManager eManager;
    private SkullSpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        eManager = GetComponent<EnemyManager>();
        spawner = transform.parent.GetComponent<SkullSpawner>();
    }

    public void removeSkulls()
    {

        if (spawner != null)
        {
            print("check");
            spawner.Skulls.Remove(this.gameObject);
        }
        else
        {
            return;
        }
    }
}
