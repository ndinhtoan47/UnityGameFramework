namespace GameFramework.CustomEditor
{
	public interface ICustomInspectorDrawer : System.IComparable
	{
		EInspectorComponent GetComponentType();
		void Reload(UnityEngine.Object target);
		void DrawInspectorGUI();
	}
}