using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioOrganizer
{
	//this class represent a single craft. it describe the item used as a recipe
	public class oCraft
	{

		public sItem Recipe; //object used as a recipe
		public sItem[] Inputs = new sItem[] { }; //inputs of the recipe
		public sItem[] Outputs = new sItem[] { }; //output of the recipe

		public bool IsMadeInFurnace = false; //indicate if it is made inside a furnace
		

		public oCraft(sItem sRecipe, sItem[] sInputs, sItem[] sOutputs, bool sIsFurnace = false)
		{
			this.Recipe = sRecipe;
			this.Inputs = sInputs;
			this.Outputs = sOutputs;
			this.IsMadeInFurnace = sIsFurnace;
		}


	}
}
