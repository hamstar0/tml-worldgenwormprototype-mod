using System;
using System.Collections.Generic;
using Terraria;
using Terraria.World.Generation;


namespace WorldGenWormPrototype {
	public partial class CrystalCaveWormGen : WormGen {
		public CrystalCaveWormGen(
					GenerationProgress progress,
					float thisProgress,
					int tileX,
					int tileY )
			: base( progress, thisProgress, tileX, tileY, 150, 200 ) { }


		////////////////

		protected override WormNode GenerateNode( int maxNodes ) {
			int radius;
			int minWidth = 3;
			int maxWidth = 12;

			if( this.Nodes.Count <= 2 ) {
				radius = WorldGen.genRand.Next( (int)( (float)maxWidth * 1.5f ), maxWidth * 3 ); // start fat
				radius /= this.Nodes.Count + 1;
			} else if( this.Nodes.Count >= ( maxNodes - 5 ) ) {
				float range = 6 - ( maxNodes - this.Nodes.Count );
				radius = (int)( (float)minWidth / (float)range );  // taper
			} else {
				radius = WorldGen.genRand.Next( minWidth, maxWidth );
			}

			if( this.Nodes.Count == 0 ) {
				return new WormNode { TileX = this.OriginTileX, TileY = this.OriginTileY, Radius = radius };
			}

			return this.CreateNextCrystaCaveNode( radius );
		}


		////////////////

		private WormNode CreateNextCrystaCaveNode( int radius ) {
			int tests = 14;
			int tilePadding = 6;
			WormNode currNode = this.Nodes[this.Nodes.Count - 1];

			var testNodes = new List<WormNode>( tests );
			for( int i = 0; i < tests; i++ ) {
				WormNode testNode = WormGen.CreateTestNode( currNode, radius );
				testNodes.Add( testNode );
			}

			WormNode bestNode = null;
			float prevGauged = -1f;
			foreach( WormNode testNode in testNodes ) {
				float gauged = this.GaugeCrystalCaveNode( testNode, tilePadding );
				if( prevGauged != -1 && gauged > prevGauged ) { continue; }

				prevGauged = gauged;
				bestNode = testNode;
			}

			return bestNode;
		}


		////////////////

		private float GaugeCrystalCaveNode( WormNode node, float tilePadding ) {
			float gauged = 0f;

			foreach( WormNode existingNode in this.Nodes ) {
				float value = (float)existingNode.GetDistance( node );

				value -= existingNode.Radius + node.Radius + tilePadding;
				if( value < 0f ) {
					value = 100000 - value;
				}

				gauged += value;
			}

			return gauged / (float)this.Nodes.Count;
		}
	}
}