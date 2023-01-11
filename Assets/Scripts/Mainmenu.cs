using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Mainmenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = PlayerPrefs.GetInt("score").ToString();
    }

    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quitting()
    {
        Application.Quit();
    }
}
