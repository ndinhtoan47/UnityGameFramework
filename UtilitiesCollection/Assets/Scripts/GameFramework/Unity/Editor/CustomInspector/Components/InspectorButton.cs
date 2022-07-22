namespace GameFramework.Unity.Editor.Components
{
    using System;
    using UnityEngine;
    using UnityEditor;
    using System.Reflection;
    using System.Collections.Generic;
    using GameFramework.CustomAttribute;
    using GameFramework.Unity.Editor.CustomInspector;

    public class InspectorButton : ICustomInspectorDrawer
    {
        private struct ButtonAction
        {
            public string ButtonName;
            public MethodInfo MethodInfo;
            public InspectorButtonAttribute Attribute;
        }

        private bool _isFoldout = false;
        private Vector2 _scrollViewPosition = Vector2.zero;

        private readonly int MaxItemPerRow = 3;
        private List<ButtonAction> _btnActions = null;
        private MonoBehaviour _targetBehaviour = null;

        public EInspectorComponent GetComponentType() => EInspectorComponent.InspectorButton;

        public void DrawInspectorGUI()
        {
            if (_btnActions == null || _btnActions.Count == 0)
            {
                return;
            }

            _isFoldout = EditorGUILayout.Foldout(_isFoldout, "Inspector Buttons");
            if (_isFoldout)
            {
                if (_btnActions != null && _btnActions.Count > 0)
                {
                    _scrollViewPosition = GUILayout.BeginScrollView(_scrollViewPosition, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

                    GUILayout.BeginVertical();
                    int itemPerRow = 0;
                    for (int i = 0; i < _btnActions.Count; i++)
                    {
                        if (itemPerRow == 0)
                        {
                            GUILayout.BeginHorizontal();
                        }

                        ButtonAction act = _btnActions[i];
                        if (GUILayout.Button(act.ButtonName, GUILayout.Width(100), GUILayout.Height(25)))
                        {
                            act.MethodInfo.Invoke(_targetBehaviour, null);
                        }
                        itemPerRow++;

                        if ((itemPerRow == MaxItemPerRow) || (i == _btnActions.Count - 1))
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
            _targetBehaviour = target as MonoBehaviour;
            if (_btnActions == null)
            {
                _btnActions = new List<ButtonAction>();
            }
            _btnActions.Clear();

            BindingFlags allMembers =
                                BindingFlags.Static | BindingFlags.Instance |
                                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

            Type targetType = _targetBehaviour.GetType();
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
                        ButtonAction btnAction = new ButtonAction()
                        {
                            MethodInfo = methodInfos[i],
                            Attribute = att,
                            ButtonName = att.ButtonName
                        };
                        if (string.IsNullOrEmpty(att.ButtonName))
                        {
                            btnAction.ButtonName = methodInfos[i].Name;
                        }
                        _btnActions.Add(btnAction);
                    }
                }
            }
        }

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
    }

}