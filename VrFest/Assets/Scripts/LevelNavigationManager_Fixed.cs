using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace YourNamespace
{
    /// <summary>
    /// Manages level navigation with UI buttons - Fixed version with debugging
    /// </summary>
    public class LevelNavigationManagerFixed : MonoBehaviour
    {
        [Serializable]
        public class Level
        {
            [SerializeField]
            public GameObject levelObject;

            [SerializeField]
            public string levelName;

            [SerializeField]
            public string sceneName;
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
        private Button m_ExitToLobbyButton;

        [SerializeField]
        private Button m_LoadLevelSceneButton;

        [Header("Scene Transition Settings")]
        [SerializeField]
        private string m_LobbySceneName = "Lobby";

        [SerializeField]
        private float m_SceneTransitionDelay = 0.5f;

        [SerializeField]
        private bool m_ShowLoadingText = true;

        [Header("Debug Settings")]
        [SerializeField]
        private bool m_EnableDebugLogs = true;

        private int m_CurrentLevelIndex = 0;

        public int CurrentLevelIndex => m_CurrentLevelIndex;
        public int TotalLevels => m_LevelList.Count;

        void Start()
        {
            LogDebug("LevelNavigationManager Start - Button References:");
            LogDebug($"Previous Button: {(m_PreviousButton != null ? "Assigned" : "NULL")}");
            LogDebug($"Next Button: {(m_NextButton != null ? "Assigned" : "NULL")}");
            LogDebug($"Exit Button: {(m_ExitToLobbyButton != null ? "Assigned" : "NULL")}");
            LogDebug($"Load Level Button: {(m_LoadLevelSceneButton != null ? "Assigned" : "NULL")}");
            LogDebug($"Level List Count: {m_LevelList.Count}");

            // Проверка ссылок на кнопки
            if (m_PreviousButton == null)
                Debug.LogError("Previous Button reference is NULL! Assign it in inspector.");
            
            if (m_NextButton == null)
                Debug.LogError("Next Button reference is NULL! Assign it in inspector.");
                
            if (m_ExitToLobbyButton == null)
                Debug.LogError("Exit To Lobby Button reference is NULL! Assign it in inspector.");
                
            if (m_LoadLevelSceneButton == null)
                Debug.LogError("Load Level Scene Button reference is NULL! Assign it in inspector.");

            // Проверка списка уровней
            if (m_LevelList.Count == 0)
                Debug.LogError("Level List is empty! Add levels in inspector.");

            if (m_PreviousButton != null)
                m_PreviousButton.onClick.AddListener(OnPreviousButtonClicked);

            if (m_NextButton != null)
                m_NextButton.onClick.AddListener(OnNextButtonClicked);

            if (m_ExitToLobbyButton != null)
                m_ExitToLobbyButton.onClick.AddListener(ExitToLobby);

            if (m_LoadLevelSceneButton != null)
                m_LoadLevelSceneButton.onClick.AddListener(LoadCurrentLevelScene);

            UpdateLevelDisplay();
            UpdateNavigationButtons();
        }

        void OnDestroy()
        {
            if (m_PreviousButton != null)
                m_PreviousButton.onClick.RemoveListener(OnPreviousButtonClicked);

            if (m_NextButton != null)
                m_NextButton.onClick.RemoveListener(OnNextButtonClicked);

            if (m_ExitToLobbyButton != null)
                m_ExitToLobbyButton.onClick.RemoveListener(ExitToLobby);

            if (m_LoadLevelSceneButton != null)
                m_LoadLevelSceneButton.onClick.RemoveListener(LoadCurrentLevelScene);
        }

        private void LogDebug(string message)
        {
            if (m_EnableDebugLogs)
            {
                Debug.Log($"[LevelNavigationManager] {message}");
            }
        }

        public void LoadCurrentLevelScene()
        {
            if (m_LevelList.Count == 0 || m_CurrentLevelIndex >= m_LevelList.Count)
            {
                Debug.LogWarning("No levels available or invalid level index");
                return;
            }

            string sceneName = m_LevelList[m_CurrentLevelIndex].sceneName;

            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning($"No scene name specified for level: {m_LevelList[m_CurrentLevelIndex].levelName}");
                return;
            }

            LogDebug($"Loading level scene: {sceneName}");

            if (m_LoadLevelSceneButton != null)
            {
                m_LoadLevelSceneButton.interactable = false;

                var textComponent = m_LoadLevelSceneButton.GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null && m_ShowLoadingText)
                {
                    textComponent.text = "Loading...";
                }
            }

            Invoke(nameof(ExecuteLevelSceneLoad), m_SceneTransitionDelay);
        }

        private void ExecuteLevelSceneLoad()
        {
            string sceneName = m_LevelList[m_CurrentLevelIndex].sceneName;

            try
            {
                SceneManager.LoadScene(sceneName);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load level scene: {sceneName}. Error: {e.Message}");

                if (m_LoadLevelSceneButton != null)
                {
                    m_LoadLevelSceneButton.interactable = true;

                    var textComponent = m_LoadLevelSceneButton.GetComponentInChildren<TextMeshProUGUI>();
                    if (textComponent != null)
                    {
                        textComponent.text = "Play Level";
                    }
                }
            }
        }

        public void ExitToLobby()
        {
            LogDebug("Returning to lobby...");

            if (m_ExitToLobbyButton != null)
                m_ExitToLobbyButton.interactable = false;

            Invoke(nameof(LoadLobbyScene), m_SceneTransitionDelay);
        }

        private void LoadLobbyScene()
        {
            if (!string.IsNullOrEmpty(m_LobbySceneName))
            {
                try
                {
                    SceneManager.LoadScene(m_LobbySceneName);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to load lobby scene: {m_LobbySceneName}. Error: {e.Message}");
                    SceneManager.LoadScene(0);
                }
            }
            else
            {
                SceneManager.LoadScene(0);
            }
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

            m_LevelList[m_CurrentLevelIndex].levelObject.SetActive(true);

            if (m_LevelNameTextField != null)
            {
                m_LevelNameTextField.text = m_LevelList[m_CurrentLevelIndex].levelName;
            }
        }

        private void UpdateNavigationButtons()
        {
            LogDebug("Updating navigation buttons...");

            if (m_PreviousButton != null)
            {
                bool canGoPrevious = HasPreviousLevel();
                m_PreviousButton.interactable = canGoPrevious;
                LogDebug($"Previous Button interactable: {canGoPrevious}");
            }
            else
            {
                Debug.LogWarning("Previous Button is NULL!");
            }

            if (m_NextButton != null)
            {
                bool canGoNext = HasNextLevel();
                m_NextButton.interactable = canGoNext;
                LogDebug($"Next Button interactable: {canGoNext}");
            }
            else
            {
                Debug.LogWarning("Next Button is NULL!");
            }

            if (m_LoadLevelSceneButton != null)
            {
                bool hasValidScene = m_LevelList.Count > 0 &&
                                   m_CurrentLevelIndex < m_LevelList.Count &&
                                   !string.IsNullOrEmpty(m_LevelList[m_CurrentLevelIndex].sceneName);

                m_LoadLevelSceneButton.interactable = hasValidScene;
                LogDebug($"Load Level Button interactable: {hasValidScene}");
                LogDebug($"Level List Count: {m_LevelList.Count}, Current Index: {m_CurrentLevelIndex}");
                
                if (m_LevelList.Count > 0 && m_CurrentLevelIndex < m_LevelList.Count)
                {
                    LogDebug($"Current Level Scene Name: '{m_LevelList[m_CurrentLevelIndex].sceneName}'");
                }
            }
            else
            {
                Debug.LogWarning("Load Level Button is NULL!");
            }
        }

        // UI Button methods
        public void OnNextButtonClicked()
        {
            LogDebug("Next Button Clicked");
            NextLevel();
        }

        public void OnPreviousButtonClicked()
        {
            LogDebug("Previous Button Clicked");
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

        public string GetCurrentLevelSceneName()
        {
            return m_LevelList.Count > 0 ? m_LevelList[m_CurrentLevelIndex].sceneName : "";
        }

        public int GetCurrentLevelNumber()
        {
            return m_CurrentLevelIndex + 1;
        }

        // Метод для принудительного обновления кнопок (можно вызвать извне)
        public void ForceUpdateButtons()
        {
            LogDebug("Force updating buttons...");
            UpdateNavigationButtons();
        }

        // Метод для проверки состояния кнопок
        public void CheckButtonStates()
        {
            LogDebug("=== Button States Check ===");
            LogDebug($"Previous Button: {(m_PreviousButton != null ? $"Active={m_PreviousButton.interactable}" : "NULL")}");
            LogDebug($"Next Button: {(m_NextButton != null ? $"Active={m_NextButton.interactable}" : "NULL")}");
            LogDebug($"Exit Button: {(m_ExitToLobbyButton != null ? $"Active={m_ExitToLobbyButton.interactable}" : "NULL")}");
            LogDebug($"Load Level Button: {(m_LoadLevelSceneButton != null ? $"Active={m_LoadLevelSceneButton.interactable}" : "NULL")}");
            LogDebug($"Current Level Index: {m_CurrentLevelIndex}");
            LogDebug($"Total Levels: {m_LevelList.Count}");
            LogDebug("=========================");
        }
    }
}
