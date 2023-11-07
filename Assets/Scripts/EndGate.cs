using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGate : Gate
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Marble>(out Marble m) && GameController.Marbles.Contains(m))
        {
            MarbleTimes[GameController.Marbles.IndexOf(m)] = GameController.RaceTime;
            m.State = Marble.MarbleState.Finished;
        }

    }
}
