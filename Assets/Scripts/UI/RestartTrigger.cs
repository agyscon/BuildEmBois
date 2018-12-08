using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
