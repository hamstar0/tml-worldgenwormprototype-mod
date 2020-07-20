using System;
using Terraria;


namespace WorldGenWormPrototype {
	public class WormNode {
		public int TileX;
		public int TileY;
		public int TileRadius;
		public int NodeSpacing;

		protected WormGen Parent;



		////////////////

		public WormNode( int tileX, int tileY, int tileRadius, int nodeSpacing, WormGen parent ) {
			this.TileX = tileX;
			this.TileY = tileY;
			this.TileRadius = tileRadius;
			this.NodeSpacing = nodeSpacing;
			this.Parent = parent;
		}

		public double GetDistance( WormNode node ) {
			int diffX = node.TileX - this.TileX;
			int diffY = node.TileY - this.TileY;
			return Math.Sqrt( ( diffX * diffX ) + ( diffY * diffY ) );
		}


		////////////////

		public virtual void Paint( Func<float, float> scale, Func<int, int, float, bool> painter ) {
			float rad = scale( this.TileRadius );
			int radSqr = (int)( rad * rad );
			int minX = this.TileX - (int)rad;
			int maxX = this.TileX + (int)rad;
			int minY = this.TileY - (int)rad;
			int maxY = this.TileY + (int)rad;

			for( int i = minX; i < maxX; i++ ) {
				for( int j = minY; j < maxY; j++ ) {
					int xDist = i - this.TileX;
					int yDist = j - this.TileY;
					int distSqr = ( xDist * xDist ) + ( yDist * yDist );

					if( distSqr > radSqr ) {
						if( j >= this.TileY ) {
							break;
						} else {
							continue;
						}
					}

					if( i >= 0 && i < Main.maxTilesX && j >= 0 && j < Main.maxTilesY ) {
						float perc = (float)distSqr / (float)radSqr;

						if( painter(i, j, perc) ) {
							this.Parent.PostPaintTile( this, i, j );
						}
					}
				}
			}
		}
	}
}