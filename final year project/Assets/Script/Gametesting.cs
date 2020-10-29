using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;


public class Gametesting : MonoBehaviour
{
    public Text playerdisplay;
    public Text scoredisplay;

    private void Awake()
    {
        //check are we logged in 
        if (DBManager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("01 mainmenu");  
        }
        playerdisplay.text = "Player" + DBManager.username;
        scoredisplay.text = "Score" + DBManager.score;

    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());

    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score", DBManager.score);

        WWW www = new WWW("http://localhost/sqlconnect/savedatatesting.php", form);
        yield return www;

        //see if we got any error from connecting to php
        if (www.text == "0")
        {
            Debug.Log("Game Saved");
        }
        else
        {
            Debug.Log("Save Failed. Error # " + www.text);
        }

        DBManager.LogOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene("01 mainmenu");
    }

    public void IncreaseScore()
    {
        DBManager.score++;
        scoredisplay.text = "Score" + DBManager.score;

    }


}
