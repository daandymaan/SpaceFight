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
        if(owner.GetComponent<ShipSystems>().squadLeader)
        {
            owner.GetComponent<FollowPathBehaviour>().path = owner.GetComponent<ShipSystems>().path;
            owner.GetComponent<FollowPathBehaviour>().enabled = true;
        } 
        else 
        {
            owner.GetComponent<FollowLeaderBehaviour>().leader = owner.GetComponent<ShipSystems>().targetLeader;
            owner.GetComponent<FollowLeaderBehaviour>().enabled = true;
        }

    }
    public override void Think()
    {
        //If leader dies while following
        if(owner.GetComponent<FollowLeaderBehaviour>().leader == null)
        {
            owner.GetComponent<FollowLeaderBehaviour>().leader = owner.GetComponent<ShipSystems>().targetLeader;
        }
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
                    if(angleFromEnemy <= 60)
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
                    else 
                    {
                        //If enemy is behind but within detection range
                        owner.ChangeState(new Avoid());
                    }
                }

                //Distance to enemy is within shooting range
                if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().shootingRange && distanceFromEnemy.magnitude > 10)
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
                //Ememy to close
                if(distanceFromEnemy.magnitude <= 10)
                {
                    owner.ChangeState(new Swerve());
                }
        }
    }
    public override void Exit()
    {
        owner.GetComponent<FollowPathBehaviour>().enabled = false;
        owner.GetComponent<FollowLeaderBehaviour>().enabled = false;
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
        if(owner.GetComponent<ShipSystems>().targetEnemy == null ||  owner.GetComponent<AttackBehaviour>().enemyTarget == null)
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
                if(angleFromEnemy <= 60)
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
                else 
                {
                    //If enemy is behind but within detection range
                    owner.ChangeState(new Avoid());
                }
            }

            //Distance to enemy is within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().shootingRange && distanceFromEnemy.magnitude > 10)
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

            //Ememy to close
            if(distanceFromEnemy.magnitude <= 10)
            {
                owner.ChangeState(new Swerve());
            }

            // //Distance to enemy is outside of all ranges
            // if(distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().detectionRange)
            // {
            //     owner.ChangeState(new Cruise());
            // }
        }
    }
    public override void Exit()
    {
        owner.GetComponent<AttackBehaviour>().enabled = false;
    }
}


public class Swerve : State 
{
    
    public override void Enter() 
    {
        owner.GetComponent<SwerveBehaviour>().enemyTarget = owner.GetComponent<ShipSystems>().targetEnemy;
        owner.GetComponent<SwerveBehaviour>().enabled = true;
    }
    public override void Think() 
    {
        if(owner.GetComponent<SwerveBehaviour>().swerveComplete == true)  
        {
            if(owner.GetComponent<ShipSystems>().targetEnemy == null ||  owner.GetComponent<SwerveBehaviour>().enemyTarget == null)
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
                    if(angleFromEnemy <= 60)
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
                    else 
                    {
                        //If enemy is behind but within detection range
                        owner.ChangeState(new Avoid());
                    }
                }

                //Distance to enemy is within shooting range
                if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().shootingRange && distanceFromEnemy.magnitude > 10)
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

                if(distanceFromEnemy.magnitude <= 10)
                {
                    owner.ChangeState(new Flee());
                }  

                //Distance to enemy is outside of all ranges
                if(distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().detectionRange)
                {
                    owner.ChangeState(new Cruise());
                }              
            }
        }
        else 
        {
            if(Vector3.Distance(owner.GetComponent<ShipSystems>().targetEnemy.transform.position, owner.GetComponent<ShipSystems>().transform.position) > owner.GetComponent<ShipSystems>().detectionRange)
            {
                owner.ChangeState(new Cruise());
            }
        }
    }
    public override void Exit() 
    { 
        owner.GetComponent<SwerveBehaviour>().enabled = false;
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
        if(owner.GetComponent<ShipSystems>().targetEnemy == null ||  owner.GetComponent<FleeBehaviour>().enemyTarget == null)
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
                if(angleFromEnemy <= 60)
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Pursue());
                    }
                }
                else 
                {
                    //If enemy is behind but within detection range
                    owner.ChangeState(new Avoid());
                }
            }

            //Distance to enemy is within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().shootingRange && distanceFromEnemy.magnitude > 10)
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

            // //Distance to enemy is outside of all ranges
            // if(distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().detectionRange)
            // {
            //     owner.ChangeState(new Cruise());
            // }
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
        if(owner.GetComponent<ShipSystems>().targetEnemy == null ||  owner.GetComponent<CombatAvoidanceBehaviour>().enemyTarget == null)
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
                if(angleFromEnemy <= 60)
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
                if(angleFromEnemy <= 180)
                {
                    if(owner.GetComponent<ShipSystems>().ammo > 0)
                    {
                        owner.ChangeState(new Pursue());
                    } 
                    else 
                    {
                        owner.ChangeState(new Swerve());
                    }
                }
            }

            //Distance to enemy is within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().shootingRange && distanceFromEnemy.magnitude > 10)
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

            //Ememy to close
            if(distanceFromEnemy.magnitude <= 10)
            {
                owner.ChangeState(new Swerve());
            }

            // //Distance to enemy is outside of all ranges
            // if(distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().detectionRange)
            // {
            //     owner.ChangeState(new Cruise());
            // }
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
        if(owner.GetComponent<ShipSystems>().targetEnemy == null || owner.GetComponent<PursueBehaviour>().enemyTarget== null)
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
                if(angleFromEnemy <= 60)
                {
                    //The ship has ammo
                    if(owner.GetComponent<ShipSystems>().ammo < 0)
                    {
                        owner.ChangeState(new Flee());
                    }
                }
                else 
                {
                    //If enemy is behind but within detection range
                    owner.ChangeState(new Avoid());
                } 
            }

            //Distance to enemy is within shooting range
            if(distanceFromEnemy.magnitude < owner.GetComponent<ShipSystems>().shootingRange && distanceFromEnemy.magnitude > 10)
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

            //Ememy to close
            if(distanceFromEnemy.magnitude <= 10)
            {
                owner.ChangeState(new Swerve());
            }

            // //Distance to enemy is outside of all ranges            
            // if(distanceFromEnemy.magnitude > owner.GetComponent<ShipSystems>().detectionRange)
            // {
            //     owner.ChangeState(new Cruise());
            // }            
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
        owner.GetComponent<DeathBehaviour>().enabled = true;
    }
}
