using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace VNCreator
{
    public class VNCreator_DisplayUI : DisplayBase
    {
        [Header("Like Bar")]
        public Slider likeSlider;
        [Header("New Story")]
        public bool isNewStory = false;
        [Header("Text")]
        public Text characterNameTxt;
        public Text dialogueTxt;
        [Header("Visuals")]
        public Image characterImg;
        public Image backgroundImg;
        [Header("Audio")]
        public AudioSource musicSource;
        public AudioSource soundEffectSource;
        [Header("Buttons")]
        public Button nextBtn;
        public Button previousBtn;
        public Button saveBtn;
        public Button menuButton;
        public Button optButton;
        [Header("Choices")]
        public Button choiceBtn1;
        public Button choiceBtn2;
        public Button choiceBtn3;
        [Header("End")]
        [Scene]
        public string endScene;
        [Header("Options menu")]
        public GameObject optionsMenu;
        [Header("Main menu")]
        [Scene]
        public string mainMenu;

        private bool isTyping = false;

        void Start()
        {
            nextBtn.onClick.AddListener(delegate { NextNode(0); });
            if(previousBtn != null)
                previousBtn.onClick.AddListener(Previous);
            if(saveBtn != null)
                saveBtn.onClick.AddListener(Save);
            if (menuButton != null)
                menuButton.onClick.AddListener(ExitGame);
            if (optButton != null)
                optButton.onClick.AddListener(OpenOptionsMenu);

            if(choiceBtn1 != null)
                choiceBtn1.onClick.AddListener(delegate { NextNode(0); });
            if(choiceBtn2 != null)
                choiceBtn2.onClick.AddListener(delegate { NextNode(1); });
            if(choiceBtn3 != null)
                choiceBtn3.onClick.AddListener(delegate { NextNode(2); });

            likeSlider.value = LikeBar.Instance.GetLikeLevel();
            likeSlider.gameObject.SetActive(currentNode.showLikeBar);

            StartCoroutine(DisplayCurrentNode());
        }

        protected override void NextNode(int _choiceId)
        {
            if (!isTyping)
            {
                if (lastNode)
                {
                    EndStory();
                    return;
                }

                base.NextNode(_choiceId);
                StartCoroutine(DisplayCurrentNode());
            }
        }

        IEnumerator DisplayCurrentNode()
        {
            likeSlider.gameObject.SetActive(currentNode.showLikeBar);

            characterNameTxt.text = currentNode.characterName;
            if (currentNode.characterSpr != null)
            {
                characterImg.sprite = currentNode.characterSpr;
                characterImg.color = Color.white;
            }
            else
            {
                characterImg.color = new Color(1, 1, 1, 0);
            }
            if(currentNode.backgroundSpr != null)
                backgroundImg.sprite = currentNode.backgroundSpr;

            if (currentNode.choices <= 1) 
            {
                nextBtn.gameObject.SetActive(true);

                choiceBtn1.gameObject.SetActive(false);
                choiceBtn2.gameObject.SetActive(false);
                choiceBtn3.gameObject.SetActive(false);

                previousBtn.gameObject.SetActive(loadList.Count != 1);
            }
            else
            {
                nextBtn.gameObject.SetActive(false);

                choiceBtn1.gameObject.SetActive(true);
                choiceBtn1.transform.GetChild(0).GetComponent<Text>().text = currentNode.choiceOptions[0];

                choiceBtn2.gameObject.SetActive(true);
                choiceBtn2.transform.GetChild(0).GetComponent<Text>().text = currentNode.choiceOptions[1];

                if (currentNode.choices == 3)
                {
                    choiceBtn3.gameObject.SetActive(true);
                    choiceBtn3.transform.GetChild(0).GetComponent<Text>().text = currentNode.choiceOptions[2];
                }
                else
                {
                    choiceBtn3.gameObject.SetActive(false);
                }
            }

            if (currentNode.backgroundMusic != null)
                VNCreator_MusicSource.Instance.PlayMusic(currentNode.backgroundMusic);
            if (currentNode.soundEffect != null)
                VNCreator_SfxSource.Instance.PlaySound2D(currentNode.soundEffect);

            if (currentNode.likeGain != 0)
            {
                LikeBar.Instance.ChangeLikeLevel(currentNode.likeGain);
                likeSlider.value = LikeBar.Instance.GetLikeLevel();
            }

            // escrever o texto de dialogo
            dialogueTxt.text = string.Empty;
            if (GameOptions.isInstantText)
            {
                isTyping = false;
                dialogueTxt.text = currentNode.dialogueText;
            }
            else
            {
                isTyping = true;
                VNCreator_SfxSource.Instance.PlaySoundOnLoop("Type");

                char[] _chars = currentNode.dialogueText.ToCharArray();
                string fullString = string.Empty;
                for (int i = 0; i < _chars.Length; i++)
                {
                    fullString += _chars[i];
                    dialogueTxt.text = fullString;
                    yield return new WaitForSeconds(0.01f/ GameOptions.readSpeed);
                }

                isTyping = false;
                VNCreator_SfxSource.Instance.StopLoop();
            }
        }

        protected override void Previous()
        {
            base.Previous();
            StartCoroutine(DisplayCurrentNode());
        }

        void ExitGame()
        {
            SceneManager.LoadScene(mainMenu, LoadSceneMode.Single);
        }

        void OpenOptionsMenu()
        {
            optionsMenu.SetActive(true);
        }

        void EndStory()
        {
            if (isNewStory)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                //SceneManager.SetActiveScene(SceneManager.GetSceneByName(endScene));
            }
            else
            {
                SceneManager.LoadScene(endScene, LoadSceneMode.Single);
            }
        }

        private void OnDisable()
        {
            GameObject progressManager = GameObject.FindWithTag("ProgressManager");

            if (progressManager)
            {
                progressManager.GetComponent<ProgressManager>().ActivateMoveCanvas(true);
            }
        }
    }
}