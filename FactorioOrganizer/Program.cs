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
		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			Crafts.CreateDefualtVanillaItems(); //defaults items are always the firsts to be loaded. after, it'll load the vanilla items from a .fomod file, who they'll all override the default items. if the user doesn't have that file, these default items are a kind of "emergency items" or something like that.


			Application.Run(new Form1());
		}
	}
}
