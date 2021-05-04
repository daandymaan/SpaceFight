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
        //There is an enemy ship that exists 
       if(owner.GetComponent<ShipSystems>().targetEnemy != null)
       {
            //Distance from enemy ship
            Vector3 distanceFromEnemy = owner.GetComponent<ShipSystems>().targetEnemy.transform.position - owner.transform.position;
            //Angle from enemy ship
            float angleFromEnemy = Vector3.Angle(owner.transform.forward, distanceFromEnemy);

            //Distance to enemy is within detection range but not within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().detectionRange && distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().shootingRange)
            {
                //Angle to ship is within FOV
                if(angleFromEnemy <= 45)
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Pursue());
                    }
                    else 
                    {
                        owner.ChangeState(new Flee());
                    }
                } 
            }

            //Distance to enemy is within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().shootingRange)
            {
                //Angle to ship is within FOV
                if(angleFromEnemy <= 45)
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Attack());
                    }
                    else 
                    {
                        owner.ChangeState(new Avoid());
                    }
                } 
                else 
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Pursue());
                    }
                    else
                    {
                        owner.ChangeState(new Flee());
                    }
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
        owner.GetComponent<AttackBehaviour>().enemyTarget = owner.GetComponent<ShipSystems>().targetEnemy;
        owner.GetComponent<AttackBehaviour>().enabled = true;

    }
    public override void Think()
    {
        if(owner.GetComponent<ShipSystems>().targetEnemy == null)
        {
            owner.ChangeState(new Cruise());
        } 
        else
        {
            //Distance from enemy ship
            Vector3 distanceFromEnemy = owner.GetComponent<ShipSystems>().targetEnemy.transform.position - owner.transform.position;
            //Angle from enemy ship
            float angleFromEnemy = Vector3.Angle(owner.transform.forward, distanceFromEnemy);

            //Distance to enemy is within detection range but not within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().detectionRange && distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().shootingRange)
            {
                if(angleFromEnemy <= 45)
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Pursue());
                    }
                    else 
                    {
                        owner.ChangeState(new Flee());
                    }
                } 
            }

            //Distance to enemy is within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().shootingRange)
            {
                //Angle to ship is within FOV
                if(angleFromEnemy <= 45)
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Attack());
                    }
                    else 
                    {
                        owner.ChangeState(new Avoid());
                    }
                } 
                else 
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Pursue());
                    }
                    else
                    {
                        owner.ChangeState(new Flee());
                    }
                }
            }

            //Distance to enemy is outside of all ranges
            if(distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().detectionRange)
            {
                owner.ChangeState(new Cruise());
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
        owner.GetComponent<FleeBehaviour>().enemyTarget = owner.GetComponent<ShipSystems>().targetEnemy;
        owner.GetComponent<FleeBehaviour>().enabled = true;
    }
    public override void Think()
    {
        if(owner.GetComponent<ShipSystems>().targetEnemy == null)
        {
            owner.ChangeState(new Cruise());
        } 
        else 
        {
            //Distance from enemy ship
            Vector3 distanceFromEnemy = owner.GetComponent<ShipSystems>().targetEnemy.transform.position - owner.transform.position;
            //Angle from enemy ship
            float angleFromEnemy = Vector3.Angle(owner.transform.forward, distanceFromEnemy);

            //Distance to enemy is within detection range but not within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().detectionRange && distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().shootingRange)
            {
                //Angle to ship is within FOV
                if(angleFromEnemy <= 45)
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Pursue());
                    }
                } 
            }

            //Distance to enemy is within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().shootingRange)
            {
                //Angle to ship is within FOV
                if(angleFromEnemy <= 45)
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Attack());
                    }
                    else 
                    {
                        owner.ChangeState(new Avoid());
                    }
                } 
                else 
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Pursue());
                    }
                }
            }

            //Distance to enemy is outside of all ranges
            if(distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().detectionRange)
            {
                owner.ChangeState(new Cruise());
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
        owner.GetComponent<CombatAvoidanceBehaviour>().enemyTarget = owner.GetComponent<ShipSystems>().targetEnemy;
        owner.GetComponent<CombatAvoidanceBehaviour>().enabled = true;
    }
    public override void Think()
    {
       if(owner.GetComponent<ShipSystems>().targetEnemy == null)
        {
            owner.ChangeState(new Cruise());
        }
        else 
        {
            //Distance from enemy ship
            Vector3 distanceFromEnemy = owner.GetComponent<ShipSystems>().targetEnemy.transform.position - owner.transform.position;
            //Angle from enemy ship
            float angleFromEnemy = Vector3.Angle(owner.transform.forward, distanceFromEnemy);

            //Distance to enemy is within detection range but not within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().detectionRange && distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().shootingRange)
            {
                //Angle to ship is within FOV
                if(angleFromEnemy <= 45)
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Pursue());
                    }
                    else 
                    {
                        owner.ChangeState(new Flee());
                    }
                } 
            }

            //Distance to enemy is within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().shootingRange)
            {
                 //Angle to ship is within FOV
                if(angleFromEnemy <= 45)
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Attack());
                    }
                } 
                else 
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Pursue());
                    }
                    else 
                    {
                        owner.ChangeState(new Flee());
                    }
                }
            }

            //Distance to enemy is outside of all ranges
            if(distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().detectionRange)
            {
                owner.ChangeState(new Cruise());
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
        owner.GetComponent<PursueBehaviour>().enemyTarget = owner.GetComponent<ShipSystems>().targetEnemy;
        owner.GetComponent<PursueBehaviour>().enabled = true;
    }
    public override void Think()
    {
        if(owner.GetComponent<ShipSystems>().targetEnemy == null)
        {
            owner.ChangeState(new Cruise());
        }
        else 
        {
            //Distance from enemy ship
            Vector3 distanceFromEnemy = owner.GetComponent<ShipSystems>().targetEnemy.transform.position - owner.transform.position;
            //Angle from enemy ship
            float angleFromEnemy = Vector3.Angle(owner.transform.forward, distanceFromEnemy);
            //Distance to enemy is outside of all ranges

            //Distance to enemy is within detection range but not within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().detectionRange && distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().shootingRange)
            {
                //Angle to ship is within FOV
                if(angleFromEnemy <= 45)
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo < 0)
                    {
                        owner.ChangeState(new Flee());
                    }
                } 
            }

            //Distance to enemy is within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().shootingRange)
            {
                 //Angle to ship is within FOV
                if(angleFromEnemy <= 45)
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Attack());
                    }
                } 
                else 
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo < 0)
                    {
                        owner.ChangeState(new Flee());
                    }
                }
            }

            //Distance to enemy is outside of all ranges            
            if(distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().detectionRange)
            {
                owner.ChangeState(new Cruise());
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
        if (owner.GetComponent<ShipSystems>().health <= 0)
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
        SteeringBehaviour[] sbs = owner.GetComponent<ShipSystems>().GetComponents<SteeringBehaviour>();
        foreach(SteeringBehaviour sb in sbs)
        {
            sb.enabled = false;
        }
        owner.GetComponent<StateMachine>().enabled = false;    
    }
}
