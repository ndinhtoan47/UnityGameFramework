namespace GameFramework.Pattern
{
    public class ColliderEventTrigger2D : ColliderEventTrigger
    {        
        public event System.Action<UnityEngine.Collider2D> OnTriggerEnter_2D;
        public event System.Action<UnityEngine.Collider2D> OnTriggerExit_2D;

        private void OnTriggerEnter2D(UnityEngine.Collider2D other)
        {
            OnTriggerEnter_2D?.Invoke(other);
        }

        private void OnTriggerExit2D(UnityEngine.Collider2D other)
        {
            OnTriggerExit_2D?.Invoke(other);
        }

        public override void SetInteractable(bool en)
        {
            UnityEngine.Collider2D col = GetComponent<UnityEngine.Collider2D>();
            if (col)
            {
                col.enabled = en;
            }
        }

        public override void ReleaseEvents()
        {
            base.ReleaseEvents();
            OnTriggerEnter_2D = null;
            OnTriggerExit_2D = null;
        }
    }
}
