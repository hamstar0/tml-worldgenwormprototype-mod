using System;
using System.Collections.Generic;
using Terraria;


namespace WorldGenWormPrototype {
	public partial class CrystalCaveForkGen : CrystalCaveGen {
		public static new CrystalCaveForkGen Create( int tileX, int tileY, int length, int forkCount = 0 ) {
			var randForks = new List<WormGen>( forkCount );

			for( int i = 0; i < forkCount; i++ ) {
				int randLen = WorldGen.genRand.Next(
					Math.Max( length / 8, 2 ),
					Math.Max( length / 4, 3 )
				);

				CrystalCaveGen fork = new CrystalCaveForkGen( 0, 0, randLen, new List<WormGen>() );

				randForks.Add( fork );
			}

			return new CrystalCaveForkGen( tileX, tileY, length, randForks );
		}



		////////////////

		protected CrystalCaveForkGen( int tileX, int tileY, int length, IList<WormGen> forks )
					: base( tileX, tileY, length, forks ) { }


		////////////////

		protected override WormNode CreateKeyNode( WormSystemGen wormSys ) {
			int minWidth = CrystalCaveGen.MinNormalRadius;
			int maxWidth = CrystalCaveGen.MaxNormalRadius;

			WormNode newNode = base.CreateKeyNode( wormSys );

			if( this.KeyNodes.Count <= 2 ) {
				newNode.Radius = WorldGen.genRand.Next( minWidth, maxWidth );
			}

			return newNode;
		}


		////////////////

		public override int CalculateFurthestKeyNode() {
			return this.TotalNodes;
		}
	}
}