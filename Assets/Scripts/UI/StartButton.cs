using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
    public string nextSceneName; 

    // This method takes whatever was specified fr the 
    // string name to be the next scene to switch to
    public void StartButtonPressed()
    {
        print("Called");
        SceneManager.LoadScene(nextSceneName);
    }
}
