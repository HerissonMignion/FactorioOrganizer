using System;

namespace FactorioOrganizer
{
	/*
	 * /!\ /!\ /!\ important note:
	 * 
	 * The class oItem and oCraft are used to represent items and crafts of the game. THEY ARE NEVER DUPLICATED. There is never and should never be mutiple oItem or oCraft
	 * that represents the same item or craft. There is, should and must be only one oItem and oCraft for every item and craft, never more than one.
	 * 
	 * Everywhere in the project, when an item or a craft is required, either the .Name property (string) is used or THE object itself is given/returned.
	 * 
	 * This ensure that when a oItem or a oCraft is modified (for exemple: when importing objects of a mod), everybody has the same information about the same item.
	 * 
	 */

		
	//this class represents an item of the game. it contains its name and what can it be. equivalent of a member of FOType.
	public class oItem //struct
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
					return this.ModName + "_$_" + this.ItemName; // the $ is because some people might want to use _ in their objects name so i added $ and then _ for readability
				}
				else //this item is not from a mod
				{
					return this.ItemName;
				}
			}
		}

		public bool IsBelt; //indicate if the item can be a belt

		//indicate if the item is used as a recipe
		public bool IsRecipe
		{
			get
			{
				return Crafts.IsARecipe(this.Name);
			}
		}

		public int IconIndex; //index of the icon (image) of the item inside Crafts.listIcons

		public oItem(string sItemName, bool sIsBelt = true, string sModName = "")
		{
			this.ItemName = sItemName;
			this.IsBelt = sIsBelt;
			//this.IsRecipe = sIsRecipe;
			this.ModName = sModName;
			this.IconIndex = -1;
		}

	}
}