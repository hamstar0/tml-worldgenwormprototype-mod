using System;
using System.Collections;
using System.Collections.Generic;
using Terraria;


namespace WorldGenWormPrototype {
	public abstract partial class WormGen : IEnumerable<WormNode> {
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

		public int OriginTileX { get; private set; }
		public int OriginTileY { get; private set; }
		public int TotalNodes { get; private set; }

		protected IList<WormNode> KeyNodes = new List<WormNode>();



		////////////////

		public WormGen( int tileX, int tileY, int minNodes, int maxNodes ) {
			this.OriginTileX = tileX;
			this.OriginTileY = tileY;
			this.TotalNodes = WorldGen.genRand.Next( minNodes, maxNodes );
		}


		////////////////

		IEnumerator IEnumerable.GetEnumerator() => this.KeyNodes.GetEnumerator();

		public IEnumerator<WormNode> GetEnumerator() => this.KeyNodes.GetEnumerator();


		////////////////

		public abstract bool GenerateNextKeyNode( WormSystemGen wormSystem );
	}
}