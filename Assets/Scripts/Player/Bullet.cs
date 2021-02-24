using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody2D body;
    public float speed;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        body.AddForce(new Vector2(speed,0));
        Invoke("SelfDestruct", 4f);
    }

    void OnCollisionEnter2D(Collision2D hit) {
        if (hit.gameObject.tag == "Enemy")
        {
            hit.gameObject.GetComponent<EnemyManager>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (hit.gameObject.tag =="Player")
        {

        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    void SelfDestruct() {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
