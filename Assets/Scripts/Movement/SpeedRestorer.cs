using UnityEngine;

public class SpeedRestorer : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;

    public void RestoreVelocity()
    {
        if (_character.IsAlive)
        {
            if (_character.IsInjured)
            {
                _character.SetIndexedSpeed(IndexOfSpeed.Injured);
                return;
            }

            _character.SetIndexedSpeed(IndexOfSpeed.Normal);
        }
    }
}
