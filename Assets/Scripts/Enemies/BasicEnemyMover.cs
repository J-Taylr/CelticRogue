using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyManager))]
public class BasicEnemyMover : MonoBehaviour
{
   
    [Header("Movement")]
    public int speed;
    public int damage;
    public float distance = 5;
    public float Thrust;
    public float KnockBackx;
    public float KnockBackY;

    private bool movingRight = true;
    public bool attacking = false;
    public Rigidbody2D RB;

    private BoxCollider2D bc;
    public Transform groundDetection;
    public Transform wallDetection;
    public Vector3 r;
    [SerializeField] private LayerMask lm;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCast();
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }

    public void PlayerCast()
    {
        if (attacking == false)
        {
            wallCheck();
            EnemyMove();
        }
        Debug.DrawLine(transform.position + transform.right * bc.size.x, transform.position + transform.right * distance, Color.green);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.right * bc.size.x, transform.right, distance);

        if (hit.collider != null && hit.collider.tag == "Player" && attacking == false)
        {
            print(hit.collider);
            StartCoroutine("Attack");
        }
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
    public void wallCheck()
    {
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, transform.right, distance, lm);
        if (wallInfo.collider == true)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                //Debug.Log("test");
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    public void State1()
    {
        RB.AddForce(new Vector2(0f, 15f), ForceMode2D.Impulse);
    }
    public void State2()
    {
        RB.AddForce(transform.right * Thrust, ForceMode2D.Impulse);
    }
    IEnumerator Attack()
    {
        attacking = true;
        State1();
        yield return new WaitForSeconds(1.5f);
        State2();
        yield return new WaitForSeconds(1.5f);
        attacking = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("playerhit");

            other.gameObject.GetComponent<PlayerManager>().TakeDamage(damage);
            other.gameObject.GetComponent<PlayerManager>().CheckHealth();
            Rigidbody2D PRB = other.gameObject.GetComponent<Rigidbody2D>();
            float pPos = transform.position.x;
            Vector2 KB = new Vector2(pPos, KnockBackY);
            if (transform.position.x < other.transform.position.x)
            {
             PRB.AddForce(new Vector2(KnockBackx, KnockBackY), ForceMode2D.Impulse);
            }
            if (transform.position.x > other.transform.position.x)
            {
                PRB.AddForce(new Vector2(-KnockBackx, KnockBackY), ForceMode2D.Impulse);
            }
        }
    }
}

