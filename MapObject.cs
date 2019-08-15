using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FactorioOrganizer
{

	//une belt. c'est plus petit qu'une machine
	public class MapObject
	{

		public MOType MapType;
		
		
		public FOType OutType
		{
			get
			{
				try
				{
					if (this.MapType == MOType.Belt)
					{
						return this.BeltOutput;
					}
					//before, output wasn't an array. there was only one output. when i made output an array, i did that to keep the code compatible until i change everything and i leave it there. 
					if (this.MapType == MOType.Machine)
					{
						return this.Outputs[0];
					}
				}
				catch
				{
					return FOType.none;
				}
				return FOType.none;
			}
		}


		public FOType BeltOutput = FOType.none; //content of the belt, if this is a belt ////    contenue de la belt

		public FOType[] Outputs = new FOType[] { };
		public void SetOutput(FOType newout)
		{
			this.Outputs = new FOType[] { newout };
		}
		public void SetOutput(FOType[] newouts)
		{
			this.Outputs = newouts;
		}

		public FOType[] Inputs = new FOType[] { };


		//for a machine, the recipe define both the inputs and outputs
		public FOType TheRecipe = FOType.none; //actuel recipe. used only if this is a machine ////    recette actuel. utilisé seulement si this est une machine





		public bool IsFurnace = false;
		public bool NeedCoal = true; //if this is a furnace, indicate if this need coal ////    si this est une furnace, indique si this a besoin de coal
		



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





		// ////   pour une belt, StartOut est le contenue qu'elle transporte. pour une machine, StartOut est le recipe

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
		//public MapObject(MOType StartMapType, FOType[] StartOutputs)
		//{
		//	this.MapType = StartMapType;
		//	this.SetOutput(StartOutputs);
		//	if (this.MapType == MOType.Machine)
		//	{
		//		this.SetRecipe(StartOutputs[0]);
		//	}
		//}



		//define inputs and outputs according to the recipe ////    défini les input et output qui vont avec la recette spécifié
		public void SetRecipe(FOType Recipe)
		{
			if (this.MapType == MOType.Belt)
			{
				this.SetOutput(Recipe);
			}
			if (this.MapType == MOType.Machine)
			{
				this.TheRecipe = Recipe;
				this.Outputs = Utilz.GetRecipeOutputs(Recipe);
				this.Inputs = Utilz.GetRecipeInputs(Recipe);
				this.IsFurnace = Utilz.IsRecipeMadeInFurnace(Recipe);
			}
		}



		//return if this map object touch to a specific virtual coordinate ////    retourne si this touche à une coordonné
		public bool IsTouch(float vx, float vy)
		{
			float deltax = vx - this.vpos.X;
			if (deltax < 0f) { deltax = this.vpos.X - vx; }
			float deltay = vy - this.vpos.Y;
			if (deltay < 0f) { deltay = this.vpos.Y - vy; }
			if (this.MapType == MOType.Machine)
			{
				return deltax <= this.VirtualWidth / 2f && deltay <= this.VirtualWidth / 2f;
			}
			else
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
