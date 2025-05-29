using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    [SerializeField] KeyCode PickUp;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3))
        {
            if (hit.collider.tag == "Key" )
            {
                if (Input.GetKeyDown(PickUp))
                {
                    hit.collider.gameObject.GetComponent<KeyEvent>().UnlockDoor();
                }
            }
            if (hit.collider.tag == "Door")
            {
                if (Input.GetKeyDown(PickUp))
                {
                    hit.collider.gameObject.GetComponent<AdvancedDoor>().HandleDoorInteraction();
                }
            }
        }
    }
}