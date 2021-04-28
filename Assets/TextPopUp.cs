using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextPopUp : MonoBehaviour
{
    public float speed;

    public GameObject Text;
    public GameObject Camera;

    public Vector3 CamLocation1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("Popup");
          //  StartCoroutine("Zoom");
        }
    }
    IEnumerator Popup()
    {
        Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        Text.gameObject.SetActive(false);
    }
   /* IEnumerator Zoom()
    {
        float step = speed * Time.deltaTime;
        Camera.transform.position = Vector3.MoveTowards(transform.position, CamLocation1, step);
        yield return new WaitForSeconds(2f);
    }*/
}
