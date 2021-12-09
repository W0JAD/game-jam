using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.cardLock.activeInHierarchy == false && GameManager.Instance.keyLock.activeInHierarchy == false && GameManager.Instance.codeLock.activeInHierarchy == false){
            GameManager.Instance.StopAllCoroutines();
            SceneManager.LoadScene(2);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
