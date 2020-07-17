using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.World.Generation;


namespace WorldGenWormPrototype {
	public abstract partial class WormSystemGen : IEnumerable<WormNode> {
		public int NodeCount => this.Nodes.Count;


		////////////////

		protected IList<WormNode> Nodes = new List<WormNode>();
		protected int OriginTileX;
		protected int OriginTileY;



		////////////////

		public WormSystemGen( GenerationProgress progress, float thisProgress, IList<WormGen> wormDefs ) {
			var genWormDefs = new HashSet<WormGen>( wormDefs );
			int maxNodes = wormDefs.Max( wg => wg.CalculateFurthestNodeDepth() );
			float progStep = thisProgress / (float)maxNodes;

			do {
				foreach( WormGen wormDef in genWormDefs.ToArray() ) {
					if( !wormDef.GenerateNextKeyNode(this, out WormGen fork) ) {
						genWormDefs.Remove( wormDef );
					}

					if( fork != null ) {
						genWormDefs.Add( fork );
					}

					IList<WormNode> interpNodes = wormDef.CreateInterpolatedNodesFromRecentNodes();
					if( interpNodes.Count > 0 ) {
						this.Nodes = this.Nodes.Union( interpNodes ).ToList();
					}
				}

				progress.Value += progStep;
			} while( genWormDefs.Count > 0 );
		}


		////////////////

		IEnumerator IEnumerable.GetEnumerator() => this.Nodes.GetEnumerator();

		public IEnumerator<WormNode> GetEnumerator() => this.Nodes.GetEnumerator();
	}
}