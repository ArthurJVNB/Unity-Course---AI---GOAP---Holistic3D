using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

namespace Project.AI
{
	public class NavMeshSurfaceUpdater : MonoBehaviour
	{
		[SerializeField] private List<NavMeshSurface> _surfaces = new();
		[SerializeField] private bool _onEnable;

		private void OnEnable()
		{
			if (_onEnable)
				UpdateSurfaces();
		}

		public void UpdateSurfaces()
		{
			if (_surfaces.Count == 0) return;
			foreach (NavMeshSurface surface in _surfaces)
			{
				if (surface.navMeshData)
					surface.UpdateNavMesh(surface.navMeshData);
				else
					surface.BuildNavMesh();
			}
				
		}
	}
}
