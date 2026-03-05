using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menubuttons : MonoBehaviour
{
    public string level = "Level1";
    //public Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*public void CameraLeft()
    {
        print("moved L");
        anim.SetInteger("menuMoveState", 1);
    }

    public void CameraRight()
    {
        print("moved R");
        anim.SetInteger("menuMoveState", -1);
    }

    public void CameraStop()
    {
        print("moved null");
        anim.SetInteger("menuMoveState", 0);
    }*/


    public void EnterLevel()
    {
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Application.Quit();
        print("Quit Game");
    } 
}
