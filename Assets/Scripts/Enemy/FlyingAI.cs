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
        Vector2 direction = gameObject.transform.position - player.transform.position;

        rb.MovePosition((Vector2)transform.position - (direction * speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerManager>().currentHealth -= damage;
            player.GetComponent<PlayerManager>().CheckHealth();
        }
    }
}
