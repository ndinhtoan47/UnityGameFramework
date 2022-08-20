namespace GameFramework.Unity
{
    using UnityEngine;

    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(BoxCollider))]
    public class NoneUI3DButton : Pattern.ColliderEventTrigger3D
    {
        [SerializeField] private MeshFilter _targetGraphic;
        public MeshFilter TargetGraphic => _targetGraphic;

        [SerializeField] private BoxCollider _eventReceiver2D;
        public BoxCollider EventReceiver => _eventReceiver2D;

        private void Reset()
        {
            _targetGraphic = GetComponent<MeshFilter>();
            _eventReceiver2D = GetComponent<BoxCollider>();
        }
    }
}
