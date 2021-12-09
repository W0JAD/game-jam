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
            Debug.Log("Pressed");
            TextMeshProUGUI[] labels = GameManager.Instance.screenController.inGame.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI label in labels)
            {
                if (label.enabled)
                {
                    Debug.Log(label.name);
                    switch (label.name)
                    {
                        case "Card":
                            GameManager.Instance.card = true;
                            TextMeshProUGUI[] objs = FindObjectsOfType<TextMeshProUGUI>();
                            foreach (TextMeshProUGUI obj in objs)
                            {
                                if (obj.name == "FeedbackPickup") obj.enabled = true;
                            }
                            break;
                        case "Code":
                            GameManager.Instance.code = true;
                            TextMeshProUGUI[] objs1 = FindObjectsOfType<TextMeshProUGUI>();
                            foreach (TextMeshProUGUI obj in objs1)
                            {
                                if (obj.name == "FeedbackPress") obj.enabled = true;
                            }
                            break;
                        case "Key":
                            GameManager.Instance.key = true;
                            TextMeshProUGUI[] objs2 = FindObjectsOfType<TextMeshProUGUI>();
                            foreach (TextMeshProUGUI obj in objs2)
                            {
                                if (obj.name == "FeedbackSearch") obj.enabled = true;
                            }
                            break;
                        case "FakeCard":

                            break;
                        case "FakeCode":

                            break;
                        case "FakeKey":

                            break;
                        case "TrapCard":

                            break;
                        case "TrapCode":

                            break;
                        case "TrapKey":

                            break;
                    }
                }
            }
        }
    }
}
