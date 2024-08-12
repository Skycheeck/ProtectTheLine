using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class RestartScreen : Singleton<RestartScreen>
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Button _restartButton;

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
            gameObject.SetActive(false);
        }

        private static void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}