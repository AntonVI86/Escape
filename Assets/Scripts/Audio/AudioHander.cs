using UnityEngine;
using UnityEngine.Audio;

public class AudioHander
{
    private const float OffVolumeValue = -80;
    private const float OnVolumeValue = 0;

    private const string MusicKey = "MusicVolume";
    private const string SoundKey = "SoundVolume";

    private AudioMixer _audioMixer;

    private float _deadZone = 0.01f;

    public AudioHander(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
    }

    public bool IsMusicOn() => IsVolumeOn(MusicKey);
    public bool IsSoundOn() => IsVolumeOn(SoundKey);

    public void OffMusic() => OffVolume(MusicKey);
    public void OnMusic() => OnVolume(MusicKey);
    public void OffSound() => OffVolume(SoundKey);
    public void OnSound() => OnVolume(SoundKey);
    private bool IsVolumeOn(string key)
        => _audioMixer.GetFloat(key, out float volume) && Mathf.Abs(volume - OnVolumeValue) < _deadZone;

    private void OnVolume(string key) => _audioMixer.SetFloat(key, OnVolumeValue);
    private void OffVolume(string key) => _audioMixer.SetFloat(key, OffVolumeValue);
}
