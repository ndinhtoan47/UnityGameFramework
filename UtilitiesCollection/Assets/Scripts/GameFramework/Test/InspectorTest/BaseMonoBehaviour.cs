using UnityEngine;

public class BaseMonoBehaviour : MonoBehaviour
{
	[GameFramework.CustomEditor.InspectorButton(nameof(Function1))]
	public virtual void Function1()
	{
		Debug.Log("Base Class Function 1");
	}

	[GameFramework.CustomEditor.InspectorButton(nameof(Function2))]
	public virtual void Function2()
	{
		Debug.Log("Base Class Function 2");
	}

	[GameFramework.CustomEditor.InspectorButton(nameof(Function3))]
	public virtual void Function3()
	{
		Debug.Log("Base Class Function 3");
	}

	[GameFramework.CustomEditor.InspectorButton(nameof(Function4))]
	public virtual void Function4()
	{
		Debug.Log("Base Class Function 4");
	}
}
