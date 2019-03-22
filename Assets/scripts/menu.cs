using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void battleMode()
    {
        SceneManager.LoadScene("battleMenu");
    }
    public void friendlyMode()
    {
        SceneManager.LoadScene("menu2");
    }
    public void warning()
    {
        SceneManager.LoadScene("warning");
    }
    public void forest()
    {
        SceneManager.LoadScene("peace");
    }
    public void cave()
    {
        SceneManager.LoadScene("scene");
    }
    public void battleCave()
    {
        SceneManager.LoadScene("battle");
    }
    public void back()
    {
        SceneManager.LoadScene("menu");
    }
    public void endGame()
    {
        Application.Quit();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
