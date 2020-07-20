using System;
using System.Collections.Generic;


namespace WorldGenWormPrototype {
	public partial class CrystalCaveGen : WormGen {
		protected override WormNode CreateKeyNode( WormSystemGen wormSys ) {
			this.CalculateRadiusAndPadding( out int radius, out int padding );

			WormNode newNode;
			if( this.KeyNodes.Count == 0 ) {
				newNode = new WormNode( this.OriginTileX, this.OriginTileY, radius, padding, this );
			} else {
				newNode = this.CreateNextCrystalCaveKeyNode( wormSys, radius, padding );
			}

			return newNode;
		}


		protected virtual WormNode CreateNextCrystalCaveKeyNode( WormSystemGen wormSys, int radius, int padding ) {
			int tests = 14;
			int tilePadding = 8;

			WormNode currNode = this.KeyNodes[ this.KeyNodes.Count - 1 ];

			var testNodes = this.CreateTestNodes( tests, radius, padding, currNode );

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

		protected virtual IList<WormNode> CreateTestNodes( int count, int radius, int padding, WormNode currNode ) {
			var testNodes = new List<WormNode>( count );

			for( int i = 0; i < count; i++ ) {
				WormNode testNode = this.CreateTestNode( currNode, radius, padding );
				testNodes.Add( testNode );
			}

			return testNodes;
		}
	}
}