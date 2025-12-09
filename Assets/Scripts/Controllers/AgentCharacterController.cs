using UnityEngine;
using UnityEngine.AI;

public class AgentCharacterController : Controller, ITransformPosition
{
    public const int LeftMouseButton = 0;

    private AgentCharacter _character;
    private PointGetter _targetGetter;

    private Vector3 _target;

    private float _minDistanceToTarget;

    private NavMeshPath _pathToTarget = new NavMeshPath();

    public Vector3 Position => _target;

    public AgentCharacterController(AgentCharacter character, float minDistanceToTarget)
    {
        _character = character;
        _minDistanceToTarget = minDistanceToTarget;

        _targetGetter = new PointGetter();
    }

    public override void UpdateLogic(float deltaTime)
    {
        if (_character.IsAlive == false)
            return;

        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            _target = _targetGetter.GetPoint();
        }

        if (_target == Vector3.zero)
            return;

        _character.SetRotationDirection(_character.CurrentVelocity);

        if (_character.TryGetPath(_target, _pathToTarget))
        {
            float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

            if (IsTargetReached(distanceToTarget))
            {
                _character.StopMove();
            }

            if (EnoughCornersToPath(_pathToTarget))
            {
                _character.ResumeMove();
                _character.SetDestination(_target);
                return;
            }
        }

        _character.StopMove();
    }

    private bool IsTargetReached(float distanceToTarget) => distanceToTarget <= _minDistanceToTarget;
    private bool EnoughCornersToPath(NavMeshPath pathToTarget) => _pathToTarget.corners.Length > 1;
}
