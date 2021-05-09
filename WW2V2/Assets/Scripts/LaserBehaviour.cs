using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed = 30f;
    void Start()
    {

        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0,bulletSpeed * Time.deltaTime);
    }
}
