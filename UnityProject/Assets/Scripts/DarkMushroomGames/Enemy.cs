using DarkMushroomGames.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace DarkMushroomGames
{
    /// <summary>
    /// Base enemy class. Handles references and things to make sure it works with navmesh.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent)),RequireComponent(typeof(HitPoints))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField, Tooltip("The target the agent will track.")]
        private Transform target;

        [SerializeField,Tooltip("The list of clips that will be played when the enemy dies.")]
        private AudioClip[] killedClips;

        private HitPoints _hitPoints;
        private NavMeshAgent _navMeshAgent;

        
        public void Awake()
        {
            if (target == null)
            {
                target = GameObject.FindWithTag("Player").transform;
            }
            
            _hitPoints = GetComponent<HitPoints>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Update()
        {
            _navMeshAgent.destination = target.position;

            if (_hitPoints.HitPointsLeft <= 0)
            {
                SoundManager.Instance.PlaySoundEffectClip(killedClips,transform);
                Destroy(gameObject);
            }
        }
    }
}
