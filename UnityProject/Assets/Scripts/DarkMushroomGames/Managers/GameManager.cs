using DarkMushroomGames.Architecture;
using UnityEngine;

namespace DarkMushroomGames.Managers
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        public delegate void GamePaused();
        public delegate void GameUnPaused();

        public static event GamePaused OnGamePause;
        public static event GameUnPaused OnGameUnPause;

        public bool IsPaused { get; private set; } = false;

        public void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        public void PauseGame()
        {
            Debug.Log("Pausing game.");
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            OnGamePause!.Invoke();
            IsPaused = true;
        }

        public void UnPauseGame()
        {
            Debug.Log("UnPausing Game.");
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            OnGameUnPause!.Invoke();
            IsPaused = false;
        }

        public void Restart()
        {
            DarkMushroomSceneManager.LoadScene(0);
            UnPauseGame();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}