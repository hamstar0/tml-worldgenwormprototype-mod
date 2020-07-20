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


		protected override void CalculateRadiusAndPadding( out int radius, out int padding ) {
			int minRad = CrystalCaveGen.MinNormalRadius;
			int maxRad = CrystalCaveGen.MaxNormalRadius;
			int startRange = CrystalCaveGen.StartRadiusInterpolateLength;   //2
			int endRange = CrystalCaveGen.EndRadiusInterpolateLength;   //5
			int remainingNodes = this.TotalNodes - this.KeyNodes.Count;

			padding = WorldGen.genRand.Next( minRad, maxRad );

			if( this.KeyNodes.Count >= (this.TotalNodes - endRange) ) {
				float radStep = minRad / (float)remainingNodes;
				int steps = (endRange + 1) - remainingNodes;

				radius = minRad - (int)(radStep * (float)steps);  // taper
				return;
			}

			if( this.KeyNodes.Count <= startRange ) {
				float startMinRadScale = (float)startRange * 0.75f;

				radius = WorldGen.genRand.Next( // start fat
					(int)( (float)maxRad * startMinRadScale ),
					(int)( (float)maxRad * startMinRadScale * 2f )
				);
				radius /= this.KeyNodes.Count + 1;
				return;
			}

			radius = padding;
		}


		////////////////

		protected virtual float GaugeCrystalCaveNode(
					WormSystemGen wormSys,
					WormNode testNode,
					WormNode prevNode,
					float tilePadding ) {
			float gauged = 0f;

			foreach( WormNode existingNode in wormSys ) {
				float value = (float)existingNode.GetDistance( testNode );

				value -= existingNode.TileRadius + testNode.TileRadius + tilePadding;
				if( value < 0f ) {
					value = 100000 - value;	// too close penalty
				}

				gauged += value;
			}

			return gauged / (float)wormSys.NodeCount;
		}
	}
}
