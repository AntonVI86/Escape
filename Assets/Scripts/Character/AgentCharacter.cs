using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDamageable, IHealable
{
    private NavMeshAgent _agent;

    private AgentJumper _jumper;

    public event Action Hited;
    public event Action Healed;
    public event Action Died;

    private AgentMover _mover;
    private DirectionalRotator _rotator;

    private float _currentHealth;

    private float _healthToInjured = 30;

    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _jumpSpeed;

    [SerializeField] private float _maxHealth;
    [SerializeField] private ParticleSystem _vfx;
    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Vector3 CurrentPosition => transform.position;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public bool IsInjured => (_currentHealth / _maxHealth) * 100 <= _healthToInjured;

    public bool IsAlive => _currentHealth > 0;
    public bool InJumpProcess => _jumper.InProcess();

    public ParticleSystem Vfx => _vfx;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;

        _currentHealth = _maxHealth;

        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
        _jumper = new AgentJumper(_jumpSpeed, _agent, this, _curve);
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
    }

    public void SetDestination(Vector3 position) => _mover.SetDestination(position);
    public void StopMove() => _mover.Stop();
    public void ResumeMove() => _mover.Resume();
    public void SetIndexedSpeed(float index) => _agent.speed = _moveSpeed * index;
    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);
    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget)
        => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);

    public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
    {
        if (_agent.isOnOffMeshLink)
        {
            offMeshLinkData = _agent.currentOffMeshLinkData;
            return true;
        }

        offMeshLinkData = default(OffMeshLinkData);
        return false;
    }

    public void Jump(OffMeshLinkData offMeshLinkData) => _jumper.Jump(offMeshLinkData);

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        _currentHealth -= damage;
        SetDestination(transform.position);
        Hited?.Invoke();

        if(_currentHealth < 0)
        {
            _currentHealth = 0;
            SetDestination(transform.position);
            
            Died?.Invoke();
        }

        ChangeSpeed();
    }

    private void ChangeSpeed()
    {
        SetIndexedSpeed(IndexOfSpeed.Hitted);

        if (_currentHealth <= _healthToInjured)
            SetIndexedSpeed(IndexOfSpeed.Injured);
    }

    public void Heal(float value)
    {
        if (value < 0)
            return;

        _currentHealth += value;
        Healed?.Invoke();

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }
}
