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
		public static string ProgramPath = Application.ExecutablePath;
		public static string ProgramFolder { get { return System.IO.Path.GetDirectoryName(ProgramPath); } }



		public enum NextFormToShow
		{
			none,

			Form1,
			FormModEditerEmpty, //empty means that no mod file is given to her
			FormHelp1, //the help form of the form1
			FormHelpMod //the help form of the mod editer
		}

		public static NextFormToShow ActualNextForm = NextFormToShow.Form1; // Form1       FormModEditerEmpty


		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Crafts.CreateDefaultVanillaItems(); //defaults items are always the firsts to be loaded. after, it'll load the vanilla items from a .fomod file, who they'll all override the default items. if the user doesn't have that file, these default items are a kind of "emergency items" or something like that.

			//check the command line args
			string[] args = System.Environment.GetCommandLineArgs();
			//we have arguments if this array is greater than 1
			if (args.Length > 1)
			{
				try
				{

					/*
					 * the help forms are on another process than the editer so the user can consult them even if they are in the middle of a dialog phase.
					 * 
					 * 
					 * 
					 * command line args:
					 * 
					 * -help1      show the help form of form1
					 * -helpmods   show the help form of the mod editer
					 * 
					 */
					
					if (args.Length >= 2)
					{
						//MessageBox.Show(args[1]);

						//we check here the second arg
						if (args[1] == "-help1")
						{
							Program.ActualNextForm = NextFormToShow.FormHelp1;
						}
						if (args[1] == "-helpmods")
						{
							Program.ActualNextForm = NextFormToShow.FormHelpMod;
						}
					}

					

				}
				catch
				{
					//if there's an error while reading arguments, we set it to form1
					Program.ActualNextForm = NextFormToShow.Form1;
				}
			}
			

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
				if (Program.ActualNextForm == NextFormToShow.FormHelp1)
				{
					Program.ActualNextForm = NextFormToShow.none;
					f = new FormHelp();
					f.TopMost = true;
				}
				if (Program.ActualNextForm == NextFormToShow.FormHelpMod)
				{
					Program.ActualNextForm = NextFormToShow.none;
					f = new FormHelpMod();
					f.TopMost = true;
				}

				Application.Run(f);
			}
		}
	}
}
