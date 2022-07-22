namespace GameFramework.Unity.Editor.Components

{
    using System;
    using UnityEditor;
    using UnityEngine;
    using System.Reflection;
    using System.Collections;
    using System.Collections.Generic;
    using GameFramework.CustomAttribute;
    using GameFramework.Unity.Editor.CustomInspector;

    public class InspectorDropdown : ICustomInspectorDrawer
    {
        private class DropdownInfo
        {
            public int Current;
            public string[] DropdownValues;
            public MethodInfo OnValueChanged;

            public readonly string FieldName;
            public readonly IEnumerable Values;

            public DropdownInfo(string fieldName, IEnumerable values)
            {
                Current = 0;
                Values = values;                
                FieldName = SplitCamelCase(fieldName);
                DropdownValues = InspectorDropdown.CreateDropdownValues(values).ToArray();
            }
        }
        private struct DropdownGUIDefine
        {
            // Dropdown
            public Vector2 DDSize;
            public float DDLineSpace;
            public float DDIndentation;

            // Label
            public Vector2 LBSize;

            // Reset btn
            public Vector2 BTResetDD;
        }

        private readonly string[] NoneArray = new string[] { "None" };
        private readonly DropdownGUIDefine DDGUIDefine = new DropdownGUIDefine()
        {
            DDSize = new Vector2(200.0f, 20.0f),
            DDLineSpace = 5.0f,
            DDIndentation = 80.0f,

            LBSize = new Vector2(100.0f, 20.0f),
            BTResetDD = new Vector2(50.0f, 20.0f),
        };

        private bool _isFoldout = false;
        private Vector2 _scrollViewPosition = Vector2.zero;

        private MonoBehaviour _targetBehaviour = null;
        private List<DropdownInfo> _dropdownInfos = null;

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
            if (_dropdownInfos == null || _dropdownInfos.Count == 0)
            {
                return;
            }

            _isFoldout = EditorGUILayout.Foldout(_isFoldout, "Inspector Dropdowns");
            if (_isFoldout)
            {
                GUILayout.BeginScrollView(_scrollViewPosition, GUILayout.Width(300.0f), GUILayout.ExpandHeight(true));
                GUILayout.BeginVertical();
                for (int i = 0; i < _dropdownInfos.Count; i++)
                {
                    DropdownInfo info = _dropdownInfos[i];
                    int lastIndex = info.Current;
                    int selectedIndex = DrawOneDropdown(info, i);
                    if(lastIndex != selectedIndex && info.OnValueChanged != null)
                    {
                        info.Current = selectedIndex;
                        info.OnValueChanged.Invoke(_targetBehaviour, new object[] { selectedIndex });
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndScrollView();
            }
        }

        public EInspectorComponent GetComponentType() => EInspectorComponent.InspectorDropdown;

        public void Reload(UnityEngine.Object target)
        {
            _targetBehaviour = target as MonoBehaviour;
            if (_dropdownInfos == null)
            {
                _dropdownInfos = new List<DropdownInfo>();
            }
            _dropdownInfos.Clear();

            BindingFlags allMembers =
                                BindingFlags.Instance |
                                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

            Type targetType = _targetBehaviour.GetType();
            FieldInfo[] fieldInfos = targetType.GetFields(allMembers);

            BindingFlags callbackMethods = BindingFlags.Instance | BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.NonPublic;
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                InspectorDropdownAttribute att = fieldInfos[i].GetCustomAttribute(typeof(InspectorDropdownAttribute)) as InspectorDropdownAttribute;
                if (att != null)
                {
                    // Currently, don't support parameter for inspector button
                    object field = fieldInfos[i].GetValue(target);
                    if (field is IEnumerable)
                    {
                        DropdownInfo info = new DropdownInfo(fieldInfos[i].Name, fieldInfos[i].GetValue(target) as IEnumerable);
                        if (!string.IsNullOrEmpty(att.OnValueChanged))
                        {
                            info.OnValueChanged = targetType.GetMethod(att.OnValueChanged, callbackMethods); 
                        }
                        _dropdownInfos.Add(info);
                    }
                }
            }
        }

        private int DrawOneDropdown(DropdownInfo info, int index)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset", GUILayout.Width(DDGUIDefine.BTResetDD.x), GUILayout.Height(DDGUIDefine.BTResetDD.y)))
            {
                Reload(_targetBehaviour);
            }
            GUILayout.Label(info.FieldName, GUILayout.Width(DDGUIDefine.LBSize.x));

            string[] items = info.DropdownValues.Length > 0 ? info.DropdownValues : NoneArray;

            Rect ddRect = new Rect(DDGUIDefine.LBSize.x + DDGUIDefine.DDIndentation, index * (DDGUIDefine.DDSize.y + DDGUIDefine.DDLineSpace), DDGUIDefine.DDSize.x, DDGUIDefine.DDSize.y);
            info.Current = EditorGUI.Popup(
                ddRect,
                info.Current,
                items);

            GUILayout.EndHorizontal();
            return info.Current;
        }
        
        private static List<string> CreateDropdownValues(IEnumerable enumerable)
        {
            List<string> items = new List<string>();
            IEnumerator enumerator = enumerable.GetEnumerator();
            int count = 0;
            while (enumerator.MoveNext())
            {
                items.Add(count + " :: " + enumerator.Current);
                count++;
            }
            return items;
        }

        public static string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }
    }
}
