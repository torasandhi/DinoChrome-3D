using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacles : MonoBehaviour
{
    [SerializeField] private List<GameObject> staticObstacles = new List<GameObject>();


    private void OnEnable()
    {
        SpawnObstacleObjects();
    }

    private void OnDisable()
    {
        ResetObstacleObjects();
    }

    private void SpawnObstacleObjects()
    {
        List<int[]> patterns = new List<int[]>
    {
        new int[] { },
        new int[] { 0 },           // left
        new int[] { 1 },           // middle
        new int[] { 2 },           // right
        new int[] { 0, 1 },        // left + middle
        new int[] { 0, 2 },        // left + right
        new int[] { 1, 2 }         // middle + right
    };

        //Disable all obstacles first
        foreach (GameObject obj in staticObstacles)
        {
            obj.SetActive(false);
        }

        int[] chosenPattern = patterns[Random.Range(0, patterns.Count)];

        foreach (int laneIndex in chosenPattern)
        {
            if (laneIndex >= 0 && laneIndex < staticObstacles.Count)
            {
                staticObstacles[laneIndex].SetActive(true);
            }
        }
    }

    private void ResetObstacleObjects()
    {
        foreach (GameObject obj in staticObstacles)
        {
            obj.SetActive(false);
        }
    }
}
