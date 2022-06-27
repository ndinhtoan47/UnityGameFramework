namespace GameFramework.Utilities
{
	using System.Collections.Generic;

	// Types

	using Color		= UnityEngine.Color;
	using Vector3	= UnityEngine.Vector3;
	using Gizmos	= UnityEngine.Gizmos;

	// -- Types


	public class GmPointDrawer : IGizmosDrawer
	{
		public Color Color = Color.blue;
		public float Size = 0.2f;
		public List<Vector3> Points { get; private set; } = new List<Vector3>();

		public void RandomGizmosColor()
		{
			Color = UnityEngine.Random.ColorHSV();
		}

		public void Draw()
		{
			Gizmos.color = Color;
			for (int i = 0; i < Points.Count; i++)
			{
				Gizmos.DrawSphere(Points[i], Size);
			}
		}
	}
}