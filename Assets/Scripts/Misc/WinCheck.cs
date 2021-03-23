using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheck : MonoBehaviour
{
    public int points;
    public GameObject winUI;

    public GameObject JanusOne;
    public GameObject JanusTwo;
    public GameObject JanusThree;

    public void Check() {
        if (points >= 3)
        {
            winUI.SetActive(true);
        }
    }

    private void Update()
    {
        CheckJANUS();
    }


    public void CheckJANUS()
    {
        switch (points)
        {
            case 1:
                JanusOne.SetActive(true);
                break;

            case 2:
                JanusTwo.SetActive(true);
                break;

            case 3:
                JanusThree.SetActive(true);
                break;

        }
    }

}
