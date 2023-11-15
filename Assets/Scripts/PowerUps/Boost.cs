using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Boost", menuName = "Ehren/PowerUps/Boost")]
public class Boost : BasePowerUp
{
    public override void Activate(Marble target)
    {
        Debug.Log("Boost " + target.gameObject.name);
        if (target.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity *= 2;
        }
    }
}

