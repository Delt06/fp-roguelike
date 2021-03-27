using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Levels.Generation
{
	public sealed class LevelPathTracer
	{
		public LevelPathTracer([NotNull] Random random, int minObstacles, int maxObstacles)
		{
			_random = random ?? throw new ArgumentNullException(nameof(random));
			_minObstacles = minObstacles;
			_maxObstacles = maxObstacles;
		}

		public bool TryTracePath(TilePosition entryPosition, TilePosition exitPosition, int width, int height,
			LinkedList<TilePosition> path)
		{
			_visited.Clear();
			_pendingPositions.Clear();
			_from.Clear();

			_pendingPositions.Enqueue(entryPosition);

			while (_pendingPositions.Count > 0)
			{
				var position = _pendingPositions.Dequeue();
				if (position.Equals(exitPosition))
					break;

				if (_visited.Contains(position))
					continue;

				_visited.Add(position);
				TryAddNeighbor(position, position.Add(1), width, height);
				TryAddNeighbor(position, position.Add(-1), width, height);
				TryAddNeighbor(position, position.Add(y: 1), width, height);
				TryAddNeighbor(position, position.Add(y: -1), width, height);
			}

			if (!_from.ContainsKey(exitPosition))
				return false;

			var currentPosition = exitPosition;

			while (!currentPosition.Equals(entryPosition))
			{
				var from = _from[currentPosition];
				path.AddFirst(currentPosition);
				currentPosition = from;
			}

			path.AddFirst(currentPosition);
			return true;
		}

		private void TryAddNeighbor(TilePosition from, TilePosition position, int width, int height)
		{
			if (_obstacles.Contains(position)) return;
			if (_visited.Contains(position)) return;
			if (!IsInsideBounds(position, width, height)) return;

			_from[position] = from;
			_pendingPositions.Enqueue(position);
		}

		private static bool IsInsideBounds(TilePosition position, int width, int height) =>
			0 <= position.X && position.X < width &&
			0 <= position.Y && position.Y < height;

		public void GenerateObstacles(TilePosition entryPosition, TilePosition exitPosition, int width, int height)
		{
			_obstacles.Clear();

			var obstaclesCount = _random.Next(_minObstacles, _maxObstacles + 1);

			for (var i = 0; i < obstaclesCount; i++)
			{
				var x = _random.Next(width);
				var y = _random.Next(height);
				var position = new TilePosition(x, y);
				if (position.Equals(entryPosition)) continue;
				if (position.Equals(exitPosition)) continue;

				_obstacles.Add(position);
				_path.Clear();
				if (TryTracePath(entryPosition, exitPosition, width, height, _path)) continue;

				_obstacles.Remove(position);
				return;
			}
		}


		private readonly int _minObstacles;
		private readonly int _maxObstacles;
		private readonly Random _random;
		private readonly ISet<TilePosition> _obstacles = new HashSet<TilePosition>();
		private readonly HashSet<TilePosition> _visited = new HashSet<TilePosition>();
		private readonly Queue<TilePosition> _pendingPositions = new Queue<TilePosition>();
		private readonly Dictionary<TilePosition, TilePosition> _from = new Dictionary<TilePosition, TilePosition>();
		private readonly LinkedList<TilePosition> _path = new LinkedList<TilePosition>();
	}
}