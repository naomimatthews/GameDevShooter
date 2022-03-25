using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManager instance;

    public void FindGameButton()
    {
         SceneManager.LoadScene("Character Selection");
    }

    public void HelpButton()
    {
          SceneManager.LoadScene("Help");
    }

    public void OptionsButton()
    {
         SceneManager.LoadScene("Options");
    }
}
