using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertWalk : MonoBehaviour
{
    PlayerMovement playermovement;


    void OnTriggerEnter2D (Collider2D check) {
        if (check.tag == "Player")
        {

            check.gameObject.transform.rotation = Quaternion.Euler(0f,180f,180f);
            check.gameObject.GetComponent<Rigidbody2D>().gravityScale = -4f;

            playermovement = check.gameObject.GetComponent<PlayerMovement>();
            playermovement.jumpForce = -16f;
            playermovement.doubleJumpForce = -12f;
            playermovement.inGravityField = true;
        }
    }

    void OnTriggerExit2D(Collider2D leaf) {
        if (leaf.tag == "Player")
        {
            leaf.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            leaf.gameObject.GetComponent<Rigidbody2D>().gravityScale = 4f;
            leaf.gameObject.GetComponent<PlayerMovement>().jumpForce = 16f;
            leaf.gameObject.GetComponent<PlayerMovement>().doubleJumpForce = 12f;
            playermovement.inGravityField = false;
        }
    }
}
