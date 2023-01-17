using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndSlide : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private float _timer = 2.0f;
    private float _interpolationRate = 0;

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
        float current = 0;
        float lerpValue = 0;

        final[0] = gameObject;

        for (int i = 0; i < waypoints.Length; i++) {
            final[i+1] = waypoints[i];
        }

        // sort the pool of game objects in order of proximity to our object (the smaller the X axis, the higher smaller the index)
        System.Array.Sort(final, (a, b) => (b.transform.position.x.CompareTo(a.transform.position.x)));

        _interpolationRate += Time.deltaTime;

        while (counter < nbWaypoints)
        {
            if (final[counter].transform)
            {
                Transform currentWaypoint = final[counter].transform;

                lerpValue = Mathf.InverseLerp(0, _timer, current);

                currentCube.transform.position = Vector3.Lerp(currentCube.transform.position, currentWaypoint.position, lerpValue);
                counter++;
                current += Time.deltaTime;

                yield return new WaitForSeconds(_timer);
            }
        }

        if (counter == nbWaypoints)
        {
            Destroy(currentCube);
        }
    }
}
