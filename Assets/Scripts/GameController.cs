using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController
{
    public static List<Marble> Marbles { get; private set; } = new List<Marble>();
    public static float RaceStartTime { get; private set; } = 0;

    public static int Join(Marble newMarble)
    {
        Marbles.Add(newMarble);
        if (RaceStartTime == 0) // need to trigger this on start of race but for now this works
            RaceStartTime = Time.time;
        return Marbles.IndexOf(newMarble);

    }
}
