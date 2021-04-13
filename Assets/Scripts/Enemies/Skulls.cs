using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skulls : MonoBehaviour
{
    private EnemyManager eManager;
    // Start is called before the first frame update
    void Start()
    {
        eManager = GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
       if(eManager.currentHealth <= 0)
        {
            removeSkulls();
        }
    }

    public void removeSkulls()
    {
        SkullSpawner spawner = transform.parent.GetComponent<SkullSpawner>();
        if (spawner != null)
        {
            Debug.Log("skull removed");
            spawner.Skulls.Remove(this.gameObject);
        }
    }
}
