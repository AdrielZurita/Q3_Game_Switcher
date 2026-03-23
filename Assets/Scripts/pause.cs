using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;
    public ObjectPlsHelp objectPlsHelp;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Time.timeScale = 1f;
                isPaused = false;
                pauseMenu.SetActive(false);
                objectPlsHelp.canMove = true;
                objectPlsHelp.canThrow = true;
                Cursor.lockState = CursorLockMode.Locked; 
                Cursor.visible = false;
                objectPlsHelp.canCam = true;
            }
            else
            {
                Time.timeScale = 0f;
                isPaused = true;
                pauseMenu.SetActive(true);
                objectPlsHelp.canMove = false;
                objectPlsHelp.canThrow = false;
                Cursor.lockState = CursorLockMode.None; 
                Cursor.visible = true;
                objectPlsHelp.canCam = false;
            }
        }
    }
}
