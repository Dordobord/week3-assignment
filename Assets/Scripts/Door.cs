using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform firstRoom;
    [SerializeField] private Transform SecondRoom;
    [SerializeField] private CameraController camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
                camera.movetoNewLevel(SecondRoom);
            else 
                camera.movetoNewLevel(firstRoom);
        }
    }
}
