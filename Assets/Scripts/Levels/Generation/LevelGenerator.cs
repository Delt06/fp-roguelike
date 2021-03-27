using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Levels.Generation
{
	public sealed class LevelGenerator
	{
		public LevelGenerator([NotNull] LevelEntryGenerator entryGenerator, [NotNull] LevelExitGenerator exitGenerator,
			[NotNull] LevelPathTracer pathTracer, [NotNull] LevelCorridorsGenerator corridorsGenerator)
		{
			_entryGenerator = entryGenerator ?? throw new ArgumentNullException(nameof(entryGenerator));
			_exitGenerator = exitGenerator ?? throw new ArgumentNullException(nameof(exitGenerator));
			_pathTracer = pathTracer ?? throw new ArgumentNullException(nameof(pathTracer));
			_corridorsGenerator = corridorsGenerator ?? throw new ArgumentNullException(nameof(corridorsGenerator));
		}


		public void Generate([NotNull] LevelTile[,] tiles)
		{
			if (tiles == null) throw new ArgumentNullException(nameof(tiles));

			tiles.ClearWith(LevelTile.Empty);

			var width = tiles.GetWidth();
			var height = tiles.GetHeight();
			var entryPosition = _entryGenerator.GenerateEntryPosition(width, height);
			var exitPosition = _exitGenerator.GenerateExitPosition(width, height, entryPosition);

			_pathTracer.GenerateObstacles(entryPosition, exitPosition, width, height);

			_path.Clear();
			if (_pathTracer.TryTracePath(entryPosition, exitPosition, width, height, _path))
			{
				AddPath(tiles);
				_corridorsGenerator.Generate(tiles, _path);
			}

			tiles[entryPosition.X, entryPosition.Y].Type = LevelTileType.Entry;
			tiles[exitPosition.X, exitPosition.Y].Type = LevelTileType.Exit;
		}

		private void AddPath(LevelTile[,] tiles)
		{
			TilePosition? previousPosition = null;

			foreach (var position in _path)
			{
				tiles[position.X, position.Y] = LevelTile.Room();

				if (previousPosition.HasValue) LevelTileConnector.Connect(tiles, previousPosition.Value, position);

				previousPosition = position;
			}
		}

		private readonly LevelEntryGenerator _entryGenerator;
		private readonly LevelExitGenerator _exitGenerator;
		private readonly LevelPathTracer _pathTracer;
		private readonly LevelCorridorsGenerator _corridorsGenerator;
		private readonly LinkedList<TilePosition> _path = new LinkedList<TilePosition>();
	}
}