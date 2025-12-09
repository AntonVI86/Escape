using UnityEngine;

public class PointToMoveDisplayer : MonoBehaviour
{
    [SerializeField] private InputPlayer _input;
    [SerializeField] private GameObject _flag;

    private AgentCharacterController _controller => _input.AgentController as AgentCharacterController;
    public void Show()
    {
        if(_input.AgentController is AgentCharacterController)
        {
            _flag.transform.position = _controller.Target;
            _flag.SetActive(true);
        }
    }

    public void Hide()
    {
        _flag.SetActive(false);
    }
}
