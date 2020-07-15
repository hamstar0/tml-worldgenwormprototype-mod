using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;


namespace WorldGenWormPrototype {
	public abstract partial class WormGen : IEnumerable<WormNode> {
		public IList<WormNode> CreateInterpolatedNodesFromRecentNodes() {
			var nodes = new List<WormNode>();
			if( this.KeyNodes.Count < 2 ) {
				return nodes;
			}

			WormNode prevNode = this.KeyNodes[ this.KeyNodes.Count - 2 ];
			WormNode currNode = this.KeyNodes[ this.KeyNodes.Count - 1 ];

			double xDist = currNode.TileX - prevNode.TileX;
			double yDist = currNode.TileY - prevNode.TileY;
			double dist = Math.Sqrt( (xDist*xDist) + (yDist*yDist) );

			double incIntervals = 2d;
			if( dist < incIntervals ) {
				nodes.Add( currNode );
				return nodes;
			}

			for( double i=incIntervals; i<dist; i+=incIntervals ) {
				double perc = i / dist;
				int x = prevNode.TileX + (int)(xDist * perc);
				int y = prevNode.TileY + (int)(yDist * perc);
				int rad = (int)MathHelper.Lerp( (float)prevNode.Radius, (float)currNode.Radius, (float)perc );

				nodes.Add( new WormNode { TileX = x, TileY = y, Radius = rad } );
			}

			nodes.Add( currNode );
			return nodes;
		}
	}
}