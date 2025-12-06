using UnityEngine;

public class CharacterGetter : MonoBehaviour
{
    [SerializeField] private AgentCharacter _agent;

    public AgentCharacter Agent => _agent;
}
