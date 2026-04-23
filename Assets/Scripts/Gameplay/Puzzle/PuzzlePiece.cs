using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Puzzle
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PuzzlePiece : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private readonly float _snapThreshold = 0.5f;

        private bool _isSnapped;
        private bool _isDragging;

        private Vector3 _targetPosition;
        private Vector3 _dragOffset;
        private Camera _mainCamera;

        public void Setup(Vector3 targetWorldPosition)
        {
            _targetPosition = targetWorldPosition;
            _mainCamera = Camera.main;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isSnapped) return;

            Vector3 worldPos = ScreenToWorld(eventData.position);

            _dragOffset = transform.position - worldPos;
            _isDragging = true;

            var pos = transform.position;

            transform.position = new Vector3(pos.x, pos.y, -10f);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isSnapped || !_isDragging) return;

            Vector3 worldPos = ScreenToWorld(eventData.position);
            var newPos = worldPos + _dragOffset;
            newPos.z = -10f;

            transform.position = newPos;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isSnapped) return;
            _isDragging = false;

            if (Vector3.Distance(transform.position, _targetPosition) < _snapThreshold)
                Snap();
            else
                ResetZ();
        }

        private Vector3 ScreenToWorld(Vector2 screenPos)
        {
            var v = _mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));
            v.z = 0f;
            return v;
        }

        private void Snap()
        {
            transform.position = _targetPosition;
            _isSnapped = true;
            _isDragging = false;
        }

        private void ResetZ()
        {
            var pos = transform.position;
            transform.position = new Vector3(pos.x, pos.y, _targetPosition.z);
        }
    }
}