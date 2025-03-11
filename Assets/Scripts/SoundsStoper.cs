using UnityEngine;

public class SoundsStoper : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _sounds;
    [SerializeField] private int maxSounds;

    private int activeSounds = 0;
    private int _lastPlayedIndex = 0;

    void Start()
    {
        foreach (AudioClip sound in _sounds)
        {
            Resources.Load<AudioClip>(sound.name);
        }
    }

    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            activeSounds = Mathf.Max(0, activeSounds - 1);
        }
    }

    public void PlaySound()
    {
        if (_audioSource.isPlaying && activeSounds <= maxSounds)
        {
            activeSounds++;
        }

        if (activeSounds <= maxSounds)
        {
            int RandomIndex;
            do
            {
                RandomIndex = Random.Range(0, _sounds.Length);
            } while (RandomIndex == _lastPlayedIndex);

            _lastPlayedIndex = RandomIndex;
            _audioSource.PlayOneShot(_sounds[RandomIndex]);
        }
    }
}

