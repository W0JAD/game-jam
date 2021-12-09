using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public CharacterController player;
    [HideInInspector] public float xAxis;
    [HideInInspector] public float yAxis;
    public float sensitivity = 1f;
    public bool invert = false;
    public float speed = 5f;
    public float jumpSpeed = 7.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private float turner;
    private float looker;
    private Transform camera;

    private void Start()
    {
        camera = transform.GetChild(0);
    }
    void Update()
    {
        TextMeshProUGUI[] deflabels = GameManager.Instance.screenController.inGame.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI deflabel in deflabels)
        {
            if (deflabel.name == "Pickup" || deflabel.name == "Press" || deflabel.name == "Search")
            {
                deflabel.enabled = false;
            }
        }
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded && GameManager.Instance.playerLocked == false)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        if (GameManager.Instance.camLocked == false && GameManager.Instance.playerLocked == false)
        {
            turner = Input.GetAxis("Mouse X") * sensitivity;
            looker = -Input.GetAxis("Mouse Y") * sensitivity;
            transform.eulerAngles = transform.eulerAngles + new Vector3(invert ? xAxis : -xAxis, Mathf.Clamp(yAxis, -90, 90), 0f);
        }
            if (turner != 0 && GameManager.Instance.playerLocked == false)
        {
            transform.eulerAngles += new Vector3(0, turner, 0);
        }
        if (looker != 0 && GameManager.Instance.playerLocked == false)
        {
            transform.eulerAngles += new Vector3(looker, 0, 0);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        if(GameManager.Instance.playerLocked == false)
        {
            controller.Move(moveDirection * Time.deltaTime);
        }
        bool hit = Physics.Raycast(camera.transform.position, transform.forward, out RaycastHit detectedObjective, 2f);
        Debug.DrawRay(camera.transform.position, transform.forward * 2f, Color.red);

        if (hit)
        {
            switch (detectedObjective.transform.tag)
            {
                case "Card":
                    GameManager.Instance.GetObjectByName("Pickup").enabled = true;
                    break;
                case "Code":
                    GameManager.Instance.GetObjectByName("Press").enabled = true;
                    break;
                case "Key":
                    GameManager.Instance.GetObjectByName("Search").enabled = true;
                    break;
                case "FakeCard":
                    GameManager.Instance.GetObjectByName("Pickup").enabled = true;
                    break;
                case "FakeCode":
                    GameManager.Instance.GetObjectByName("Press").enabled = true;
                    break;
                case "FakeKey":
                    GameManager.Instance.GetObjectByName("Search").enabled = true;
                    break;
                case "TrapCard":
                    GameManager.Instance.GetObjectByName("Pickup").enabled = true;
                    break;
                case "TrapCode":
                    GameManager.Instance.GetObjectByName("Press").enabled = true;
                    break;
                case "TrapKey":
                    GameManager.Instance.GetObjectByName("Search").enabled = true;
                    break;
                default:
                    TextMeshProUGUI[] deflabels1 = GameManager.Instance.screenController.inGame.GetComponentsInChildren<TextMeshProUGUI>();
                    foreach (TextMeshProUGUI deflabel1 in deflabels)
                    {
                        if (deflabel1.name == "Pickup" || deflabel1.name == "Press" || deflabel1.name == "Search")
                        {
                            deflabel1.enabled = false;
                        }
                    }
                    break;
            }
        }

        GameManager.Instance.hit = hit;
        GameManager.Instance.targetedObj = detectedObjective;
    }
}
