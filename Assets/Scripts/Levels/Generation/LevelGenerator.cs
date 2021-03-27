using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Random = System.Random;

namespace Levels.Generation
{
	public class LevelGenerator
	{ 
		public LevelGenerator(int seed, int maxEntryPadding, int minDistanceFromExitToEntry, int minObstacles, int maxObstacles)
		{
			var random = new Random(seed);
			_levelEntryGenerator = new LevelEntryGenerator(random, maxEntryPadding);
			_levelExitGenerator = new LevelExitGenerator(random, minDistanceFromExitToEntry);
			_pathTracer = new LevelPathTracer(random, minObstacles, maxObstacles);
		}
		
		public void Generate([NotNull] LevelTile[,] tiles)
		{
			if (tiles == null) throw new ArgumentNullException(nameof(tiles));
			
			var width = tiles.GetLength(0);
			var height = tiles.GetLength(1);
			Clear(tiles, width, height);

			var entryPosition = _levelEntryGenerator.GenerateEntryPosition(width, height);
			var exitPosition = _levelExitGenerator.GenerateExitPosition(width, height, entryPosition);

			_pathTracer.GenerateObstacles(entryPosition, exitPosition, width, height);

			_path.Clear();
			if (_pathTracer.TryTracePath(entryPosition, exitPosition, width, height, _path))
			{
				TilePosition? previousPosition = null;

				foreach (var position in _path)
				{
					tiles[position.X, position.Y] = LevelTile.Room();
				
					if (previousPosition.HasValue)
					{
						LevelTileConnector.Connect(tiles, previousPosition.Value, position);
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
		
		private readonly LevelEntryGenerator _levelEntryGenerator;
		private readonly LevelExitGenerator _levelExitGenerator;
		private readonly LevelPathTracer _pathTracer;
		private readonly LinkedList<TilePosition> _path = new LinkedList<TilePosition>();
	}
}