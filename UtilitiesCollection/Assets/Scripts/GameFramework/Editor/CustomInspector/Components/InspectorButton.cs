namespace GameFramework.CustomEditor.Components
{
	using System;
	using UnityEngine;
	using System.Reflection;
	using System.Collections.Generic;
	using UnityEditor;

	public class InspectorButton : ICustomInspectorDrawer
	{
		protected struct ButtonAction
		{
			public MethodInfo methodInfo;
			public InspectorButtonAttribute attribute;
		}

		protected MonoBehaviour targetBehaviour = null;

		protected int MAX_ITEM_PER_ROW = 3;

		protected bool _isFoldout = false;
		protected List<ButtonAction> actions = null;
		private Vector2 _btnScrollViewPosition = Vector2.zero;

		public EInspectorComponent GetComponentType() => EInspectorComponent.InspectorButton;

		public void DrawInspectorGUI()
		{
			_isFoldout = EditorGUILayout.Foldout(_isFoldout, "Inspector Buttons");
			if (_isFoldout)
			{
				if (actions != null && actions.Count > 0)
				{
					_btnScrollViewPosition = GUILayout.BeginScrollView(_btnScrollViewPosition, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

					GUILayout.BeginVertical();
					int itemPerRow = 0;
					for (int i = 0; i < actions.Count; i++)
					{
						if (itemPerRow == 0)
						{
							GUILayout.BeginHorizontal();
						}

						ButtonAction act = actions[i];
						if (GUILayout.Button(act.attribute.ButtonName, GUILayout.Width(100), GUILayout.Height(25)))
						{
							act.methodInfo.Invoke(targetBehaviour, null);
						}
						itemPerRow++;

						if ((itemPerRow == MAX_ITEM_PER_ROW) || (i == actions.Count - 1))
						{
							itemPerRow = 0;
							GUILayout.EndHorizontal();
						}

					}

					GUILayout.EndVertical();
					GUILayout.EndScrollView();
				}
			}
		}

		public void Reload(UnityEngine.Object target)
		{
			targetBehaviour = target as MonoBehaviour;
			if (actions == null)
			{
				actions = new List<ButtonAction>();
			}
			actions.Clear();

			BindingFlags allMembers =
								BindingFlags.Static | BindingFlags.Instance |
								BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

			Type targetType = targetBehaviour.GetType();
			MethodInfo[] methodInfos = targetType.GetMethods(allMembers);

			for (int i = 0; i < methodInfos.Length; i++)
			{
				InspectorButtonAttribute att = methodInfos[i].GetCustomAttribute(typeof(InspectorButtonAttribute)) as InspectorButtonAttribute;
				if (att != null)
				{
					// Currently, don't support parameter for inspector button
					ParameterInfo[] paramInfos = methodInfos[i].GetParameters();
					if (paramInfos == null || paramInfos.Length == 0)
					{
						actions.Add(new ButtonAction()
						{
							methodInfo = methodInfos[i],
							attribute = att,
						});
					}
				}
			}
		}

		public int CompareTo(object obj)
		{
			if (obj != null)
			{
				ICustomInspectorDrawer drawer = obj as ICustomInspectorDrawer;
				if(drawer != null)
				{
					return ((int)drawer.GetComponentType()).CompareTo((int)this.GetComponentType());
				}
			}
			return 1;
		}
	}

}