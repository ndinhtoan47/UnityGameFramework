namespace GameFramework.Test
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GameFramework.CustomAttribute;

    [RequireComponent(typeof(Camera))]
    public class CameraFrus : MonoBehaviour
    {
        [InspectorDropdown(OnValueChanged = nameof(OnTestArrayChangeIndex))]
        public Vector3[] testArray;

        [InspectorDropdown]
        public List<Vector3> testList;

        private void OnTestArrayChangeIndex(int index)
        {
            Debug.Log(index);
        }

        public string testString;

        private new Camera camera;

        [InspectorButton]
        private void Awake()
        {
            camera = GetComponent<Camera>();
        }
        void Update()
        {
            // this example shows the different camera frustums when using asymmetric projection matrices (like those used by OpenVR).

            Vector3[] frustumCorners = new Vector3[4];
            camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), camera.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

            for (int i = 0; i < 4; i++)
            {
                var worldSpaceCorner = camera.transform.TransformVector(frustumCorners[i]);
                Debug.DrawRay(camera.transform.position, worldSpaceCorner, Color.blue);
            }

            camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), camera.farClipPlane, Camera.MonoOrStereoscopicEye.Left, frustumCorners);

            for (int i = 0; i < 4; i++)
            {
                var worldSpaceCorner = camera.transform.TransformVector(frustumCorners[i]);
                Debug.DrawRay(camera.transform.position, worldSpaceCorner, Color.green);
            }

            camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), camera.farClipPlane, Camera.MonoOrStereoscopicEye.Right, frustumCorners);

            for (int i = 0; i < 4; i++)
            {
                var worldSpaceCorner = camera.transform.TransformVector(frustumCorners[i]);
                Debug.DrawRay(camera.transform.position, worldSpaceCorner, Color.red);
            }
        }
    }
}
