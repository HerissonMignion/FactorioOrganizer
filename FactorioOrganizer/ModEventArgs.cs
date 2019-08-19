using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioOrganizer
{
	public class ModEventArgs : EventArgs
	{
		public oMod Mod;

		public ModEventArgs(oMod sMod)
		{
			this.Mod = sMod;
		}
	}
}
