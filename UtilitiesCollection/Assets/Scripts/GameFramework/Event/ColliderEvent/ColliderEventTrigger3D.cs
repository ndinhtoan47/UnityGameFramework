namespace GameFramework.Pattern
{
    public class ColliderEventTrigger3D : ColliderEventTrigger
    {        
        public event System.Action<UnityEngine.Collider> OnTriggerEnter_3D;
        public event System.Action<UnityEngine.Collider> OnTriggerExit_3D;

        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            OnTriggerEnter_3D?.Invoke(other);
        }

        private void OnTriggerExit(UnityEngine.Collider other)
        {
            OnTriggerExit_3D?.Invoke(other);
        }

        public override void SetInteractable(bool en)
        {
            UnityEngine.Collider col = GetComponent<UnityEngine.Collider>();
            if (col)
            {
                col.enabled = en;
            }
        }

        public override void ReleaseEvents()
        {
            base.ReleaseEvents();
            OnTriggerEnter_3D = null;
            OnTriggerExit_3D = null;
        }
    }
}
