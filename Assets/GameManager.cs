using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text score;
    public GameObject panel;
    public void EndGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void WinGame()
    {
        panel.SetActive(true);
        score.text = "You won!";
    }
}
