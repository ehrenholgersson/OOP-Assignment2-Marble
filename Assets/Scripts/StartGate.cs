using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGate : Gate
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.TryGetComponent<Marble>(out Marble m) && GameController.Marbles.Contains(m))
        {
            if (m.State == Marble.MarbleState.Setup)
            {
                m.State = Marble.MarbleState.Racing;
            }
        }

    }
}
