namespace GameFramework.CustomEditor
{

	[System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class InspectorButtonAttribute : System.Attribute
	{
		public readonly string ButtonName = null;
		public InspectorButtonAttribute(string btnName)
		{
			this.ButtonName = btnName;
		}
	}
}
