using UnityEngine;

namespace DarkMushroomGames
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField, Tooltip("The string that will show up when the player can interact with this interactable.")]
        private string interactionMessage;

        public string InterActionMessage
        {
            get { return interactionMessage; }

        }
    }
}
