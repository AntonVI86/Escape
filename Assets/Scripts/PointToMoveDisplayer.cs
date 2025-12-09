using UnityEngine;

public class PointToMoveDisplayer : MonoBehaviour
{
    [SerializeField] private InputPlayer _input;
    [SerializeField] private GameObject _flag;

    private ITransformPosition _positionSender => _input.AgentController as ITransformPosition;
    public void Show()
    {
        if(_input.AgentController is ITransformPosition)
        {
            _flag.transform.position = _positionSender.Position;
            _flag.SetActive(true);
        }
    }

    public void Hide()
    {
        _flag.SetActive(false);
    }
}
