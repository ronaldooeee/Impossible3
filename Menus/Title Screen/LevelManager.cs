using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public Transform mainMenu, settingsMenu, exitMenu, audioMenu, graphicMenu;
     
    public void LoadScene()
    {
        Application.LoadLevel(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }   

    public void SettingsMenu(bool clicked)
    {
        if (clicked == true)
        {
            settingsMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            settingsMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }

    }

    public void AudioMenu(bool clicked)
    {
        if (clicked == true)
        {
            audioMenu.gameObject.SetActive(clicked);
            settingsMenu.gameObject.SetActive(false);
        }
        else
        {
            audioMenu.gameObject.SetActive(clicked);
            settingsMenu.gameObject.SetActive(true);
        }
    }

    public void GraphicsMenu(bool clicked)
    {
        if (clicked == true)
        {
            graphicMenu.gameObject.SetActive(clicked);
            settingsMenu.gameObject.SetActive(false);
        }
        else
        {
            graphicMenu.gameObject.SetActive(clicked);
            settingsMenu.gameObject.SetActive(true);
        }
    }

    public void ExitMenu(bool clicked)
    {
        if (clicked == true)
        {
            exitMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            exitMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }
}
