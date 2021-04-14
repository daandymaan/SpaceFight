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
    public override void Enter()
    {
        owner.GetComponent<FollowPathBehaviour>().enabled = true;
    }
    public override void Think()
    {
        if(owner.GetComponent<Ship>().targetEnemy != null)
        {
            Vector3 distanceFromEnemy = owner.GetComponent<Ship>().targetEnemy.transform.position - owner.transform.position;
            //Within shooting range of enemy
            if(distanceFromEnemy.magnitude < owner.GetComponent<Ship>().shootingRange)
            {
                //Enemy in FOV and ship has ammo
                if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) < 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Attack());
                }
                //Enemy not in FOV and ship has ammo
                else if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) > 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Avoid());
                }
                else 
                {
                    owner.ChangeState(new Flee());
                }
            } 
            //Outside of shooting range of enemy
            else
            {
                //Enemy in FOV and ship has ammo
                if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) < 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Pursue());
                }
                //Enemy not in FOV and ship has ammo
                else if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) > 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Avoid());
                }
                else 
                {
                    owner.ChangeState(new Flee());
                }
            }
        }
    }
    public override void Exit()
    {
        owner.GetComponent<FollowPathBehaviour>().enabled = false;
    }
}
public class Attack : State 
{
    public override void Enter()
    {
        owner.GetComponent<AttackBehaviour>().enabled = true;
    }
    public override void Think()
    {
        if(owner.GetComponent<Ship>().targetEnemy == null)
        {
            owner.ChangeState(new Cruise());
        } 
        else 
        {
            Vector3 distanceFromEnemy = owner.GetComponent<Ship>().targetEnemy.transform.position - owner.transform.position;

            if(distanceFromEnemy.magnitude < owner.GetComponent<Ship>().shootingRange)
            {
                //Enemy in FOV and has Ammo
                if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) > 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Avoid());
                }
                else 
                {
                    owner.ChangeState(new Flee());
                }
            } 
            else
            {
                //Enemy in FOV and ship has ammo
                if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) < 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Pursue());
                }
                //Enemy not in FOV and ship has ammo
                else if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) > 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Avoid());
                }
                else 
                {
                    owner.ChangeState(new Flee());
                }
            }
        }

    }
    public override void Exit()
    {
        owner.GetComponent<AttackBehaviour>().enabled = false;
    }
}
public class Flee : State
{
    public override void Enter()
    {
        owner.GetComponent<FleeBehaviour>().enabled = true;
    }
    public override void Think()
    {
        if(owner.GetComponent<Ship>().targetEnemy == null)
        {
            owner.ChangeState(new Cruise());
        } 
        else 
        {
            Vector3 distanceFromEnemy = owner.GetComponent<Ship>().targetEnemy.transform.position - owner.transform.position;

            if(distanceFromEnemy.magnitude < owner.GetComponent<Ship>().shootingRange)
            {
                //Enemy in FOV and ship has ammo
                if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) < 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Attack());
                }
                //Enemy in FOV and has Ammo
                if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) > 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Avoid());
                }
            } 
            else
            {
                //Enemy in FOV and ship has ammo
                if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) < 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Pursue());
                }
                //Enemy not in FOV and ship has ammo
                if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) > 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Avoid());
                }
            }
        }

       
    }
    public override void Exit()
    {
        owner.GetComponent<FleeBehaviour>().enabled = false;
    }
}
public class Avoid : State
{
    public override void Enter()
    {
        owner.GetComponent<CombatAvoidanceBehaviour>().enabled = true;
    }
    public override void Think()
    {
       if(owner.GetComponent<Ship>().targetEnemy == null)
        {
            owner.ChangeState(new Cruise());
        }
        else 
        {
            Vector3 distanceFromEnemy = owner.GetComponent<Ship>().targetEnemy.transform.position - owner.transform.position;
            //Within shooting range of enemy
            if(distanceFromEnemy.magnitude < owner.GetComponent<Ship>().shootingRange)
            {
                //Enemy in FOV and ship has ammo
                if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) < 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Attack());
                }
                //No ammo
                if(owner.GetComponent<Ship>().ammo == 0)
                {
                    owner.ChangeState(new Flee());
                }
            } 
            //Outside of shooting range of enemy
            else
            {
                //Enemy in FOV and ship has ammo
                if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) < 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Pursue());
                }
                //No ammo
                if(owner.GetComponent<Ship>().ammo == 0)
                {
                    owner.ChangeState(new Flee());
                }
            }
        }
    }
    public override void Exit()
    {
        owner.GetComponent<CombatAvoidanceBehaviour>().enabled = false;
    }
}

public class Pursue : State
{
    public override void Enter()
    {
        owner.GetComponent<PursueBehaviour>().enabled = true;
    }
    public override void Think()
    {
        if(owner.GetComponent<Ship>().targetEnemy == null)
        {
            owner.ChangeState(new Cruise());
        }
        else 
        {
            Vector3 distanceFromEnemy = owner.GetComponent<Ship>().targetEnemy.transform.position - owner.transform.position;
            //Within shooting range of enemy
            if(distanceFromEnemy.magnitude < owner.GetComponent<Ship>().shootingRange)
            {
                //Enemy in FOV and ship has ammo
                if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) < 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Attack());
                }
                //Enemy not in FOV and ship has ammo
                else if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) > 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Avoid());
                }
                else 
                {
                    owner.ChangeState(new Flee());
                }
            } 
            //Outside of shooting range of enemy
            else
            {
                //Enemy not in FOV and ship has ammo
                if(Vector3.Angle(owner.transform.forward, distanceFromEnemy) > 45 && owner.GetComponent<Ship>().ammo != 0)
                {
                    owner.ChangeState(new Avoid());
                }
                //No Ammo
                if(owner.GetComponent<Ship>().ammo == 0) 
                {
                    owner.ChangeState(new Flee());
                }
            }
        }
    }
    public override void Exit()
    {
        owner.GetComponent<PursueBehaviour>().enabled = false;
    }
}

public class Alive : State 
{
    public override void Think()
    {
        if (owner.GetComponent<Ship>().health <= 0)
        {
            owner.ChangeState(new Destroyed());
            owner.SetGlobalState(new Destroyed());
            return;
        }
    }
}

public class Destroyed :State
{
    public override void Enter()
    {
        SteeringBehaviour[] sbs = owner.GetComponent<Ship>().GetComponents<SteeringBehaviour>();
        foreach(SteeringBehaviour sb in sbs)
        {
            sb.enabled = false;
        }
        owner.GetComponent<StateMachine>().enabled = false;    
    }
}
