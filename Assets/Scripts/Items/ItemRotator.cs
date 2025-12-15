using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    [SerializeField] private AudioClip _pickupVfx;

    private float _speedRotate = 10f;

    private float _time;
    private Vector3 _defaultPosition;

    private void Awake()
    {
        _defaultPosition = transform.position;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * _speedRotate * Time.deltaTime);

        Swing();

    }
    private void Swing()
    {
        float value = 5;

        _time += Time.deltaTime;

        transform.position = _defaultPosition + Vector3.up * Mathf.Sin(_time)/value;
    }
}
