using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FactorioOrganizer
{


	public class MapObject
	{

		public MOType MapType;
		
		


		public FOType BeltOutput = FOType.none; //content of the belt, if this is a belt ////    contenue de la belt

		public FOType[] Outputs = new FOType[] { };
		

		public FOType[] Inputs = new FOType[] { };


		//for a machine, the recipe define both the inputs and outputs
		public FOType TheRecipe = FOType.none; //actuel recipe. used only if this is a machine ////    recette actuel. utilisé seulement si this est une machine





		public bool IsFurnace = false;
		public bool NeedCoal = true; //if this is a furnace, indicate if this need coal ////    si this est une furnace, indique si this a besoin de coal

		//i guess that some mods require other things than coal to make things so, in the future, we might have to replace NeedCoal by NeededCombustibles which will be an array of sItem and a static list of arrays in Crafts who describe every combustibles options to toggle.
		//we can also make the change of NeedCoal to NeededCombustibles only if any mods were loaded. if no mod loaded, we stay with NeedCoal.

		//or a lazy and less user friendly option is to remove NeedCoal and possibly IsFurnace and make one recipe for every combination of for what would be every combinaison of NeededCombustibles.

		//another preferable solution we be leave it like that. current vanilla items won't be affected. modded items will have one recipe for every combinaisons of what represented NeededCombustibles in the other solution.






		public Bitmap GetImage()
		{
			if (this.MapType == MOType.Belt)
			{
				return Utilz.GetAssociatedIcon(this.BeltOutput);
			}
			if (this.MapType == MOType.Machine)
			{
				return Utilz.GetAssociatedIcon(this.TheRecipe);
			}
			return FactorioOrganizer.Properties.Resources.fish;
		}
		public bool HasImage { get { return this.GetImage() != null; } }



		public PointF vpos = new PointF(0f, 0f);
		public float VirtualWidth
		{
			get
			{
				if (this.MapType == MOType.Belt) { return 1f; }
				if (this.MapType == MOType.Machine) { return 3f; }
				return 1f;
			}
		}




		
		//for a belt, StartOut is its content. for a machine, StartOut is the recipe
		public MapObject(MOType StartMapType = MOType.Belt, FOType StartOut = FOType.none)
		{
			this.MapType = StartMapType;

			if (this.MapType == MOType.Belt)
			{
				this.BeltOutput = StartOut; //define the output ////    set la sortie/contenue de la belt sur l'élément spécifié
			}
			if (this.MapType == MOType.Machine)
			{
				this.SetRecipe(StartOut);
			}
		}




		//define inputs and outputs according to the recipe ////    défini les input et output qui vont avec la recette spécifié
		public void SetRecipe(FOType Recipe)
		{
			if (this.MapType == MOType.Belt) //SetRecipe is usually not called for belts
			{
				this.BeltOutput = Recipe;
			}
			if (this.MapType == MOType.Machine)
			{
				//set the recipe and set everything
				this.TheRecipe = Recipe;
				this.Outputs = Utilz.GetRecipeOutputs(Recipe);
				this.Inputs = Utilz.GetRecipeInputs(Recipe);
				this.IsFurnace = Utilz.IsRecipeMadeInFurnace(Recipe);
			}
		}



		//return if this map object touch to a specific virtual coordinate ////    retourne si this touche à une coordonné
		public bool IsTouch(float vx, float vy)
		{
			//get pos difference in absolute value. abosule values are very usefull when not using pythagore theorem
			float deltax = vx - this.vpos.X;
			if (deltax < 0f) { deltax = this.vpos.X - vx; }
			float deltay = vy - this.vpos.Y;
			if (deltay < 0f) { deltay = this.vpos.Y - vy; }
			if (this.MapType == MOType.Machine) //if this is a machine, we check absolute pos difference a see if it fits inside the square
			{
				//machines are square. we don't analyse radius/dist to middle
				return deltax <= this.VirtualWidth / 2f && deltay <= this.VirtualWidth / 2f;
			}
			else //if this is a belt, we use pythagore theorem and check if it fits inside the radius
			{
				return Math.Sqrt((deltax * deltax) + (deltay * deltay)) <= this.VirtualWidth / 2f;
			}
		}

		public float DistTo(float vx, float vy)
		{
			float deltax = vx - this.vpos.X;
			float deltay = vy - this.vpos.Y;
			return (float)(Math.Sqrt((deltax * deltax) + (deltay * deltay)));
		}



		public bool IsAllInputPresent = false; //used by RefreshImage. indicate if all inputs are present. it's reseted and recalculated every RefreshImage ////    utilisé par RefreshImage. indique si tout les input sont présent



		public MapObject GetCopy()
		{
			//prepare the content to send to the constructor ////    obtient les contenue à envoyer au constructeur
			FOType copyrecipe = this.BeltOutput;
			if (this.MapType == MOType.Machine)
			{
				copyrecipe = this.TheRecipe;
			}

			MapObject copy = new MapObject(this.MapType, copyrecipe);
			copy.NeedCoal = this.NeedCoal;
			return copy;
		}

	}
}
