using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using static Levels.LevelTilemapExtensions;

namespace Levels.Generation
{
	public sealed class LevelCorridorsGenerator
	{
		public void Generate([NotNull] LevelTile[,] tiles, [NotNull] LinkedList<TilePosition> path)
		{
			if (tiles == null) throw new ArgumentNullException(nameof(tiles));
			if (path == null) throw new ArgumentNullException(nameof(path));

			foreach (var position in path)
			{
				if (_random.NextDouble() > _probability) continue;
				GenerateCorridor(tiles, position);
			}
		}

		private void GenerateCorridor(LevelTile[,] tiles, TilePosition origin)
		{
			for (var depth = 0; depth < _maxDepth; depth++)
			{
				var neighbor = GetRandomNeighbor(origin);
				var width = tiles.GetWidth();
				var height = tiles.GetHeight();
				if (!IsInsideBounds(neighbor, width, height)) return;
				if (tiles[neighbor.X, neighbor.Y].Type != LevelTileType.None) return;

				tiles[neighbor.X, neighbor.Y] = LevelTile.Room();
				LevelTileConnector.Connect(tiles, origin, neighbor);
				origin = neighbor;
			}
		}

		private TilePosition GetRandomNeighbor(TilePosition position)
		{
			var index = _random.Next(4);
			return index switch
			{
				0 => position.Add(1),
				1 => position.Add(-1),
				2 => position.Add(y: 1),
				3 => position.Add(y: -1),
				_ => position,
			};
		}

		public LevelCorridorsGenerator([NotNull] Random random, int maxDepth, float probability)
		{
			_random = random ?? throw new ArgumentNullException(nameof(random));
			_maxDepth = maxDepth;
			_probability = probability;
		}

		private readonly Random _random;
		private readonly int _maxDepth;
		private readonly float _probability;
	}
}