using DarkMushroomGames.Architecture;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Serialization;

namespace DarkMushroomGames.Managers
{
    /// <summary>
    /// A scene manager that builds on Unity's scene manager but allows for more control on what scenes should
    /// stay loaded an which ones should be unloaded. 
    /// </summary>
    public class DarkMushroomSceneManager : MonoBehaviourSingleton<DarkMushroomSceneManager>
    {
        [SerializeField,Tooltip("The scene that has to be loaded that has all high level managers.")]
        private string managersSceneKey;

        [SerializeField,Tooltip("The scene that is the initial phase of the game, spawning the player in the hub.")]
        private string hubSceneKey;

        [SerializeField,Tooltip("The splashscreen to load for the company.")]
        private string splashScreenKey;

        public void Start()
        {
            Debug.Log("Loading the Managers Scene.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(managersSceneKey, LoadSceneMode.Additive);
            StartCoroutine(DelayedLoad());
        }

        public void LoadScene(int sceneBuildNumber)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneBuildNumber);
        }

        public void LoadScene(string sceneName)
        {
            LoadScene(SceneManager.GetSceneByName(sceneName).buildIndex);
        }

        private IEnumerator DelayedLoad()
        {
            Debug.Log("Loading the splash scene.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(splashScreenKey, LoadSceneMode.Additive);
            yield return new WaitForSeconds(6);
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(splashScreenKey);
            Debug.Log("Loading the Hub.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(hubSceneKey, LoadSceneMode.Additive);
        }

        //TODO: This is a quick fix to stop restarting the game from crashing. At some point the scene management needs to be cleaned up and set up better.
        public void RestartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(hubSceneKey, LoadSceneMode.Single);
        }
    }

}

