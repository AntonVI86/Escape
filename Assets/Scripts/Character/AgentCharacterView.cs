using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int IsJumpingKey = Animator.StringToHash("IsJumping");
    private readonly int HitKey = Animator.StringToHash("Hit");
    private readonly int DieKey = Animator.StringToHash("Die");

    private float _minValueToMoveAnimation = 0.05f;
    private int _injureLayerIndex = 1;
    private float _injureLayerWeight = 1;

    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private PointToMoveDisplayer _pointDisplayer;
    [SerializeField] private AudioClip _hitSfx;

    private void OnEnable()
    {
        _character.Hited += OnHited;
        _character.Died += OnDied;
        _character.Healed += OnHealed;
    }

    private void Update()
    {
        _animator.SetBool(IsJumpingKey, _character.InJumpProcess);

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

    private void OnHited()
    {
        _animator.SetTrigger(HitKey);
        AudioPlayer.Instance.PlaySound(_hitSfx);

        if (_character.IsInjured == true)
        {
            _animator.SetLayerWeight(_injureLayerIndex, _injureLayerWeight);
        }
    }

    private void OnDied()
    {
        _animator.SetTrigger(DieKey);
    }

    private void OnHealed()
    {
        _animator.SetLayerWeight(_injureLayerIndex, 0);
        _character.Vfx.Play();
    }

    private void OnDisable()
    {
        _character.Hited -= OnHited;
        _character.Died -= OnDied;
        _character.Healed -= OnHealed;
    }
}
