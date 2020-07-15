using System;
using Terraria;


namespace WorldGenWormPrototype {
	public class WormNode {
		public int TileX;
		public int TileY;
		public int Radius;



		////////////////

		public double GetDistance( WormNode node ) {
			int diffX = node.TileX - this.TileX;
			int diffY = node.TileY - this.TileY;
			return Math.Sqrt( ( diffX * diffX ) + ( diffY * diffY ) );
		}
	}
}