using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    private float minutes, seconds;
    [SerializeField] private TextMeshProUGUI text;

    private void Update()
    {
        minutes = Mathf.Floor(GameManager.Instance.timeLeft / 60);
        seconds = Mathf.RoundToInt(GameManager.Instance.timeLeft % 60);

        if (seconds >= 10)
        {
            text.text = $"{minutes}:{seconds}";
        } else
        {
            text.text = $"{minutes}:0{seconds}";
        }
    }
}
