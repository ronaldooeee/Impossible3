using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class titleMenu : MonoBehaviour {
    public Canvas exitMenu;
    public Button startText;
    public Button exitText;
	// Use this for initialization
	void Start ()
    {
        exitMenu = exitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        exitMenu.enabled = false;
	}
    public void ExitPress()
    {
        exitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }
    public void NoPress()
    {
        exitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }
	// Update is called once per frame
    public void StartLevel()
    {
        Application.LoadLevel(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
