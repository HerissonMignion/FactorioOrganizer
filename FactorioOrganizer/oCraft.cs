using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


	//this class represent a single craft. it describe the item used as a recipe.
	public class oCraft
	{

		public oItem Recipe; //object used as a recipe. EVERY oCraft should be uniquely identified by their .Recipe. there shouldn't be multiple craft with the exact same .Recipe, at least for now.
		public oItem[] Inputs = new oItem[] { }; //inputs of the recipe
		public oItem[] Outputs = new oItem[] { }; //output of the recipe

		public bool IsMadeInFurnace = false; //indicate if it is made inside a furnace
		

		public oCraft(oItem sRecipe, oItem[] sInputs, oItem[] sOutputs, bool sIsFurnace = false)
		{
			this.Recipe = sRecipe;
			this.Inputs = sInputs;
			this.Outputs = sOutputs;
			this.IsMadeInFurnace = sIsFurnace;
		}

		public override string ToString()
		{
			string rep = "CRAFT(";
			rep += this.Recipe.Name + ",";
			//inputs
			rep += "input(";
			bool isfirst = true;
			foreach (oItem i in this.Inputs)
			{
				if (!isfirst) { rep += ","; }
				rep += i.Name;
				//next iteration
				isfirst = false;
			}
			rep += "),";

			//outputs
			rep += "outputs(";
			isfirst = true;
			foreach (oItem i in this.Outputs)
			{
				if (!isfirst) { rep += ","; }
				rep += i.Name;
				//next iteration
				isfirst = false;
			}
			rep += "),";

			if (!this.IsMadeInFurnace) { rep += "NOT"; }
			rep += "madeInFurnace";
			rep += ")";
			return rep;
		}
	}
}
