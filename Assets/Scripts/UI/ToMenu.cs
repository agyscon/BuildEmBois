using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenu : MonoBehaviour {

    // This method takes whatever was specified fr the 
    // string name to be the next scene to switch to
    public void MenuButtonPressed()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
