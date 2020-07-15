using System;
using System.Collections.Generic;
using Terraria;
using Terraria.World.Generation;


namespace WorldGenWormPrototype {
	public abstract partial class WormGen {
		public static WormNode CreateTestNode( WormNode prevNode, int radius ) {
			float randDir = WorldGen.genRand.NextFloat() * (float)Math.PI * 2f;
			double dist = radius + prevNode.Radius;
			double x = Math.Cos( randDir ) * dist;
			double y = Math.Sin( randDir ) * dist;

			return new WormNode {
				TileX = (int)x + prevNode.TileX,
				TileY = (int)y + prevNode.TileY,
				Radius = radius
			};
		}



		////////////////

		protected IList<WormNode> Nodes = new List<WormNode>();
		protected int OriginTileX;
		protected int OriginTileY;



		////////////////

		public WormGen(
					GenerationProgress progress,
					float thisProgress,
					int tileX,
					int tileY,
					int minNodes,
					int maxNodes ) {
			this.OriginTileX = tileX;
			this.OriginTileY = tileY;

			int keyNodeCount = WorldGen.genRand.Next( minNodes, maxNodes );
			int currExpectedNodeCount = keyNodeCount;
			float progressUnit = (float)thisProgress / (float)keyNodeCount;

			for( int i=0; i<keyNodeCount; i++ ) {
				WormNode nextNode = this.GenerateNode( currExpectedNodeCount );

				if( i >= 1 ) {
					WormNode prevNode = this.Nodes[ this.Nodes.Count - 1 ];
					IList<WormNode> interNodes = this.CreateInterpolatedNodes( prevNode, nextNode );

					foreach( WormNode interNode in interNodes ) {
						this.Nodes.Add( interNode );
						currExpectedNodeCount++;
					}
				}

				this.Nodes.Add( nextNode );

				progress.Value += progressUnit;
			}
		}


		////////////////

		protected abstract WormNode GenerateNode( int maxNodes );
	}
}