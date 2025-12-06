using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] private AgentCharacter _agentCharacter;
    [SerializeField] private ParticleSystem _pointView;

    private Controller _agentController;

    private float _minDistanceToTarget = 0.05f;

    private Controller _randomMoveController;
    private Controller _playerController;

    private Timer _timer;

    private void Awake()
    {
        _randomMoveController = new RandomMoveCharacterController(_agentCharacter, _agentCharacter.CurrentPosition);
        _playerController = new AgentCharacterController(_agentCharacter, _minDistanceToTarget, _pointView);

        _agentController = _playerController;

        _agentController.Enable();

        _timer = new Timer();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _timer.ResetTimeToChange();
            ChangeController(_playerController);
        }

        if (_timer.IsTimeOut())
            ChangeController(_randomMoveController);

        _agentController.Update(Time.deltaTime);
    }

    private void ChangeController(Controller newController)
    {
        _agentController.Disable();
        _agentController = newController;
        _agentController.Enable();
    }
}
