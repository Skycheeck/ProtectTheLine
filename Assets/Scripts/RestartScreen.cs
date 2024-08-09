using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class RestartScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Button _restartButton;

        private void Awake()
        {
            _restartButton.onClick.AddListener(RestartGame);
        }

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}