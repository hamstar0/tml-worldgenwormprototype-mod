using System;
using System.Collections.Generic;
using Terraria;


namespace WorldGenWormPrototype {
	public partial class CrystalCaveGen : WormGen {
		protected override WormNode CreateKeyNode( WormSystemGen wormSys ) {
			int radius;
			int minWidth = CrystalCaveGen.MinNormalRadius;
			int maxWidth = CrystalCaveGen.MaxNormalRadius;

			if( this.KeyNodes.Count <= 2 ) {
				radius = WorldGen.genRand.Next( (int)( (float)maxWidth * 1.5f ), maxWidth * 3 ); // start fat
				radius /= this.KeyNodes.Count + 1;
			} else if( this.KeyNodes.Count >= (this.TotalNodes - 5) ) {
				float range = 6 - (this.TotalNodes - this.KeyNodes.Count);
				radius = (int)( (float)minWidth / (float)range );  // taper
			} else {
				radius = WorldGen.genRand.Next( minWidth, maxWidth );
			}

			WormNode newNode;
			if( this.KeyNodes.Count == 0 ) {
				newNode = new WormNode { TileX = this.OriginTileX, TileY = this.OriginTileY, Radius = radius };
			} else {
				newNode = this.CreateNextCrystalCaveKeyNode( wormSys, radius );
			}

			return newNode;
		}


		protected virtual WormNode CreateNextCrystalCaveKeyNode( WormSystemGen wormSys, int radius ) {
			int tests = 14;
			int tilePadding = 8;

			WormNode currNode = this.KeyNodes[ this.KeyNodes.Count - 1 ];
			var testNodes = this.CreateTestNodes( tests, radius, currNode );

			WormNode bestNode = null;
			float prevGauged = -1f;
			foreach( WormNode testNode in testNodes ) {
				float gauged = this.GaugeCrystalCaveNode( wormSys, testNode, tilePadding );
				if( prevGauged != -1 && gauged > prevGauged ) { continue; }

				prevGauged = gauged;
				bestNode = testNode;
			}

			return bestNode;
		}


		////////////////

		protected virtual IList<WormNode> CreateTestNodes( int count, int radius, WormNode currNode ) {
			var testNodes = new List<WormNode>( count );

			for( int i = 0; i < count; i++ ) {
				WormNode testNode = WormGen.CreateTestNode( currNode, radius );
				testNodes.Add( testNode );
			}

			return testNodes;
		}
	}
}