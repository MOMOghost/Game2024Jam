using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleControll:MonoBehaviour 
{
    // Start is called before the first frame update

    static public void GoStart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale= 1.0f;
    }
    static public void Quit()
    {
        Application.Quit();

    }
    
}
