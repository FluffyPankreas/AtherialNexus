using DarkMushroomGames.Architecture;
using UnityEngine;

namespace DarkMushroomGames.Managers
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        public delegate void GamePaused();
        public delegate void GameUnPaused();

        public static event GamePaused OnGamePause;
        public static event GamePaused OnGameUnPause;
        public bool IsPaused { get; private set; } = false;

        public void PauseGame()
        {
            Debug.Log("Pausing game.");
            Time.timeScale = 0;
            IsPaused = true;
            OnGamePause!.Invoke();
        }

        public void UnPauseGame()
        {
            Debug.Log("UnPausing Game.");
            Time.timeScale = 1;
            IsPaused = false;
            OnGameUnPause!.Invoke();
        }
    }
}