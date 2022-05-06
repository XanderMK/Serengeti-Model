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

    [Header("Other")]
    [SerializeField] private float simSpeed;

    Vector2 moveInput;
    float upDownInput;

    float lookX;
    float lookY;

    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (simSpeed != Time.timeScale)
            Time.timeScale = simSpeed;

        transform.localEulerAngles = new Vector3(lookX, lookY, 0f);

        cc.Move((transform.forward * moveInput.y + transform.right * moveInput.x).normalized * moveSpeed * Time.deltaTime + Vector3.up * upDownInput * moveSpeed * Time.deltaTime);
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value) {
        Vector2 input = value.Get<Vector2>();

        lookX -= input.y * camSensitivity; // Vertical rotation
        lookY += input.x * camSensitivity; // Horizontal rotation

        lookX = Mathf.Clamp(lookX, -90f, 90f);
    }

    void OnMoveUpAndDown(InputValue value) {
        upDownInput = value.Get<float>();
    }
}
