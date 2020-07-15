using System;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;


namespace WorldGenWormPrototype {
	public abstract partial class WormGen {
		public void PaintNodePath( GenerationProgress progress, float thisProgress ) {
			float progressUnit = (float)thisProgress / (float)this.Nodes.Count;

			for( int i = 0; i < this.Nodes.Count; i++ ) {
				this.PaintNode(
					this.Nodes[i],
					//r => r * (float)Math.Max( r / 8f, 2f ),
					r => r * 2,
					(t) => {
						t.type = TileID.Granite;
						t.wall = WallID.GraniteUnsafe;
						t.slope( 0 );
						t.active( true );
					}
				);

				progress.Value += progressUnit * 0.5f;
			}

			for( int i = 0; i < this.Nodes.Count; i++ ) {
				this.PaintNode(
					this.Nodes[i],
					r => r,
					(t) => {
						t.active( false );
						t.wall = WallID.GraniteUnsafe;
					}
				);

				progress.Value += progressUnit * 0.5f;
			}
		}


		////////////////

		public void PaintNode( WormNode node, Func<float, float> scale, Action<Tile> painter ) {
			float rad = scale( node.Radius );
			int radSqr = (int)( rad * rad );
			int minX = node.TileX - (int)rad;
			int maxX = node.TileX + (int)rad;
			int minY = node.TileY - (int)rad;
			int maxY = node.TileY + (int)rad;

			for( int i = minX; i < maxX; i++ ) {
				for( int j = minY; j < maxY; j++ ) {
					int xDist = i - node.TileX;
					int yDist = j - node.TileY;
					int distSqr = (xDist * xDist) + (yDist * yDist);

					if( distSqr > radSqr ) {
						if( j >= node.TileY ) {
							break;
						} else {
							continue;
						}
					}

					if( i >= 0 && i < Main.maxTilesX && j >= 0 && j < Main.maxTilesY ) {
						painter( Framing.GetTileSafely(i, j) );
					}
				}
			}
		}
	}
}