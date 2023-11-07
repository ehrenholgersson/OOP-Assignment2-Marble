using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public float[] MarbleTimes { get; private set; }

    private void Start() // this will have to be on enable or called some other way
    {
        //MarbleTimes = new float[GameController.Marbles.Count];
        GameController.OnRaceStart += Setup;
    }

    public void Setup()
    {
        MarbleTimes = new float[GameController.Marbles.Count];
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Marble>(out Marble m)&& GameController.Marbles.Contains(m))
        {
            if (MarbleTimes[GameController.Marbles.IndexOf(m)] <=0 )
                MarbleTimes[GameController.Marbles.IndexOf(m)] = GameController.RaceTime;
        }
    }

}
