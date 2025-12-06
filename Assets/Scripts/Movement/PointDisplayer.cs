using UnityEngine;

public class PointDisplayer : IShowPosition
{
    private ParticleSystem _pointViewPrefab;

    public PointDisplayer(ParticleSystem pointViewPrefab)
    {
        _pointViewPrefab = pointViewPrefab;
    }

    public void ShowPoint(Vector3 position)
    {
        Object.Instantiate(_pointViewPrefab, position, Quaternion.identity);
    }
}
