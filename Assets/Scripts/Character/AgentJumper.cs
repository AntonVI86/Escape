using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentJumper
{
    private float _speed;
    private NavMeshAgent _agent;

    private AnimationCurve _yOffsetCurve;

    private Coroutine _jumpProcess;

    private MonoBehaviour _coroutineRunner;

    public AgentJumper(float speed, NavMeshAgent agent, MonoBehaviour coroutineRunner, AnimationCurve yOffsetCurve)
    {
        _speed = speed;
        _agent = agent;
        _coroutineRunner = coroutineRunner;
        _yOffsetCurve = yOffsetCurve;
    }

    public void Jump(OffMeshLinkData offMeshLinkData)
    {
        if (InProcess())
            return;

        _jumpProcess = _coroutineRunner.StartCoroutine(JumpProcess(offMeshLinkData));
    }

    public bool InProcess() => _jumpProcess != null;

    private IEnumerator JumpProcess(OffMeshLinkData offMeshLinkData)
    {
        Vector3 startPos = offMeshLinkData.startPos;
        Vector3 endPos = offMeshLinkData.endPos;

        float duration = Vector3.Distance(startPos, endPos) / _speed;

        float progress = 0;

        while (progress < duration)
        {
            float yOffset = _yOffsetCurve.Evaluate(progress/duration);

            _agent.transform.position = Vector3.Lerp(startPos, endPos, progress / duration) + Vector3.up * yOffset;
            progress += Time.deltaTime;

            yield return null;
        }

        _agent.CompleteOffMeshLink();
        _jumpProcess = null;
    }
}
