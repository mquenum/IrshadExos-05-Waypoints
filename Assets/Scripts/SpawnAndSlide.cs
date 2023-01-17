using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnAndSlide : MonoBehaviour
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
        GameObject[] final = new GameObject[waypoints.Length + 1];
        GameObject currentCube = Instantiate(_object);
        int nbWaypoints = final.Length;
        int counter = 0;
        float elapsedTime = 0;

        final[0] = gameObject;

        for (int i = 0; i < waypoints.Length; i++)
        {
            final[i + 1] = waypoints[i];
        }

        final = SortWaypoints(final);

        while (elapsedTime < _timer)
        {
            currentCube.transform.position = Vector3.Lerp(final[counter].transform.position, final[counter + 1].transform.position, (elapsedTime / _timer));
            elapsedTime += Time.deltaTime;

            if (currentCube.transform.position == final[counter + 1].transform.position)
            {
                counter++;
            }

            yield return null;
        }

        if (counter + 1 == nbWaypoints)
        {
            Destroy(currentCube);
        }
    }

    private GameObject[] SortWaypoints(GameObject[] waypoints)
    {
        // sort the pool of game objects in order of proximity to our object (the smaller the X axis, the higher smaller the index)
        System.Array.Sort(waypoints, (a, b) => (b.transform.position.x.CompareTo(a.transform.position.x)));

        return waypoints;
    }
}
