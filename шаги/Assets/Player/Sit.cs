using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sit : MonoBehaviour
{
    [Header("���������")]
    public float crouchHeight = 1.0f;      // ������ ��� ����������
    public float crouchSpeed = 3f;         // �������� � �������
    public KeyCode crouchKey = KeyCode.LeftControl;

    private float originalHeight;          // �������� ������
    private float originalSpeed;          // �������� ��������
    private bool isCrouching = false;
    private CapsuleCollider col;
    private CharacterController controller;

    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        controller = GetComponent<CharacterController>();

        // ���������� �������� ���������
        if (col != null)
        {
            originalHeight = col.height;
        }
        else if (controller != null)
        {
            originalHeight = controller.height;
        }

        // �������� �������� �� ������� �������� (�������� �� ���)
        originalSpeed = GetComponent<PlayerMovement>().speed;
    }

    void Update()
    {
        // ��� ������� ������� � ��������/������
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = !isCrouching;  // ����������� ���������
            SetCrouch(isCrouching);
        }
        if (isCrouching && !Input.GetKey(crouchKey))
        {
            float rayLength = originalHeight - crouchHeight;
            if (!Physics.Raycast(transform.position, Vector3.up, rayLength))
            {
                isCrouching = false;
                SetCrouch(false);
            }
        }
    }

    void SetCrouch(bool crouch)
    {
        // �������� ���������
        if (col != null)
        {
            col.height = crouch ? crouchHeight : originalHeight;
            col.center = new Vector3(0, crouch ? crouchHeight / 2 : originalHeight / 2, 0);
        }
        else if (controller != null)
        {
            controller.height = crouch ? crouchHeight : originalHeight;
            controller.center = new Vector3(0, crouch ? crouchHeight / 2 : originalHeight / 2, 0);
        }

        // ������ �������� (�����������)
        GetComponent<PlayerMovement>().speed = crouch ? crouchSpeed : originalSpeed;
    }
}
