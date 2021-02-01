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
}