namespace GameFramework.Test
{
    using UnityEngine;

    public class BaseMonoBehaviour : MonoBehaviour
    {
        public Vector3 TestField = Vector3.zero;

        [GameFramework.CustomAttribute.InspectorButton]
        public virtual void Function1()
        {
            Debug.Log("Base Class Function 1");
        }

        [GameFramework.CustomAttribute.InspectorButton]
        public virtual void Function2()
        {
            Debug.Log("Base Class Function 2");
        }

        [GameFramework.CustomAttribute.InspectorButton]
        public virtual void Function3()
        {
            Debug.Log("Base Class Function 3");
        }

        [GameFramework.CustomAttribute.InspectorButton(ButtonName = "Custom name Btn")]
        public virtual void Function4()
        {
            Debug.Log("Base Class Function 4");
        }
    }
}
