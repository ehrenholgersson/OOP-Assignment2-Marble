using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour // Object for Cinemachine camera to follow
{
    // Start is called before the first frame update
    void Start()
    {
        GameController.OnRaceStart += AttachCamera;
    }

    // Update is called once per frame
    void AttachCamera()
    {
        if (GameController.PlayerMarble != null)
        {
            transform.position = GameController.PlayerMarble.transform.position;
            transform.SetParent(GameController.PlayerMarble.transform, true);
        }
    }
    private void OnDestroy()
    {
        GameController.OnRaceStart -= AttachCamera;
    }
}
