using System;
using DarkMushroomGames.Architecture;
using UnityEngine;

namespace AetherialNexus
{
    public class ResourceManager : MonoBehaviourSingleton<ResourceManager>
    {
        /// <summary>
        /// The number of levels that are in the game.
        /// </summary>
        public const int NumberOfLevels = 4;
            
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
