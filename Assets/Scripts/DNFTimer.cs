using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DNFTimer 
{
    float _timer;
    Marble.MarbleState _dnfState;
    bool _continue = true;
    public DNFTimer(float time, Marble.MarbleState state) 
    {
        _timer = Time.time + time;
        _dnfState = state;
        TimeOut();

    }
    public void Stop()
    {
        _continue = false;
    }
    async void TimeOut()
    {
        while (Time.time < _timer)
        {
            if (!_continue)
            {
                return;
            }
            await Task.Delay(25);
        }
        if (GameController.PlayerMarble != null && GameController.PlayerMarble.State == _dnfState)
        {
            UIText.DisplayText("DNF!!!");
            GameController.Instance.DelayedMenu(5);
        }
    }

}
