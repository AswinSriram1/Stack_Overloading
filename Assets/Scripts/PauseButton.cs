using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    

    public void Pause()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        gameObject.SetActive(true);
    }
}
