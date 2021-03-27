using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Random = System.Random;

namespace Levels
{
	public class LevelGenerator
	{
		private readonly Random _random;
		private readonly int _maxEntryPadding;
		private readonly int _minDistanceFromExitToEntry;
		private readonly int _minObstacles;
		private readonly int _maxObstacles;
		private readonly HashSet<TilePosition> _obstacles = new HashSet<TilePosition>();
		private readonly HashSet<TilePosition> _visited = new HashSet<TilePosition>();
		private readonly Queue<TilePosition> _pendingPositions = new Queue<TilePosition>();
		private readonly Dictionary<TilePosition, TilePosition> _from = new Dictionary<TilePosition, TilePosition>();
		private readonly LinkedList<TilePosition> _path = new LinkedList<TilePosition>();

		public LevelGenerator(int seed, int maxEntryPadding, int minDistanceFromExitToEntry, int minObstacles, int maxObstacles)
		{
			_random = new Random(seed);
			_maxEntryPadding = maxEntryPadding;
			_minDistanceFromExitToEntry = minDistanceFromExitToEntry;
			_minObstacles = minObstacles;
			_maxObstacles = maxObstacles;
		}
		
		public void Generate([NotNull] LevelTile[,] tiles)
		{
			if (tiles == null) throw new ArgumentNullException(nameof(tiles));
			
			var width = tiles.GetLength(0);
			var height = tiles.GetLength(1);
			Clear(tiles, width, height);

			var entryPosition = GenerateEntryPosition(width, height);
			var exitPosition = GenerateExitPosition(width, height, entryPosition);

			GenerateObstacles(entryPosition, exitPosition, width, height);

			if (TryTracePath(entryPosition, exitPosition, width, height))
			{
				TilePosition? previousPosition = null;

				foreach (var position in _path)
				{
					tiles[position.X, position.Y] = LevelTile.Room();
				
					if (previousPosition.HasValue)
					{
						Connect(tiles, previousPosition.Value, position);
					}
				
					previousPosition = position;
				}
			}

			tiles[entryPosition.X, entryPosition.Y].Type = LevelTileType.Entry;
			tiles[exitPosition.X, exitPosition.Y].Type = LevelTileType.Exit;
		}

		private static void Clear(LevelTile[,] tiles, int width, int height)
		{
			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					tiles[x, y] = LevelTile.Empty;
				}
			}
		}

		private TilePosition GenerateEntryPosition(int width, int height)
		{
			var corner = GetRandomCorner();
			var rect = GetCornerRect(corner, width, height, _maxEntryPadding);
			var entryPosition = GetRandomPositionIn(rect);
			return entryPosition;
		}

		private LevelCorner GetRandomCorner()
		{
			var index = _random.Next() % 4;
			return (LevelCorner) index;
		}

		private TilePosition GetRandomPositionIn(TileRect rect)
		{
			var x = rect.Min.Y + _random.Next() % rect.Width;
			var y = rect.Min.Y + _random.Next() % rect.Height;
			return new TilePosition(x, y);
		}

		private static TileRect GetCornerRect(LevelCorner corner, int width, int height, int size)
		{
			int minX, maxX, minY, maxY;

			if (corner.IsSouth())
			{
				minY = 0;
				maxY = size - 1;
			}
			else
			{
				minY = height - size;
				maxY = height - 1;
			}

			if (corner.IsEast())
			{
				minX = 0;
				maxX = size - 1;
			}
			else
			{
				minX = width - size;
				maxX = width - 1;
			}

			var rect = new TileRect(minX, minY, maxX, maxY);
			rect.Clamp(width, height);
			return rect;
		} 
		
		private TilePosition GenerateExitPosition(int width, int height, TilePosition entryPosition)
		{
			var minX = entryPosition.X - _minDistanceFromExitToEntry;
			var minY = entryPosition.Y - _minDistanceFromExitToEntry;
			var maxX = entryPosition.X + _minDistanceFromExitToEntry;
			var maxY = entryPosition.Y + _minDistanceFromExitToEntry;
			var entryRect = new TileRect(minX, minY, maxX, maxY);
			entryRect.Clamp(width, height);

			return GetRandomTileExcludingRect(width, height, entryRect);
		}

		private TilePosition GetRandomTileExcludingRect(int width, int height, TileRect excludedRect)
		{
			var totalArea = width * height;
			var entryRectArea = excludedRect.Area;
			var availableTilesCount = totalArea - entryRectArea;
			if (availableTilesCount == 0) return new TilePosition(0, 0);
			var exitTileIndex = _random.Next() % availableTilesCount;

			var tileIndex = 0;

			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					var position = new TilePosition(x, y);
					if (excludedRect.Contains(position)) continue;

					if (tileIndex == exitTileIndex)
						return position;

					tileIndex++;
				}
			}
			
			return TilePosition.Zero;
		}

		private void GenerateObstacles(TilePosition entryPosition, TilePosition exitPosition, int width, int height)
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
				if (TryTracePath(entryPosition, exitPosition, width, height)) continue;
				
				_obstacles.Remove(position);
				return;
			}
		}

		private bool TryTracePath(TilePosition entryPosition, TilePosition exitPosition, int width, int height)
		{
			_visited.Clear();
			_pendingPositions.Clear();
			_from.Clear();
			_path.Clear();
			
			_pendingPositions.Enqueue(entryPosition);

			while (_pendingPositions.Count > 0)
			{
				var position = _pendingPositions.Dequeue();
				if (position.Equals(exitPosition))
					break;

				if (_visited.Contains(position))
					continue;

				_visited.Add(position);
				TryAddNeighbor(position, position.Add(x: 1), width, height);
				TryAddNeighbor(position, position.Add(x: -1), width, height);
				TryAddNeighbor(position, position.Add(y: 1), width, height);
				TryAddNeighbor(position, position.Add(y: -1), width, height);
			}

			if (!_from.ContainsKey(exitPosition))
				return false;

			var currentPosition = exitPosition;

			while (!currentPosition.Equals(entryPosition))
			{
				var from = _from[currentPosition];
				_path.AddFirst(currentPosition);
				currentPosition = from;
			}

			_path.AddFirst(currentPosition);
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

		private static void Connect(LevelTile[,] tiles, TilePosition tile1, TilePosition tile2)
		{
			ConnectHorizontally(tiles, tile1, tile2);
			ConnectVertically(tiles, tile1, tile2);
		}

		private static void ConnectHorizontally(LevelTile[,] tiles, TilePosition tile1, TilePosition tile2)
		{
			var dx = tile2.X - tile1.X;
			if (dx == 0) return;
			
			if (dx > 0)
			{
				tiles[tile1.X, tile1.Y].HasEastDoor = true;
				tiles[tile2.X, tile2.Y].HasWestDoor = true;
			}
			else
			{
				tiles[tile1.X, tile1.Y].HasWestDoor = true;
				tiles[tile2.X, tile2.Y].HasEastDoor = true;
			}
		}

		private static void ConnectVertically(LevelTile[,] tiles, TilePosition tile1, TilePosition tile2)
		{
			var dy = tile2.Y - tile1.Y;
			if (dy == 0) return;
			
			if (dy > 0)
			{
				tiles[tile1.X, tile1.Y].HasNorthDoor = true;
				tiles[tile2.X, tile2.Y].HasSouthDoor = true;
			}
			else
			{
				tiles[tile1.X, tile1.Y].HasSouthDoor = true;
				tiles[tile2.X, tile2.Y].HasNorthDoor = true;
			}
		}
	}
}