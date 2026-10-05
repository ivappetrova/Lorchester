using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip defaultClip;

        private void Awake()
        {
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
        }

        // For UnityEvents: plays the default clip.
        public void PlaySound()
        {
            Play(defaultClip);
        }

        // For scripts: plays any clip you hand it.
        public void Play(AudioClip clip)
        {
            if (audioSource != null && clip != null)
                audioSource.PlayOneShot(clip);
        }
    }
}