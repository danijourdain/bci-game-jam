using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chooseTrack();
    }
    private float musicTimer = 0f; // timer to keep track of when to change the music track
    private float musicChangeInterval = 180f; // interval in seconds to change the music track
    // Update is called once per frame
    void Update()
    {
        musicTimer += Time.deltaTime;
        if (musicTimer >= musicChangeInterval)
        {
            chooseTrack();
            musicTimer = 0f;
        }
    }

    void chooseTrack()
    {
        // choose a track to play randomly from the list of tracks in the AudioManager
        int trackIndex = Random.Range(0, 3); // assuming there are 2 music tracks in the AudioManager
        switch (trackIndex)
        {
            case 0:
                AudioManager.Instance.ChangeMusic(AudioManager.SoundType.Music1);
                break;
            case 1:
                AudioManager.Instance.ChangeMusic(AudioManager.SoundType.Music2);
                break;
            case 2:
                AudioManager.Instance.ChangeMusic(AudioManager.SoundType.Music3);
                break;
            case 3:
                AudioManager.Instance.ChangeMusic(AudioManager.SoundType.Music4);
                break;
            case 4:
                AudioManager.Instance.ChangeMusic(AudioManager.SoundType.Music5);
                break;
            default:
                break;
        }
    }
}
