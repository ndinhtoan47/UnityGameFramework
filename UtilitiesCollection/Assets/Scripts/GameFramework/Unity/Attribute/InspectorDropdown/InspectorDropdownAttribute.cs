namespace GameFramework.CustomAttribute
{
    public class InspectorDropdownAttribute : UnityEngine.PropertyAttribute
    {
        /// <summary>
        /// Callback name is delegate System.Action<int>
        /// </summary>
        public string OnValueChanged;

        public InspectorDropdownAttribute()
        {

        }
    }
}
