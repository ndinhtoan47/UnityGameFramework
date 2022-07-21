namespace GameFramework.CustomAttribute
{
    using UnityEditor.Compilation;
    using GameFramework.Utilities;
    using System;
    using System.Linq;

    public sealed class AttributeValidator
    {
        public readonly static string[] Assamblies = new string[]
        {
            "GameFramework",
            "Assembly-CSharp-firstpass",
        };
        public static void Validate()
        {
            System.Reflection.Assembly[] a = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < a.Length; i++)
            {
                UnityEngine.Debug.Log(string.Join("\n", a[i].GetTypes().ToList()));
            }

            Assembly[] assemblies = CompilationPipeline.GetAssemblies(AssembliesType.Player);
            for (int i = 0; i < assemblies.Length; i++)
            {
                break;
                if (Assamblies.FindIndex(assemblies[i].name) >= 0)
                {
                    UnityEngine.Debug.Log(assemblies[i].outputPath);
                    UnityEngine.Debug.Log(string.Join("\n", assemblies[i].sourceFiles));
                }
            }
        }
    }
}
