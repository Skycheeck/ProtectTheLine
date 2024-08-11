using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class HealthWidget : Singleton<HealthWidget>
    {
        [SerializeField] private TMP_Text _text;
        
        public void UpdateHealth(float health)
        {
            _text.text = $"Health: {health:F0}";
        }
    }
}