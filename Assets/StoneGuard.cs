using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGuard : MonoBehaviour
{
    public GameObject pos;
    public float speed;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        pos = GameObject.Find("CrushPos");
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("hit");
            MoveTo();
        }
    }
    void MoveTo()
    {
        float step = speed * Time.deltaTime;
        Vector2.MoveTowards(transform.position, pos.transform.position, step);
    }
}
