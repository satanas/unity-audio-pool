using UnityEngine;

/*
 * This class creates a pool of audio sources to play multiple audio clips simultaneously
 */
public class AudioPool : MonoBehaviour {
    private const string ERROR_MESSAGE = "Sound dropped, no free channels available. Consider increasing the number of channels";
    
    public int channels;

    private AudioSource[] sources;

    void Start() {
        sources = new AudioSource[channels];
        for (int i = 0; i < sources.Length; i++) {
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].playOnAwake = false;
            sources[i].loop = false;
        }
    }

    public void PlayOneShot(AudioClip sound, float volume) {
        AudioSource audioSrc = GetFreeChannel();
        if (audioSrc != null) {
            audioSrc.PlayOneShot(sound, volume);
        } else {
            Debug.Log(ERROR_MESSAGE);
        }
    }

    public void PlayDelayed(AudioClip sound, float volume, float delay) {
        AudioSource audioSrc = GetFreeChannel();
        if (audioSrc != null) {
            audioSrc.clip = sound;
            audioSrc.volume = volume;
            audioSrc.PlayDelayed(delay);
        } else {
            Debug.Log(ERROR_MESSAGE);
        }
    }

    AudioSource GetFreeChannel() {
        AudioSource rtn = null;

        for (int i = 0; i < sources.Length; i++) {
            if (!sources[i].isPlaying) {
                rtn = sources[i];
                break;
            }
        }

        return rtn;
    }
}
