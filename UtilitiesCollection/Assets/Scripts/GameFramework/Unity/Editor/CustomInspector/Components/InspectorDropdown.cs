namespace GameFramework.CustomEditor
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;
    using GameFramework.CustomAttribute;
    using System.Collections;

    public class InspectorDropdown : ICustomInspectorDrawer
    {
        protected class DropdownInfo
        {
            public int Current;
            public string FieldName;
            public IEnumerable Values;
            public Action<int> OnValueChanged;
        }

        protected MonoBehaviour targetBehaviour = null;
        protected List<DropdownInfo> actions = null;

        public int CompareTo(object obj)
        {
            if (obj != null)
            {
                ICustomInspectorDrawer drawer = obj as ICustomInspectorDrawer;
                if (drawer != null)
                {
                    return ((int)drawer.GetComponentType()).CompareTo((int)this.GetComponentType());
                }
            }
            return 1;
        }

        public void DrawInspectorGUI()
        {
            if (actions == null || actions.Count == 0)
            {
                return;
            }
            GUILayout.BeginVertical();
            for (int i = 0; i < actions.Count; i++)
            {
                DrawOneDropdown(actions[i], i);
            }
            GUILayout.EndVertical();
        }

        public EInspectorComponent GetComponentType() => EInspectorComponent.InspectorDropdown;

        public void Reload(UnityEngine.Object target)
        {
            targetBehaviour = target as MonoBehaviour;
            if (actions == null)
            {
                actions = new List<DropdownInfo>();
            }
            actions.Clear();

            BindingFlags allMembers =
                                BindingFlags.Instance |
                                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

            Type targetType = targetBehaviour.GetType();
            FieldInfo[] fieldInfos = targetType.GetFields(allMembers);

            for (int i = 0; i < fieldInfos.Length; i++)
            {
                InspectorDropdownAttribute att = fieldInfos[i].GetCustomAttribute(typeof(InspectorDropdownAttribute)) as InspectorDropdownAttribute;
                if (att != null)
                {
                    // Currently, don't support parameter for inspector button
                    object field = fieldInfos[i].GetValue(target);
                    Debug.Log(field);
                    if (field is IEnumerable)
                    {
                        actions.Add(new DropdownInfo()
                        {
                            Current = 0,
                            FieldName = fieldInfos[i].Name,
                            Values = fieldInfos[i].GetValue(target) as IEnumerable,
                            OnValueChanged = att.OnValueChanged,
                        });
                    }
                }
            }
        }
        private static readonly Rect rect = new Rect(0, 0, 200, 20);
        private void DrawOneDropdown(DropdownInfo info, int index)
        {
            GUILayout.BeginHorizontal();
            List<string> items = new List<string>();
            IEnumerator enumerator = info.Values.GetEnumerator();
            while(enumerator.MoveNext())
            {
                items.Add("" + enumerator.Current);
            }
            GUILayout.Label(info.FieldName);
            info.Current = UnityEditor.EditorGUI.Popup(new Rect(0.0f, 50.0f + index * 25.0f, 200, 20), info.Current, items.ToArray());
            GUILayout.EndHorizontal();
        }
    }
}
