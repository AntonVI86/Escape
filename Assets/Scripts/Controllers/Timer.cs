using UnityEngine;

public class Timer
{
    private float _defaultTimeToChangeController = 3f;
    private float _timeToChangeController;

    public Timer()
    {
        ResetTimeToChange();
    }

    public void ResetTimeToChange() => _timeToChangeController = _defaultTimeToChangeController;

    public bool IsTimeOut()
    {
        if(_timeToChangeController > 0)
        {
            _timeToChangeController -= Time.deltaTime;
        }

        if (_timeToChangeController <= 0)
            return true;

        return false;
    }
}
