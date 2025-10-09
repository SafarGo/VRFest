using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YourNamespace
{
    /// <summary>
    /// Manages level navigation with UI buttons
    /// </summary>
    public class LevelNavigationManager : MonoBehaviour
    {
        [Serializable]
        public class Level
        {
            [SerializeField]
            public GameObject levelObject;

            [SerializeField]
            public string levelName;
        }

        [Header("Level Settings")]
        [SerializeField]
        private List<Level> m_LevelList = new List<Level>();

        [Header("UI References")]
        [SerializeField]
        private TextMeshProUGUI m_LevelNameTextField;

        [SerializeField]
        private Button m_PreviousButton;

        [SerializeField]
        private Button m_NextButton;

        [SerializeField]
        private TextMeshProUGUI m_PageCounterText;

        private int m_CurrentLevelIndex = 0;

        public int CurrentLevelIndex => m_CurrentLevelIndex;
        public int TotalLevels => m_LevelList.Count;

        void Start()
        {
            // Настраиваем обработчики кнопок
            if (m_PreviousButton != null)
                m_PreviousButton.onClick.AddListener(OnPreviousButtonClicked);

            if (m_NextButton != null)
                m_NextButton.onClick.AddListener(OnNextButtonClicked);

            UpdateLevelDisplay();
            UpdateNavigationButtons();
        }

        void OnDestroy()
        {
            // Отписываемся от событий при уничтожении объекта
            if (m_PreviousButton != null)
                m_PreviousButton.onClick.RemoveListener(OnPreviousButtonClicked);

            if (m_NextButton != null)
                m_NextButton.onClick.RemoveListener(OnNextButtonClicked);
        }

        public void NextLevel()
        {
            if (m_LevelList.Count == 0) return;

            m_LevelList[m_CurrentLevelIndex].levelObject.SetActive(false);
            m_CurrentLevelIndex = (m_CurrentLevelIndex + 1) % m_LevelList.Count;
            UpdateLevelDisplay();
            UpdateNavigationButtons();
        }

        public void PreviousLevel()
        {
            if (m_LevelList.Count == 0) return;

            m_LevelList[m_CurrentLevelIndex].levelObject.SetActive(false);
            m_CurrentLevelIndex = (m_CurrentLevelIndex - 1 + m_LevelList.Count) % m_LevelList.Count;
            UpdateLevelDisplay();
            UpdateNavigationButtons();
        }

        public void GoToLevel(int levelIndex)
        {
            if (m_LevelList.Count == 0 || levelIndex < 0 || levelIndex >= m_LevelList.Count) return;

            m_LevelList[m_CurrentLevelIndex].levelObject.SetActive(false);
            m_CurrentLevelIndex = levelIndex;
            UpdateLevelDisplay();
            UpdateNavigationButtons();
        }

        private void UpdateLevelDisplay()
        {
            if (m_LevelList.Count == 0) return;

            // Активируем текущий уровень
            m_LevelList[m_CurrentLevelIndex].levelObject.SetActive(true);

            // Обновляем текстовое поле с названием уровня
            if (m_LevelNameTextField != null)
            {
                m_LevelNameTextField.text = m_LevelList[m_CurrentLevelIndex].levelName;
            }

            // Обновляем счетчик страниц
            if (m_PageCounterText != null)
            {
                m_PageCounterText.text = $"{m_CurrentLevelIndex + 1} / {m_LevelList.Count}";
            }
        }

        private void UpdateNavigationButtons()
        {
            // Обновляем состояние кнопок в зависимости от текущей позиции
            if (m_PreviousButton != null)
            {
                m_PreviousButton.interactable = HasPreviousLevel();

                // Можно также менять прозрачность для неактивных кнопок
                var previousButtonColors = m_PreviousButton.colors;
                if (!HasPreviousLevel())
                {
                    previousButtonColors.disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    m_PreviousButton.colors = previousButtonColors;
                }
            }

            if (m_NextButton != null)
            {
                m_NextButton.interactable = HasNextLevel();

                var nextButtonColors = m_NextButton.colors;
                if (!HasNextLevel())
                {
                    nextButtonColors.disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    m_NextButton.colors = nextButtonColors;
                }
            }
        }

        // UI Button methods
        public void OnNextButtonClicked()
        {
            NextLevel();
        }

        public void OnPreviousButtonClicked()
        {
            PreviousLevel();
        }

        // Public methods for external control
        public bool HasNextLevel()
        {
            return m_CurrentLevelIndex < m_LevelList.Count - 1;
        }

        public bool HasPreviousLevel()
        {
            return m_CurrentLevelIndex > 0;
        }

        public string GetCurrentLevelName()
        {
            return m_LevelList.Count > 0 ? m_LevelList[m_CurrentLevelIndex].levelName : "";
        }

        public int GetCurrentLevelNumber()
        {
            return m_CurrentLevelIndex + 1;
        }
    }
}