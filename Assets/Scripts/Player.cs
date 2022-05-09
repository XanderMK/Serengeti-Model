using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{

    public CharacterController cc;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float camSensitivity;

    Vector2 moveInput;
    float upDownInput;

    float lookX;
    float lookY;

    bool isInMenu = false;

    public GameObject menu;

    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        transform.localEulerAngles = new Vector3(lookX, lookY, 0f);

        cc.Move((transform.forward * moveInput.y + transform.right * moveInput.x).normalized * moveSpeed * (Time.deltaTime/Time.timeScale) + Vector3.up * upDownInput * moveSpeed * (Time.deltaTime/Time.timeScale));
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value) {
        if (!isInMenu)
        {
            Vector2 input = value.Get<Vector2>();

            lookX -= input.y * camSensitivity; // Vertical rotation
            lookY += input.x * camSensitivity; // Horizontal rotation

            lookX = Mathf.Clamp(lookX, -90f, 90f);
        }
    }

    void OnMoveUpAndDown(InputValue value) {
        upDownInput = value.Get<float>();
    }

    void OnToggleMenu()
    {
        isInMenu = !isInMenu;
        if (isInMenu)
        {
            menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        } else
        {
            menu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
