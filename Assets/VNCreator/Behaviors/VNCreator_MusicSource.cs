using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNCreator
{
    public class VNCreator_MusicSource : MonoBehaviour
    {
        public static VNCreator_MusicSource Instance;

        [SerializeField]
        private MusicLibrary musicLibrary;
        [SerializeField]
        private AudioSource musicSource;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void PlayMusic(string trackName, float fadeDuration = 0.5f)
        {
            StartCoroutine(AnimateMusicCrossFade(musicLibrary.GetClipFromName(trackName), fadeDuration));
        }

        public void PlayMusic(AudioClip track, float fadeDuration = 0.5f)
        {
            StartCoroutine(AnimateMusicCrossFade(musicLibrary.GetClipFromFile(track), fadeDuration));
        }

        IEnumerator AnimateMusicCrossFade(AudioClip nextTrack, float fadeDuration = 0.5f)
        {
            float percent = 0;
            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeDuration;
                musicSource.volume = Mathf.Lerp(1f, 0, percent);
                yield return null;
            }

            musicSource.clip = nextTrack;
            musicSource.Play();

            percent = 0;
            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeDuration;
                musicSource.volume = Mathf.Lerp(0, 1f, percent);
                yield return null;
            }
        }
    }

    

    /*
    [RequireComponent(typeof(AudioSource))]
    public class VNCreator_MusicSource : MonoBehaviour
    {
        AudioSource source;

        public static VNCreator_MusicSource instance;

        private void Awake()
        {
            instance = this;
            source = GetComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = true;
            source.volume = GameOptions.musicVolume;
        }

        public void Play(AudioClip clip)
        {
            source.clip = clip;
            source.Play();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    */
}
