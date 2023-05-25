using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float degreesPerSecond = 90f;
    void Start()
    {
        
    }

    void Update()
    {
        float stepY = degreesPerSecond * Time.deltaTime;
        transform.Rotate(0, stepY, 0);
    }
}
