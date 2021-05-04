using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullsMiniBoss : MonoBehaviour
{
    private EnemyManager eManager;
    private SpawnerMiniBoss spawner;

    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        eManager = GetComponent<EnemyManager>();
        spawner = transform.parent.GetComponent<SpawnerMiniBoss>();
        eManager.MSkull = true;
        spawner.Skulls.Add(this.gameObject);
    }

    public void removeSkulls()
    {
        if (spawner != null)
        {
            Debug.Log("remove");

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
