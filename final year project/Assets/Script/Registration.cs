using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;

    //middle man for class for calling enumarator 
    public void CallRegister()
    {
        StartCoroutine(Register());        
    }

    IEnumerator Register()
    {
        //passing information 
        //form = pass information to server, more secured 
        WWWForm form = new WWWForm();
        //creating empty form 
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
     

        WWW www = new WWW("http://localhost/sqlconnect/register.php" , form);
        yield return www;
        
        //www to retrieve content of the url

        if (www.text == "0")
        {
            // 0 means no error, the account has been created 
            Debug.Log("User creation success");
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
            //back to main menu
        }
        else
        {
            Debug.Log("User creation failed. Error #" + www.text);
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
