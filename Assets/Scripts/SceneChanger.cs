using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // be able to assign a scene to change to easily.
    [Header("Make sure the scene has been added to the build settings!")]
    public string sceneToChangeTo;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToChangeTo);
        }
    }
}
