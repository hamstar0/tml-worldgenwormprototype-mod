using System;
using System.Collections.Generic;
using Terraria;
using Terraria.World.Generation;


namespace WorldGenWormPrototype {
	public partial class CrystalCaveSystemGen : WormSystemGen {
		public static CrystalCaveSystemGen Create( GenerationProgress progress, float thisProgress, int tileX, int tileY ) {
			int totalLength = WorldGen.genRand.Next( 150, 200 );
			int len1 = WorldGen.genRand.Next( 50, totalLength - 50 );
			int len2 = totalLength - len1;

			int totalForks = WorldGen.genRand.Next( 4, 8 );
			int forks1 = WorldGen.genRand.Next( 2, totalForks );
			int forks2 = totalForks - forks1;

			var wormDefs = new List<WormGen> {
				CrystalCaveGen.Create( tileX, tileY, len1, forks1 ),
				CrystalCaveGen.Create( tileX, tileY, len2, forks2 ),
			};

			return new CrystalCaveSystemGen( progress, thisProgress, wormDefs );
		}



		////////////////

		private CrystalCaveSystemGen( GenerationProgress progress, float thisProgress, IList<WormGen> wormDefs )
					: base( progress, thisProgress, wormDefs ) {
		}
	}
}