using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePowerUp : ScriptableObject
{
    public Sprite icon;

    public abstract void Activate(Marble target);

}
