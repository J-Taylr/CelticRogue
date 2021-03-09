using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerManager playerManager;
    public PlayerController playerController;

    [Header("Ranged Attack")]
    public GameObject bullet;
    private GameObject newBullet;
    public bool ranged;
    public bool dOTRanged;

    [Header("Slam Attack")]
    public bool slam;
    bool slamming;
    public float slamRadius;
    public int slamDamage;
    public LayerMask slamMask;
    public float slamForce;

    [Header("Dash Attacks")]
    public bool dashEndExplosion;
    public bool dashAttack;
    public int dashDamage;

    [Header("Wall Jump Buffs")]
    public float wallJumpBuffDuration;
    bool wallJumped;
    public int wallJumpBuff;
    public bool wallJumpExplosion;
    public bool walljumpDamageBoost;
    public bool wallJumpInvincibility;

    [Header("On Damage Upgrades")]
    public bool rage;
    public int damageResist;
    public int damageMultiplier;
    public float rageDuration;
    public bool dOTAttacker;
    public int dOTTicks, dOTDamage;
    public float dOTSplit;
    public bool tempInvincibility;
    public float invincDuration;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (slamming && controller.isGrounded)
        {
            SlamAttack();
            gameObject.GetComponent<Rigidbody2D>().gravityScale /= 5f;
            slamming = false;
        }
        if (wallJumped && controller.isGrounded && wallJumpExplosion)
        {
            SlamAttack();
            wallJumped = false;
        }
    }

    public void SlamAttack()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, slamRadius, slamMask);
        foreach (Collider2D obj in objects)
        {
            obj.GetComponent<EnemyManager>().TakeDamage(slamDamage);

            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * slamForce);
        }
    }

    void OnCollisionEnter2D(Collision2D hitCheck)
    {
        if (hitCheck.gameObject.tag == "Enemy" && dashAttack && playerController.dashing)
        {
            hitCheck.gameObject.GetComponent<EnemyManager>().TakeDamage(dashDamage);
            print("GetSmacked");
        }
    }

    public void Ranged()
    {
        if (ranged && gameObject.transform.localScale.x < 0f)
        {
            newBullet = Instantiate(bullet, new Vector3(gameObject.transform.position.x - 1f, gameObject.transform.position.y, 0f), Quaternion.identity);
            newBullet.GetComponent<Bullet>().speed = -500f;
            if (dOTRanged)
            {
                newBullet.GetComponent<Bullet>().dot = true;
            }
        }
        else if (ranged && gameObject.transform.localScale.x > 0)
        {
            newBullet = Instantiate(bullet, new Vector3(gameObject.transform.position.x + 1f, gameObject.transform.position.y, 0f), Quaternion.identity);
            newBullet.GetComponent<Bullet>().speed = 500f;
            if (dOTRanged)
            {
                newBullet.GetComponent<Bullet>().dot = true;
            }
        }

        if (!controller.isGrounded && slam)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale *= 5f;
            slamming = true;
        }
    }

    public IEnumerator WallJumpBuffs()
    {
        wallJumped = true;
        if (walljumpDamageBoost)
        {
            playerManager.strikeDamage *= wallJumpBuff;
        }
        if (wallJumpInvincibility)
        {
            playerManager.invincible = true;
        }
        yield return new WaitForSeconds(wallJumpBuffDuration);
        wallJumped = false;
        playerManager.invincible = false;
        if (walljumpDamageBoost)
        {
            playerManager.strikeDamage /= wallJumpBuff;
        }
    }

    public void DamageResponse(GameObject hit)
    {
        if (rage && hit.tag == "Enemy")
        {
            StartCoroutine(RageAct());
        }
        if (hit.tag == "Enemy" && tempInvincibility)
        {
            StartCoroutine(DamageInvincibilty());
        }
        if (hit.tag == "Enemy" && dOTAttacker)
        {
            hit.GetComponent<EnemyManager>().DOT(dOTTicks, dOTDamage, dOTSplit);
        }
    }

    public IEnumerator RageAct()
    {
        playerManager.damageResist += damageResist;
        playerManager.strikeDamage *= damageMultiplier;
        yield return new WaitForSeconds(rageDuration);
        playerManager.damageResist -= damageResist;
        playerManager.strikeDamage /= damageMultiplier;
    }

    public IEnumerator DamageInvincibilty()
    {
        playerManager.invincible = true;
        yield return new WaitForSeconds(invincDuration);
        playerManager.invincible = false;
    }
}
