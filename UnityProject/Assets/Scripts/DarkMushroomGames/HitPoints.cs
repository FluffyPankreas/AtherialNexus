using System;
using AetherialNexus;
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

        [SerializeField,Tooltip("The prefab that is used to display floating text.")]
        private FloatingText floatTextPrefab;

        public int HitPointsLeft { get; private set; }
        public int MaxHitPoints => maxHitPoints;

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
            if(floatTextPrefab == null)
                return;
            
            var floatingText = Instantiate(floatTextPrefab);
            floatingText.transform.parent = transform;
            floatingText.transform.localPosition = Vector3.zero;
            floatingText.SetNumber(hitPointsToSubtract);
            floatingText.transform.parent = null;
        }
    }
}
