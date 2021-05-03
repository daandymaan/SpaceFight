using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody;
    private float bulletSpeed = 10.0f;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.Force);
    }
}
