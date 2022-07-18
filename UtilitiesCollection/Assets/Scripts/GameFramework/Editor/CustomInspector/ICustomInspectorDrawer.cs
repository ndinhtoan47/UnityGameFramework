namespace GameFramework.CustomEditor
{
	public interface ICustomInspectorDrawer : System.IComparable
	{
		public EInspectorComponent GetComponentType();
		public void Reload(UnityEngine.Object target);
		public void DrawInspectorGUI();
	}
}