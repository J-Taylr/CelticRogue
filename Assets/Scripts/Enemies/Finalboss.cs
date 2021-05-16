using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finalboss : MonoBehaviour
{
    public bool Boss1 = false;
    public bool Boss2 = false;
    public bool Boss3 = false;
    
    // Start is called before the first frame update
    void start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if(Boss1 == true & Boss2 == true & Boss3 == true)
        {
            Time.timeScale = 0;
        }
    }
}
