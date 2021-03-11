using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyManager))]

public class FlyingAI : MonoBehaviour
{
    public int damage;
    public float speed;
    public float Range;
    public GameObject player;
    public Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        float dist = Vector2.Distance(player.transform.position, transform.position);
        if(dist <= Range)
        {
            print("in range");
            Vector2 direction = gameObject.transform.position - player.transform.position;
            rb.MovePosition((Vector2)transform.position - (direction * speed * Time.deltaTime));
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerManager>().currentHealth -= damage;
            player.GetComponent<PlayerManager>().CheckHealth();
        }
        ContactPoint2D contact = other.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Vector3 inverpos = player.transform.InverseTransformPoint(pos);
        if (inverpos.x > 0)
        {

        }


    }
  
}
