using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
 
    public GameObject instructionMenu;

    public AudioSource menuMusic;
    public AudioSource startGame;
    public AudioSource openInst;
    public AudioSource closeInst;

    public void StartGame()
    {
        startGame.Play();
        menuMusic.Stop();
        Invoke("LoadScene", 1);
    }


    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenInstructions()
    {
        openInst.Play();
        instructionMenu.SetActive(true);
    }

    public void CloseInstructions()
    {
        closeInst.Play();
        instructionMenu.SetActive(false);
    }



}
