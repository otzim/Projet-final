#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shooter
{
    public class MainMenuManager : MonoBehaviour
    {
        #region Show in inspector

        [SerializeField] private string _mainMenuSceneName = default;
        [SerializeField] private string _firstSceneName = default;

        #endregion


        #region UI methods

        public void NewGame()
        {
            SceneManager.LoadScene(_firstSceneName);
            SceneManager.LoadScene("DemoScene_Dungeon", LoadSceneMode.Additive); 
            SceneManager.LoadScene("Demo Night", LoadSceneMode.Additive); 
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(_mainMenuSceneName);
        }

        #endregion
    }
}