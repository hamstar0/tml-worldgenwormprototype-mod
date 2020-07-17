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

		public WormSystemGen(
					GenerationProgress progress,
					float thisProgress,
					float postProcessProgress,
					IList<WormGen> wormDefs ) {
			ISet<WormGen> genWormDefs = new HashSet<WormGen>( wormDefs );

			this.GenerateNodes( progress, thisProgress, genWormDefs );

			if( this.PostProcessNodes(progress, postProcessProgress, out genWormDefs) ) {
				this.GenerateNodes( progress, postProcessProgress, genWormDefs );
			}
		}

		////

		private void GenerateNodes( GenerationProgress progress, float thisProgress, ISet<WormGen> worms ) {
			int maxNodes = worms.Max( wg => wg.CalculateFurthestNodeDepth() );
			float progStep = thisProgress / (float)maxNodes;

			do {
				foreach( WormGen wormDef in worms.ToArray() ) {
					if( !wormDef.GenerateNextKeyNode(this, out WormGen fork) ) {
						worms.Remove( wormDef );
						continue;
					}

					if( fork != null ) {
						worms.Add( fork );
					}

					IList<WormNode> interpNodes = wormDef.CreateInterpolatedNodesFromRecentNodes();
					if( interpNodes.Count > 0 ) {
						this.Nodes = this.Nodes.Union( interpNodes ).ToList();
					}
				}

				progress.Value += progStep;
			} while( worms.Count > 0 );
		}


		////////////////

		IEnumerator IEnumerable.GetEnumerator() => this.Nodes.GetEnumerator();

		public IEnumerator<WormNode> GetEnumerator() => this.Nodes.GetEnumerator();


		////////////////
		
		protected virtual bool PostProcessNodes(
					GenerationProgress progress,
					float postProcessProgress,
					out ISet<WormGen> newWorms ) {
			newWorms = null;
			return false;
		}
	}
}