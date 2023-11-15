using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "POW", menuName = "Ehren/PowerUps/POW")]

public class Pow : BasePowerUp
{
    public override void Activate(Marble target)
    {
        Debug.Log("Pow " + target.gameObject.name);
    }
}
