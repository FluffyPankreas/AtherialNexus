using UnityEngine;
using UnityEngine.AI;

namespace DarkMushroomGames.Architecture
{
    public static class NavMeshExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="roamingDistance"></param>
        /// <param name="layerMask"></param>
        /// <returns></returns>
        public static Vector3 RandomNavSphere(Vector3 origin, float roamingDistance, int layerMask)
        {
            var randDirection = Random.insideUnitSphere * roamingDistance;

            randDirection += origin;
            NavMesh.SamplePosition(randDirection, out var navHit, roamingDistance, layerMask);
            return navHit.position;
        }
    }
}
