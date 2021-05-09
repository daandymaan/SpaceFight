using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "o_laser")
        {
            GameObject.Find("McCannon").GetComponent<ShipSystems>().health = GameObject.Find("McCannon").GetComponent<ShipSystems>().health - 5; 
        } 
    }
}
