using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvent : MonoBehaviour
{
    [SerializeField] AdvancedDoor Door; // �������� ��� �� AdvancedDoor

    public void UnlockDoor()
    {
        Door.Unlock();
        Destroy(gameObject);
    }
}