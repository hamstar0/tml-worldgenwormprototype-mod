using System;
using System.Collections.Generic;
using Terraria;


namespace WorldGenWormPrototype {
	public partial class CrystalCaveGen : WormGen {
		public override int CalculateFurthestKeyNode() {
			int largest = this.TotalNodes;
			foreach( KeyValuePair<int, WormGen> kv in this._Forks ) {
				if( (kv.Key + kv.Value.TotalNodes) > largest ) {
					largest = kv.Key + kv.Value.TotalNodes;
				}
			}

			return largest;
		}


		////////////////

		protected virtual float GaugeCrystalCaveNode( WormSystemGen wormSys, WormNode testNode, WormNode prevNode, float tilePadding ) {
			float gauged = 0f;

			foreach( WormNode existingNode in wormSys ) {
				float value = (float)existingNode.GetDistance( testNode );

				value -= existingNode.Radius + testNode.Radius + tilePadding;
				if( value < 0f ) {
					value = 100000 - value;	// too close penalty
				}

				gauged += value;
			}

			return gauged / (float)wormSys.NodeCount;
		}
	}
}
