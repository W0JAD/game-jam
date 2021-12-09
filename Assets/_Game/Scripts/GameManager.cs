using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int timeLeft = 300;
    public bool camLocked = false;
    public IEnumerator timer;
    public static GameManager Instance;
    public ScreenController screenController;
    public string[] objectives;
    public bool card, key, code;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("There is already a game manager.");
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        screenController = GetComponent<ScreenController>();
        timer = CountDown();
        StartCoroutine(timer);
    }

    private void Update()
    {
        SetFields();
    }

    public IEnumerator CountDown()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1f);
            timeLeft -= 1;
        }
        StopCoroutine(timer);
    }

    private void SetFields()
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (Canvas c in canvases)
        {
            switch (c.name)
            {
                case "InGameScreen":
                    screenController.inGame = c; break;
                case "RuleBreakScreen":
                    screenController.ruleBreak = c; break;
                case "GameOverScreen":
                    screenController.gameOver = c; break;
                case "PauseScreen":
                    screenController.pause = c; break;
            }
        }
        PlayerController playerController = FindObjectOfType<PlayerController>();
        screenController.playerController = playerController;
    }
}
