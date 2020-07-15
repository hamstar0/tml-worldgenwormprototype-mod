using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;


namespace WorldGenWormPrototype {
	public abstract partial class WormGen {
		public IList<WormNode> CreateInterpolatedNodes( WormNode prevNode, WormNode nextNode ) {
			double incIntervals = 2d;
			var nodes = new List<WormNode>();

			double xDist = nextNode.TileX - prevNode.TileX;
			double yDist = nextNode.TileY - prevNode.TileY;
			double dist = Math.Sqrt( (xDist*xDist) + (yDist*yDist) );

			if( dist < incIntervals ) {
				return nodes;
			}

			for( double i=incIntervals; i<dist; i+=incIntervals ) {
				double perc = i / dist;
				int x = prevNode.TileX + (int)(xDist * perc);
				int y = prevNode.TileY + (int)(yDist * perc);
				int rad = (int)MathHelper.Lerp( (float)prevNode.Radius, (float)nextNode.Radius, (float)perc );

				nodes.Add( new WormNode { TileX = x, TileY = y, Radius = rad } );
			}

			return nodes;
		}
	}
}