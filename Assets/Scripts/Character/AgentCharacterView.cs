using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int HitKey = Animator.StringToHash("Hit");
    private readonly int DieKey = Animator.StringToHash("Die");

    private float _minValueToMoveAnimation = 0.05f;
    private int _injureLayerIndex = 1;
    private float _injureLayerWeight = 1;

    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private PointToMoveDisplayer _pointDisplayer;

    private void OnEnable()
    {
        _character.HealthValueChanged += OnHealthValueChanged;
        _character.Died += OnDied;
    }

    private void Update()
    {
        if (_character.CurrentVelocity.magnitude >= _minValueToMoveAnimation)
            StartRunning();
        else
            StopRunning();
    }

    private void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
        _pointDisplayer.Show();
    }

    private void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);
        _pointDisplayer.Hide();
    }

    private void OnHealthValueChanged()
    {
        _animator.SetTrigger(HitKey);

        if (_character.IsInjured == true)
        {
            _animator.SetLayerWeight(_injureLayerIndex, _injureLayerWeight);
        }
    }

    private void OnDied()
    {
        _animator.SetTrigger(DieKey);
    }

    private void OnDisable()
    {
        _character.HealthValueChanged -= OnHealthValueChanged;
        _character.Died -= OnDied;
    }
}
