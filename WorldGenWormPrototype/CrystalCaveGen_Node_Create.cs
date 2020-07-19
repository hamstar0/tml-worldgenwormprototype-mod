using System;
using System.Collections.Generic;
using Terraria;


namespace WorldGenWormPrototype {
	public partial class CrystalCaveGen : WormGen {
		protected override WormNode CreateKeyNode( WormSystemGen wormSys ) {
			int radius;
			int minRad = CrystalCaveGen.MinNormalRadius;
			int maxRad = CrystalCaveGen.MaxNormalRadius;
			int startRange = CrystalCaveGen.StartRadiusInterpolateLength;	//2
			int endRange = CrystalCaveGen.EndRadiusInterpolateLength;   //5
			
			if( this.KeyNodes.Count >= (this.TotalNodes - endRange) ) {
				float range = (endRange + 1) - (this.TotalNodes - this.KeyNodes.Count);
				radius = (int)( (float)minRad / range );  // taper
			} else if( this.KeyNodes.Count <= startRange ) {
				float startMinRadScale = (float)startRange * 0.75f;
				radius = WorldGen.genRand.Next(
					(int)((float)maxRad * startMinRadScale),
					(int)((float)maxRad * startMinRadScale * 2f)
				); // start fat
				radius /= this.KeyNodes.Count + 1;
			} else {
				radius = WorldGen.genRand.Next( minRad, maxRad );
			}

			WormNode newNode;
			if( this.KeyNodes.Count == 0 ) {
				newNode = new WormNode( this.OriginTileX, this.OriginTileY, radius, this );
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
				float gauged = this.GaugeCrystalCaveNode( wormSys, testNode, currNode, tilePadding );
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
				WormNode testNode = this.CreateTestNode( currNode, radius );
				testNodes.Add( testNode );
			}

			return testNodes;
		}
	}
}