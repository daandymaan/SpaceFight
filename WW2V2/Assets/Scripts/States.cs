using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public StateMachine owner;
    public virtual void Enter() {}
    public virtual void Exit() { }
    public virtual void Think() { }
}

public class Cruise : State
{

}
public class Attack : State 
{

}
public class Flee : State
{

}
public class Avoid : State
{

}

public class Alive : State 
{
    
}

public class Destroyed :State
{

}
