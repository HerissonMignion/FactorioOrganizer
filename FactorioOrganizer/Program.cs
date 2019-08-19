using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactorioOrganizer
{
	static class Program
	{
		public static void wdebug(object text) { System.Diagnostics.Debug.WriteLine(text); }


		public enum NextFormToShow
		{
			none,

			Form1,
			FormModEditerEmpty //empty means that to mod file is given to her
		}

		public static NextFormToShow ActualNextForm = NextFormToShow.Form1;


		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			Crafts.CreateDefualtVanillaItems(); //defaults items are always the firsts to be loaded. after, it'll load the vanilla items from a .fomod file, who they'll all override the default items. if the user doesn't have that file, these default items are a kind of "emergency items" or something like that.


			while (Program.ActualNextForm != NextFormToShow.none)
			{
				Form f = null;

				if (Program.ActualNextForm == NextFormToShow.Form1)
				{
					Program.ActualNextForm = NextFormToShow.none;
					f = new Form1();
				}
				if (Program.ActualNextForm == NextFormToShow.FormModEditerEmpty)
				{
					Program.ActualNextForm = NextFormToShow.none;
					f = new FormModEditer();
				}

				Application.Run(f);
			}
		}
	}
}
