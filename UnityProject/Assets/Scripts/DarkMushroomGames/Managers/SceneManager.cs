using DarkMushroomGames.Architecture;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DarkMushroomGames.Managers
{
    public class SceneManager : MonoBehaviourSingleton<SceneManager>
    {
        [SerializeField,Tooltip("The scene that has to be loaded that has all high level managers.")]
        private Scene managersScene;

        public void Start()
        {
            Debug.Log("Loading the Managers Scene.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(managersScene.name, LoadSceneMode.Additive);
        }

        public static void LoadScene(int sceneBuildNumber)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneBuildNumber);
        }
    }
}

