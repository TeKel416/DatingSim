using UnityEngine;
using UnityEngine.UI;

namespace VNCreator 
{ 
    public class LikeBar : MonoBehaviour
    {
        public static LikeBar Instance;

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

        public float likeLevel = 0;

        public void SetLikeLevel(float likeValue)
        {
            likeLevel = likeValue;
        }

        public float GetLikeLevel()
        {
            return likeLevel;
        }

        public void ChangeLikeLevel(float likeGain)
        {
            likeLevel += likeGain;
        }
    }
}