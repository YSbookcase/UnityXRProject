using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.IsVictory)
        {
            Debug.Log("Victory Triggered by Player");
            GameManager.Instance.Victory();
        }
    }
}
