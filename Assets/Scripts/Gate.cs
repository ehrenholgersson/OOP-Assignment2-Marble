using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public float[] MarbleTimes { get; private set; }

    private void OnEnable()
    {
         MarbleTimes = new float[GameController.Marbles.Count];
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Marble>(out Marble m)&& GameController.Marbles.Contains(m))
        {
            MarbleTimes[GameController.Marbles.IndexOf(m)] = Time.time - GameController.RaceStartTime;
        }
    }

}
