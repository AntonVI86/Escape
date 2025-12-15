using UnityEngine;
using UnityEngine.UI;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private ItemSpawner _spawner;

    [SerializeField] private Image _OnOffView;

    private void Update()
    {
        if (_spawner.IsEnable)
            _OnOffView.enabled = true;
        else
            _OnOffView.enabled = false;
    }
}
