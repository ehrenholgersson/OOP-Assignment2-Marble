using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMarble : MonoBehaviour
{
    MeshRenderer _mr;
    Rigidbody _rb;
    Collider _col;
    // Start is called before the first frame update
    void Start()
    {
        _mr = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
        GameController.OnRaceStart += Setup;
    }

    void Setup()
    {
        _mr.enabled = true;
        _col.enabled = true;
        _rb.constraints = RigidbodyConstraints.None;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
