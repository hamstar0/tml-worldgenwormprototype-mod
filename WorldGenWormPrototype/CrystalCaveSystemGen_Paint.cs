using System;
using Terraria;
using Terraria.ID;


namespace WorldGenWormPrototype {
	public partial class CrystalCaveSystemGen : WormSystemGen {
		protected override bool PaintTileInner( int i, int j ) {
			Tile t = Framing.GetTileSafely( i, j );
			bool changed = t.active() || t.wall != WallID.GraniteUnsafe || t.liquid > 0;

			t.active( false );
			t.wall = WallID.GraniteUnsafe;
			t.liquid = 0;
			return changed;
		}

		protected override bool PaintTileOuter( int i, int j ) {
			Tile t = Framing.GetTileSafely( i, j );
			bool changed = !t.active()
					|| t.type != TileID.Granite
					|| t.wall != WallID.GraniteUnsafe;

			t.type = TileID.Granite;
			t.wall = WallID.GraniteUnsafe;
			t.slope( 0 );
			t.active( true );
			return changed;
		}
	}
}