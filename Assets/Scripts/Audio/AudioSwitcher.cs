using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSwitcher : MonoBehaviour
{
    private AudioHander _audioHandler;

    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private Button _sound;
    [SerializeField] private Button _music;

    private bool _isMusicOn = true;
    private bool _isSoundOn = true;

    private void Awake()
    {
        _audioHandler = new AudioHander(_audioMixer);
    }

    public bool IsMusicOn => _isMusicOn == true;
    public bool IsSoundOn => _isSoundOn == true;

    public void OnOffMusic()
    {
        _isMusicOn = !_isMusicOn;

        if (_isMusicOn)
            _audioHandler.OnMusic();
        else
            _audioHandler.OffMusic();
    }

    public void OnOffSound()
    {
        _isSoundOn = !_isSoundOn;

        if (_isSoundOn)
            _audioHandler.OnSound();
        else
            _audioHandler.OffSound();
    }
}
