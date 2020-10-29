using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button registerButton;
    public Button loginButton;
    public Button playButton;

    public Text playerDisplay;

    private void Start()
    {
        if (DBManager.LoggedIn)
        {
            playerDisplay.text = "Welcome " + DBManager.username;
        }

        //to ensure the button interactability 
        registerButton.interactable = !DBManager.LoggedIn;
        loginButton.interactable = !DBManager.LoggedIn;
        playButton.interactable = DBManager.LoggedIn;
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene(2);
        //load scene registermenu
                          
    }

    public void GoToLogin()
    {
        SceneManager.LoadScene(3);
        //load scene loginmenu

    }

    public void GoToGame()
    {
        SceneManager.LoadScene(4);
        //load scene loginmenu

    }
}
