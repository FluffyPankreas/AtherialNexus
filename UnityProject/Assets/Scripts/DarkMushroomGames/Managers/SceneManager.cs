using DarkMushroomGames.Architecture;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DarkMushroomGames.Managers
{
    public class SceneManager : MonoBehaviourSingleton<SceneManager>
    {
        [SerializeField,Tooltip("The scene that has to be loaded that has all high level managers.")]
        private string managersSceneKey;

        [SerializeField,Tooltip("The scene that is the initial phase of the game, spawning the player in the hub.")]
        private string hubSceneKey; 

        public void Start()
        {
            Debug.Log("Loading the Managers Scene.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(managersSceneKey, LoadSceneMode.Additive);

            Debug.Log("Loading the Hub.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(hubSceneKey, LoadSceneMode.Additive);
        }

        public static void LoadScene(int sceneBuildNumber)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneBuildNumber);
        }
    }
}

