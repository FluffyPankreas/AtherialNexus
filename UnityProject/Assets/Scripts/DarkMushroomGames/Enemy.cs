using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


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

        [SerializeField,Tooltip("The attack distance of the agent.")]
        private float attackDistance;

        [SerializeField, Tooltip("Damage done with attacks.")]
        private int attackDamage;
        
        [SerializeField, Tooltip("The location the attack happens.")]
        private Transform attackLocation;

        

        [SerializeField] private LayerMask attacksHitLayer;
        [SerializeField] private float attackRadius;

        [SerializeField]
        private float telegraphingTime = 1f;
        [SerializeField]
        private float attackTime = 0.2f;
        
        private HitPoints _hitPoints;
        private NavMeshAgent _navMeshAgent;
        private bool _attacking = false;

        
        public void Awake()
        {
            Debug.Assert(target != null,
                "Currently the target has to be assigned in the editor. Please make sure to assign an appropriate transform.");
            
            _hitPoints = GetComponent<HitPoints>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _navMeshAgent.stoppingDistance = attackDistance;
        }

        public void Update()
        {
            _navMeshAgent.destination = target.position;

            if (_navMeshAgent.remainingDistance <= attackDistance && !_attacking)
            {
                _navMeshAgent.isStopped = true;
                _attacking = true;
                TelegraphAttack();
            }
            
            if (_hitPoints.HitPointsLeft <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void TelegraphAttack()
        {
            GetComponent<MeshRenderer>().material.color = Color.yellow;
            Invoke(nameof(Attack),telegraphingTime);
        }
        
        private readonly Collider[] _results = new Collider[2];
        private void Attack()
        {
            GetComponent<MeshRenderer>().material.color = Color.red;

            
            var hits = Physics.OverlapSphereNonAlloc(attackLocation.position, attackRadius, _results, attacksHitLayer);
            for (int i = 0; i < hits; i++)
            {
                var hp = _results[i].GetComponent<HitPoints>();
                hp.SubtractHitPoints(attackDamage);
            }


            Invoke(nameof(ActivateAgent), attackTime);
        }

        private void ActivateAgent()
        {
            _navMeshAgent.isStopped = false;
            _attacking = false;
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}
