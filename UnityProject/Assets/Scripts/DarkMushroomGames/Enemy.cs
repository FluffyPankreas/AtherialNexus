using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace DarkMushroomGames
{
    /// <summary>
    /// Base enemy class. Handles references and things to make sure it works with navmesh.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField, Tooltip("The target the agent will track.")]
        private Transform target;
        private NavMeshAgent _navMeshAgent;

        
        public void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Update()
        {
            if (target != null)
            {
                _navMeshAgent.destination = target.position;
            }
        }
    }
}
