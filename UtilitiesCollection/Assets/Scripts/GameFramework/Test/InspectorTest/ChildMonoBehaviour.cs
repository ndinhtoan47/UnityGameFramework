namespace GameFramework.Test
{
    using UnityEngine;

    public class ChildMonoBehaviour : BaseMonoBehaviour
    {
        [GameFramework.CustomAttribute.InspectorButton]
        public void OneChildButton()
        {
            Debug.Log("Call From Child Function");
        }

        public override void Function1()
        {
            Debug.Log("Function1 overrided");
        }
    }
}
