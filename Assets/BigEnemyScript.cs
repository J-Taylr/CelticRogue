using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyScript : MonoBehaviour
{
    [Header("Movement")]
    public int speed;
    public int damage;
    public float distance = 5;
    private bool movingRight = true;
    public bool attacking = false;
    public Rigidbody2D RB;

    private BoxCollider2D bc;
    public Transform groundDetection;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }
    public void EnemyMove()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
