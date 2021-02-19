using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAI : MonoBehaviour
{
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

        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));

    }

}
