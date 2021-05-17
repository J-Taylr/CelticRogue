using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject controlPanel;
    public GameObject mapPanel;

   
    private void OnDisable()
    {
        controlPanel.SetActive(false);
        mapPanel.SetActive(false);
    }

    public void GoToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenControls()
    {
        controlPanel.SetActive(true);
    }
    public void CloseControls()
    {
        controlPanel.SetActive(false);
    }

    public void OpenMap()
    {
        mapPanel.SetActive(true);
    }

    public void CloseMap()
    {
        mapPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
