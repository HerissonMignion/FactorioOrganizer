using System;

namespace FactorioOrganizer
{
	public class ModItemEventArgs : EventArgs
	{
		public oMod.ModItem ModItem;
		public ModItemEventArgs(oMod.ModItem sModItem)
		{
			this.ModItem = sModItem;
		}
	}
	public class ModCraftEventArgs : EventArgs
	{
		public oMod.ModCraft ModCraft;
		public ModCraftEventArgs(oMod.ModCraft sModCraft)
		{
			this.ModCraft = sModCraft;
		}
	}
	//public class ModImageEventArgs : EventArgs
	//{
	//	public oMod.ModImage ModImage;
	//	public ModImageEventArgs(oMod.ModImage sModImage)
	//	{
	//		this.ModImage = sModImage;
	//	}
	//}
	
}