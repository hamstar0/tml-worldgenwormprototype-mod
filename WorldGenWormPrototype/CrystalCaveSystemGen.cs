using System;
using System.Collections.Generic;
using Terraria;
using Terraria.World.Generation;


namespace WorldGenWormPrototype {
	public partial class CrystalCaveSystemGen : WormSystemGen {
		public static CrystalCaveSystemGen Create( GenerationProgress progress, float thisProgress, int tileX, int tileY ) {
			var wormDefs = new List<WormGen> {
				CrystalCaveGen.Create( tileX, tileY )
			};

			return new CrystalCaveSystemGen( progress, thisProgress, wormDefs );
		}



		////////////////

		private CrystalCaveSystemGen( GenerationProgress progress, float thisProgress, IList<WormGen> wormDefs )
					: base( progress, thisProgress, wormDefs ) {
		}
	}
}