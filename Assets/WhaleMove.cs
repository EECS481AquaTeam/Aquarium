using UnityEngine;
using System.Collections;

public class Whale : MonoBehaviour
{

    public Vector3 currentPosition;
    public Vector3 targetPosition;
    public Vector3 targetPosition1;
    public Vector3 targetPosition2;
    public Vector3 targetPosition3;
    public Vector3 targetPosition4;


    public bool V3Equal(Vector3 a, Vector3 b)
    {
        //return Vector3.SqrMagnitude(a - b) < 0.0001;
        return Vector3.SqrMagnitude(a - b) < 0.01;

    }
    // Use this for initialization
    void Start()
    {
        targetPosition1 = new Vector3(-4.8f, -7.7f, -1f);
        targetPosition2 = new Vector3(-4.8f, 7.7f, -1f);
        targetPosition3 = new Vector3(4.8f, 7.7f, -1f);
        targetPosition4 = new Vector3(4.8f, -7.7f, -1f);

        currentPosition = this.transform.position;

        if (V3Equal(currentPosition, targetPosition1))
            targetPosition = targetPosition2;
        else if (V3Equal(currentPosition, targetPosition2))
            targetPosition = targetPosition3;
        else if (V3Equal(currentPosition, targetPosition3))
            targetPosition = targetPosition4;
        else if (V3Equal(currentPosition, targetPosition4))
            targetPosition = targetPosition1;
    }

    void Update()
    {
        //move towards a target at a set speed.
        currentPosition = this.transform.position;
        Debug.Log(currentPosition);
        if (V3Equal(currentPosition, targetPosition1))
        {
            targetPosition = targetPosition2;

        }
        else if (V3Equal(currentPosition, targetPosition2))
        {
            targetPosition = targetPosition3;

        }
        else if (V3Equal(currentPosition, targetPosition3))
        {
            targetPosition = targetPosition4;

        }
        else if (V3Equal(currentPosition, targetPosition4))
        {
            targetPosition = targetPosition1;

        }
        MoveTowardsTarget();
    }
    void MoveTowardsTarget()
    {
        //the speed, in units per second, we want to move towards the target
        float speed = 2;
        //move towards the center of the world (or where ever you like)
        currentPosition = this.transform.position;
        //if(Vector3.Distance(currentPosition, targetPosition) > .1f) { 
        Vector3 directionOfTravel = targetPosition - currentPosition;
        //now normalize the direction, since we only want the direction information
        directionOfTravel.Normalize();
        //scale the movement on each axis by the directionOfTravel vector components

        this.transform.Translate(
            (directionOfTravel.x * speed * Time.deltaTime),
            (directionOfTravel.y * speed * Time.deltaTime),
            (directionOfTravel.z * speed * Time.deltaTime),
            Space.World);
    }
}
