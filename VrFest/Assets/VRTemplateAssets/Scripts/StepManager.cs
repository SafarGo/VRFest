using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace YourNamespace
{
    /// <summary>
    /// Manages level navigation with left/right controls
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

        [SerializeField]
        private TextMeshProUGUI m_LevelNameTextField;

        [SerializeField]
        private List<Level> m_LevelList = new List<Level>();

        [SerializeField]
        private KeyCode m_LeftKey = KeyCode.LeftArrow;

        [SerializeField]
        private KeyCode m_RightKey = KeyCode.RightArrow;

        private int m_CurrentLevelIndex = 0;

        public int CurrentLevelIndex => m_CurrentLevelIndex;
        public int TotalLevels => m_LevelList.Count;

        void Start()
        {
            UpdateLevelDisplay();
        }

        void Update()
        {
            // Keyboard navigation
            if (Input.GetKeyDown(m_LeftKey))
            {
                PreviousLevel();
            }
            else if (Input.GetKeyDown(m_RightKey))
            {
                NextLevel();
            }
        }

        public void NextLevel()
        {
            if (m_LevelList.Count == 0) return;

            m_LevelList[m_CurrentLevelIndex].levelObject.SetActive(false);
            m_CurrentLevelIndex = (m_CurrentLevelIndex + 1) % m_LevelList.Count;
            UpdateLevelDisplay();
        }

        public void PreviousLevel()
        {
            if (m_LevelList.Count == 0) return;

            m_LevelList[m_CurrentLevelIndex].levelObject.SetActive(false);
            m_CurrentLevelIndex = (m_CurrentLevelIndex - 1 + m_LevelList.Count) % m_LevelList.Count;
            UpdateLevelDisplay();
        }

        public void GoToLevel(int levelIndex)
        {
            if (m_LevelList.Count == 0 || levelIndex < 0 || levelIndex >= m_LevelList.Count) return;

            m_LevelList[m_CurrentLevelIndex].levelObject.SetActive(false);
            m_CurrentLevelIndex = levelIndex;
            UpdateLevelDisplay();
        }

        private void UpdateLevelDisplay()
        {
            if (m_LevelList.Count == 0) return;

            m_LevelList[m_CurrentLevelIndex].levelObject.SetActive(true);

            if (m_LevelNameTextField != null)
            {
                m_LevelNameTextField.text = $"{m_LevelList[m_CurrentLevelIndex].levelName} ({m_CurrentLevelIndex + 1}/{m_LevelList.Count})";
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
    }
}