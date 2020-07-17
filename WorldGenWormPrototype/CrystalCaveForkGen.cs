using System;
using System.Collections.Generic;
using Terraria;


namespace WorldGenWormPrototype {
	public partial class CrystalCaveForkGen : CrystalCaveGen {
		public CrystalCaveForkGen( int tileX, int tileY, int length )
					: base( tileX, tileY, length, new List<WormGen>() ) { }


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

		public override int CalculateFurthestNodeDepth() {
			return this.TotalNodes;
		}
	}
}