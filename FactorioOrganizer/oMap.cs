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
		


	
		public oMap()
		{

		}
		public oMap(string filepath)
		{
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
					MapObject mo = new MapObject(MOType.Belt, Utilz.GetFOTypeAssociatedToString(strBeltOutput));
					mo.vpos.X = Convert.ToSingle(sr.ReadLine().Replace(".", ","));
					mo.vpos.Y = Convert.ToSingle(sr.ReadLine().Replace(".", ","));
					this.listMO.Add(mo);
				}
				if (newitem == "machine")
				{
					string strRecipe = sr.ReadLine();
					MapObject mo = new MapObject(MOType.Machine, Utilz.GetFOTypeAssociatedToString(strRecipe));
					mo.NeedCoal = sr.ReadLine() == "true";
					mo.vpos.X = Convert.ToSingle(sr.ReadLine().Replace(".", ","));
					mo.vpos.Y = Convert.ToSingle(sr.ReadLine().Replace(".", ","));
					this.listMO.Add(mo);
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
		
		//return the closest object with specified output and maptype.
		//for machine, it's assumed it must search in every outputs.
		//return null if nothing found
		public MapObject FindClosestMoWithOutput(MapObject mo, MOType MapType, FOType ft)
		{
			MapObject mo1 = null;
			List<MapObject> lco = this.listMO.FindAll(x => x.MapType == MapType); //we first extract MapObject of the desired type
			lco.Remove(mo); //remove the object in the middle of the map 
			if (lco.Count > 0)
			{
				//we sort items from their distance to mo
				lco = lco.OrderBy(x => x.DistTo(mo.vpos.X, mo.vpos.Y)).ToList();
				if (MapType == MOType.Belt)
				{
					//research the closest belt of the desired output. lco is sorted by distance. going through the list from the beginning is going through the closest to the farthest
					foreach (MapObject submo in lco)
					{
						if (submo.MapType == MOType.Belt) //always true because of the second line of this function
						{
							if (submo.BeltOutput == ft) //check it has the desired output
							{
								mo1 = submo;
								break;
							}
						}
					}
				}
				if (MapType == MOType.Machine)
				{
					bool canexit = false;
					//research the closest machine with a desired output. lco is sorted by distance
					foreach (MapObject submo in lco)
					{
						if (submo.MapType == MOType.Machine) //always true because of the second line of this function
						{
							//search for an output of the desired type
							foreach (FOType subout in submo.Outputs)
							{
								//check if this output is ok
								if (subout == ft) 
								{
									mo1 = submo;
									canexit = true;
									break;
								}
							}
						}
						if (canexit) { break; }
					}
				}
			}
			
			return mo1;
		}

		//not used anywhere
		public MapObject FindClosestMoWithInput(MapObject mo, FOType ft)
		{
			MapObject mo1 = null;
			List<MapObject> lco = new List<MapObject>();
			lco.AddRange(this.listMO);
			lco.Remove(mo); //retire l'object qui est au centre
			if (lco.Count > 0)
			{
				lco = lco.OrderBy(x => x.DistTo(mo.vpos.X, mo.vpos.Y)).ToList();
				bool canexit = false;
				foreach (MapObject submo in lco)
				{
					//after the if, it check it has the desired input ////    après les if, vérifie s'il y a le input désiré
					if (submo.MapType == MOType.Belt)
					{
						if (submo.BeltOutput == ft)
						{
							mo1 = submo;
							break;
						}
					}
					if (submo.MapType == MOType.Machine)
					{
						//look through the inputs ////    recherche dans les input de la machine
						foreach (FOType subft in submo.Inputs)
						{
							//check if they re the same type ////    check s'il sont de même type
							if (subft == ft)
							{
								mo1 = submo;
								canexit = true;
								break;
							}
						}
					}
					if (canexit) { break; }
				}
			}
			return mo1;
		}

		//return the closest belt with desired output
		public MapObject FindClosestBeltWithInput(MapObject mo, FOType ft)
		{
			MapObject mo1 = null;
			List<MapObject> lco = this.listMO.FindAll(x => x.MapType == MOType.Belt); //sort for belts
			lco.Remove(mo); //remove the object at the middle of the map
			if (lco.Count > 0)
			{
				//we sort by distance
				lco = lco.OrderBy(x => x.DistTo(mo.vpos.X, mo.vpos.Y)).ToList();
				//find the closest belt
				foreach (MapObject submo in lco)
				{
					//after the if, it check it has the desired input
					if (submo.MapType == MOType.Belt) //always true because of the second line of this function
					{
						if (submo.BeltOutput == ft) //check the type
						{
							mo1 = submo;
							break;
						}
					}
				}
			}
			return mo1;
		}

		
		//return the closest belt with a compatible output
		//the only purpose of giving mo to the function is to know the coordinate. don't be confused
		public MapObject GetCompatibleBeltCloseTo(MapObject mo, FOType OutType)
		{
			MapObject mo1 = null;
			List<MapObject> lco = this.listMO.FindAll(x => (x.MapType == MOType.Belt) && (x.BeltOutput == OutType)); //get belts of compatible output
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
			List<MapObject> lco = this.listMO.FindAll(x => x.MapType == MOType.Belt && x.BeltOutput == mo.BeltOutput);
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
					alll.Add(mo.BeltOutput.ToString());
					alll.Add(this.ConvertFloatToString(mo.vpos.X));
					alll.Add(this.ConvertFloatToString(mo.vpos.Y));
					
				}
				if (mo.MapType == MOType.Machine)
				{
					alll.Add("machine");
					alll.Add(mo.TheRecipe.ToString());
					alll.Add(mo.NeedCoal.ToString().ToLower()); //tolower just to be sure
					alll.Add(this.ConvertFloatToString(mo.vpos.X));
					alll.Add(this.ConvertFloatToString(mo.vpos.Y));
					
				}
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
