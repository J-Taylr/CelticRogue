using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSpawner : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> Skulls;
    public Vector3 size;
    public Vector3 center;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        center = gameObject.transform.localPosition;
        if (Skulls.Count <= 3)
        {
            Spawn();
        }
    }
    public void Spawn()
    {

        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        GameObject skull = Instantiate(prefab, pos, Quaternion.identity);
        Skulls.Add(skull);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.localPosition + center, size);
    }
}
