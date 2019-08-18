using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FactorioOrganizer
{
	public static class Crafts
	{
		public static bool AnyModLoaded = false; //indicate if any mod things were added

		public static List<sItem> listItems = new List<sItem>(); //this list contains every existing items. (belt & recipe) + belt only + recipe only
		public static List<oCraft> listCrafts = new List<oCraft>(); //this list contains every existing crafts

		public static List<Bitmap> listIcons = new List<Bitmap>(); //this list constains every existing icons of items.



		public static sItem ConvertFotTosItem(FOType ft)
		{
			string sft = ft.ToString();
			int index = 0;
			while (index < Crafts.listItems.Count)
			{
				if (Crafts.listItems[index].Name == sft)
				{
					return Crafts.listItems[index];
				}
				//next iteration
				index++;
			}
			return new sItem("none", false, false, "");
		}
		public static sItem[] ConvertArrayFotTosItem(FOType[] fts)
		{
			List<sItem> rep = new List<sItem>();
			foreach (FOType ft in fts)
			{
				rep.Add(Crafts.ConvertFotTosItem(ft));
			}
			return rep.ToArray();
		}



		//for both items and crafts (especialy crafts), if it already exist, we should override the aleady existing one. this would allow us to make a backup object list inside the program if the user don't have any files.

		public static void AddItem(sItem newitem, Bitmap ItemIcon)
		{
			Crafts.listItems.Add(newitem);
			Crafts.listIcons.Add(ItemIcon);
			newitem.IconIndex = Crafts.listIcons.Count - 1;
		}

		public static void AddCraft(oCraft newcraft)
		{
			Crafts.listCrafts.Add(newcraft);
		}






		//in future, Form1 will have a TabControl. there will be one uiToolBox per TabPage per mod.
		//when a mod will be added, Form1 will make a new tab and a new uiToolBox for the items of the mod.
		//Form1 will be listening to this event. yes, it's possible to achive this without events, but i'll do it with events. change my mind :)

		public static event EventHandler<ModEventArgs> ModAdded;

		public static void AddMod(oMod newmod)
		{


			//raise the event
			if (Crafts.ModAdded != null)
			{
				Crafts.ModAdded(null, new ModEventArgs(newmod));
			}
		}







	}
}
