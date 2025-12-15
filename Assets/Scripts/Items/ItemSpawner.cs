using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private float _radiusOfSpawn = 2f;
    private float _timeToSpawn = 3f;

    private float _maxAngle = 360;

    private Coroutine _spawn;
    private bool _isEnable;

    [SerializeField] private HealthPotion _itemPrefab;
    [SerializeField] private Transform _transform;

    public bool IsEnable => _isEnable;

    public void Toggle()
    {
        _isEnable = !_isEnable;

        if (_isEnable)
            Launch();
        else
            Stop();
    }

    private void Launch()
    {
        if (_spawn != null)
            StopCoroutine(_spawn);

        _spawn = StartCoroutine(SpawnProcess());
    }
    
    private void Stop() => StopCoroutine(_spawn);

    private Vector3 GetPointToSpawn()
    {
        float randomAngle = Random.Range(0, _maxAngle);

        Vector3 spawnPosition = new Vector3(
            _transform.position.x + _radiusOfSpawn * Mathf.Cos(randomAngle * Mathf.Rad2Deg), 
            _transform.position.y, 
            _transform.position.z + _radiusOfSpawn * Mathf.Sin(randomAngle * Mathf.Rad2Deg));

        return spawnPosition;
    }

    private IEnumerator SpawnProcess()
    {
        float time = 0;

        while (true)
        {
            time += Time.deltaTime;

            if (time >= _timeToSpawn)
            {
                Instantiate(_itemPrefab, GetPointToSpawn(), Quaternion.identity);
                time = 0;
            }

            yield return null;
        }
    }
}