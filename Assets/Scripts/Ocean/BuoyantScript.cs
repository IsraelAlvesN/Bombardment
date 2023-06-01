using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyantScript : MonoBehaviour
{
    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;

    public float bouyancyForce = 10;
    private Rigidbody thisRigidBody;

    private bool hasTouchedWater;

    private void Awake()
    {
        thisRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //check if underwater
        float diffY = transform.position.y;
        bool isUnderwater = diffY < 0;

        if (isUnderwater)
        {
            hasTouchedWater = true;
        }

        //ignore if never touched water
        if (!hasTouchedWater)
        {
            return;
        }

        //buoyancy logic
        if (isUnderwater)
        {
            Vector3 vector = Vector3.up * bouyancyForce * -diffY;

            thisRigidBody.AddForce(vector, ForceMode.Acceleration);
        }

        thisRigidBody.drag = isUnderwater ? underWaterDrag : airDrag;
        thisRigidBody.angularDrag = isUnderwater ? underWaterAngularDrag : airAngularDrag;
    }
}
