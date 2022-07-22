namespace GameFramework.CustomAttribute
{

	[System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class InspectorButtonAttribute : System.Attribute
	{
		public string ButtonName = null;
		public InspectorButtonAttribute()
		{

		}
	}
}
