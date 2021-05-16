using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSpawner : MonoBehaviour
{
    public int max;

    public GameObject prefab;
    public Transform player;

    public List<GameObject> Skulls;

    public Vector3 size;
    public Vector3 center;
    public float spawnRange;

    public bool isActive;

    public EnemyManager EM;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        EM = gameObject.GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        center = gameObject.transform.localPosition;

        if (isActive)
        {
            if (Skulls.Count <= max)
            {
                Spawn();
            }
        }
        else
        {
            return;
        }
    }
    public void Spawn()
    {

        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        GameObject skull = Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.localPosition + center, size);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }


    public void CheckDistance()
    {
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance <= spawnRange)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }

   
}
