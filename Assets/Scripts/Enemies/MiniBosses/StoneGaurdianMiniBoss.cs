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

    public Animator A;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        pos = GameObject.Find("CrushPos");
        R = this.GetComponent<Rigidbody2D>();
        PR = Player.GetComponent<Rigidbody2D>();
        A = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (M == true)
        {
            MoveTo();

        }
        else
        {
            M = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            M = true;
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
            StartCoroutine("K");
        }
    }
    void MoveTo()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, pos.transform.position, step);
        StartCoroutine("Crush");
    }
    IEnumerator Crush()
    {
        A.SetBool("Shake", true);
        yield return new WaitForSeconds(3);
        A.SetBool("Shake", false);
        M = false;
        Attack();
        StartCoroutine("CoolDown");
    }
    IEnumerator K()
    {
        Player.layer = 11;
        yield return new WaitForSeconds(3);
        Player.layer = 8;

    }
    IEnumerator CoolDown()
    {
        StartCoroutine("Fire");
        Bc.enabled = false;
        yield return new WaitForSeconds(3);
        Bc.enabled = true;
    }
    void Attack()
    {
        R.AddForce(-transform.up * power, ForceMode2D.Impulse);
    }
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0);
        Quaternion rot = new Quaternion(0, 0, 180, 0);
        Instantiate(Projectile, transform.position, transform.rotation);
        Instantiate(Projectile, transform.position, rot);
    }
}
