using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIAudioSource : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("按钮点击声音")] public AudioClip clickAudioClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ClickSourcePlay()
    {
        audioSource.PlayOneShot(clickAudioClip);
    }
}
