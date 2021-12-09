using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int timeLeft;
    public bool camLocked = false;
    public Coroutine timer;
    public static GameManager Instance;
    public ScreenController screenController;
    public bool card, key, code, hit;
    public RaycastHit targetedObj;
    public bool restartedAfterGameOver;
    public GameObject keyLock, codeLock, cardLock;
    public bool playerLocked = false;

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
        timer = Instance.StartCoroutine(Instance.CountDown());
    }

    private void Update()
    {
        SetFields();
        if (timeLeft <= 0)
        {
            screenController.inGame.enabled = false;
            screenController.pause.enabled = false;
            screenController.ruleBreak.enabled = false;
            screenController.gameOver.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            camLocked = true;
        }
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
        GameObject[] locks = FindObjectsOfType<GameObject>();
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
        foreach (GameObject _lock in locks)
        {
            switch (_lock.name)
            {
                case "KeyLock":
                    keyLock = _lock; break;
                case "CodeLock":
                    codeLock = _lock; break;
                case "CardLock":
                    cardLock = _lock; break;
            }
        }
        PlayerController playerController = FindObjectOfType<PlayerController>();
        screenController.playerController = playerController;
    }

    public TextMeshProUGUI GetObjectByName(string name)
    {
        for (int i = 0; i < screenController.inGame.transform.childCount; i++)
        {
            TextMeshProUGUI label = screenController.inGame.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            if (label != null)
            {
                if (label.name == name)
                {
                    return label;
                }
            }
        }
        return null;
    }

    public TextMeshProUGUI GetObjectById(int id)
    {
        for (int i = 0; i < screenController.inGame.transform.childCount; i++)
        {
            if (i == id)
            {
                TextMeshProUGUI label = screenController.inGame.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                return label;
            }
        }
        return null;
    }

    public string GetTargetedObjectTag()
    {
        if (hit)
        {
            return targetedObj.transform.tag switch
            {
                "Card" => "Card",
                "Code" => "Code",
                "Key" => "Key",
                "FakeCard" => "FakeCard",
                "FakeCode" => "FakeCode",
                "FakeKey" => "FakeKey",
                "TrapCard" => "TrapCard",
                "TrapCode" => "TrapCode",
                "TrapKey" => "TrapKey",
                "KeyLock" => "KeyLock",
                "CodeLock" => "CodeLock",
                "CardLock" => "CardLock",
                _ => null,
            };
        }
        return null;
    }

    public IEnumerator DisableAfter(int seconds, TextMeshProUGUI obj)
    {
        if (obj.enabled)
        {
            yield return new WaitForSeconds(seconds);
            obj.enabled = false;
        }
    }

    public void RestartAfterGameOver()
    {
        timeLeft = 600;
        StopAllCoroutines();
        Invoke(nameof(StartCor), 0.1f);
    }

    private void StartCor()
    {
        StartCoroutine(CountDown());
    }
}
