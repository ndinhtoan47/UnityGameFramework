namespace GameFramework.Pattern
{
    using System;
    using UnityEngine;

    public class ColliderEventTrigger : MonoBehaviour
    {
        private bool _isPressed = false;
        public event Action OnPointerExit;
        public event Action OnPointerEnter;
        public event Action OnPointerPress;
        public event Action OnPointerRelease;
        public event Action OnPointerDrag;
        public event Action OnPointerOver;        

        private void OnMouseDown()
        {
            if (!_isPressed)
            {
                _isPressed = true;
                OnPointerPress?.Invoke();
            }
        }

        private void OnMouseUp()
        {
            if (_isPressed)
            {
                _isPressed = false;
                OnPointerRelease?.Invoke();
            }
        }

        private void OnMouseExit()
        {
            OnPointerExit?.Invoke();
        }

        private void OnMouseDrag()
        {
            if (_isPressed)
            {
                OnPointerDrag?.Invoke();
            }
        }

        private void OnMouseOver()
        {
            OnPointerOver?.Invoke();
        }

        private void OnMouseEnter()
        {
            OnPointerEnter?.Invoke();
        }

		public void InvokeOnPress()
        {
            OnPointerPress?.Invoke();
        }

        public virtual void ReleaseEvents()
        {
            OnPointerExit = null;
            OnPointerEnter = null;
            OnPointerPress = null;
            OnPointerRelease = null;
            OnPointerDrag = null;
            OnPointerOver = null;
        }
        
        public virtual void SetInteractable(bool en) { }
    }
}

