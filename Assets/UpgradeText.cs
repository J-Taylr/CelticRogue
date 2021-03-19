using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeText : MonoBehaviour
{
    public TextMeshPro text;
    public Animator animator;

      
    public void ChangeText(string type, float amount)
    {
        text.text = ("+" + amount + type);
        animator.SetTrigger("Start");
    }

}
