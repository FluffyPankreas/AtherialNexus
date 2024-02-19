using System;
using UnityEngine;

namespace DarkMushroomGames
{
    /// <summary>
    /// Component for anything that needs hit points. Provides an interface to change the value.
    /// </summary>
    public class HitPoints : MonoBehaviour
    {
        [SerializeField, Tooltip("The maximum hit points for the agent.")]
        private int maxHitPoints = 10;

        public int HitPointsLeft { get; private set; }

        public void Awake()
        {
            HitPointsLeft = maxHitPoints;
        }

        public void AddHitPoints(int hitPointsToAdd)
        {
            HitPointsLeft += hitPointsToAdd;
        }

        public void SubtractHitPoints(int hitPointsToSubtract)
        {
            HitPointsLeft -= hitPointsToSubtract;
        }
    }
}
