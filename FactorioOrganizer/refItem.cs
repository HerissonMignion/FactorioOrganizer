using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioOrganizer
{

	//a simple reference to an item. descripbe how/where to find an item

	public class refItem
	{

		public string ItemName = "item1";
		public string ModName = "-";


		//automatically replace - by its mod name
		public string GetValidModName(string ParentModName)
		{
			if (this.ModName == "-")
			{
				return ParentModName;
			}
			else
			{
				return this.ModName;
			}
		}


		public refItem()
		{

		}
		public refItem(string sItemName, string sModName)
		{
			this.ItemName = sItemName;
			this.ModName = sModName;
		}


		public refItem GetCopy()
		{
			return new refItem(this.ItemName, this.ModName);
		}
	}
}
