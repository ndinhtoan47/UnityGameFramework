using UnityEngine;

public class BaseMonoBehaviour : MonoBehaviour
{
    [GameFramework.CustomAttribute.PropertyType(typeof(MonoBehaviour))]
    public Vector3 TestField = Vector3.zero;

	[GameFramework.CustomAttribute.InspectorButton(nameof(Function1))]
	public virtual void Function1()
	{
		Debug.Log("Base Class Function 1");
	}

	[GameFramework.CustomAttribute.InspectorButton(nameof(Function2))]
	public virtual void Function2()
	{
		Debug.Log("Base Class Function 2");
	}

	[GameFramework.CustomAttribute.InspectorButton(nameof(Function3))]
	public virtual void Function3()
	{
		Debug.Log("Base Class Function 3");
	}

	[GameFramework.CustomAttribute.InspectorButton(nameof(Function4))]
	public virtual void Function4()
	{
		Debug.Log("Base Class Function 4");
	}
}
