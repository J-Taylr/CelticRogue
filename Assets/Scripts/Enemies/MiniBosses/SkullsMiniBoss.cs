using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullsMiniBoss : MonoBehaviour
{
    private EnemyManager eManager;
    private SpawnerMiniBoss spawner;
    // Start is called before the first frame update
    void Start()
    {
        eManager = GetComponent<EnemyManager>();
        spawner = transform.parent.GetComponent<SpawnerMiniBoss>();
        spawner.Skulls.Add(this.gameObject);
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
