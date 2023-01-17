using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnAndTeleport : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private float _timer = 2.0f;

    void OnMouseDown()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        GameObject currentCube = Instantiate(_object);
        int nbWaypoints = waypoints.Length;
        int counter = 0;

        // sort the pool of game objects in order of proximity to our object (the smaller the X axis, the higher smaller the index)
        System.Array.Sort(waypoints, (a, b) => (a.transform.position.x.CompareTo(b.transform.position.x)));

        while (counter < nbWaypoints)
        {
            Transform currentWaypoint = waypoints[counter].transform;

            currentCube.transform.position = currentWaypoint.position;
            currentCube.transform.rotation = Quaternion.identity;
            counter++;

            yield return new WaitForSeconds(_timer);
        }

        if (counter == nbWaypoints)
        {
            Destroy(currentCube);
        }
    }
}
