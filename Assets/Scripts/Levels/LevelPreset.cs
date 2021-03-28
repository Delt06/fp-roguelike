using System;
using Levels.Generation;
using UnityEngine;
using Random = System.Random;

namespace Levels
{
	[CreateAssetMenu]
	public sealed class LevelPreset : ScriptableObject, ILevelPreset
	{
		[SerializeField] private int _seed = 0;

		[Header("Size"), SerializeField, Min(1)]
		private int _width = 5;

		[SerializeField, Min(1)] private int _height = 5;

		[Header("Entry and Exit"), SerializeField, Min(0)]
		private int _maxEntryPadding = 2;

		[SerializeField, Min(0)] private int _minDistanceFromEntryToExit = 2;

		[Header("Obstacles"), SerializeField, Min(0)]
		private int _minObstacles = 1;

		[SerializeField, Min(0)] private int _maxObstacles = 3;

		[Header("Corridors"), SerializeField, Range(0f, 1f)]
		private float _corridorProbability = 1f;

		[SerializeField, Min(0)] private int _maxCorridorsDepth = 4;

		[SerializeField, HideInInspector] private LevelTile[] _tiles = Array.Empty<LevelTile>();
		[SerializeField, HideInInspector] private TilePosition _entryPosition = TilePosition.Zero;
		[SerializeField, HideInInspector] private TilePosition _exitPosition = TilePosition.Zero;

		public LevelTile[,] GetTiles() => ToGrid(_tiles, _width, _height);

		public TilePosition EntryPosition => _entryPosition;

		public TilePosition ExitPosition => _exitPosition;

		public void Regenerate()
		{
			var generator = CreateGenerator();
			var tiles = new LevelTile[_width, _height];
			generator.Generate(tiles, out _entryPosition, out _exitPosition);
			_tiles = ToFlatArray(tiles);
		}

		private LevelGenerator CreateGenerator()
		{
			var random = new Random(_seed);
			var levelEntryGenerator = new LevelEntryGenerator(random, _maxEntryPadding);
			var levelExitGenerator = new LevelExitGenerator(random, _minDistanceFromEntryToExit);
			var pathTracer = new LevelPathTracer(random, _minObstacles, _maxObstacles);
			var corridorsGenerator = new LevelCorridorsGenerator(random, _maxCorridorsDepth, _corridorProbability);
			var generator = new LevelGenerator(levelEntryGenerator, levelExitGenerator, pathTracer, corridorsGenerator);
			return generator;
		}

		private static T[] ToFlatArray<T>(T[,] grid)
		{
			var width = grid.GetLength(0);
			var height = grid.GetLength(1);
			var array = new T[width * height];

			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					array[y * width + x] = grid[x, y];
				}
			}

			return array;
		}

		private static T[,] ToGrid<T>(T[] array, int width, int height)
		{
			var grid = new T[width, height];
			if (array.Length != width * height) return grid;

			for (var i = 0; i < array.Length; i++)
			{
				var x = i % width;
				var y = i / width;
				grid[x, y] = array[i];
			}

			return grid;
		}

		private void OnValidate()
		{
			_maxObstacles = Mathf.Max(_maxObstacles, _minObstacles);
			Regenerate();
		}
	}
}