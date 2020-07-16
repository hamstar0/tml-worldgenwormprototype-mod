using System;
using System.Collections.Generic;
using Terraria;


namespace WorldGenWormPrototype {
	public partial class CrystalCaveGen : WormGen {
		public CrystalCaveGen( int tileX, int tileY, int length ) : base( tileX, tileY, length ) { }


		////////////////

		public override bool GenerateNextKeyNode( WormSystemGen wormSys ) {
			if( this.KeyNodes.Count >= this.TotalNodes ) {
				return false;
			}

			int radius;
			int minWidth = 3;
			int maxWidth = 12;

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
				newNode = this.CreateNextCrystaCaveNode( wormSys, radius );
			}

			this.KeyNodes.Add( newNode );

			return true;
		}


		////////////////

		private WormNode CreateNextCrystaCaveNode( WormSystemGen wormSys, int radius ) {
			int tests = 14;
			int tilePadding = 8;

			WormNode currNode = this.KeyNodes[ this.KeyNodes.Count - 1 ];

			var testNodes = new List<WormNode>( tests );
			for( int i = 0; i < tests; i++ ) {
				WormNode testNode = WormGen.CreateTestNode( currNode, radius );
				testNodes.Add( testNode );
			}

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

		private float GaugeCrystalCaveNode( WormSystemGen wormSys, WormNode node, float tilePadding ) {
			float gauged = 0f;

			foreach( WormNode existingNode in wormSys ) {
				float value = (float)existingNode.GetDistance( node );

				value -= existingNode.Radius + node.Radius + tilePadding;
				if( value < 0f ) {
					value = 100000 - value;
				}

				gauged += value;
			}

			return gauged / (float)wormSys.NodeCount;
		}
	}
}