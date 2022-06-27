namespace GameFramework.Utilities
{
	using System.Collections.Generic;

	// Types

	using Color		= UnityEngine.Color;
	using Ray		= UnityEngine.Ray;
	using Gizmos	= UnityEngine.Gizmos;

	// -- Types

	public class GmRayDrawer : IGizmosDrawer
	{
		public Color Color = Color.blue;
		public List<Ray> Rays { get; private set; } = new List<Ray>();

		public void Draw()
		{
			if(Rays == null || Rays.Count == 0)
			{
				return;
			}
			Gizmos.color = Color;
			for (int i = 0; i < Rays.Count; i++)
			{
				Gizmos.DrawRay(Rays[i]);
			}
		}
	}

}