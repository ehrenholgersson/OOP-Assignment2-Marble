using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndGate : Gate
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Marble>(out Marble m) && GameController.Marbles.Contains(m))
        {
            if (MarbleTimes[GameController.Marbles.IndexOf(m)]>0) 
            {
                return;
            }
            MarbleTimes[GameController.Marbles.IndexOf(m)] = GameController.RaceTime;
            m.State = Marble.MarbleState.Finished;
            if (m == GameController.PlayerMarble)
            {
                int position = 1;
                foreach (Marble mrPos in GameController.Marbles)
                {
                    if (MarbleTimes[GameController.Marbles.IndexOf(mrPos)] != 0 && MarbleTimes[GameController.Marbles.IndexOf(mrPos)] < MarbleTimes[GameController.Marbles.IndexOf(m)])
                    {
                        position++;
                    }
                }
                switch (position)
                {
                    case 1:
                        UIText.DisplayText("You came 1st!");
                        break;
                    case 2:
                        UIText.DisplayText("You came 2nd!");
                        break;
                    case 3:
                        UIText.DisplayText("You came 3rd!");
                        break;
                    default:
                        UIText.DisplayText("you came " + position + "th");
                        break;
                }
            }
        }

    }
}
