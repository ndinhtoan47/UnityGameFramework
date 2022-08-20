namespace GameFramework.Unity.UI
{
    using GameFramework.Common;

	[UnityEngine.RequireComponent(typeof(UnityEngine.UI.Image))]
	public class ImageButton : CustomEventTrigger
	{
        [UnityEngine.SerializeField] private UnityEngine.UI.Image _targetGraphic;
        public UnityEngine.UI.Image TargetGraphic => _targetGraphic;

        protected override void Reset()
        {
            base.Reset(); 
            _targetGraphic = GetComponent<UnityEngine.UI.Image>();
        }
    }
}