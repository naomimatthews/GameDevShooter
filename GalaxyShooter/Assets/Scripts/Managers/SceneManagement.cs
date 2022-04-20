using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManager instance;

    public  void FindGameButton()
    {
         SceneManager.LoadScene(1);
    }

    public void HelpButton()
    {
          SceneManager.LoadScene(2);
    }

    public void OptionsButton()
    {
         SceneManager.LoadScene(3);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
}
