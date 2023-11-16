using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.TryGetComponent<Marble>(out Marble m) && collision.collider.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rB))
        {
            rB.velocity *= 1.4f;// increase velocity to bounce out of lane
        }
    }
}
