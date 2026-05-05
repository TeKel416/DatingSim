using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace VNCreator
{
    public class VNCreator_OptionsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;

        public Slider masterVolumeSlider;
        public Slider musicVolumeSlider;
        public Slider sfxVolumeSlider;
        public Slider readSpeedSlider;
        public Toggle instantTextToggle;
        public Button backButton;

        [Header("Menu Objects")]
        public GameObject optionsMenu;
        public GameObject mainMenu;

        void Start()
        {
            GameOptions.InitilizeOptions();

            if (masterVolumeSlider != null)
            {
                masterVolumeSlider.onValueChanged.AddListener(GameOptions.SetMasterVolumeValue);
                masterVolumeSlider.onValueChanged.AddListener(UpdateMasterVolume);
                masterVolumeSlider.value = GameOptions.masterVolume;
            }
            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.onValueChanged.AddListener(GameOptions.SetMusicVolumeValue);
                musicVolumeSlider.onValueChanged.AddListener(UpdateMusicVolume);
                musicVolumeSlider.value = GameOptions.musicVolume;
            }
            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.onValueChanged.AddListener(GameOptions.SetSFXVolume);
                sfxVolumeSlider.onValueChanged.AddListener(UpdateSFXVolume);
                sfxVolumeSlider.value = GameOptions.sfxVolume;
            }
            if (readSpeedSlider != null)
            {
                readSpeedSlider.value = GameOptions.readSpeed;
                readSpeedSlider.onValueChanged.AddListener(GameOptions.SetReadingSpeed);
            }
            if (instantTextToggle != null)
            {
                instantTextToggle.isOn = GameOptions.isInstantText;
                instantTextToggle.onValueChanged.AddListener(GameOptions.SetInstantText);
            }

            backButton.onClick.AddListener(Back);
        }

        void Back()
        {
            if (mainMenu) mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }

        public void UpdateMasterVolume(float volume)
        {
            audioMixer.SetFloat("Master_Vol", volume);
        }

        public void UpdateMusicVolume(float volume)
        {
            audioMixer.SetFloat("Music_Vol", volume);
        }

        public void UpdateSFXVolume(float volume)
        {
            audioMixer.SetFloat("SFX_Vol", volume);
        }
    }
}
