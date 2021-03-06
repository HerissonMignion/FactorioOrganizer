﻿using System;
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



		public oItem BeltOutput = Crafts.listItems[0]; //content of the belt, if this is a belt

		public oItem[] Outputs
		{
			get
			{
				if (this.TheCraft != null)
				{
					return this.TheCraft.Outputs;
				}
				else
				{
					return new oItem[] { };
				}
			}
		}
		public oItem[] Inputs
		{
			get
			{
				if (this.TheCraft != null)
				{
					return this.TheCraft.Inputs;
				}
				else
				{
					return new oItem[] { };
				}
			}
		}
		public oItem TheRecipe = Crafts.listItems[0]; //actuel recipe. used only if this is a machine
		public oCraft TheCraft = null;


		
		public bool IsFurnace
		{
			get
			{
				if (this.TheCraft != null)
				{
					return this.TheCraft.IsMadeInFurnace;
				}
				else
				{
					return false;
				}
			}
		} // = false;




		public bool NeedCoal = true; //if this is a furnace, indicate if this need coal ////    si this est une furnace, indique si this a besoin de coal

		//i guess that some mods require other things than coal to make things so, in the future, we might have to replace NeedCoal by NeededCombustibles which will be an array of sItem and a static list of arrays in Crafts who describe every combustibles options to toggle.
		//we can also make the change of NeedCoal to NeededCombustibles only if any mods were loaded. if no mod loaded, we stay with NeedCoal.

		//or a lazy and less user friendly option is to remove NeedCoal and possibly IsFurnace and make one recipe for every combination of for what would be every combinaison of NeededCombustibles.

		//another preferable solution we be leave it like that. current vanilla items won't be affected. modded items will have one recipe for every combinaisons of what represented NeededCombustibles in the other solution.






		public Bitmap GetImage()
		{
			if (this.MapType == MOType.Belt)
			{
				return Crafts.GetAssociatedIcon(this.BeltOutput);
			}
			if (this.MapType == MOType.Machine)
			{
				return Crafts.GetAssociatedIcon(this.TheRecipe);
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



		
		public MapObject(MOType StartMapType, oItem StartOut)
		{
			this.MapType = StartMapType; //must be define before calling SetRecipe
			if (this.MapType == MOType.Belt)
			{
				this.BeltOutput = StartOut;
			}
			if (this.MapType == MOType.Machine)
			{
				this.SetRecipe(StartOut);
			}
		}



		
		public void SetRecipe(oItem Recipe)
		{
			if (this.MapType == MOType.Belt)
			{
				this.BeltOutput = Recipe;
			}
			if (this.MapType == MOType.Machine)
			{
				this.TheRecipe = Recipe;
				oCraft c = Crafts.GetCraftFromRecipe(Recipe); //gets the craft
				this.TheCraft = c;
				//if (c != null)
				//{
				//	this.Outputs = c.Outputs;
				//	this.Inputs = c.Inputs;
				//	this.IsFurnace = c.IsMadeInFurnace;
				//}
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
			oItem copyrecipe = this.BeltOutput;
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
