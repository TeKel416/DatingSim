using UnityEngine;

namespace VNCreator
{
    public class VNCreator_SfxSource : MonoBehaviour
    {
        public static VNCreator_SfxSource Instance;

        [SerializeField]
        private SoundLibrary sfxLibrary;
        [SerializeField]
        private AudioSource sfx2DSource;

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

        public void PlaySound3D(AudioClip clip, Vector3 pos)
        {
            if (clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, pos);
            }
        }

        public void PlaySound3D(string soundName, Vector3 pos)
        {
            PlaySound3D(sfxLibrary.GetClipFromName(soundName), pos);
        }

        public void PlaySound2D(string soundName)
        {
            sfx2DSource.PlayOneShot(sfxLibrary.GetClipFromName(soundName));
        }

        public void PlaySound2D(AudioClip clip)
        {
            sfx2DSource.PlayOneShot(clip);
        }

        public void PlaySoundOnLoop(AudioClip clip)
        {
            sfx2DSource.loop = true;
            sfx2DSource.clip = clip;
            sfx2DSource.Play();
        }

        public void PlaySoundOnLoop(string soundName)
        {
            sfx2DSource.loop = true;
            sfx2DSource.clip = sfxLibrary.GetClipFromName(soundName);
            sfx2DSource.Play();
        }

        public void StopLoop()
        {
            sfx2DSource.loop = false;
            sfx2DSource.clip = null;
        }

        /*AudioSource source;

        public static VNCreator_SfxSource instance;

        void Awake()
        {
            instance = this;
            source = GetComponent<AudioSource>();
            source.playOnAwake = false;
            source.volume = GameOptions.sfxVolume;
        }

        public void Play(AudioClip clip)
        {
            source.clip = clip;
            source.Play();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }*/
    }
}
