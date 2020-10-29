using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class DBManager 
{
    public static string username;
    public static int score;
    //public static int duration;
    //public static int dbtest;
    //public static decimal dbfloat;

    //use to check are we logged in 
    public static bool LoggedIn
    {
        get
        {
            return username != null;
            //if true, we has looged in 
        }
    }

    public static void LogOut()
    {
        //set username back to null when we have logged out 
        username = null;
    }
}
