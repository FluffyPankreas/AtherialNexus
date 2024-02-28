using System;
using DarkMushroomGames.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DarkMushroomGames
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField,Tooltip("The panel that shows the options.")]
        private Transform optionsPanel;

        public void Awake()
        {
            optionsPanel.gameObject.SetActive(false);
        }

        public void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                if (GameManager.Instance.IsPaused)
                {
                    GameManager.Instance.UnPauseGame();
                    optionsPanel.gameObject.SetActive(false);
                }
                else
                {
                    GameManager.Instance.PauseGame();
                    optionsPanel.gameObject.SetActive(true);
                }
            }
        }
    }
}
