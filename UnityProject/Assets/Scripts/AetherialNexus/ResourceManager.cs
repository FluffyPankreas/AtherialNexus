using System;
using DarkMushroomGames.Architecture;
using Unity.Mathematics;
using UnityEngine;

namespace AetherialNexus
{
    public class ResourceManager : MonoBehaviourSingleton<ResourceManager>
    {
        /// <summary>
        /// The number of levels that are in the game.
        /// </summary>
        public const int NumberOfLevels = 4;

        [SerializeField,Tooltip("The prefab that will be instantiated when resources are dropped.")]
        private ResourceDrop resourcePrefab;
        
        private int[] _mechanicalParts = new int[3];
        private int[] _organicMatter = new int[3];
        private int[] _atherialEssence = new int[3];

        private int[] _realityEssence = new int[NumberOfLevels];

        public void AddMechanicalParts(int amountToAdd, int tier)
        {
            Debug.Log("Adding mechanical parts.");
            _mechanicalParts[tier] += amountToAdd;
            Save();
        }

        public bool RemoveMechanicalParts(int amountToRemove, int tier)
        {
            if (_mechanicalParts[tier] > amountToRemove)
            {
                _mechanicalParts[tier] -= amountToRemove;
                Save();
                return true;
            }
            
            Save();
            return false;
        }
        
        public void AddOrganicMatter(int amountToAdd, int tier)
        {
            _organicMatter[tier] += amountToAdd;
            Save();
        }
        
        public bool RemoveOrganicMatter(int amountToRemove, int tier)
        {
            if (_organicMatter[tier] > amountToRemove)
            {
                _organicMatter[tier] -= amountToRemove;
                Save();
                return true;
            }
            
            Save();
            return false;
        }
        
        public void AddAetherialEssence(int amountToAdd, int tier)
        {
            _atherialEssence[tier] += amountToAdd;
            Save();
        }
        
        public bool RemoveAtherialEssence(int amountToRemove, int tier)
        {
            if (_atherialEssence[tier] > amountToRemove)
            {
                _atherialEssence[tier] -= amountToRemove;
                Save();
                return true;
            }
            
            Save();
            return false;
        }
        
        public void AddRealityEssence(int amountToAdd, int level)
        {
            _realityEssence[level] += amountToAdd;
            Save();
        }
        
        public bool RemoveRealityEssence(int amountToRemove, int level)
        {
            if (_realityEssence[level] > amountToRemove)
            {
                _realityEssence[level] -= amountToRemove;
                Save();
                return true;
            }
            
            Save();
            return false;
        }

        public void InstantiateResource(Vector3 location, ResourceDropRange resourceData)
        {
            var newResource = Instantiate(resourcePrefab, location, Quaternion.identity);
            newResource.InitializeValues(resourceData.amount, resourceData.tier);
        }

        private void Save()
        {
            // throw new NotImplementedException();
        }

        private void Load()
        {
            // throw new NotImplementedException();
        }
    }
}
