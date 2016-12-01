using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Transform pauseMenu, pSettingsMenu, pExitMenu, pAudioMenu, pGraphicMenu;

    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseMenu.gameObject.SetActive(true);
            }
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pauseMenu.gameObject.SetActive(false);
            }

        }

    }
}
