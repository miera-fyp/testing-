using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class Laser : MonoBehaviour
{
    //to choose any controller that we want
    public SteamVR_LaserPointer laserPointer;

    private void Awake()
    {
        laserPointer.PointerClick += PointerClick;
    }

    public void OnClickedButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("04 game menu");
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Exit Button")
        {
            Debug.Log("Exit Button CLicked. You will be directed to Game Menu Scene !");
        }
    }
}
