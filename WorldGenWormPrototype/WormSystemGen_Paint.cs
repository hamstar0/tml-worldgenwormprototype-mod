using System;
using System.Collections.Generic;
using Terraria;
using Terraria.World.Generation;


namespace WorldGenWormPrototype {
	public abstract partial class WormSystemGen : IEnumerable<WormNode> {
		public void PaintNodes( GenerationProgress progress, float thisProgress ) {
			float progressUnit = (float)thisProgress / (float)this.Nodes.Count;

			for( int i = 0; i < this.Nodes.Count; i++ ) {
				this.Nodes[i].Paint( r => r * 2, this.PaintTileOuter );
				progress.Value += progressUnit * 0.5f;
			}

			for( int i = 0; i < this.Nodes.Count; i++ ) {
				this.Nodes[i].Paint( r => r, this.PaintTileInner );
				progress.Value += progressUnit * 0.5f;
			}
		}


		////////////////

		protected abstract bool PaintTileInner( int i, int j );

		protected abstract bool PaintTileOuter( int i, int j );
	}
}