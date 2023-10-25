using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {Setup, Race, Finish }

public class GameController : MonoBehaviour
{
    public static List<Marble> Marbles { get; private set; } = new List<Marble>();

    public static Marble PlayerMarble;
    public static float RaceStartTime { get; private set; } = -10;

    public static State GameState = State.Setup;

    [SerializeField] GameObject _arrowPrefab;
    GameObject _arrow;
    [SerializeField] GameObject _marblePrefab;
    Vector3 _velocityDirection;
    public static GameController Instance { get; private set; }
    bool _spawning = false;

    Camera cam;

    public static int Join(Marble newMarble)
    {
        Marbles.Add(newMarble);
        if (RaceStartTime == 0) // need to trigger this on start of race but for now this works
            RaceStartTime = Time.time;
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
    }

    public static void RemoveMarble(Marble m)
    {
        if (GameController.Marbles.Contains(m))
            GameController.Marbles.Remove(m);
    }

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        cam = Camera.main;
    }

    private void Update()
    {
        RaceStartTime += Time.deltaTime;
        if (GameState == State.Setup)
        {
            if (Input.GetMouseButtonDown(0) && !_spawning)
            {
                Debug.Log("Go!");
                StartCoroutine("SpawnMarble");
            }
            else if (Input.GetMouseButtonDown(0))
                Debug.Log("Broken");
            if (_arrow!=null)
            {
                _arrow.transform.localScale = new Vector3(1, _velocityDirection.magnitude, 1);
                _arrow.transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, new Vector2(_velocityDirection.x, _velocityDirection.y)));
                _arrow.transform.position = PlayerMarble.transform.position + (_velocityDirection / 2);
            }
            if (!(RaceStartTime < 0))
            {
                StartRace();
                GameState = State.Race;
            }
        }
    }

    private IEnumerator SpawnMarble()
    {
        Debug.Log("spawn new Player Marble");
        _spawning = true;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask mask = ~0;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Ray Hit on spawn location");
            // spawn marble
            if (PlayerMarble != null)
            {
                Destroy(PlayerMarble.gameObject);
                Destroy(_arrow);
            }
            GameObject gO = Instantiate(_marblePrefab);
            PlayerMarble = gO.GetComponent<Marble>();
            gO.transform.position = hit.point - new Vector3(0, 0, 0.5f); // pull back from impact point by width of marble
            _arrow = Instantiate(_arrowPrefab);

            while (Input.GetMouseButton(0))
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                    _velocityDirection = hit.point - PlayerMarble.transform.position ;
                Debug.Log("update direction: " + _velocityDirection);
                //update velocity vector and draw arrow indicator
                yield return new WaitForEndOfFrame();
            }
        }
        _spawning = false;

        yield break;
    }
}
