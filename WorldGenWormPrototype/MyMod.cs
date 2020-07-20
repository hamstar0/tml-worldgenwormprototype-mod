using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;


namespace WorldGenWormPrototype {
	public class WGWMod : Mod {
		public static WGWMod Instance { get; private set; }



		////////////////

		public override void Load() {
			WGWMod.Instance = this;
		}

		public override void Unload() {
			WGWMod.Instance = null;
		}
	}



	public class WGWWorld : ModWorld {
		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			int idx = tasks.FindIndex( t => t.Name == "Underworld" ); //Terrain
			if( idx == -1 ) {
				idx = 1;
			}

			var pass = new WormGenPass();

			tasks.Insert( idx, pass );	//+1

			totalWeight += pass.Weight;
		}
	}
}