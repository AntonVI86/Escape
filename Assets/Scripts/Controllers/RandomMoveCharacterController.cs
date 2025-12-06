using UnityEngine;
using UnityEngine.AI;

public class RandomMoveCharacterController : Controller
{
    private AgentCharacter _character;

    private Vector3 _defaultPosition;
    private Vector3 _targetPoint;

    private float _minDistanceToTarget = 0.2f;
    private float _minDistanceToDefaultPoint = -5f;
    private float _maxDistanceToDefaultPoint = 5f;

    private NavMeshPath _pathToTarget = new NavMeshPath();

    public RandomMoveCharacterController(AgentCharacter character, Vector3 defaultPosition)
    {
        _character = character;
        _defaultPosition = defaultPosition;

        SetTargetPoint();
    }

    public override void UpdateLogic(float deltaTime)
    {
        if (_character.IsAlive == false)
            return;

        _character.SetRotationDirection(_character.CurrentVelocity);

        if (_character.TryGetPath(_targetPoint, _pathToTarget) == false)
        {
            SetTargetPoint();
        }

        float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

        if (IsTargetReached(distanceToTarget))
        {
            _character.StopMove();
            SetTargetPoint();
        }

        if (EnoughCornersToPath(_pathToTarget))
        {
            _character.ResumeMove();
            _character.SetDestination(_targetPoint);
            return;
        }

        _character.StopMove();
    }

    private void SetTargetPoint()
    {
        _targetPoint = new Vector3(_defaultPosition.x + Random.Range(_minDistanceToDefaultPoint, _maxDistanceToDefaultPoint),
            _defaultPosition.y, _defaultPosition.z + Random.Range(_minDistanceToDefaultPoint, _maxDistanceToDefaultPoint));
    }

    private bool IsTargetReached(float distanceToTarget) => distanceToTarget <= _minDistanceToTarget;
    private bool EnoughCornersToPath(NavMeshPath pathToTarget) => _pathToTarget.corners.Length > 1;
}
