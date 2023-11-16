using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "POW", menuName = "Ehren/PowerUps/POW")]

public class Pow : BasePowerUp
{
    const float _strength = 200;
    public override void Activate(Marble target)
    {
        Debug.Log("Pow " + target.gameObject.name);
        foreach (Marble m in GameController.Marbles)
        {
            float distance = Vector3.Distance(target.transform.position, m.transform.position);
            if (m != target && distance <= 5) 
            {
                if (m.gameObject.TryGetComponent<Rigidbody>(out Rigidbody marbleRb))
                {
                    marbleRb.AddForce((m.transform.position - target.transform.position).normalized * (6 - distance) * _strength);
                }
            }
        }
        if (target == GameController.PlayerMarble)
        {
            UIText.DisplayText("POW!");
        }
    }
}
