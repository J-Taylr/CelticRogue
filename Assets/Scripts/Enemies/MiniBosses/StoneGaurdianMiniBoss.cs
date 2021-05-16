using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGaurdianMiniBoss : MonoBehaviour
{

    public BoxCollider2D Bc;
    public BoxCollider2D Bc2;

    public GameObject Player;
    public GameObject pos;
    public GameObject Projectile;


    public float speed;
    public float power;

    public int KB;
    public int damage;

    private Rigidbody2D R;
    private Rigidbody2D PR;
    public bool M;
    public bool shooting = true;
    private bool Timeron = false;

    public Animator A;

    public float timeRemaining = 3;
    public float MaxTime = 3;
    public EnemyManager EM;
    public Finalboss FB;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        pos = GameObject.Find("CrushPos");
        R = this.GetComponent<Rigidbody2D>();
        PR = Player.GetComponent<Rigidbody2D>();
        A = GetComponent<Animator>();
        EM = gameObject.GetComponent<EnemyManager>();
        FB = GameObject.Find("Bossmanager").GetComponent<Finalboss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Timeron)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out");
                Timeron = false;
                timeRemaining = MaxTime;
                Attack();
            }
        }
        if (Timeron == true)
        {
            MoveTo();

        }
        if (EM.currentHealth <= 0)
        {
            FB.Boss3 = true;
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("GroundCheck"))
        {
            StartCoroutine("Fire");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Timeron = true;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            other.gameObject.GetComponent<PlayerManager>().currentHealth -= damage;
            if (transform.position.x <= other.transform.position.x)
            {
                Debug.Log("Smack");
                PR.AddForce(new Vector2(KB, 10), ForceMode2D.Impulse);
            }

            if (transform.position.x > other.transform.position.x)
            {
                Debug.Log("Smack2");
                PR.AddForce(new Vector2(-KB, 10), ForceMode2D.Impulse);
            }
            StartCoroutine("Layerswap");
        }
       
    }
    void MoveTo()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, pos.transform.position, step);
        StartCoroutine("CoolDown");

    }
    void Attack()
    {
        R.AddForce(-transform.up * power, ForceMode2D.Impulse);
    }
   
    IEnumerator Layerswap()
    {
        Player.layer = 11;
        yield return new WaitForSeconds(3);
        Player.layer = 8;

    }
    IEnumerator CoolDown()
    {
        Bc.enabled = false;
        yield return new WaitForSeconds(6);
        Bc.enabled = true;
    }

    IEnumerator Fire()
    {
        if (shooting == true)
        {
            Quaternion rot = new Quaternion(0, 0, 180, 0);
            Instantiate(Projectile, transform.position, transform.rotation);
            Instantiate(Projectile, transform.position, rot);
            shooting = false;
            yield return new WaitForSeconds(3);
            shooting = true;

        }
    }
}
