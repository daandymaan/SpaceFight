using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public List<GameObject> shipCameraPos = new List<GameObject>();
    public int shipIndex = 0;
    public GameObject ship;
    public GameObject target;
    public Transform targetFocus;

    void Awake()
    {
        getOsturLeaderTarget();
        StartCoroutine(changeCameraPosition());
    }
    void LateUpdate()
    {
        if(ship == null || target == null)
        {
            getOsturLeaderTarget();
        } 
        else 
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime);
            transform.LookAt(targetFocus);
        }
    }

    void getOsturLeaderTarget()
    {
        shipCameraPos.Clear();
        GameObject[] targets =  GameObject.FindGameObjectsWithTag("ostur");
        foreach(GameObject go in targets)
        {
            if(go.GetComponent<ShipSystems>().squadLeader)
            {
                ship = go;
            }
        }
        GameObject cameraGOInShip =  ship.gameObject.transform.Find("Camera_positions").gameObject;
        for(int i = 0; i < cameraGOInShip.transform.childCount; i++)
        {
            shipCameraPos.Add(cameraGOInShip.transform.GetChild(i).gameObject);
        }
        target = shipCameraPos[0];
    }

    IEnumerator changeCameraPosition() 
    {
        while(true)
        {

            if(ship.GetComponent<FollowPathBehaviour>().enabled == true)
            {
                int checkpoint = ship.GetComponent<FollowPathBehaviour>().path.next % 3;
                checkpoint = Mathf.Clamp(checkpoint, 1 , 2);
                target = shipCameraPos[checkpoint];
                targetFocus = ship.transform;
            } 
            else if(ship.GetComponent<AttackBehaviour>().enabled == true)
            {
                target = shipCameraPos[3];
                targetFocus = ship.GetComponent<ShipSystems>().targetEnemy.gameObject.transform;
            } 
            else if(ship.GetComponent<ShipSystems>().targetEnemy!=null && ship.GetComponent<ShipSystems>().targetEnemy.GetComponent<ShipSystems>().health == 0)
            {
                target = shipCameraPos[0];
                targetFocus = ship.GetComponent<ShipSystems>().targetEnemy.gameObject.transform;
            }
            else 
            {
                target = shipCameraPos[0];
                targetFocus = ship.transform;
            }
            yield return new WaitForSeconds(2f);
        } 
    }
}
