namespace GameFramework.Utilities
{
	using System.Collections.Generic;

	// Types

	using Color = UnityEngine.Color;
	using Vector3 = UnityEngine.Vector3;
	using Gizmos = UnityEngine.Gizmos;

	// -- Types

	public class GmLineDrawer : IGizmosDrawer
	{
		public bool IsClosed = false;

		public Color LineColor = Color.green;
		public Color PointColor = Color.red;

		public float PointSize = 0.2f;
		public bool IsDrawPoints = true;

		public List<Vector3> Points { get; private set; } = new List<Vector3>();

		public void Draw()
		{
			if (Points == null || Points.Count < 2)
			{
				return;
			}
			Gizmos.color = LineColor;
			Vector3 lastPoint = Points[0];
			
			if (IsDrawPoints)
			{
				Gizmos.color = PointColor;
				Gizmos.DrawSphere(lastPoint, PointSize);
			}

			for (int i = 1; i < Points.Count; i++)
			{
				Vector3 curPoint = Points[i];

				Gizmos.color = LineColor;
				Gizmos.DrawLine(lastPoint, curPoint);

				if (IsDrawPoints)
				{
					Gizmos.color = PointColor;
					Gizmos.DrawSphere(curPoint, PointSize);
				}

				lastPoint = curPoint;
			}

			if (IsClosed && Points.Count >= 3)
			{
				Gizmos.color = LineColor;
				Gizmos.DrawLine(lastPoint, Points[0]);
			}
		}
	}
}