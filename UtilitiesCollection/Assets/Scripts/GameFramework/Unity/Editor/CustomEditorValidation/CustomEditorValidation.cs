using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameFramework.CustomAttribute;

[InitializeOnLoad]
public class CustomEditorValidation
{
    static CustomEditorValidation()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            // AttributeValidator.Validate();
        }
    }
}
