using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveBehaviour : SteeringBehaviour
{
    public GameObject enemyTarget;
    public bool swerveComplete;
    public List<Vector3> swervePoints = new List<Vector3>();
    public int nextPoint;
    public float radius;
    public float theta;
    // public float FOVAngle;


    void OnEnable()
    {

        swerveComplete = false;
        swervePoints.Clear();
        nextPoint = 0;
        radius = Vector3.Distance(ship.transform.position, enemyTarget.transform.position);
        theta = Mathf.PI * 2.0f / (float) radius;
        if(ship.transform.tag == "ostur")
        {
            getSwervePointsOstur();
        } 
        else if(ship.transform.tag == "jibinis")
        {
            getSwervePointsJibins();
        } 
        else 
        {
            getSwervePointsOstur();
        }
    }

    void OnDisable()
    {
        enemyTarget = null;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override Vector3 Calculate()
    {
        if(swerveComplete == false)
        {
            if(Vector3.Distance(ship.transform.position, swervePoints[nextPoint]) < radius/3){
                return ship.SeekForce(getNextPoint());
            }
            else
            {
                return ship.SeekForce(swervePoints[nextPoint]);
            }

        } 
        else 
        {
            return ship.SeekForce(swervePoints[nextPoint]);
        }
    }

    public void getSwervePointsJibins()
    {
        int startIndex = Mathf.RoundToInt(radius/4);
        int finishIndex = Mathf.RoundToInt(radius/2);
        for(int i = startIndex; i < finishIndex; i++)
        {
            Vector3 pos = new Vector3(Mathf.Sin(theta * i) * (enemyTarget.transform.position.x + (enemyTarget.transform.forward.x * radius)), enemyTarget.transform.position.y + (enemyTarget.transform.forward.y * radius), Mathf.Cos(theta * i) * (enemyTarget.transform.position.z + (enemyTarget.transform.forward.z * radius)));
            swervePoints.Add(pos);
        }
    }

    public void  getSwervePointsOstur()
    {
        int startIndex = Mathf.RoundToInt(radius);
        int finishIndex = Mathf.RoundToInt(radius - (radius/3));

        for(int i = startIndex; i > finishIndex; i--)
        {
            Vector3 pos = new Vector3(Mathf.Sin(theta * i) * (enemyTarget.transform.position.x + (enemyTarget.transform.forward.x * radius)), enemyTarget.transform.position.y + (enemyTarget.transform.forward.y * radius), Mathf.Cos(theta * i) * (enemyTarget.transform.position.z + (enemyTarget.transform.forward.z * radius)));
            swervePoints.Add(pos);
        }
    }

    public Vector3 getNextPoint()
    {
        if(nextPoint < swervePoints.Count -1 )
        {
            Vector3 point = swervePoints[nextPoint];
            nextPoint++;
            return point;
        } 
        else 
        {
            swerveComplete = true;
            return swervePoints[nextPoint];
        }
    }

}
