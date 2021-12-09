using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenController : MonoBehaviour
{
    public Canvas inGame, ruleBreak, gameOver, pause;
    public PlayerController playerController;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && inGame.enabled && !ruleBreak.enabled && !gameOver.enabled)
        {
            inGame.enabled = false;
            pause.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.camLocked = true;
            GameManager.Instance.StopAllCoroutines();
            return;
        }

        if (Input.GetKeyDown(KeyCode.I) && !inGame.enabled && !ruleBreak.enabled && !gameOver.enabled && pause.enabled)
        {
            inGame.enabled = true;
            pause.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.Instance.camLocked = false;
            GameManager.Instance.StartCoroutine(GameManager.Instance.timer);
            return;
        }
    }
}
