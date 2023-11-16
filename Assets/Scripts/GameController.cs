using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum State {Setup, Race, Finish }

public class GameController : MonoBehaviour
{
    public static List<Marble> Marbles { get; private set; } = new List<Marble>();

    public static Marble PlayerMarble;
    public static float RaceTime { get; private set; } = -10;

    public static State GameState = State.Setup;
    public List<Color> NPCColors;

    //[SerializeField] GameObject _arrowPrefab;
    [SerializeField] TextMeshProUGUI _guiTimer;
    //GameObject _arrowOld;
    [SerializeField] Image _arrow;
    [SerializeField] GameObject _marblePrefab;
    Vector3 _velocityDirection;
    Vector2 _canvasSize = new Vector2(1920, 1080);
    public static GameController Instance { get; private set; }
    bool _spawning = false;

    Camera cam;

    public static Action OnRaceStart;



    public static int Join(Marble newMarble)
    {
        Marbles.Add(newMarble);
        return Marbles.IndexOf(newMarble);
    }

    void StartRace()
    {
        //spawn non-player marbles
        if (PlayerMarble != null)
        {
            Rigidbody rB = PlayerMarble.gameObject.GetComponent<Rigidbody>();
            rB.isKinematic = false;
            rB.AddForce(-_velocityDirection*200);
        }
        _arrow.gameObject.SetActive(false);
        OnRaceStart?.Invoke();
    }

    public static void RemoveMarble(Marble m)
    {
        if (GameController.Marbles.Contains(m))
            GameController.Marbles.Remove(m);
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        cam = Camera.main;
    }

    private void Update()
    {
        RaceTime += Time.deltaTime;
        if (GameState == State.Setup)
        {
            if (Input.GetMouseButtonDown(0) && !_spawning)
            {
                Debug.Log("Go!");
                StartCoroutine("SpawnMarble");
            }
            else if (Input.GetMouseButtonDown(0))
                Debug.Log("Broken");
            //if (_arrowOld!=null)
            //{
            //    _arrowOld.transform.localScale = new Vector3(1, _velocityDirection.magnitude, 1);
            //    _arrowOld.transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, new Vector2(_velocityDirection.x, _velocityDirection.y)));
            //    _arrowOld.transform.position = PlayerMarble.transform.position + (_velocityDirection / 2);
            //}
            if (!(RaceTime < 0))
            {
                StartRace();
                GameState = State.Race;
            }
        }
        if (PlayerMarble ==null || PlayerMarble.State != Marble.MarbleState.Finished)
        {
            _guiTimer.text = MathF.Abs(RaceTime - (RaceTime % 1)) + ":" + MathF.Abs((int)((RaceTime % 1) * 100));
            if (RaceTime < 0)
                _guiTimer.text = "-" + _guiTimer.text;
        }
    }

    private IEnumerator SpawnMarble()
    {
        Vector2 arrowStart;
        Vector2 arrowEnd;
        Debug.Log("spawn new Player Marble");
        _spawning = true;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Default");
        if (Physics.Raycast(ray, out hit, 200,mask))
        {
            Debug.Log("Ray Hit on spawn location");
            // spawn marble
            if (PlayerMarble != null)
            {
                Destroy(PlayerMarble.gameObject);
                //Destroy(_arrowOld);
            }
            GameObject gO = Instantiate(_marblePrefab);
            PlayerMarble = gO.GetComponent<Marble>();
            gO.transform.position = hit.point - new Vector3(0, 0, 0.5f); // pull back from impact point by width of marble
            //_arrowOld = Instantiate(_arrowPrefab);
            _arrow.gameObject.SetActive(true);
            arrowStart = cam.WorldToScreenPoint(PlayerMarble.transform.position);// * (Screen.currentResolution.width/_canvasSize.x);// don't think this would work outside of 16:9!!
            arrowEnd = arrowStart;

            while (Input.GetMouseButton(0))
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    _velocityDirection = hit.point - PlayerMarble.transform.position;
                    arrowEnd = Input.mousePosition;
                }
                _arrow.rectTransform.position = (arrowStart + arrowEnd) / 2;
                _arrow.rectTransform.localScale = new Vector3(_arrow.rectTransform.localScale.x, (arrowStart - arrowEnd).magnitude / _arrow.rectTransform.rect.height * (_canvasSize.x/ Screen.width), _arrow.rectTransform.localScale.z);
                _arrow.rectTransform.localRotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, (arrowStart - arrowEnd)));
                Debug.Log("update direction: " + _velocityDirection);
                //update velocity vector and draw arrow indicator
                yield return new WaitForEndOfFrame();
            }
        }
        _spawning = false;

        yield break;
    }
}
