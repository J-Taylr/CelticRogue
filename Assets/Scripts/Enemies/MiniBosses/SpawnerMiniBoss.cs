using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMiniBoss : MonoBehaviour
{
    private bool Wave1 = true;
    private bool Wave2 = true;
    private bool Wave3 = true;
    public int max;

    public GameObject prefab;

    public List<GameObject> Skulls;

    public Vector3 size;
    public Vector3 center;

    public EnemyManager EM;
    // Start is called before the first frame update
    void Start()
    {
        EM = gameObject.GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        center = gameObject.transform.localPosition;
        if (Skulls.Count <= max)
        {
            Spawn();
        }
        if (EM.currentHealth <= 35 & Wave1 == true)
        {
            W1();
        }
        if (EM.currentHealth <= 25 & Wave2 == true)
        {
            W2();
        }
        if (EM.currentHealth <= 15 & Wave3 == true)
        {
            W3();
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
    }
    private void W1()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Wave1 = false;
    }
    private void W2()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Wave2 = false;
    }
    private void W3()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Instantiate(prefab, pos, Quaternion.identity, this.gameObject.transform);
        Wave3 = false;
    }
}
