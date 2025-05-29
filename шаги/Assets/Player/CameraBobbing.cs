using Cinemachine; // ����������: ���� CinemaChine
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobbing : MonoBehaviour // �������� ��� ������ � "Camera"
{
    [Header("��������� �������")]
    public float walkingBobbingSpeed = 14f; // �������� �������
    public float bobbingAmount = 0.05f; // ���� �������
    public CinemachineVirtualCamera virtualCamera; // ����������: ���� CinemaChineVirtualCamera

    private float defaultYPos = 0;
    private float timer = 0;

    void Start()
    {
        if (virtualCamera != null)
            defaultYPos = virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y;
    }

    void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            // ����� ��������
            timer += Time.deltaTime * walkingBobbingSpeed;
            float newYPos = defaultYPos + Mathf.Sin(timer) * bobbingAmount;

            var transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            transposer.m_FollowOffset.y = newYPos;
        }
        else
        {
            // ����� ����� �� ����� - ������� ������� � �������� ���������
            timer = 0;
            var transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            transposer.m_FollowOffset.y = Mathf.Lerp(transposer.m_FollowOffset.y, defaultYPos, Time.deltaTime * 2f);
        }
    }
}