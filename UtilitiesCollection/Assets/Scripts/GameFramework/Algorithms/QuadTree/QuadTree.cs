namespace Algorithms
{
	using System.Collections.Generic;

	public struct RectInt
	{
		public int x;
		public int y;
		public int width;
		public int height;

		public RectInt(int inX, int inY, int inWidth, int inHeight)
		{
			x = inX;
			y = inY;
			width = inWidth;
			height = inHeight;
		}
	}

	public interface IQuadNode
	{
		public uint GetId();
		public RectInt GetRect();
	}

	public class QuadTree<QNode> where QNode : IQuadNode
	{
		private int MAX_OBJECT = 5; // use CalculateMaxObject
		private int QUAD_TREE_DEPTH = 5; // use CalculateTreeDepth

		private readonly int _depth;
		private readonly RectInt _bound;
		private readonly List<QNode> _nodes;
		private readonly QuadTree<QNode>[] _quadtrees;

		public static QuadTree<QNode> CreateQuadTree(int maxDepth, int maxObjects, RectInt bound)
		{
			QuadTree<QNode> quadtree = new QuadTree<QNode>(0, bound);
			quadtree.MAX_OBJECT = maxObjects;
			quadtree.QUAD_TREE_DEPTH = maxDepth;
			return quadtree;
		}

		private QuadTree(int depth, RectInt bound)
		{
			_depth = depth;
			_bound = bound;
			_nodes = new List<QNode>();
			_quadtrees = new QuadTree<QNode>[4];
		}

		/// <summary>
		/// Clear quadtree
		/// </summary>
		public void Clear()
		{
			_nodes.Clear();

			for (int i = 0; i < _quadtrees.Length; i++)
			{
				if (_quadtrees[i] != null)
				{
					_quadtrees[i].Clear();
					_quadtrees[i] = null;
				}
			}
		}

		/// <summary>
		/// Splits quadtree
		/// </summary>
		private void Split()
		{
			int halfWidth = _bound.width / 2;
			int halfHeight = _bound.height / 2;
			int x = _bound.x;
			int y = _bound.y;

			_quadtrees[0] = new QuadTree<QNode>(_depth + 1, new RectInt(x, y, halfWidth, halfHeight));
			_quadtrees[1] = new QuadTree<QNode>(_depth + 1, new RectInt(x + halfWidth, y, halfWidth, halfHeight));
			_quadtrees[2] = new QuadTree<QNode>(_depth + 1, new RectInt(x, y + halfHeight, halfWidth, halfHeight));
			_quadtrees[3] = new QuadTree<QNode>(_depth + 1, new RectInt(x + halfWidth, y + halfHeight, halfWidth, halfHeight));
		}

		public List<QNode> GetNodes()
		{
			return _nodes;
		}

		public bool HasChildren()
		{
			return _quadtrees[0] != null;
		}

		/// <summary>
		/// Check Node wheter inside quad or not
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public bool IsInBoundary(QNode node)
		{
			RectInt rect = node.GetRect();
			return !(rect.x > _bound.x + _bound.width ||
					 rect.x + rect.width < _bound.x ||
					 rect.y > _bound.y + _bound.height ||
					 rect.y + rect.height < _bound.y);
		}


		/// <summary>
		/// Check a point wheter inside quad or not
		/// </summary>
		/// <returns></returns>
		public bool IsPointInBoundary(int x, int y)
		{
			return (x >= _bound.x &&
					x <= _bound.x + _bound.width &&
					y >= _bound.y &&
					y <= _bound.y + _bound.height);
		}

		/// <summary>
		/// Insert Node
		/// </summary>
		/// <param name="node">Node will be inserted</param>
		public void Insert(QNode node)
		{
			if (HasChildren())
			{
				for (int i = 0; i < _quadtrees.Length; i++)
				{
					if (_quadtrees[i] != null && _quadtrees[i].IsInBoundary(node))
					{
						_quadtrees[i].Insert(node);
					}
				}
			}
			else
			{
				// add Node into current quadtree
				if (this.IsInBoundary(node))
				{
					_nodes.Add(node);
				}

				// split and move Node in list into it’s corresponding nodes
				if (_nodes.Count > MAX_OBJECT && _depth < QUAD_TREE_DEPTH)
				{
					this.Split();

					while (_nodes.Count > 0)
					{
						for (int i = 0; i < _quadtrees.Length; i++)
						{
							if (_quadtrees[i] != null && _quadtrees[i].IsInBoundary(_nodes[_nodes.Count - 1]))
							{
								_quadtrees[i].Insert(_nodes[_nodes.Count - 1]);
							}
						}

						_nodes.RemoveAt(_nodes.Count - 1);
					}
				}
			}
		}

		/// <summary>
		/// Get list of nodes can occur collision
		/// </summary>
		/// <param name="outList">Output list of Node exculude input Node</param>
		/// <param name="node">Input Node use to determine</param>
		public void Retrieve(List<QNode> outList, QNode node)
		{
			if (outList == null)
			{
				return;
			}

			if (_quadtrees != null)
			{
				for (int i = 0; i < _quadtrees.Length; i++)
				{
					if (_quadtrees[i] != null && _quadtrees[i].IsInBoundary(node))
					{
						_quadtrees[i].Retrieve(outList, node);
					}
				}
			}

			if (this.IsInBoundary(node))
			{
				foreach (QNode i in _nodes)
				{
					if (node.GetId() != i.GetId())
					{
						outList.Add(i);
					}
				}
			}
		}

		/// <summary>
		/// Get quad contains a point
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public QuadTree<QNode> GetQuad(int x, int y)
		{
			if (!IsPointInBoundary(x, y))
			{
				return null;
			}

			for (int i = 0; i < _quadtrees.Length; i++)
			{
				if (_quadtrees[i] != null && _quadtrees[i].IsPointInBoundary(x, y))
				{
					return _quadtrees[i].GetQuad(x, y);
				}
			}

			return this;
		}

		public void Remove(QNode node)
		{
			int index = -1;
			for (int i = 0; i < _nodes.Count; i++)
			{
				if (node.GetId() == _nodes[i].GetId())
				{
					index = i;
					break;
				}
			}

			if (index >= 0)
			{
				_nodes.RemoveAt(index);
			}

			for (int i = 0; i < _quadtrees.Length; i++)
			{
				if (_quadtrees[i] != null && _quadtrees[i].IsInBoundary(node))
				{
					_quadtrees[i].Remove(node);
				}
			}

			if (IsEmpty())
			{
				Clear();
			}
		}

		public void Update(QNode node)
		{
			Remove(node);
			Insert(node);
		}

		public int CountNodes()
		{
			int count = 0;
			for (int i = 0; i < _quadtrees.Length; i++)
			{
				if (_quadtrees[i] != null)
				{
					count += _quadtrees[i].CountNodes();
				}
			}
			return count + _nodes.Count;
		}

		public bool IsEmpty()
		{
			if (_nodes.Count > 0)
			{
				return false;
			}

			for (int i = 0; i < _quadtrees.Length; i++)
			{
				if (!_quadtrees[i].IsEmpty())
				{
					return false;
				}
			}

			return true;
		}

		public bool IsRoot()
		{
			return _depth == 0;
		}

		/// <summary>
		/// Calculate depth of tree in common case
		/// </summary>
		/// <param name="minDistance">Smallest distance from any point to target object</param>
		/// <param name="sideLength">Side length of initalize square which contains all node</param>
		/// <returns></returns>
		public static int CalculateTreeDepth(int minDistance, int sideLength)
		{
			const int min = 2;
			if (minDistance < sideLength)
			{
				return min;
			}

			double temp = (double)sideLength / (double)minDistance;
			double result = System.Math.Log(temp) + (double)(3.0f / 2.0f);

			return (int)System.Math.Round(result);
		}

		/// <summary>
		/// Calculate max object in a tree in common case
		/// </summary>
		/// <param name="minDistance">Smallest distance from any point to target object</param>
		/// <param name="sideLength">Side length of initalize square which contains all node</param>
		/// <returns></returns>
		public static int CalculateMaxObject(int minDistance, int sideLength)
		{
			int depth = CalculateTreeDepth(minDistance, sideLength);

			int minSideLength = sideLength / depth;

			int unitOnEdge = minSideLength / minDistance;

			if (unitOnEdge <= 1)
			{
				return 1;
			}

			return unitOnEdge * 2;
		}
	}
}