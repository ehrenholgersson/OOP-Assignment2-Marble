using UnityEngine;

public class Marble : MonoBehaviour
{
    public int MarbleIndex { get; private set; }
    public MarbleState State;
    public enum MarbleState { Setup, Placed, Racing, Finished}

    private void Awake()
    {
        MarbleIndex = GameController.Join(this);
        State = MarbleState.Setup;
    }
    private void OnDestroy()
    {
        if (GameController.Marbles.Contains(this))
            GameController.RemoveMarble(this);
    }
}
