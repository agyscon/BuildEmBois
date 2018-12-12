using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TransitionScene : MonoBehaviour {

    public Text loadingText;
    public string nextScene;

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(waitForSeconds());
        StartCoroutine(waitForOneSecond());

    }

    IEnumerator waitForSeconds()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator waitForOneSecond()
    {
        yield return new WaitForSeconds(1.5f);
        loadingText.text = loadingText.text + ".";
    }
}
