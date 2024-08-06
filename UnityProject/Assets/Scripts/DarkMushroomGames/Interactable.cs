using System;
using AetherialNexus;
using DarkMushroomGames.Managers;
using TMPro;
using UnityEngine;

namespace DarkMushroomGames
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField, Tooltip("The string that will show up when the player can interact with this interactable.")]
        private string interactionMessage;

        [SerializeField,Tooltip("The UI element that will display the message.")]
        private TMP_Text messageWidget;

        private const float ActiveTime = 0.25f;
        public string InteractionMessage => interactionMessage;

        private float _activeTimeLeft = 0;
        
        public void Awake()
        {
            messageWidget.text = interactionMessage;
        }

        public void Update()
        {
            // Disable the widget if the active time has run out. 
            _activeTimeLeft = Mathf.Clamp(_activeTimeLeft, 0, float.MaxValue) - Time.deltaTime;
            if (_activeTimeLeft <= 0)
            {
                messageWidget.gameObject.SetActive(false);
            }
        }

        //TODO: This needs to be changed at some point into an interface or something. This is just temporary to make it work somewhat like it's supposed to for now.
        public void Activate()
        {
            _activeTimeLeft = ActiveTime;
            messageWidget.gameObject.SetActive(true);
        }
        
    }
}
