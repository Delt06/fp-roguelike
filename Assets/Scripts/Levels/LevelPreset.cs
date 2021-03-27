using System;
using Levels.Generation;
using UnityEngine;

namespace Levels
{
	[CreateAssetMenu]
	public sealed class LevelPreset : ScriptableObject
	{
		[SerializeField, Min(1)] private int _width = 5;
		[SerializeField, Min(1)] private int _height = 5;
		[SerializeField] private int _seed = 0;
		[SerializeField, Min(0)] private int _maxEntryPadding = 2;
		[SerializeField, Min(0)] private int _minDistanceFromEntryToExit = 2;
		[SerializeField, Min(0)] private int _minObstacles = 1;
		[SerializeField, Min(0)] private int _maxObstacles = 3;
		[SerializeField, HideInInspector] private LevelTile[] _tiles = Array.Empty<LevelTile>();

		public LevelTile[,] GetTiles() => ToGrid(_tiles, _width, _height);

		public void Regenerate()
		{
			var tiles = new LevelTile[_width, _height];
			var generator = new LevelGenerator(_seed, _maxEntryPadding, _minDistanceFromEntryToExit, _minObstacles, _maxObstacles);
			generator.Generate(tiles);
			_tiles = ToFlatArray(tiles);
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