using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebTest : MonoBehaviour
{
    // passing data from to database to unity 

    // Start is called before the first frame update
    IEnumerator Start()
    {
        WWW request = new WWW("http://localhost/sqlconnect/webtest.php");
        yield return request;

        //getting requrst from php 
        //Debug.Log(request.text);

        //each time it see any \t in php it will split the sentence 
        //into several sentence 
        string[] webResult = request.text.Split('\t');

        /*foreach (string s in webResult)
        {
            Debug.Log(s);
        }*/

        //first line in php
        Debug.Log(webResult[0]);

        int webNumber = int.Parse(webResult[1]);
        Debug.Log(webNumber);



    }

  
}
