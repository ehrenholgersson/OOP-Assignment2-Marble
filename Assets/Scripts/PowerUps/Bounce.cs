using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Bounce", menuName = "Ehren/PowerUps/Bounce")]

public class Bounce : BasePowerUp
{
    [SerializeField] PhysicMaterial _bouncyMat;

    public override void Activate(Marble target)
    {
        Debug.Log("Bounce " + target.gameObject.name);
        if (target.TryGetComponent<Collider>(out Collider c))
        {
            RemoveBounce(c, 5, c.material);
            c.material = _bouncyMat;
        }
        if (target == GameController.PlayerMarble)
        {
            UIText.DisplayText("BOUNCY!");
        }
    }

    async void RemoveBounce(Collider target, float effectTime, PhysicMaterial oldMat)
    {
        float timer = Time.time;
        while (Time.time < timer + effectTime)
        {
            await Task.Delay(500);
        }
        Debug.Log(target.gameObject.name + " has been de-bounced");
        target.material = oldMat;
    }
    

}
