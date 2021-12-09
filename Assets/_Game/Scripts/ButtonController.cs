using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.timeLeft = 900;
        GameManager.Instance.StopAllCoroutines();
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.camLocked = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
