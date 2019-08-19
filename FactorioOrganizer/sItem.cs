using System;

namespace FactorioOrganizer
{

	//this class represents an item of the game. it contains its name and what can it be. equivalent of a member of FOType
	public struct sItem
	{
		//these properties are not meant to be edited by anybody after the creation of the item
		public string ModName; //an empty mod name means it's not from a mod
		public string ItemName; //the item name

		//we should create a private variable, "computed" in the constructor, that contains the answer to return every time because this property will be called a lot of times
		//this property represents the name of the item used internaly. the mod name is combined with the variable name so we should avoid name conflicts.
		//for vanilla items, their modname is "vanilla" and is not included in the Name property.
		public string Name
		{
			get
			{
				if (this.ModName != "vanilla") //this item comes from a mod
				{
					return this.ModName + "$_" + this.ItemName; // the $ is because some people might want to use _ in their objects name so i added $ and then _ for readability
				}
				else //this item is not from a mod
				{
					return this.ItemName;
				}
			}
		}

		public bool IsBelt; //indicate if the item can be a belt
		public bool IsRecipe; //indicate if the item can be a recipe

		public int IconIndex; //index of the icon of the item inside Crafts.listIcons

		public sItem(string sItemName, bool sIsBelt = true, bool sIsRecipe = true, string sModName = "")
		{
			this.ItemName = sItemName;
			this.IsBelt = sIsBelt;
			this.IsRecipe = sIsRecipe;
			this.ModName = sModName;
			this.IconIndex = -1;
		}

	}
}