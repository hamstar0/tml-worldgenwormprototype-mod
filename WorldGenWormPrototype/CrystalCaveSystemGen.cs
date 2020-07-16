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

			var wormDefs = new List<WormGen> {
				new CrystalCaveGen( tileX, tileY, len1 ),
				new CrystalCaveGen( tileX, tileY, len2 ),
			};

			return new CrystalCaveSystemGen( progress, thisProgress, wormDefs );
		}



		////////////////

		private CrystalCaveSystemGen( GenerationProgress progress, float thisProgress, IList<WormGen> wormDefs )
					: base( progress, thisProgress, wormDefs ) {
		}
	}
}