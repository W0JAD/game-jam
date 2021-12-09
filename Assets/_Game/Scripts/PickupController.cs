using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupController : MonoBehaviour
{
    private IEnumerator waiter;
    private void Start()
    {
        GameManager.Instance.key = false;
        GameManager.Instance.card = false;
        GameManager.Instance.code = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            string objTag = GameManager.Instance.GetTargetedObjectTag();
            if (objTag == "TrapCode" || objTag == "TrapKey" || objTag == "TrapCard")
            {
                GameManager.Instance.StopAllCoroutines();
                GameManager.Instance.screenController.KillPlayer();
            }
            for (int i = 0; i < GameManager.Instance.screenController.inGame.transform.childCount; i++)
            {
                TextMeshProUGUI feedback = GameManager.Instance.GetObjectById(i);
                if (feedback != null && !feedback.enabled)
                {
                    switch (feedback.name)
                    {
                        case "FeedbackPickup":
                            if (objTag == "Card")
                            {
                                GameManager.Instance.card = true;
                                feedback.enabled = true;
                                GameManager.Instance.StartCoroutine(GameManager.Instance.DisableAfter(4, feedback));
                            }
                            break;
                        case "FeedbackPress":
                            if (objTag == "Code")
                            {
                                GameManager.Instance.code = true;
                                feedback.enabled = true;
                                GameManager.Instance.StartCoroutine(GameManager.Instance.DisableAfter(4, feedback));
                            }
                            break;
                        case "FeedbackSearch":
                            if (objTag == "Key")
                            {
                                GameManager.Instance.key = true;
                                feedback.enabled = true;
                                GameManager.Instance.StartCoroutine(GameManager.Instance.DisableAfter(4, feedback));
                            }
                            break;
                        case "FeedbackPickupFake":
                            if (objTag == "FakeCard")
                            {
                                feedback.enabled = true;
                                GameManager.Instance.StartCoroutine(GameManager.Instance.DisableAfter(4, feedback));
                            }
                            break;
                        case "FeedbackPressFake":
                            if (objTag == "FakeCode")
                            {
                                feedback.enabled = true;
                                GameManager.Instance.StartCoroutine(GameManager.Instance.DisableAfter(4, feedback));
                            }
                            break;
                        case "FeedbackSearchFake":
                            if (objTag == "FakeKey")
                            {
                                feedback.enabled = true;
                                GameManager.Instance.StartCoroutine(GameManager.Instance.DisableAfter(4, feedback));
                            }
                            break;
                        case "NoKey":
                            if (objTag == "KeyLock")
                            {
                                if (!GameManager.Instance.key)
                                {
                                    feedback.text = "Potrzebujesz klucza ¿eby otworzyæ t¹ k³ódkê";
                                    feedback.enabled = true;
                                    GameManager.Instance.StartCoroutine(GameManager.Instance.DisableAfter(4, feedback));
                                } else
                                {
                                    GameManager.Instance.keyLock.SetActive(false);
                                }
                            }
                            else if (objTag == "CodeLock")
                            {
                                if (!GameManager.Instance.code)
                                {
                                    feedback.text = "Potrzebujesz kodu ¿eby otworzyæ t¹ k³ódkê";
                                    feedback.enabled = true;
                                    GameManager.Instance.StartCoroutine(GameManager.Instance.DisableAfter(4, feedback));
                                } else
                                {
                                    GameManager.Instance.codeLock.SetActive(false);
                                }
                            }
                            else if (objTag == "CardLock")
                            {
                                if (!GameManager.Instance.card)
                                {
                                    feedback.text = "Potrzebujesz karty ¿eby otworzyæ t¹ k³ódkê";
                                    feedback.enabled = true;
                                    GameManager.Instance.StartCoroutine(GameManager.Instance.DisableAfter(4, feedback));
                                } else
                                {
                                    GameManager.Instance.cardLock.SetActive(false);
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}
