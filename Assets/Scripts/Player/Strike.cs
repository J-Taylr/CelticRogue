using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    [Header("Components")]
    public PlayerManager playerManager;
    public Rigidbody2D Player;
    public Animator animator;
    public Transform attackPointR;
    public Transform attackPointUp;
    public Transform attackPointDown;
    

    [Header("Variables")]
    public bool stickIsVertical = false;
    public int Recoil;
    public int RecoilD;
    public float attackRange = 0.5f;
    public int critIncrease = 3;
    public LayerMask enemyLayer;

    public bool coolDown = false;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        
    }


    public void AttackUp()
    {
        print("attackUp");
        if (coolDown == false)
        {
            animator.SetTrigger("UpAttack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (RollCritical() == true)
                {
                    enemy.GetComponent<EnemyManager>().TakeDamage(playerManager.strikeDamage * critIncrease);
                }
                else
                {                   
                    enemy.GetComponent<EnemyManager>().TakeDamage(playerManager.strikeDamage);
                }

            }
            StartCoroutine("CoolDown");

        }
    }
    public void AttackDown()
    {
        print("AttackDown");
        if (coolDown == false)
        {
            animator.SetTrigger("DownAttack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                Vector2 yVelocity = new Vector2(0, 0);
                Player.velocity = new Vector2(Player.velocity.x, yVelocity.y);
                Player.AddForce(new Vector2(0f, RecoilD), ForceMode2D.Impulse);

                if (RollCritical() == true)
                {
                    enemy.GetComponent<EnemyManager>().TakeDamage(playerManager.strikeDamage * critIncrease);
                }
                else
                {

                    enemy.GetComponent<EnemyManager>().TakeDamage(playerManager.strikeDamage);
                }
            }
            StartCoroutine("CoolDown");
        }
    }

    public void AttackSide()
    {
        if (!stickIsVertical)
        {

            if (coolDown == false)
            {
                StartCoroutine(SideAttack());
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointR.position, attackRange, enemyLayer);
                foreach (Collider2D enemy in hitEnemies)
                {

                    Vector2 yVelocity = new Vector2(0, 0);
                    Player.velocity = new Vector2(Player.velocity.x, yVelocity.y);

                    /*if (transform.localPosition.x < 3)
                    {
                        Player.AddForce(new Vector2(-Recoil, 0f), ForceMode2D.Impulse);
                    }
                    if (transform.localPosition.x > 3)
                    {
                        Player.AddForce(new Vector2(Recoil, 0f), ForceMode2D.Impulse);
                    }
                    print(enemy.name);
                    */

                    if (RollCritical() == true)
                    {
                        enemy.GetComponent<EnemyManager>().TakeDamage(playerManager.strikeDamage * critIncrease);

                    }
                    else
                    {

                        enemy.GetComponent<EnemyManager>().TakeDamage(playerManager.strikeDamage);

                    }

                }
                StartCoroutine("CoolDown");
            }
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



    public bool RollCritical()
    {
        int crit = Random.Range(1, 100);

        if (crit <= playerManager.critChance)
        {
            //print(crit + "CRITICAL HIT");
            return true;
        }
        else
        {
            //print(crit + "normal hit");
            return false;
        }

    }

    IEnumerator SideAttack()
    {
       // print("side attack animation");
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(.5f);
        animator.SetBool("Attack", false);
    }

    

}
