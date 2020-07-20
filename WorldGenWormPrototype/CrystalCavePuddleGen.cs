using System;
using System.Collections.Generic;
using Terraria;


namespace WorldGenWormPrototype {
	public partial class CrystalCavePuddleGen : CrystalCaveGen {
		public static new CrystalCavePuddleGen Create( int tileX, int tileY, int length, int forkCount = 0 ) {
			var randForks = new List<WormGen>( forkCount );

			for( int i = 0; i < forkCount; i++ ) {
				int randLen = WorldGen.genRand.Next( 2, 6 );

				var fork = CrystalCaveGen.Create( 0, 0, randLen, 0 );

				randForks.Add( fork );
			}

			return new CrystalCavePuddleGen( tileX, tileY, length, randForks );
		}



		////////////////

		protected CrystalCavePuddleGen( int tileX, int tileY, int length, IList<WormGen> forks )
					: base( tileX, tileY, length, forks, CrystalCaveGen.MinNormalRadius, CrystalCaveGen.MaxNormalRadius, 0, length ) { }


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

		protected override WormNode CreateForkedKeyNode( WormSystemGen wormSystem, WormNode templateNode ) {
			WormNode node = base.CreateForkedKeyNode( wormSystem, templateNode );

			node.TileRadius = (int)((float)node.TileRadius * 1.5f);
			return node;
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