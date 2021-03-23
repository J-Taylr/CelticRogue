using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public int damage;
    

    public int destroyTime = 3;
    float timeRemaining;
    void OnCollisionEnter2D(Collision2D check) {

       
        if (check.gameObject.tag =="Player")
        {
            check.gameObject.GetComponent<PlayerManager>().TakeDamage(damage,gameObject);

        }
        
    }

    private void Awake()
    {
        timeRemaining = destroyTime;
    }

    private void Update()
    {
        TimerCountdown();
    }



    public void TimerCountdown()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
            Destroy(gameObject);
        }
    }

}
