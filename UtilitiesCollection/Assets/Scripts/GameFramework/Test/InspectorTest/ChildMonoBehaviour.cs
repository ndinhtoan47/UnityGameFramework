using UnityEngine;

public class ChildMonoBehaviour : BaseMonoBehaviour
{
	[GameFramework.CustomAttribute.InspectorButton(nameof(OneChildButton))]
	public void OneChildButton()
	{
		Debug.Log("Call From Child Function");
	}

	public override void Function1()
	{
		Debug.Log("Function1 overrided");
	}
}
