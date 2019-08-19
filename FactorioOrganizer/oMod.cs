using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FactorioOrganizer
{
	//represent a mod
	public class oMod
	{

		public string ModName = ""; //name of the mod


		public class ModItem
		{
			public string ItemName; //name of the item
			public string IconName; //name of the file of the associated icon

			public bool IsBelt = true;
			public bool IsRecipe = true;

			public ModItem(string sItemName, string sIconName)
			{
				this.ItemName = sItemName;
				this.IconName = sIconName;
			}
		}
		public class ModImage
		{
			public Bitmap img;
			public string FileName; //name of the file

			public ModImage(Bitmap simg, string sFileName)
			{
				this.img = simg;
				this.FileName = sFileName;
			}
		}




		


		



	}
}
