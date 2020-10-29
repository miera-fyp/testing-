using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRoomPath : MonoBehaviour
{
    public void GoToAssessment()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("05 assessment phase");
    }

    public void GoToBowling()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("08 bowling");
    }

    public void GoToFruitNinja()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("10 fruit ninja");
    }

    public void GoToTrpohyRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("06 award exhibition");
    }
}
