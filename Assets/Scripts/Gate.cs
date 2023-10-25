using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public float[] MarbleTimes { get; private set; }

    private void Start() // thios will ahve to be on enable or called some other way
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
