using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.StopAllCoroutines();
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.camLocked = false;
        GameManager.Instance.playerLocked = false;
        GameManager.Instance.timer = GameManager.Instance.StartCoroutine(GameManager.Instance.CountDown());

        if (GameManager.Instance.timeLeft <= 0)
        {
            GameManager.Instance.RestartAfterGameOver();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
