using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> scoreObjects = new List<GameObject>();
    [SerializeField] private List<Mesh> scoreType = new List<Mesh>();


    private void OnEnable()
    {
        RandomizeScoreType();
        SpawnScoreObjects();
    }

    private void SpawnScoreObjects()
    {
        List<int[]> patterns = GetAllCombinations(scoreObjects.Count);

        //Disable all obstacles first
        foreach (GameObject obj in scoreObjects)
        {
            obj.SetActive(false);
        }

        int[] chosenPattern = patterns[Random.Range(0, patterns.Count)];

        foreach (int laneIndex in chosenPattern)
        {
            if (laneIndex >= 0 && laneIndex < scoreObjects.Count)
            {
                scoreObjects[laneIndex].SetActive(true);
            }
        }
    }

    private void RandomizeScoreType()
    {
        foreach (GameObject obj in scoreObjects)
        {
            MeshFilter meshFilter = obj.GetComponentInChildren<MeshFilter>();
            meshFilter.mesh = scoreType[Random.Range(0, scoreType.Count)];
        }
    }

    List<int[]> GetAllCombinations(int count)
    {
        List<int[]> combinations = new List<int[]>();
        int total = 1 << count; // 2^count (bit shifting)

        for (int i = 0; i < total; i++)
        {
            List<int> combo = new List<int>();
            for (int bit = 0; bit < count; bit++)
            {
                if ((i & (1 << bit)) != 0)
                {
                    combo.Add(bit);
                }
            }
            combinations.Add(combo.ToArray());
        }

        return combinations;
    }
}
