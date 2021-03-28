using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static Levels.Generation.Monsters.MonsterPositionsExt;

namespace Levels.Generation.Monsters
{
	public sealed class LevelMonsterGenerator
	{
		public void Generate([NotNull] LevelTile[,] tiles)
		{
			if (tiles == null) throw new ArgumentNullException(nameof(tiles));
			
			var width = tiles.GetWidth();
			var height = tiles.GetHeight();

			for (var tx = 0; tx < width; tx++)
			{
				for (var ty = 0; ty < height; ty++)
				{
					var tile = tiles[tx, ty];
					if (tile.Type != LevelTileType.Normal) continue;

					var data = tile.MonsterData;
					AddPositions(tile, ref data);
					tile.MonsterData = data;
					tiles[tx, ty] = tile;
				}
			}
		}

		private void AddPositions(LevelTile tile, ref MonsterData data)
		{
			foreach (var position in AllPositions)
			{
				TryAddPosition(tile, ref data, position);
			}
		}

		private void TryAddPosition(LevelTile tile, ref MonsterData data, MonsterPosition position)
		{
			if (!CanGenerateMonster(tile, position)) return;

			var randomValue = _random.NextDouble();
			var probability = _probabilities.GetProbability(position);
			if (randomValue > probability) return;
				
			data.Add(position);
		}

		private static bool CanGenerateMonster(LevelTile tile, MonsterPosition position) =>
			position switch
			{
				MonsterPosition.South => tile.HasSouthDoor,
				MonsterPosition.North => tile.HasNorthDoor,
				MonsterPosition.West => tile.HasWestDoor,
				MonsterPosition.East => tile.HasEastDoor,
				MonsterPosition.Center => true,
				_ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
			};

		public LevelMonsterGenerator([NotNull] Random random, MonsterPositionProbabilities probabilities)
		{
			_random = random ?? throw new ArgumentNullException(nameof(random));
			_probabilities = probabilities;
		}

		private readonly Random _random;
		private readonly MonsterPositionProbabilities _probabilities;
	}
}