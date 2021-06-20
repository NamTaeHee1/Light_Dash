using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowControl : MonoBehaviour
{
    [SerializeField] private Transform ArrowTransform;

    [SerializeField] private GameObject ArrowObject;
    [SerializeField] private GameObject ParentObject;

    [SerializeField] private float ArrowRotateSpeed = 3.0f;

    private void Update() => ArrowRotate();

    void ArrowRotate()
    {
        if (ArrowTransform.eulerAngles.z >= 60 || ArrowTransform.eulerAngles.z <= -120)
            ArrowRotateSpeed *= -1;
        ArrowTransform.RotateAround(ParentObject.transform.position, Vector3.forward, Time.deltaTime * ArrowRotateSpeed);
    }
}
