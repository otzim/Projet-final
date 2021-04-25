using System.Collections.Generic;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace StealthGame
{
    public class PlayerGroundChecker : MonoBehaviour
    {
        #region Show in inspector

        [Header("Parameters")]

        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _originOffset = 0.5f;
        [SerializeField] private float _originHeight = 0.6f;
        [SerializeField] private float _checkDistance = .8f;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _groundOffset;

        [Header("Debug")]

        [SerializeField] private bool _drawGizmos;
        [SerializeField] private Color _raycastsColor;
        [SerializeField] private Color _raycastHitsColor;
        [SerializeField] private Color _floorAverageColor;
        [SerializeField] private Color _normalsColor;

        #endregion

        public bool CheckGround(out Vector3 averageGroundPosition)
        {
            UpdateOriginPositions();

#if UNITY_EDITOR
            // Tous les points d'impact
            _floorPositions.Clear();
#endif
            int hitCount = 0;
            averageGroundPosition = Vector3.zero;

            for (int i = 0; i < _originPositions.Length; i++)
            {
                if (CheckGround(_originPositions[i], Vector3.down, out RaycastHit closestHit))
                {
#if UNITY_EDITOR
                    // Dessiner un point d'impact
                    _floorPositions.Add(closestHit);
#endif
                    averageGroundPosition += closestHit.point;
                    hitCount++;
                }
            }

            if (hitCount > 0)
            {
                averageGroundPosition /= hitCount;
                averageGroundPosition.y += _groundOffset;
            }

#if UNITY_EDITOR
            // Dessiner la moyenne
            if (_drawGizmos)
            {
                _groundFound = hitCount > 0;
                _groundPosition = averageGroundPosition;
            }
#endif

            return hitCount > 0;
        }

        private bool CheckGround(Vector3 origin, Vector3 direction, out RaycastHit closestHit)
        {
            //return Physics.Raycast(origin, direction, out closestHit, _checkDistance, _layerMask);

            // Version optimisée
            bool doesHit = false;
            int hitCount = Physics.RaycastNonAlloc(origin, direction, _hitBuffer, _checkDistance, _layerMask);

            closestHit = new RaycastHit();
            float minDistance = float.PositiveInfinity;
            for (int i = 0; i < hitCount; i++)
            {
                doesHit = true;
                if (_hitBuffer[i].distance < minDistance)
                {
                    closestHit = _hitBuffer[i];
                    minDistance = closestHit.distance;
                }
            }

            return doesHit;
        }

        private void UpdateOriginPositions()
        {
            _originPositions[0] = _playerTransform.TransformPoint(new Vector3(0, _originHeight, 0));
            _originPositions[1] = _playerTransform.TransformPoint(new Vector3(_originOffset, _originHeight, 0));
            _originPositions[2] = _playerTransform.TransformPoint(new Vector3(0, _originHeight, _originOffset));
            _originPositions[3] = _playerTransform.TransformPoint(new Vector3(-_originOffset, _originHeight, 0));
            _originPositions[4] = _playerTransform.TransformPoint(new Vector3(0, _originHeight, -_originOffset));
        }

        #region Debug
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
            {
                return;
            }

            Handles.color = _raycastsColor;

            for (int i = 0; i < _originPositions.Length; i++)
            {
                Vector3 position = _originPositions[i];
                Handles.SphereHandleCap(0, position, Quaternion.identity, .05f, EventType.Repaint);
                Handles.DrawDottedLine(position, position + Vector3.down * _checkDistance, 2f);
            }

            if (Application.isPlaying)
            {
                if (_groundFound)
                {
                    DrawImpactPoints();
                }
            }
            else
            {
                if (CheckGround(out _groundPosition))
                {
                    DrawImpactPoints();
                }
            }

        }

        private void DrawImpactPoints()
        {
            foreach (RaycastHit hit in _floorPositions)
            {
                Handles.color = _raycastHitsColor;
                Handles.SphereHandleCap(0, hit.point, Quaternion.identity, .05f, EventType.Repaint);
                Handles.color = _normalsColor;
                Handles.DrawLine(hit.point, hit.point + hit.normal);
            }
            Handles.color = _floorAverageColor;
            Handles.SphereHandleCap(0, _groundPosition, Quaternion.identity, .1f, EventType.Repaint);
        }

        private bool _groundFound;
        private Vector3 _groundPosition;
        private List<RaycastHit> _floorPositions = new List<RaycastHit>();

#endif
        #endregion

        #region Private

        private Vector3[] _originPositions = new Vector3[5];
        private RaycastHit[] _hitBuffer = new RaycastHit[20];

        #endregion
    }
}