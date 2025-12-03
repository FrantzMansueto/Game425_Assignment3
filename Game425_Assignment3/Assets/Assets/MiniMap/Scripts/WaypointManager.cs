using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public BoxCollider playerCollider;
    public Waypoint[] waypoints;
    public AudioClip waypointSound;
    public AudioClip finishSound;
    private AudioSource audioSource;
    private float raceStartTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        foreach (Waypoint wp in waypoints)
        {
            if (wp.CheckTrigger(playerCollider))
            {
                if (wp.waypointName == "Finish")
                { 
                    // Sound Effect by freesound_community from Pixabay
                    audioSource.PlayOneShot(finishSound);
                    float elapsed = Time.time - raceStartTime;
                    Debug.Log("Race Completed!");
                }
                else
                {
                    // Sound Effect by DRAGON-STUDIO from Pixabay
                    audioSource.PlayOneShot(waypointSound);
                    Debug.Log("Waypoint reached: " + wp.waypointName);
                }
            }
        }
    }
}
