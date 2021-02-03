using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    public int Recoil;
    public Transform attackPointR;
    public Transform attackPointUp;
    public Transform attackPointDown;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public Rigidbody2D Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttackR();
        }
        if (Input.GetKey(KeyCode.W) & Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttackUp();
        }
        if (Input.GetKey(KeyCode.S) & Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttackDown();
        }
    }
    void AttackR()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointR.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Player.AddForce(-transform.right * Recoil, ForceMode2D.Impulse);
            Debug.Log("We hit Right " + enemy.name);
        }
    }
    void AttackUp()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit up " + enemy.name);
        }
    }
    void AttackDown()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Vector2 yVelocity = new Vector2(0, 0);

            Player.velocity = new Vector2(Player.velocity.x, yVelocity.y);

            Player.AddForce(new Vector2(0f, Recoil), ForceMode2D.Impulse);
            Debug.Log("We hit Down " + enemy.name);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPointR.position, attackRange);
        Gizmos.DrawWireSphere(attackPointUp.position, attackRange);
        Gizmos.DrawWireSphere(attackPointDown.position, attackRange);
    }
}
