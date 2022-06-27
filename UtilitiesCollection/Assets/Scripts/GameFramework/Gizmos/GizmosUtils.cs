namespace GameFramework.Utilities
{	
	using UnityEngine;
	using System.Collections.Generic;

	public class GizmosUtils : Pattern.MonoSingleton<GizmosUtils>
	{
		private List<IGizmosDrawer> _drawers = new List<IGizmosDrawer>();

		private void OnDrawGizmos()
		{
			if (gameObject == null) { return; }

			Color curGmColor = Gizmos.color;

			DrawAll();

			Gizmos.color = curGmColor;
		}

		private void DrawAll()
		{
			if (_drawers == null) { return; }

			for (int i = 0; i < _drawers.Count; i++)
			{
				if (_drawers[i] != null)
				{
					_drawers[i].Draw();
				}
			}
		}

		public bool RemoveDrawer(IGizmosDrawer drawer)
		{
			for (int i = 0; i < _drawers.Count; i++)
			{
				if (_drawers[i] == drawer)
				{
					_drawers.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		public void AddDrawer(IGizmosDrawer drawer)
		{
			if (drawer == null) { return; }

			if (!_drawers.Contains(drawer))
			{
				_drawers.Add(drawer);
			}
		}
	}
}