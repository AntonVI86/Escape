using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDamageable
{
    private NavMeshAgent _agent;

    public event Action HealthValueChanged;
    public event Action Died;

    private AgentMover _mover;
    private DirectionalRotator _rotator;

    private float _currentHealth;

    private float _healthToInjured = 30;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _maxHealth;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Vector3 CurrentPosition => transform.position;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public bool IsInjured => (_currentHealth / _maxHealth) * 100 <= _healthToInjured;

    public bool IsAlive => _currentHealth > 0;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;

        _currentHealth = _maxHealth;

        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
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

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        _currentHealth -= damage;
        SetDestination(transform.position);
        HealthValueChanged?.Invoke();

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
}
