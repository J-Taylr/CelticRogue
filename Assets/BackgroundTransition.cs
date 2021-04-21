using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTransition : MonoBehaviour
{
    public GameObject player;
    public float yValue;
    
    public float transitionDistance = 10;

    public float topAlpha;
    public float belowAlpha;

   

    private Color topColour;
    private Color belowColour;

    public SpriteRenderer topBackground;
    public SpriteRenderer belowBackground;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        topColour = topBackground.color;
        belowColour = belowBackground.color;


    }


    private void Update()
    {
        yValue = player.transform.position.y;
        yValue = yValue / transitionDistance;

        LerpAlpha();

        

    }


    public void LerpAlpha()
    {
        topAlpha = Mathf.Lerp(0, 1, yValue);
        belowAlpha = -Mathf.Lerp(-1, 0, yValue);

        topColour.a = topAlpha;
        topBackground.color = topColour;

        belowColour.a = belowAlpha;
        belowBackground.color = belowColour;

    }

    // distance away from transistion point //above and below

}
