namespace GameFramework.CustomEditor
{
	using UnityEditor;
	using UnityEngine;
	using System.Collections.Generic;
	using GameFramework.CustomEditor.Components;

	public enum EInspectorComponent
	{
		InspectorButton = 1,
		InspectorDropdown = 2,
    }


	[CustomEditor(typeof(MonoBehaviour), true)]
	public class MonoBehaviourInsp : Editor
	{

		protected MonoBehaviour targetBehaviour = null;

		private bool _reloadInternal = true;
		protected bool isDrawDefault = true;
		protected List<ICustomInspectorDrawer> drawers;


		private void Draw()
		{
			if (drawers != null)
			{
				for (int i = 0; i < drawers.Count; i++)
				{
					drawers[i].DrawInspectorGUI();
				}
			}
		}
		private bool Validate()
		{
			if (targetBehaviour == null)
			{
				targetBehaviour = this.target as MonoBehaviour;
			}

			if (_reloadInternal)
			{
				OnInternalReload();
			}

			return targetBehaviour != null;
		}

		protected virtual void OnInternalReload()
		{
			RegisterComponents();
			for (int i = 0; i < drawers.Count; i++)
			{
				drawers[i].Reload(targetBehaviour);
			}
			_reloadInternal = false;
		}
		protected virtual void OnPreDraw() { }
		protected virtual void OnPostDraw() { }
		protected virtual void RegisterComponents()
		{
			RegisterComponent(new InspectorButton());
            RegisterComponent(new InspectorDropdown());
		}
		protected void RegisterComponent(ICustomInspectorDrawer drawer)
		{
			if (drawers == null)
			{
				drawers = new List<ICustomInspectorDrawer>();
			}
			if (drawers.FindIndex(d => d.GetComponentType() == drawer.GetComponentType()) < 0)
			{
				drawers.Add(drawer);
				drawers.Sort();
			}
		}

		// Unity methods
		protected virtual void OnEnable()
		{
			_reloadInternal = true;
			this.Validate();
		}

		public override void OnInspectorGUI()
		{
			if (isDrawDefault)
			{
				base.OnInspectorGUI();
			}

			if (Validate())
			{
				OnPreDraw();
				Draw();
				OnPostDraw();
			}
		}

		// -- Unity methods
	}
}
