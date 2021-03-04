using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D Player;
    public Animator animator;
    public Transform attackPointR;
    public Transform attackPointUp;
    public Transform attackPointDown;

    [Header("Variables")]
    public int Recoil;
    public int RecoilD;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    public bool coolDown = false;


    public void AttackUp(int damage)
    {
        if (coolDown == false)
        {

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                print(enemy.name);
                enemy.GetComponent<EnemyManager>().TakeDamage(damage);
            }
            StartCoroutine("CoolDown");

        }
    }
    public void AttackDown(int damage)
    {
        if (coolDown == false)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                Vector2 yVelocity = new Vector2(0, 0);
                Player.velocity = new Vector2(Player.velocity.x, yVelocity.y);
                Player.AddForce(new Vector2(0f, RecoilD), ForceMode2D.Impulse);

                enemy.GetComponent<EnemyManager>().TakeDamage(damage);
            }
            StartCoroutine("CoolDown");
        }
    }

    public void AttackR(int damage)
    {
        if (coolDown == false)
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
                print(enemy.name);
                enemy.GetComponent<EnemyManager>().TakeDamage(damage);
            }
            StartCoroutine("CoolDown");
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPointR.position, attackRange);
        Gizmos.DrawWireSphere(attackPointUp.position, attackRange);
        Gizmos.DrawWireSphere(attackPointDown.position, attackRange);
    }
    IEnumerator CoolDown()
    {
        coolDown = true;
        yield return new WaitForSeconds(0.5f);
        coolDown = false;
    }
}
