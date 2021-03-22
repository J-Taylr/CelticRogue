using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheck : MonoBehaviour
{
    public int points;
    public GameObject winUI;

    public void Check() {
        if (points >= 3)
        {
            winUI.SetActive(true);
        }
    }
}
