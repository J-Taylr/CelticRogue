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

    public bool moving = true;
    public bool A;
    public BoxCollider2D trigger;
    private BoxCollider2D bc;
    public Transform groundDetection;
    public GameObject player;
    public float offsetY = 5;

    [SerializeField] private LayerMask whatIsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (A == true)
        {
            State1();
        }
        EnemyMove();
        
    }
    public void EnemyMove()
    {
        if (moving == true)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, whatIsPlayer);
        if (groundInfo.collider == false )
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
            print(groundInfo.collider);
        }
    }
    public void State1()
    {
        transform.position = new Vector2(player.transform.position.x , offsetY);
        StartCoroutine("Stomp");

    }
    public void State2()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            moving = false;
            A = true;
        }
    }
    IEnumerator Stomp()
    {
        yield return new WaitForSeconds(1.5f);
        A = false;
    }
}
