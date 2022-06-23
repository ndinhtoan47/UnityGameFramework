namespace GameFramework.Grid
{
	using UnityEngine;
	using GameFramework.CustomEditor;

	/// <summary>
	/// Grid snap
	/// Readme: All parameters should be interger to prevent float pointer issue
	/// </summary>
	public class GameGrid2D : MonoBehaviour
	{
		public int Width = 1;
		public int Height = 1;
		public int SizeOfCell = 1;
		public bool ZAxisIsPlane = true;
		public bool IsCachePoints = false;

		/// <summary>
		/// Scalar to make a scale on the Grid
		/// </summary>
		[Range(0.1f, 1.0f)]
		public float Scalar = 1.0f;

		[ReadOnly]
		[SerializeField] private Vector3[] _points;

		[ReadOnly]
		[SerializeField] private int _heightCount;

		[ReadOnly]
		[SerializeField] private int _widthCount;

		private Vector3 _minPosition = Vector3.zero;

		public bool IsInitalized { get; private set; } = false;
		
		public Vector3 this[int x, int zOrY]
		{
			get
			{
				if (IsCachePoints)
				{
					return _points[x * _heightCount + zOrY];
				}
				else
				{
					return GetPosition(x, zOrY);
				}
			}
			private set
			{
				_points[x * _heightCount + zOrY] = value;
			}
		}

		private void Awake()
		{
			Initalize();
		}

		public void Initalize()
		{
			if (!IsInitalized)
			{
				IsInitalized = true;
				_minPosition = transform.position;
			}
		}

		////Uncomment this block to test result
		
		//private void Update()
		//{
		//	the camera should be perpendicular with y Or z axis
		//	Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//	Debug.Log(Snap(worldPoint));
		//}

		public Vector3 Snap(Vector3 point)
		{
			if (SizeOfCell == 0 || (IsCachePoints && _points == null))
			{
				return Vector3.one * -1.0f;
			}

			float xMin = _minPosition.x;
			float zOrYMin = ZAxisIsPlane ? _minPosition.z : _minPosition.y;

			float deltaX = point.x - xMin;
			float deltaZOrY = (ZAxisIsPlane ? point.z : point.y) - zOrYMin;

			int x = Mathf.RoundToInt(deltaX / (SizeOfCell * Scalar));
			int z = Mathf.RoundToInt(deltaZOrY / (SizeOfCell * Scalar));

			x = Mathf.Clamp(x, 0, _widthCount - 1);
			z = Mathf.Clamp(z, 0, _heightCount - 1);

			return GetPosition(x, z);
		}
		
		public Vector2Int GetGridIndex(Vector3 point)
		{
			if (SizeOfCell == 0 || (IsCachePoints && _points == null))
			{
				return Vector2Int.one * -1;
			}
			if (Width == 0 || Height == 0 || SizeOfCell == 0 || Mathf.Approximately(Scalar, 0.0f))
			{
				return Vector2Int.one * -1;
			}

			float xMin = _minPosition.x;
			float zOrYMin = ZAxisIsPlane ? _minPosition.z : _minPosition.y;

			float deltaX = point.x - xMin;
			float deltaZOrY = (ZAxisIsPlane ? point.z : point.y) - zOrYMin;

			int x = Mathf.RoundToInt(deltaX / (SizeOfCell * Scalar));
			int z = Mathf.RoundToInt(deltaZOrY / (SizeOfCell * Scalar));

			x = Mathf.Clamp(x, 0, _widthCount - 1);
			z = Mathf.Clamp(z, 0, _heightCount - 1);
			return new Vector2Int(x, z);
		}
		
		public Vector3 GetPosition(int x, int y)
		{
			if (Width == 0 || Height == 0 || SizeOfCell == 0 || Mathf.Approximately(Scalar, 0.0f))
			{
				return Vector3.one * -1;
			}

			if (IsCachePoints)
			{
				Vector3 cachedPoint = this[x, y];

				float cachedX = cachedPoint.x;
				float cachedZOrY = ZAxisIsPlane ? cachedPoint.z : cachedPoint.y;

				if (!Mathf.Approximately(cachedX, 0.0f) || !Mathf.Approximately(cachedZOrY, 0.0f))
				{
					// I know in an expect case that is the cachedX = 0.0 and cachedZOrY = 0.0, this if statement won't work
					// then it will be always recalculated but it's fine for all other cases
					return this[x, y];
				}
			}

			float xPos = _minPosition.x + x * SizeOfCell * Scalar;
			float zOrYPos = (ZAxisIsPlane ? _minPosition.z : _minPosition.y) + y * SizeOfCell * Scalar;

			Vector3 result = Vector3.zero;
			if (ZAxisIsPlane)
			{
				result = new Vector3(xPos, 0.0f, zOrYPos);
			}
			else
			{
				result = new Vector3(xPos, zOrYPos, 0.0f);
			}

			if (IsCachePoints)
			{
				this[x, y] = result;
			}

			return result;
		}

#if UNITY_EDITOR

		[ContextMenu("Calculate Grid")]
		public void CalculateGrid()
		{
			if (Width == 0 || Height == 0 || SizeOfCell == 0 || Mathf.Approximately(Scalar, 0.0f))
			{
				_points = null;
				Debug.LogWarning("Calculate Grid Failed!");
				return;
			}


			_points = null;
			_widthCount = Width / SizeOfCell;
			_heightCount = Height / SizeOfCell;
			_minPosition = transform.position;

			if (IsCachePoints)
			{
				_points = new Vector3[_heightCount * _widthCount];
				for (int h = 0; h < _heightCount; h++)
				{
					for (int w = 0; w < _widthCount; w++)
					{
						this[w, h] = GetPosition(w, h);
					}
				}
			}
		}

		[Header("Editor Only"), Space(20)]
		public bool IsDrawGizmos = true;
		public Color GridPointColor = Color.blue;
		public Color BorderColor = Color.red;

		private void OnDrawGizmos()
		{
			if (!IsDrawGizmos || (IsCachePoints && _points == null)) 
			{ 
				return; 
			}

			Vector3 size = Vector2.zero;
			if (ZAxisIsPlane)
			{
				size = new Vector3(Width, SizeOfCell, Height) * Scalar;
			}
			else
			{
				size = new Vector3(Width, Height, SizeOfCell) * Scalar;
			}

			Vector3 offset = new Vector3(SizeOfCell, SizeOfCell, SizeOfCell) * Scalar;

			Color curColor = Gizmos.color;

			// Draw border
			Gizmos.color = BorderColor;
			Gizmos.DrawWireCube(transform.position + (size - offset) * 0.5f, size);

			// Draw points
			Gizmos.color = GridPointColor;
			float sphereSize = 0.2f * Scalar;

			int length = _widthCount * _heightCount;
			int largestSide = _widthCount > _heightCount ? _widthCount : _heightCount;
			for (int i = 0; i < length; i++)
			{
				Vector3 p = Vector3.zero;
				if (IsCachePoints)
				{
					p = _points[i];
				}
				else
				{
					int x = i / largestSide;
					int y = i % largestSide;

					p = GetPosition(x, y);
				}
				if (ZAxisIsPlane)
				{
					p.y = transform.position.y;
				}
				else
				{
					p.z = transform.position.z;
				}
				Gizmos.DrawSphere(p, sphereSize);
			}

			Gizmos.color = curColor;
		}

		private void OnValidate()
		{
			CalculateGrid();
		}
#endif
	}
}
