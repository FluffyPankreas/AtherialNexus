using DarkMushroomGames.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

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
    }
}
