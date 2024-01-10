using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioBox", menuName = "ScriptableObjects/CreateAudioBox", order = 1)]
public class AudioBox : ScriptableObject
{
    [Serializable]
    public struct AudioParameters
    {
        public string Name;
        public AudioClip[] Clip;
        public float Volume;
        public float Pitch;
        public bool Loop;
        //public float StartDelay;

    }

    public AudioParameters  Audio;
    private AudioSource _audioSource;
    private GameObject _audioObject;

    public void PlayAudio()
    {
        if(_audioObject == null)
        {
            _audioObject = new GameObject(Audio.Name);
            _audioSource = _audioObject.AddComponent<AudioSource>();
        }
        _audioSource.clip = Audio.Clip[UnityEngine.Random.Range(0, Audio.Clip.Length)];
        _audioSource.volume = Audio.Volume;
        _audioSource.pitch = Audio.Pitch;
        _audioSource.loop = Audio.Loop;
        //_audioSource.PlayDelayed(Audio.StartDelay);
        if(Audio.Loop)
            _audioSource.Play();
        else
            _audioSource.PlayOneShot(_audioSource.clip);
    }

    public void StopAudio()
    {
        if(_audioSource != null && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}
