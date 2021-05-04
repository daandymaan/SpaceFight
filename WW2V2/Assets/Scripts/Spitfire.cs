using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitfire : ShipSystems
{
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new Cruise());
        GetComponent<StateMachine>().SetGlobalState(new Alive());
    }

    void Update()
    {
        
    }
}
