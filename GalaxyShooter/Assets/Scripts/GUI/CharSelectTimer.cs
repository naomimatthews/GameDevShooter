using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharSelectTimer : MonoBehaviour
{
    [SerializeField] Text m_timerText;

    float m_currentTime;
    public float m_startTime;

    public bool playerLockedIn;

    void Start()
    {
        playerLockedIn = false;
        m_currentTime = m_startTime;
    }

    void Update()
    {
        if (!playerLockedIn)
        {
            m_currentTime -= 1 * Time.deltaTime;
            m_timerText.text = m_currentTime.ToString("0");

            if (m_currentTime <= 0)
            {
                m_currentTime = 0;
                SceneManager.LoadScene("Main Menu");
                //go back to main menu.
            }
        }
    }

    public void LockedIn()
    {
        playerLockedIn = true;
    }
}

