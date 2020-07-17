using System;
using System.Collections.Generic;
using Terraria;


namespace WorldGenWormPrototype {
	public partial class CrystalCaveGen : WormGen {
		public const int MinNormalRadius = 3;
		public const int MaxNormalRadius = 12;



		////////////////

		public static CrystalCaveGen Create( int tileX, int tileY, int length, int forkCount ) {
			var randForks = new List<WormGen>( forkCount );
			for( int i=0; i<forkCount; i++ ) {
				int randLen = WorldGen.genRand.Next( length / 8, length / 4 );
				CrystalCaveGen fork = new CrystalCaveForkGen( 0, 0, randLen );

				randForks.Add( fork );
			}

			return new CrystalCaveGen( tileX, tileY, length, randForks );
		}



		////////////////
		
		protected CrystalCaveGen( int tileX, int tileY, int length, IList<WormGen> randForks )
					: base( tileX, tileY, length, randForks ) { }
	}
}