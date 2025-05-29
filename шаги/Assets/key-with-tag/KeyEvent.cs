using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvent : MonoBehaviour
{
    [SerializeField] AdvancedDoor Door; // Изменили тип на AdvancedDoor

    public void UnlockDoor()
    {
        Door.Unlock();
        Destroy(gameObject);
    }
}