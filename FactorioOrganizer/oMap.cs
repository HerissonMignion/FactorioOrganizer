using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioOrganizer
{
	public class oMap
	{

		public List<MapObject> listMO = new List<MapObject>();




		//this list is updated ONLY WHEN the map is saved or loaded from a path. it's nt dynamically keept updated.
		public List<string> listModNames = new List<string>(); //this list includes vanilla.

		public bool EveryItemWhereLoadedCorrectly = true; //it only matters when creating this object from a file. when creating this object from a file, if any item loaded has its name property == to "none", we know that it's not okay. if it's not okay then this variable is set to false.


		public oMap()
		{

		}
		public oMap(string filepath)
		{
			this.EveryItemWhereLoadedCorrectly = true; //will becomes false if there is at least one item that wasn't found in the item list of the static class Crafts.

			List<string> alll = System.IO.File.ReadAllLines(filepath).ToList();
			SaveReader sr = new SaveReader(alll);
			//first line is save file format version
			string strVersion = sr.ReadLine();

			while (true)
			{
				//first step is read the next object to add to the map.
				//second step is read the properties of that object. we knows in which order properties are written
				string newitem = sr.ReadLine();
				if (newitem == "exit") { break; }
				if (newitem == "belt")
				{
					string strBeltOutput = sr.ReadLine();
					MapObject mo = new MapObject(MOType.Belt, Crafts.GetItemFromName(strBeltOutput));
					mo.vpos.X = Convert.ToSingle(sr.ReadLine().Replace(".", ","));
					mo.vpos.Y = Convert.ToSingle(sr.ReadLine().Replace(".", ","));
					this.listMO.Add(mo);

					//check if the item was found when Crafts.GetItemFromName
					if (mo.BeltOutput != null)
					{
						if (mo.BeltOutput.Name == "none") { this.EveryItemWhereLoadedCorrectly = false; }
					}
					else { this.EveryItemWhereLoadedCorrectly = false; }

				}
				if (newitem == "machine")
				{
					string strRecipe = sr.ReadLine();
					MapObject mo = new MapObject(MOType.Machine, Crafts.GetItemFromName(strRecipe));
					mo.NeedCoal = sr.ReadLine() == "true";
					mo.vpos.X = Convert.ToSingle(sr.ReadLine().Replace(".", ","));
					mo.vpos.Y = Convert.ToSingle(sr.ReadLine().Replace(".", ","));
					this.listMO.Add(mo);

					//check if the item was found when Crafts.GetItemFromName
					if (mo.TheRecipe != null)
					{
						if (mo.TheRecipe.Name == "none") { this.EveryItemWhereLoadedCorrectly = false; }
					}
					else { this.EveryItemWhereLoadedCorrectly = false; }

				}
				if (newitem == "mod")
				{
					string modname = sr.ReadLine(); //get the mod name
					this.listModNames.Add(modname);
				}
			}
			
		}



		public MapObject GetObjThatTouch(float vx, float vy)
		{
			//go through the list in reverse so it check at the first place the objects that the user see in foreground. foreground objects are at the end of the list ////    il parcourt la liste à l'envers pour que ce soit dans le même ordre que le dessin. c'est à dire que les object en avant plan sont à la fin de la liste
			int index = this.listMO.Count - 1;
			while (index >= 0)
			{
				if (this.listMO[index].IsTouch(vx, vy)) { return this.listMO[index]; }
				index--;
			}
			return null;
		}
		
		//return the closest belt with a compatible output
		//the only purpose of giving mo to the function is to know the coordinate. don't be confused
		public MapObject GetCompatibleBeltCloseTo(MapObject mo, oItem OutType)
		{
			MapObject mo1 = null;
			List<MapObject> lco = this.listMO.FindAll(x => (x.MapType == MOType.Belt) && (x.BeltOutput.Name == OutType.Name)); //get belts of compatible output
			try
			{
				lco.Remove(mo);
			}
			catch { }
			if (lco.Count > 0)
			{
				//sort by distance
				lco = lco.OrderBy(x => x.DistTo(mo.vpos.X, mo.vpos.Y)).ToList();
				
				//get the closest
				mo1 = lco[0];
			}
			return mo1;
		}

		
		//return the 2 closest belts with a compatible output ////    retourne les 2 object le plus proche de lui, et avec un output compatible
		//mo must be a belt
		public MapObject[] GetCompatible2BeltsCloseTo(MapObject mo)
		{
			MapObject mo1 = null;
			MapObject mo2 = null;

			//get belts of compatible output
			List<MapObject> lco = this.listMO.FindAll(x => x.MapType == MOType.Belt && x.BeltOutput.Name == mo.BeltOutput.Name);
			try
			{
				lco.Remove(mo);
			}
			catch { }
			if (lco.Count > 0)
			{
				//sort by distance
				lco = lco.OrderBy(x => x.DistTo(mo.vpos.X, mo.vpos.Y)).ToList();

				mo1 = lco[0]; //get the first
				if (lco.Count >= 2) //check for a second
				{
					mo2 = lco[1]; //get the second
				}
			}
			return new MapObject[] { mo1, mo2 };
		}






		//simply don't create scientific notation. can be a problem when reading a save
		private string ConvertFloatToString(float fl)
		{
			string rep = fl.ToString("N20");
			//remove space
			rep = rep.Replace(" ", string.Empty); //in the past, in other programs, i had troubles replacing " " to "" and string.empty fixed the problem.
			return this.TrimStrNumber(rep);
		}

		//this function removes useless 0 ex :   "-0000012340.000456000" will return "-12340.000456"
		private string TrimStrNumber(string TheNum)
		{
			string rep = "";
			bool IsNegate = false;

			string num = TheNum.Replace("-", "");
			IsNegate = num.Length != TheNum.Length;

			if (num.Replace(".", "").Replace(",", "").Length == num.Length)
			{ // integer number
				rep = num.TrimStart("0".ToCharArray());
				if (rep.Length == 0) { return "0"; }
			}
			else // non-integer number
			{
				rep = num.Trim("0".ToCharArray());
				if (rep == "." || rep == ",") { return "0"; }
				else
				{
					string cstart = rep.Substring(0, 1);
					if (cstart == "." || cstart == ",")
					{
						rep = "0" + rep;
					}
					else
					{
						string cend = rep.Substring(rep.Length - 1);
						if (cend == "." || cend == ",")
						{
							rep = rep.Substring(0, rep.Length - 1);
						}
					}
				}

			}

			if (IsNegate) { rep = "-" + rep; }
			return rep;
		}


		public void Save(string filepath)
		{
			List<string> alll = new List<string>(); //all line
			alll.Add("v1");
			foreach (MapObject mo in this.listMO)
			{
				//the properties to write depends of the maptype
				if (mo.MapType == MOType.Belt)
				{
					alll.Add("belt");
					alll.Add(mo.BeltOutput.Name); //the name property give the save result than anyFOType.ToString() for any vanilla item would have done in the past. that's why it's backward compatible.
					alll.Add(this.ConvertFloatToString(mo.vpos.X));
					alll.Add(this.ConvertFloatToString(mo.vpos.Y));
					
				}
				if (mo.MapType == MOType.Machine)
				{
					alll.Add("machine");
					alll.Add(mo.TheRecipe.Name);
					alll.Add(mo.NeedCoal.ToString().ToLower()); //tolower just to be sure
					alll.Add(this.ConvertFloatToString(mo.vpos.X));
					alll.Add(this.ConvertFloatToString(mo.vpos.Y));
					
				}
			}

			//now we write every mod currently loaded, including vanilla.
			foreach (string modname in Crafts.listLoadedModName)
			{
				alll.Add("mod");
				alll.Add(modname);
			}



			alll.Add("exit"); //files finish with exit
			try
			{
				System.IO.File.WriteAllLines(filepath, alll); //save lines
			}
			catch { }
		}









	}
}
