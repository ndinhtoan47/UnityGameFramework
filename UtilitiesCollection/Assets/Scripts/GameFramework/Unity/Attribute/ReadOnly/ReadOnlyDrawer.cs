namespace GameFramework.CustomEditor
{
	/// <summary>
	/// This class contain custom drawer for ReadOnly attribute.
	/// </summary>
	[UnityEditor.CustomPropertyDrawer(typeof(GameFramework.CustomAttribute.ReadOnlyAttribute))]
    public class ReadOnlyDrawer : UnityEditor.PropertyDrawer
    {
        /// <summary>
        /// Unity method for drawing GUI in Editor
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="property">Property.</param>
        /// <param name="label">Label.</param>
        public override void OnGUI(UnityEngine.Rect position, UnityEditor.SerializedProperty property, UnityEngine.GUIContent label)
        {
            // Saving previous GUI enabled value
            bool previousGUIState = UnityEngine.GUI.enabled;

            // Disabling edit for property
            UnityEngine.GUI.enabled = false;

            // Drawing Property
            UnityEditor.EditorGUI.PropertyField(position, property, label);

            // Setting old GUI enabled value
            UnityEngine.GUI.enabled = previousGUIState;
        }
    }
}