using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;


namespace WorldGenWormPrototype {
	public abstract partial class WormGen : IEnumerable<WormNode> {
		protected IList<WormNode> KeyNodes = new List<WormNode>();

		protected IDictionary<int, WormGen> _Forks;


		////////////////

		public int OriginTileX { get; private set; }

		public int OriginTileY { get; private set; }

		public int TotalNodes { get; private set; }

		public IReadOnlyDictionary<int, WormGen> Forks { get; private set; }



		////////////////

		public WormGen( int tileX, int tileY, int totalNodes, IList<WormGen> randomForks ) {
			this.OriginTileX = tileX;
			this.OriginTileY = tileY;
			this.TotalNodes = totalNodes;

			this._Forks = new Dictionary<int, WormGen>( randomForks.Count );
			this.Forks = new ReadOnlyDictionary<int, WormGen>( this._Forks );

			for( int i=0; i<randomForks.Count; i++ ) {
				this._Forks[ WorldGen.genRand.Next(0, totalNodes) ] = randomForks[i];
			}
		}


		////////////////

		IEnumerator IEnumerable.GetEnumerator() => this.KeyNodes.GetEnumerator();

		public IEnumerator<WormNode> GetEnumerator() => this.KeyNodes.GetEnumerator();


		////////////////

		public abstract int CalculateFurthestKeyNode();


		////////////////

		public virtual bool GenerateNextKeyNode( WormSystemGen wormSystem, out WormGen fork ) {
			if( this.KeyNodes.Count >= this.TotalNodes ) {
				fork = null;
				return false;
			}

			WormNode nextNode = this.CreateKeyNode( wormSystem );
			this.KeyNodes.Add( nextNode );

			if( this.Forks.TryGetValue(this.KeyNodes.Count - 1, out fork) ) {
				fork.OriginTileX = nextNode.TileX;
				fork.OriginTileY = nextNode.TileY;

				WormNode forkHeadNode = fork.CreateKeyNode( wormSystem );
				fork.KeyNodes.Add( forkHeadNode );
			}

			return true;
		}


		////

		protected abstract WormNode CreateKeyNode( WormSystemGen wormSystem );


		protected WormNode CreateTestNode( WormNode prevNode, int radius ) {
			float randDir = WorldGen.genRand.NextFloat() * (float)Math.PI * 2f;
			double dist = radius + prevNode.Radius;
			double x = Math.Cos( randDir ) * dist;
			double y = Math.Sin( randDir ) * dist;

			return new WormNode(
				tileX: (int)x + prevNode.TileX,
				tileY: (int)y + prevNode.TileY,
				radius: radius,
				parent: this
			);
		}


		////////////////

		public virtual void PostPaintTile( WormNode node, int i, int j ) { }
	}
}