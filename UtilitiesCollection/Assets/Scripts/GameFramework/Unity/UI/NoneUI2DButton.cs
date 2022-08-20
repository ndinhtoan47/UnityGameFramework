namespace GameFramework.Unity
{
    using UnityEngine;
    
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class NoneUI2DButton : Pattern.ColliderEventTrigger2D
    {
        [SerializeField] private SpriteRenderer _targetGraphic;
        public SpriteRenderer TargetGraphic => _targetGraphic;

        [SerializeField] private BoxCollider2D _eventReceiver2D;
        public BoxCollider2D EventReceiver => _eventReceiver2D;

        private void Reset()
        {
            _targetGraphic = GetComponent<SpriteRenderer>();
            _eventReceiver2D = GetComponent<BoxCollider2D>();
        }
    }
}
