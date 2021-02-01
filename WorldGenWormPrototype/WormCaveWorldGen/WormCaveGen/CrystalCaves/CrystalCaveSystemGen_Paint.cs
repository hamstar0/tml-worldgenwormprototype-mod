using System;
using Terraria;
using Terraria.ID;


namespace WorldGenWormPrototype.WormCaveWorldGen.WormCaveGen.CrystalCaves {
	/// <summary>
	/// Represents a crystal cave system; a type of worm cave system world gen. Uses `Create` as its primary
	/// factory method.
	/// </summary>
	public partial class CrystalCaveSystemGen : WormSystemGen {
		protected override bool PaintTileInner( int i, int j, float percToEdge ) {
			Tile t = Framing.GetTileSafely( i, j );
			bool changed = t.active() || t.wall != WallID.GraniteUnsafe || t.liquid > 0;

			t.active( false );
			t.wall = WallID.GraniteUnsafe;
			t.liquid = 0;
			return changed;
		}

		protected override bool PaintTileOuter( int i, int j, float percToEdge ) {
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