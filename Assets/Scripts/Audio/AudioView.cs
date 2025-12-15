using UnityEngine;
using UnityEngine.UI;

public class AudioView : MonoBehaviour
{
    [SerializeField] private Sprite _musicOff;
    [SerializeField] private Sprite _musicOn;

    [SerializeField] private Sprite _soundOff;
    [SerializeField] private Sprite _soundOn;

    [SerializeField] private AudioSwitcher _audioPlayer;

    [SerializeField] private Image _musicView;
    [SerializeField] private Image _soundView;


    private void Update()
    {
        if (_audioPlayer.IsMusicOn)
            _musicView.sprite = _musicOn;
        else
            _musicView.sprite = _musicOff;

        if (_audioPlayer.IsSoundOn)
            _soundView.sprite = _soundOn;
        else
            _soundView.sprite = _soundOff;
    }
}
