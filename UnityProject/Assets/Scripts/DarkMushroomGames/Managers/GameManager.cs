using DarkMushroomGames.Architecture;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DarkMushroomGames.Managers
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        public bool IsPaused { get; private set; } = false;

        public void PauseGame()
        {
            Debug.Log("Pausing game.");
            Time.timeScale = 0;
            IsPaused = true;
        }

        public void UnPauseGame()
        {
            Debug.Log("Unpausing Game.");
            Time.timeScale = 1;
            IsPaused = false;
        }

        public void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                if (IsPaused)
                {
                    UnPauseGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }
}