using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour

{
    [SerializeField] float _cycleTimer;
    
    Image _icon;
    float timer;
    [SerializeField]Sprite _blank;
    
    [SerializeField]List<BasePowerUp> _pUps;
    int _selectedPower;


    // Start is called before the first frame update
    void Start()
    {
        _icon = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > _cycleTimer)
        {
            timer = 0;
            _selectedPower = (_selectedPower + Random.Range(1,3)) % 3;
            if (_icon != null && _pUps.Count >= _selectedPower+1)
            {
                _icon.sprite = _pUps[_selectedPower].icon;
            }
            else
            {
                _icon.sprite = null;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Marble>(out Marble m))
        {
            if (_pUps.Count >= _selectedPower + 1)
            {
                _pUps[_selectedPower].Activate(m);
            }
        }
        _selectedPower = 3;
        _icon.sprite = _blank; 
        timer = 0;
    }
}
