using UnityEngine;
using System.Collections;

public class CatmullRomSpline : MonoBehaviour
{
    public float NPCSpeed = 5.0f;
    private float rotationSmoothness = 10.0f;
    public Transform[] waypoints;
    private float currentPathTime = 0.0f;
    private int numberOfWaypoints;
    private Vector3[] points;

    void Start()
    {
        //Starts going from point through the other in order by array/ waypoints
        numberOfWaypoints = waypoints.Length;
        points = new Vector3[numberOfWaypoints];
        for (int i = 0; i < numberOfWaypoints; i++)
        {
            points[i] = waypoints[i].position;
        }
        transform.position = GetTrackPosition(0f);
    }

    void Update()
    {
        currentPathTime += NPCSpeed * Time.deltaTime;
        Vector3 currentPosition = GetTrackPosition(currentPathTime);
        Vector3 nextPosition = GetTrackPosition(currentPathTime + 0.01f);

        transform.position = currentPosition;
        Vector3 direction = nextPosition - currentPosition;

        //Handles the Rotation of car when turning
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
        }
    }

    private Vector3 GetSplinePoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float u)
    {
        //Uses Catmull Rom Calculations to determine route of car
        u = Mathf.Clamp01(u);
        float u2 = u * u;
        float u3 = u2 * u;
        Vector3 result = 0.5f * (
            (2.0f * p1) +
            (-p0 + p2) * u +
            (2.0f * p0 - 5.0f * p1 + 4.0f * p2 - p3) * u2 +
            (-p0 + 3.0f * p1 - 3.0f * p2 + p3) * u3
        );

        return result;
    }

    private Vector3 GetTrackPosition(float time)
    {
        //Uses Catmull Rom Calculations to determine route of car
        int i = Mathf.FloorToInt(time);
        float u = time - i;
        int p1Index = (i % numberOfWaypoints);
        int p2Index = ((i + 1) % numberOfWaypoints);

        int p0Index = (i - 1 + numberOfWaypoints) % numberOfWaypoints;
        int p3Index = ((i + 2) % numberOfWaypoints);

        Vector3 p0 = points[p0Index];
        Vector3 p1 = points[p1Index];
        Vector3 p2 = points[p2Index];
        Vector3 p3 = points[p3Index];


        return GetSplinePoint(p0, p1, p2, p3, u);
    }
}