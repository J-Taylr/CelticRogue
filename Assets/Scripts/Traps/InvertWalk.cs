using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertWalk : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D check) {
        if (check.tag == "Player")
        {
            check.gameObject.transform.rotation = Quaternion.Euler(0f,180f,180f);
            check.gameObject.GetComponent<Rigidbody2D>().gravityScale = -4f;
            check.gameObject.GetComponent<PlayerActions>().jumpForce = -16f;
            check.gameObject.GetComponent<PlayerActions>().doubleJumpForce = -12f;
        }
    }

    void OnTriggerExit2D(Collider2D leaf) {
        if (leaf.tag == "Player")
        {
            leaf.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            leaf.gameObject.GetComponent<Rigidbody2D>().gravityScale = 4f;
            leaf.gameObject.GetComponent<PlayerActions>().jumpForce = 16f;
            leaf.gameObject.GetComponent<PlayerActions>().doubleJumpForce = 12f;
        }
    }
}
