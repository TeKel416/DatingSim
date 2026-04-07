using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace VNCreator
{
    public class VNCreator_OptionsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;

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

            if(musicVolumeSlider != null)
            {
                musicVolumeSlider.value = GameOptions.musicVolume;
                musicVolumeSlider.onValueChanged.AddListener(GameOptions.SetMusicVolumeValue);
                musicVolumeSlider.onValueChanged.AddListener(UpdateMusicVolume);
            }
            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.value = GameOptions.sfxVolume;
                sfxVolumeSlider.onValueChanged.AddListener(GameOptions.SetSFXVolume);
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

        public void UpdateMusicVolume(float volume)
        {
            audioMixer.SetFloat("Master_Vol", volume);
        }
    }
}
