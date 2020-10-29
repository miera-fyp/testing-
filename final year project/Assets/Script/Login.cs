
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;

    //middle man for class for calling enumarator 
    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    IEnumerator LoginPlayer()
    {
        //passing information 
        //form = pass information to server, more secured 
        WWWForm form = new WWWForm();
        //creating empty form 
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        
        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);
        yield return www;
        

        // if we have log in 
        if (www.text[0] == '0')
        {
            DBManager.username = nameField.text;
            //to ensure that it pass the 2nd information which is score 
            DBManager.score = int.Parse(www.text.Split('\t')[1]);
            UnityEngine.SceneManagement.SceneManager.LoadScene("04 game menu");
            
        }
        else
        {
            Debug.Log("User Logging Failed. Error # " + www.text);
        }
    }

    public void VerifyInput()
    {
        submitButton.interactable = (nameField.text.Length >= 4 && passwordField.text.Length >= 4);
    }

    public void CallBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}