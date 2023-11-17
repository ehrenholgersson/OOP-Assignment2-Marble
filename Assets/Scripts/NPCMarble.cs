using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMarble : MonoBehaviour
{
    MeshRenderer _mr;
    Rigidbody _rb;
    Collider _col;
    Material _material;
    [SerializeField] Vector3 _minVelocity;
    [SerializeField] Vector3 _maxVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
        {
            _mr = GetComponentInChildren<MeshRenderer>();
            _rb = GetComponent<Rigidbody>();
            _col = GetComponent<Collider>();
            //_material = _mr.material;
            if (GameController.Instance.NPCColors.Count > 0)
            {
                _mr.material.color = GameController.Instance.NPCColors[Random.Range(0, GameController.Instance.NPCColors.Count)];
            }
            GameController.OnRaceStart += Setup;
            _mr.enabled = false;
            _col.enabled = false;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void Setup()
    {
        if (GameController.PlayerMarble == null || Vector3.Distance(GameController.PlayerMarble.transform.position, transform.position) > 1.2f)
        {
            _mr.enabled = true;
            _col.enabled = true;
            _rb.constraints = RigidbodyConstraints.None;
            Vector3 startForce = Vector3.Lerp(_minVelocity, _maxVelocity, Random.Range(0f, 1f));
            _rb.AddForce(startForce*200);
            Debug.Log("Added velocity: " + startForce);
        }
    }

    private void OnDestroy()
    {
        GameController.OnRaceStart -= Setup;
    }
}
