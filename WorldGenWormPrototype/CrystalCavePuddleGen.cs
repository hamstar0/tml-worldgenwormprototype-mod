using System;
using System.Collections.Generic;
using Terraria;


namespace WorldGenWormPrototype {
	public partial class CrystalCavePuddleGen : CrystalCaveForkGen {
		public static new CrystalCavePuddleGen Create( int tileX, int tileY, int length, int forkCount = 0 ) {
			var randForks = new List<WormGen>( forkCount );

			for( int i = 0; i < forkCount; i++ ) {
				int randLen = WorldGen.genRand.Next( 2, 3 );

				CrystalCaveGen fork = CrystalCaveForkGen.Create( 0, 0, randLen );

				randForks.Add( fork );
			}

			return new CrystalCavePuddleGen( tileX, tileY, length, randForks );
		}



		////////////////

		protected CrystalCavePuddleGen( int tileX, int tileY, int length, IList<WormGen> forks )
					: base( tileX, tileY, length, forks ) { }


		////////////////

		protected override float GaugeCrystalCaveNode(
					WormSystemGen wormSys,
					WormNode testNode,
					WormNode prevNode,
					float tilePadding ) {
			float gauged = base.GaugeCrystalCaveNode( wormSys, testNode, prevNode, tilePadding );
			
			float vertGauge = (testNode.TileY - prevNode.TileY) > 0f
				? 0 : 100000f;
			float horizGauge = Math.Abs( prevNode.TileX - testNode.TileX );
			horizGauge *= 10;

			return gauged + vertGauge + horizGauge;
		}


		////////////////

		public override void PostPaintTile( WormNode node, int i, int j ) {
			if( this.KeyNodes[0] == node ) {
				return;
			}

			Main.tile[i, j].liquid = 255;
			Main.tile[i, j].liquidType( 0 );
		}
	}
}