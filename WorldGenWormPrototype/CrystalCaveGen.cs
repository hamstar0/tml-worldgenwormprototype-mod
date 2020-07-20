using System;
using System.Collections.Generic;
using Terraria;


namespace WorldGenWormPrototype {
	public partial class CrystalCaveGen : WormGen {
		public const int MinNormalRadius = 3;
		public const int MaxNormalRadius = 10;
		public const int NodeRangeOfStart = 2;
		public const int NodeRangeOfEnd = 5;



		////////////////

		public static CrystalCaveGen Create( int tileX, int tileY, int length, int forkCount ) {
			var randForks = new List<WormGen>( forkCount );

			for( int i=0; i<forkCount; i++ ) {
				int randLen = WorldGen.genRand.Next( length/8, length/4 );
				var fork = new CrystalCaveGen(
					tileX: 0,
					tileY: 0,
					length: randLen,
					forks: new List<WormGen>(),
					minRadius: CrystalCaveGen.MinNormalRadius,
					maxRadius: CrystalCaveGen.MaxNormalRadius,
					startNodeCount: 0,
					endNodeCount: CrystalCaveGen.NodeRangeOfEnd
				);

				randForks.Add( fork );
			}

			return new CrystalCaveGen(
				tileX: tileX,
				tileY: tileY,
				length: length,
				forks: randForks,
				minRadius: CrystalCaveGen.MinNormalRadius,
				maxRadius: CrystalCaveGen.MaxNormalRadius,
				startNodeCount: CrystalCaveGen.NodeRangeOfStart,
				endNodeCount: CrystalCaveGen.NodeRangeOfEnd
			);
		}



		////////////////

		protected int MinRadius;
		protected int MaxRadius;
		protected int StartNodeCount;
		protected int EndNodeCount;



		////////////////

		protected CrystalCaveGen(
					int tileX,
					int tileY,
					int length,
					IList<WormGen> forks,
					int minRadius,
					int maxRadius,
					int startNodeCount,
					int endNodeCount )
					: base( tileX, tileY, length, forks ) {
			this.MinRadius = minRadius;
			this.MaxRadius = maxRadius;
			this.StartNodeCount = startNodeCount;
			this.EndNodeCount = endNodeCount;
		}
	}
}