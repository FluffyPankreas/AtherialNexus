using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DarkMushroomGames
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineRendererController : MonoBehaviour
    {

        [SerializeField, Tooltip("The amount of points for the line.")]
        private int positionCount;
        
        private LineRenderer _lineRenderer;
        public void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = positionCount;
        }

        public void SetPosition(int positionIndex, Vector3 position)
        {
            _lineRenderer.SetPosition(positionIndex, position);
        }
    }
}
