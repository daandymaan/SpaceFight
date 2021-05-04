using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerBehaviour : MonoBehaviour
{
    public float propellerSpeed = 750f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.localRotation *= Quaternion.AngleAxis(propellerSpeed * Time.deltaTime, Vector3.back);
    }
}
