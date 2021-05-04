using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messerschmitt : ShipSystems
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new Cruise());
        GetComponent<StateMachine>().SetGlobalState(new Alive());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
