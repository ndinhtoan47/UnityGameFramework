namespace GameFramework.CustomAttribute
{
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]

    public class InspectorDropdownAttribute : System.Attribute
    {
        public System.Action<int> OnValueChanged;

        public InspectorDropdownAttribute()
        {

        }
    }
}
