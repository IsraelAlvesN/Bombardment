using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10;
    void Start()
    {
        
    }

    void Update()
    {
        //Read Input
        bool isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

        //Create Movement vector
        float movementZ = isUp ? 1 : isDown ? -1 : 0;
        float movementX = isRight ? 1 : isLeft ? -1 : 0;
        Vector3 movementVector = new Vector3(movementX, 0, movementZ);

        //Apply input to character
        transform.Translate(movementVector * movementSpeed * Time.deltaTime);
    }
}
