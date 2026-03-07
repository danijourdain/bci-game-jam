using UnityEngine;
using System.Collections.Generic;
 
public class AudioManager : MonoBehaviour
{
    public enum SoundType
    {
        Laser,
        Saw,
        PlasmaBall,
        ElecticalCharge,
        Powerup,
        Shoot,
        Music_Menu,
        Music1,
        Music2,
        Music3,
        Music4,
        Music5,
        Music_Battle
        // Add more sound types as needed
    }
    public SoundType[] playing = new SoundType[5];

    [System.Serializable]
    public class Sound
    {
        public SoundType Type;
        public AudioClip Clip;
 
        [Range(0f, 1f)]
        public float Volume = 1f;
 
        [HideInInspector]
        public AudioSource Source;
    }
 
    //Singleton
    public static AudioManager Instance;
 
    //All sounds and their associated type - Set these in the inspector
    public Sound[] AllSounds;
 
    //Runtime collections
    private Dictionary<SoundType, Sound> _soundDictionary = new Dictionary<SoundType, Sound>();
    private AudioSource _musicSource;
 
    private void Awake()
    {
        //Assign singleton
        Instance = this;
 
        //Set up sounds
        foreach(var s in AllSounds)
        {
            _soundDictionary[s.Type] = s;
        }
    }
 
 
 
    //Call this method to play a sound
    public void Play(SoundType type, float playbackSpeed = 1f)
    {
        //Make sure there's a sound assigned to your specified type
        if (!_soundDictionary.TryGetValue(type, out Sound s))
        {
            Debug.LogWarning($"Sound type {type} not found!");
            return;
        }
 
        //Creates a new sound object
        var soundObj = new GameObject($"Sound_{type}");
        var audioSrc = soundObj.AddComponent<AudioSource>();
 
        //Assigns your sound properties
        audioSrc.clip = s.Clip;
        audioSrc.volume = s.Volume;
        audioSrc.pitch = playbackSpeed;
 
        //Play the sound
        audioSrc.Play();
 
        //Destroy the object
        Destroy(soundObj, s.Clip.length/playbackSpeed);

        // add sound to currently playing list
        for (int i = 0; i < playing.Length; i++)
        {
            if (playing[i] == type)
            {
                break;
            }
            if (playing[i] == 0)
            {
                playing[i] = type;
                break;
            }
        }
    }

    //is playing function
    public bool IsPlaying(SoundType type)
    {
        for (int i = 0; i < playing.Length; i++)
        {
            if (playing[i] == type)
            {
                return true;
            }
        }
        return false;
    }
 
    //Call this method to change music tracks
    public void ChangeMusic(SoundType type)
    {
        if (!_soundDictionary.TryGetValue(type, out Sound track))
        {
            Debug.LogWarning($"Music track {type} not found!");
            return;
        }
 
        if (_musicSource == null)
        {
            var container = new GameObject("SoundTrackObj");
            _musicSource = container.AddComponent<AudioSource>();
            _musicSource.loop = true;
        }
 
        _musicSource.clip = track.Clip;
        _musicSource.volume = track.Volume;
        _musicSource.Play();
    }
}