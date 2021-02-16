using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyManager))]
public class BasicEnemyMover : MonoBehaviour
{
   
    [Header("Movement")]
    public int speed;
    public float distance;
    public float Thrust;
    private bool movingRight = true;
    public bool attacking = false;
    public Rigidbody2D RB;


    public Transform groundDetection;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCast();
    }

    public void PlayerCast()
    {
        if (attacking == false)
        {
            EnemyMove();
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, distance);
        

        Debug.DrawRay(transform.position, transform.right, Color.green);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            
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

    public void State1()
    {
        RB.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
    }
    public void State2()
    {
        RB.AddForce(transform.forward * Thrust, ForceMode2D.Impulse);
    }
    IEnumerator Attack()
    {
        attacking = true;
        State1();
        yield return new WaitForSeconds(2f);
        State2();
    }
}

