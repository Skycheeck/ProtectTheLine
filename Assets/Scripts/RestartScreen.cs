using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class RestartScreen : Singleton<RestartScreen>
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Button _restartButton;
        [SerializeField] private LevelManager _levelManager;
        

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        protected override void Awake()
        {
            base.Awake();
            _restartButton.onClick.AddListener(RestartGame);
        }

        private void Start()
        {
            Disable();
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }

        private void RestartGame()
        {
            Disable();
            _levelManager.RestartGame();
        }
    }
}