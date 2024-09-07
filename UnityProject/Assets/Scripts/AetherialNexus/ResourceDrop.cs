using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AetherialNexus
{
    [RequireComponent(typeof(Rigidbody))]
    public class ResourceDrop : MonoBehaviour
    {
        [SerializeField,Tooltip("The acceleration at which the object will move towards the follow target.")]
        private float acceleration;
        
        [SerializeField,Tooltip("The geometry to show the tier of the resource.")]
        private GameObject[] tierGeometry;

        private Rigidbody _rb;
        private int _amount;
        private int _tier;
        private string _type;

        private float speed = 0;

        private GameObject _followTarget;

        public void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Start()
        {
            InitializeValues(Random.Range(50, 100), Random.Range(0, 3));
        }

        public void Update()
        {
            if (_followTarget)
            {
                var direction = _followTarget.transform.position - transform.position;
                direction = direction.normalized;

                speed += acceleration;
                _rb.velocity = speed * Time.deltaTime * direction;
            }
        }

        public void InitializeValues(int amount, int tier)
        {
            _amount = amount;
            _tier = tier;
            _type = "MechanicalParts";
            
            SetupGeometry(tier);

            gameObject.SetActive(true);
        }

        public void StartFollowing(GameObject followTarget)
        {
            Debug.Log("Start following.");
            _followTarget = followTarget;
        }
        public void OnCollisionEnter(Collision collision)
        {
            
            
            Debug.Log("OnTriggerEnter");
            if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;
        
            switch (_type)
            {
                case "MechanicalParts":
                    ResourceManager.Instance.AddMechanicalParts(_amount, _tier);
                    break;
                case "OrganicMatter":
                    ResourceManager.Instance.AddOrganicMatter(_amount, _tier);
                    break;
                case "AetherialEssence":
                    ResourceManager.Instance.AddAetherialEssence(_amount, _tier);
                    break;
                default:
                    Debug.Log("There was no type set for the resource drop.");
                    break;
            }
        
            Debug.Log("COLLECTED.");
            gameObject.SetActive(false);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;

            _followTarget = other.gameObject;
        }

        private void SetupGeometry(int tier)
        {
            for (int i = 0; i < tierGeometry.Length; i++)
            {
                tierGeometry[i].SetActive(false);
            }

            tierGeometry[tier].SetActive(true);
        }
    }
}
