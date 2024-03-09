using DarkMushroomGames.Managers;
using DarkMushroomGames.Architecture;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


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

        [SerializeField,Tooltip("The min and max values used to set the next roaming time.")]
        private Vector2 roamingTime;

        [SerializeField,Tooltip("The radius in which the enemy will wander around their anchor.")]
        private float wanderRadius;

        [SerializeField, Tooltip("The distance at which the agent will start chasing the player.")]
        private float chaseDistance;

        [SerializeField, Tooltip("The controller for the enemy animations.")]
        private Animator animationController;

        [SerializeField,Tooltip("The slider component that will indicate the character's health.")]
        private Slider healthIndicator;

        [SerializeField,Tooltip("The distance that the agent will stop from the target in order to attack.")]
        private float attackRange;

        public bool IsChasing => _chasing;
        public float AttackRange => attackRange;
        public Transform Target => target;
        
        private Transform _anchor;
        private HitPoints _hitPoints;
        private NavMeshAgent _navMeshAgent;

        private bool _chasing = false;
        private bool _roaming = true;
        private float _timer;
        private float _nextRoamTime;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public void Awake()
        {
            if (target == null)
            {
                target = GameObject.FindWithTag("Player").transform;
            }
            
            _hitPoints = GetComponent<HitPoints>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _nextRoamTime = Random.Range(roamingTime.x, roamingTime.y);
            
        }

        public void Start()
        {
            if (_anchor == null)
                _anchor = transform;
            SetNewRoamTarget();
            healthIndicator.maxValue = _hitPoints.MaxHitPoints;
            
            
        }

        public void Update()
        {
            healthIndicator.gameObject.SetActive(_hitPoints.HitPointsLeft != _hitPoints.MaxHitPoints);
            healthIndicator.value = _hitPoints.HitPointsLeft;
            if (Vector3.Distance(transform.position, target.position) < chaseDistance)
            {
                _chasing = true;
                _roaming = false;

                _navMeshAgent.stoppingDistance = attackRange;
            }
            
            if (Vector3.Distance(transform.position, target.position) >= chaseDistance && !_roaming)
            {
                _roaming = true;
                _chasing = false;
                
                _navMeshAgent.stoppingDistance = 0;
                
                SetNewRoamTarget();
            }

            if (_roaming)
            {
                
                _timer += Time.deltaTime;

                if (_timer >= _nextRoamTime)
                {
                    SetNewRoamTarget();
                }
            }

            if (_chasing)
            {
                _navMeshAgent.destination = target.position;
            }

            if ( _navMeshAgent.remainingDistance <= 0)
            {
                animationController.SetBool(IsMoving, false);
            }
            else
            {
                animationController.SetBool(IsMoving, true);
            }
            
            if (_hitPoints.HitPointsLeft <= 0)
            {
                SoundManager.Instance.PlaySoundEffectClip(killedClips,transform);
                Destroy(gameObject);
            }

        }

        public void SetAnchor(Transform newAnchor)
        {
            _anchor = newAnchor;
        }

        private void SetNewRoamTarget()
        {
            var newPos = NavMeshExtensions.RandomNavSphere(_anchor.position, wanderRadius, -1);
            _navMeshAgent.SetDestination(newPos);
            _nextRoamTime = Random.Range(roamingTime.x, roamingTime.y);
            _timer = 0;
        }
    }
}
