using System;
using DarkMushroomGames.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DarkMushroomGames
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField,Tooltip("The panel that shows the options.")]
        private Transform optionsPanel;

        [SerializeField,Tooltip("The parent that houses all the special effects instantiated on the HUD.")]
        private Transform sfxParent;

        [SerializeField,Tooltip("The prefab to instantiate when the player is hit.")]
        private GameObject bloodSplatterPrefab;

        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider effectsVolumeSlider;

        public void Awake()
        {
            optionsPanel.gameObject.SetActive(false);
        }

        public void OnEnable()
        {
            masterVolumeSlider.value = SoundManager.Instance.GetMasterVolume();
            musicVolumeSlider.value = SoundManager.Instance.GetMusicVolume();
            effectsVolumeSlider.value = SoundManager.Instance.GetEffectsVolume();
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


        public void RestartGame()
        {
            GameManager.Instance.Restart();
        }

        public void QuitGame()
        {
            GameManager.Instance.Quit();
        }
            

        public void ShowBloodSplatter()
        {
            Debug.Log("Should show splatter.");
            var splatter = Instantiate(bloodSplatterPrefab, sfxParent);
            var xPosition = Random.Range(-650, 650);
            var yPosition = Random.Range(-275, 275);


            var scaleNumber = UnityEngine.Random.Range(0.5f, 1.75f);

            splatter.transform.localPosition = new Vector3(xPosition, yPosition, 0);
            splatter.transform.localScale = new Vector3(scaleNumber, scaleNumber, scaleNumber);
        }

        public void SetMasterVolume(float level)
        {
            SoundManager.Instance.SetMasterVolume(level);
        }

        public void SetMusicVolume(float level)
        {
            SoundManager.Instance.SetMusicVolume(level);
        }
        
        public void SetEffectsVolume(float level)
        {
            SoundManager.Instance.SetEffectsVolume(level);
        }
    }
}
