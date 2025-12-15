using UnityEngine;

public class Item : MonoBehaviour, IContainPickupSound
{
    [SerializeField] private AudioClip _pickUpSfx;
    public AudioClip Sfx => _pickUpSfx;
}
