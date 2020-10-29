using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class UIButton : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    private void Awake()
    {
        //laserPointer.PointerIn += OnPointerin;
        //laserPointer.PointerOut += OnPointerOut;

        laserPointer.PointerClick += PointerClick;   
              
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "LeaveButton")
        {
            Debug.Log("Leaving the Room. Proceed to Game Main Menu !");
            UnityEngine.SceneManagement.SceneManager.LoadScene("04 game menu");      
        }
    }

}
