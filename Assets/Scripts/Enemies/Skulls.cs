using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skulls : MonoBehaviour
{
    private EnemyManager eManager;
    private SkullSpawner spawner;

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        eManager = GetComponent<EnemyManager>();
        spawner = transform.parent.GetComponent<SkullSpawner>();
        eManager.isSkull = true;
        spawner.Skulls.Add(this.gameObject);
        Player = GameObject.FindGameObjectWithTag("Player");
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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            other.gameObject.GetComponent<PlayerManager>().TakeDamage();
           
        }
    }
}
