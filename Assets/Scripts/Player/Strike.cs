using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    public int Recoil;
    public int RecoilD;
    public Transform attackPointR;
    public Transform attackPointUp;
    public Transform attackPointDown;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public Rigidbody2D Player;
    public Animator animator;

    public void AttackR(int damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointR.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            
            Vector2 yVelocity = new Vector2(0, 0);
            Player.velocity = new Vector2(Player.velocity.x, yVelocity.y);

            if (transform.localPosition.x < 3)
            {
                Player.AddForce(new Vector2(-Recoil, 0f), ForceMode2D.Impulse);
            }
            if (transform.localPosition.x > 3)
            {
                Player.AddForce(new Vector2(Recoil, 0f), ForceMode2D.Impulse);
            }

            enemy.GetComponent<EnemyManager>().TakeDamage(damage);
        }
    }
   public  void AttackUp(int damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            
            enemy.GetComponent<EnemyManager>().TakeDamage(damage);
        }

    }
    public void AttackDown(int damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Vector2 yVelocity = new Vector2(0, 0);
            Player.velocity = new Vector2(Player.velocity.x, yVelocity.y);
            Player.AddForce(new Vector2(0f, RecoilD), ForceMode2D.Impulse);
            
            enemy.GetComponent<EnemyManager>().TakeDamage(damage);
        }
    }


   

    private void OnDrawGizmosSelected()
    {
       // Gizmos.DrawWireSphere(attackPointR.position, attackRange);
       // Gizmos.DrawWireSphere(attackPointUp.position, attackRange);
       Gizmos.DrawWireSphere(attackPointDown.position, attackRange);
    }
   
}
