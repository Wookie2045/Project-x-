using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sit : MonoBehaviour
{
    [Header("Настройки")]
    public float crouchHeight = 1.0f;      // Высота при приседании
    public float crouchSpeed = 3f;         // Скорость в приседе
    public KeyCode crouchKey = KeyCode.LeftControl;

    private float originalHeight;          // Исходная высота
    private float originalSpeed;          // Исходная скорость
    private bool isCrouching = false;
    private CapsuleCollider col;
    private CharacterController controller;

    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        controller = GetComponent<CharacterController>();

        // Запоминаем исходные параметры
        if (col != null)
        {
            originalHeight = col.height;
        }
        else if (controller != null)
        {
            originalHeight = controller.height;
        }

        // Получаем скорость из скрипта движения (замените на ваш)
        originalSpeed = GetComponent<PlayerMovement>().speed;
    }

    void Update()
    {
        // При нажатии клавиши — присесть/встать
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = !isCrouching;  // Переключаем состояние
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
        // Изменяем коллайдер
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

        // Меняем скорость (опционально)
        GetComponent<PlayerMovement>().speed = crouch ? crouchSpeed : originalSpeed;
    }
}
