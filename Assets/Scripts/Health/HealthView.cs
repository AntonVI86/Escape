using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private AgentCharacter _agent;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TMP_Text _healthView;

    private void Update()
    {
        _healthBar.fillAmount = _agent.CurrentHealth / _agent.MaxHealth;
        _healthView.text = $"{_agent.CurrentHealth} / {_agent.MaxHealth}";
    }
}
