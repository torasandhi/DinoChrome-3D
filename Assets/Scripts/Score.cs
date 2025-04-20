using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int value;

    private void Update()
    {
        transform.Rotate(0f, 90f * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Score Added");
            this.gameObject.SetActive(false);
        }
    }
}
