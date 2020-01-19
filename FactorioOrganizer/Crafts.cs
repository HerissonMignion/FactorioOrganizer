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

		/*
		 * when adding a new item into the big list, we must check if this item already exist. if it already exist, we must transform the already existing object oItem
		 * to the properties of the new oItem. this is because a mod can edit other mod's items. there must be one single object (shared with every body) per each existing 
		 * items. 
		 * 
		 */


		public static List<oItem> listItems = (new oItem[] { new oItem("none", true, "vanilla") }).ToList(); //this list contains every existing items. (belt & recipe) + belt only + recipe only
		public static List<oCraft> listCrafts = new List<oCraft>(); //this list contains every existing crafts

		public static List<Bitmap> listIcons = new List<Bitmap>(); //this list constains every existing icons of items.
		public static Bitmap GetAssociatedIcon(oItem i)
		{
			//sItems contain a number who indicate the index of their icon. we don't have to use a foreach or a while.
			if (i.IconIndex >= 0 && i.IconIndex < Crafts.listIcons.Count)
			{
				return Crafts.listIcons[i.IconIndex];
			}
			return FactorioOrganizer.Properties.Resources.assembling_machine_0;
		}


		//get a craft from the item used as recipe
		public static oCraft GetCraftFromRecipe(oItem recipe)
		{
			foreach (oCraft c in Crafts.listCrafts)
			{
				if (c.Recipe.Name == recipe.Name)
				{
					return c;
				}
			}
			return null;
		}

		//returns if the given item is a recipe.
		//it checks if the given item is the recipe of any crafts.
		public static bool IsARecipe(oItem item)
		{
			return Crafts.IsARecipe(item.Name);
		}
		public static bool IsARecipe(string Name)
		{
			foreach (oCraft c in Crafts.listCrafts)
			{
				if (c.Recipe.Name == Name)
				{
					return true;
				}
			}
			return false;
		}




		/*
		 * yes, the new system can be a little bit annoying because we often have to use GetItemFromName to get anything, but expendability is necessary for mods
		 * 
		 */


		//.Name property. its internal name. the .Name property is a string that contain both the name of the item and the name of the mod, so it's unique for every item.
		//if Strict==false, it ToLower the names.
		public static oItem GetItemFromName(string name, bool Strict = false)
		{
			//search the item
			if (Strict)
			{
				foreach (oItem i in Crafts.listItems)
				{
					if (i.Name == name) { return i; }
				}
			}
			else
			{
				string tlname = name.ToLower();
				foreach (oItem i in Crafts.listItems)
				{
					if (i.Name.ToLower() == tlname) { return i; }
				}
			}
			//the none item is the first element of the list
			return Crafts.listItems[0];
		}

		//return null if it doesn't find it
		public static oItem FindItem(string ItemName, string ModName)
		{
			string sitemname = ItemName.ToLower();
			string smodname = ModName.ToLower();
			foreach (oItem i in Crafts.listItems)
			{
				if (i.ItemName.ToLower() == sitemname && i.ModName.ToLower() == smodname)
				{
					return i;
				}
			}
			return null;
		}


		//for both items and crafts (especialy crafts), if it already exist, we override the aleady existing one. this would allow us to make a backup object list inside the program if the user don't have any files. the next version of FO, the one with mod support, will have a file with it, a .fomod file who contains vanilla items/crafts. if the user don't have it, he will have the defaults items/crafts.
		//overriding already existing items/crafts, combined with mod load order, will allow mods to override other mod's items.


		public static void AddItem(oItem newitem, Bitmap ItemIcon)
		{
			bool AlreadyExist = false;
			//search if that item already exist. if so, it replace it
			int index = 0;
			while (index < Crafts.listItems.Count)
			{
				//check if it's the same item by comparing the names. the names contains 2 parts : the mod name and the item name.
				if (Crafts.listItems[index].Name == newitem.Name)
				{
					AlreadyExist = true;

					//save the index of the icon
					int iconindex = Crafts.listItems[index].IconIndex;

					//this is the already existing item. we must transform it into the newitem.
					oItem item = Crafts.listItems[index];

					//about the properties we must transfer into the already existing item : 
					//the name of the item and its modname will stay the same. IsReceipe is computed automatically and IconIndex will not change.
					//the only properties to transfer are realy just IsBelt and its image.

					item.IsBelt = newitem.IsBelt;


					//override the icon, if it had one
					if (iconindex >= 0 && iconindex < Crafts.listIcons.Count) //check it's in bound
					{
						Crafts.listIcons[iconindex] = ItemIcon;
					}

					//newitem will be lost

					break; //we don't have to check for other items if we have already found it
				}
				//next iteration
				index++;
			}
			//if the item didn't already exist, we add it
			if (!AlreadyExist)
			{
				newitem.IconIndex = Crafts.listIcons.Count; //must be done before adding it to the list
				Crafts.listItems.Add(newitem);
				Crafts.listIcons.Add(ItemIcon);
			}
		}

		public static void AddCraft(oCraft newcraft)
		{
			bool AlreadyExist = false;
			//search if that craft already exist. if so, it replace it
			int index = 0;
			while (index < Crafts.listCrafts.Count)
			{
				//check if it's the same craft. each craft are supposed to be uniquely identified by their recipe. so if the recipes names are identical, it's the same craft and we override it
				if (Crafts.listCrafts[index].Recipe.Name == newcraft.Recipe.Name)
				{
					AlreadyExist = true;

					//we copy every properties of the new craft into the already existing craft.
					oCraft craft = Crafts.listCrafts[index];
					//the receipe of the craft will stay the same

					craft.IsMadeInFurnace = newcraft.IsMadeInFurnace;
					craft.Inputs = newcraft.Inputs;
					craft.Outputs = newcraft.Outputs;


					break; //we don't have to check for other crafts if we already found it
				}
				//next iteration
				index++;
			}
			//if the craft don't already exist, we add it
			if (!AlreadyExist)
			{
				Crafts.listCrafts.Add(newcraft);
			}
		}






		//Form1 have a TabControl. there is one uiToolBox per TabPage per mod, and by default a tabpage for the vanilla items.
		//when a mod is added, Form1 make a new tab and a new uiToolBox for the items of the mod.
		//Form1 is listening to the event ModAdded. when this event is raised, it use the ModEventArgs to get the modname and other information, if needed.

		//mods can add items and crafts to other mods, modify other mods' items and crafts and maybe in the future remove items and crafts of other mods.
		//for this reason, after a mod was added, every uiToolBox must refresh its content. when adding multiple mods at once, this refresh obviously only need to
		//be done one single time, after all raise of ModAdded. that event is ModImportFinished.
		//uiToolBox are listening to ModImportFinished and when it's raised, they refresh their content.


		public static List<string> listLoadedModName = new List<string>();


		public static event EventHandler<ModEventArgs> ModAdded;
		private static void Raise_ModAdded(oMod newmod)
		{
			if (Crafts.ModAdded != null)
			{
				Crafts.ModAdded(null, new ModEventArgs(newmod));
			}
		}
		

		//this event is raised after every ModAdded, only one time.
		public static event EventHandler ModImportFinished;
		public static void Raise_ModImportFinished()
		{
			if (Crafts.ModImportFinished != null)
			{
				Crafts.ModImportFinished(null, new EventArgs());
			}
		}



		//import the content of a mod
		private static void AddMod(oMod newmod, bool RaiseFinishEvent = true)
		{
			string strmodname = newmod.ModName; //generate a valid string for the internal name of the mod. it remove every non valid caracter. we must use this one.

			bool AnyInternalItem = false; //will become true if the mod have any internal item. if still false at the end, the mod name will not be added to the list of loaded mod and the event ModAdded will not be raised for this mod.

			//////every items must be loaded then we can load the crafts.

			////load the items
			foreach (oMod.ModItem i in newmod.listItems)
			{
				////create the oItem corresponding to this ModItem
				oItem newi = new oItem(i.ItemName, i.IsBelt, i.ItemModName);
				Bitmap img = i.Img;

				//if an item belongs to its own mod, its mod name will be "-". but if that's the case, we must replace - by its mod name.
				if (newi.ModName.Trim() == "-")
				{
					AnyInternalItem = true;

					newi.ModName = strmodname;
				}
				else
				{
					//if it's an item external to this mod, we don't have to touch to its modname but the given ModItem may not have an image. if that's the case, we must find the original item, which is supposed to be already loaded because of mod load order, and get its image.


				}


				//one last check to make sure that it's not a null image
				if (img == null) { img = FactorioOrganizer.Properties.Resources.assembling_machine_0; }
				//add the new item
				Crafts.AddItem(newi, i.Img); //this void automatically check if the item already exist.
			}
			////load the crafts
			foreach (oMod.ModCraft c in newmod.listCrafts)
			{
				//the void AddCraft will automatically transfer the values of the craft we give him to the already existing craft of the same recipe, if it already exist.
				//but we still have to find the oItem that must go in the inputs and outputs, whatever the craft already exist or not.

				////gets every param to send to the constructor of oCraft.
				oItem Recipe = Crafts.FindItem(c.Recipe.ItemName, c.Recipe.GetValidModName(strmodname));

				//if the recipe was not found, we don't go any further and we pass to the next mod craft
				if (Recipe != null)
				{
					List<oItem> Inputs = new List<oItem>();
					foreach (refItem refi in c.Inputs)
					{
						oItem newinput = Crafts.FindItem(refi.ItemName, refi.GetValidModName(strmodname));
						if (newinput != null)
						{
							Inputs.Add(newinput);
						}
					}

					List<oItem> Outputs = new List<oItem>();
					foreach (refItem refi in c.Outputs)
					{
						oItem newoutput = Crafts.FindItem(refi.ItemName, refi.GetValidModName(strmodname));
						if (newoutput != null)
						{
							Outputs.Add(newoutput);
						}
					}

					//create the oCraft
					oCraft newc = new oCraft(Recipe, Inputs.ToArray(), Outputs.ToArray(), c.IsMadeInFurnace);

					//add the craft
					Crafts.AddCraft(newc);
				}
				else
				{

				}
			}



			//raise the events.
			//add the modname to the list of loaded mods.
			if (AnyInternalItem)
			{
				Crafts.listLoadedModName.Add(strmodname);

				Crafts.Raise_ModAdded(newmod);
			}
			if (RaiseFinishEvent)
			{
				Crafts.Raise_ModImportFinished();
			}
		}
		public static void AddMod(oMod newmod)
		{
			Crafts.AddMod(newmod, true);
		}

		//this void unload every current mods
		public static void UnloadEveryMods()
		{
			//remove every items, crafts and icons
			Crafts.listItems.RemoveRange(0, Crafts.listItems.Count);
			Crafts.listCrafts.RemoveRange(0, Crafts.listCrafts.Count);
			Crafts.listIcons.RemoveRange(0, Crafts.listIcons.Count);

			//remove every mod from the list of loaded mods
			Crafts.listLoadedModName.RemoveRange(0, Crafts.listLoadedModName.Count);

			//reload the vanilla items
			Crafts.CreateDefaultVanillaItems();

		}


		//this void adds multiple mods at the same time in the given order. it should be faster because it doesn't raise the events during the process.
		public static void AddMod(List<oMod> listnewmods)
		{
			//add every mod
			foreach (oMod newmod in listnewmods)
			{
				Crafts.AddMod(newmod, false);
			}

			//raise the finish event
			Crafts.Raise_ModImportFinished();
		}










		public static void CreateDefaultVanillaItems()
		{
			Crafts.listLoadedModName.Add("vanilla");


			Crafts.AddItem(new oItem("none", true, "vanilla"), FactorioOrganizer.Properties.Resources.assembling_machine_0);
			//Crafts.AddItem(new sItem("TEST", false, true, "vanilla"), imghere);

			Crafts.AddItem(new oItem("OreIron", true, "vanilla"), FactorioOrganizer.Properties.Resources.iron_ore);
			Crafts.AddItem(new oItem("OreCopper", true, "vanilla"), FactorioOrganizer.Properties.Resources.copper_ore);
			Crafts.AddItem(new oItem("IronPlate", true, "vanilla"), FactorioOrganizer.Properties.Resources.iron_plate);
			Crafts.AddItem(new oItem("CopperPlate", true, "vanilla"), FactorioOrganizer.Properties.Resources.copper_plate);
			Crafts.AddItem(new oItem("Coal", true, "vanilla"), FactorioOrganizer.Properties.Resources.coal_dark_background);
			Crafts.AddItem(new oItem("Stone", true, "vanilla"), FactorioOrganizer.Properties.Resources.stone);
			Crafts.AddItem(new oItem("Wood", true, "vanilla"), FactorioOrganizer.Properties.Resources.wood);
			Crafts.AddItem(new oItem("SteelPlate", true, "vanilla"), FactorioOrganizer.Properties.Resources.steel_plate);
			Crafts.AddItem(new oItem("Plastic", true, "vanilla"), FactorioOrganizer.Properties.Resources.plastic_bar);
			Crafts.AddItem(new oItem("StoneBrick", true, "vanilla"), FactorioOrganizer.Properties.Resources.stone_brick);

			Crafts.AddItem(new oItem("IronGear", true, "vanilla"), FactorioOrganizer.Properties.Resources.iron_gear_wheel);
			Crafts.AddItem(new oItem("IronStick", true, "vanilla"), FactorioOrganizer.Properties.Resources.iron_stick);
			Crafts.AddItem(new oItem("CopperCable", true, "vanilla"), FactorioOrganizer.Properties.Resources.copper_cable);

			Crafts.AddItem(new oItem("ChestWood", true, "vanilla"), FactorioOrganizer.Properties.Resources.wooden_chest);
			Crafts.AddItem(new oItem("ChestIron", true, "vanilla"), FactorioOrganizer.Properties.Resources.iron_chest);
			Crafts.AddItem(new oItem("ChestSteel", true, "vanilla"), FactorioOrganizer.Properties.Resources.steel_chest);
			Crafts.AddItem(new oItem("ChestLogistic", true, "vanilla"), FactorioOrganizer.Properties.Resources.logistic_chest_buffer);
			Crafts.AddItem(new oItem("StorageTank", true, "vanilla"), FactorioOrganizer.Properties.Resources.storage_tank);

			Crafts.AddItem(new oItem("Belt", true, "vanilla"), FactorioOrganizer.Properties.Resources.transport_belt);
			Crafts.AddItem(new oItem("BeltUnderground", true, "vanilla"), FactorioOrganizer.Properties.Resources.underground_belt);
			Crafts.AddItem(new oItem("BeltSplitter", true, "vanilla"), FactorioOrganizer.Properties.Resources.splitter);
			Crafts.AddItem(new oItem("BeltFast", true, "vanilla"), FactorioOrganizer.Properties.Resources.fast_transport_belt);
			Crafts.AddItem(new oItem("BeltFastUnderground", true, "vanilla"), FactorioOrganizer.Properties.Resources.fast_underground_belt);
			Crafts.AddItem(new oItem("BeltFastSplitter", true, "vanilla"), FactorioOrganizer.Properties.Resources.fast_splitter);
			Crafts.AddItem(new oItem("BeltExpress", true, "vanilla"), FactorioOrganizer.Properties.Resources.express_transport_belt);
			Crafts.AddItem(new oItem("BeltExpressUnderground", true, "vanilla"), FactorioOrganizer.Properties.Resources.express_underground_belt);
			Crafts.AddItem(new oItem("BeltExpressSplitter", true, "vanilla"), FactorioOrganizer.Properties.Resources.express_splitter);

			Crafts.AddItem(new oItem("Boiler", true, "vanilla"), FactorioOrganizer.Properties.Resources.boiler);
			Crafts.AddItem(new oItem("SteamEngine", true, "vanilla"), FactorioOrganizer.Properties.Resources.steam_engine);
			Crafts.AddItem(new oItem("SteamTurbine", true, "vanilla"), FactorioOrganizer.Properties.Resources.steam_turbine);
			Crafts.AddItem(new oItem("SolarPanel", true, "vanilla"), FactorioOrganizer.Properties.Resources.solar_panel);
			Crafts.AddItem(new oItem("Accumulator", true, "vanilla"), FactorioOrganizer.Properties.Resources.accumulator);
			Crafts.AddItem(new oItem("NuclearReactor", true, "vanilla"), FactorioOrganizer.Properties.Resources.nuclear_reactor);
			Crafts.AddItem(new oItem("HeatExchanger", true, "vanilla"), FactorioOrganizer.Properties.Resources.heat_boiler);
			Crafts.AddItem(new oItem("HeatPipe", true, "vanilla"), FactorioOrganizer.Properties.Resources.heat_pipe);
			Crafts.AddItem(new oItem("ElectricMiningDrill", true, "vanilla"), FactorioOrganizer.Properties.Resources.electric_mining_drill);
			Crafts.AddItem(new oItem("OffshorePump", true, "vanilla"), FactorioOrganizer.Properties.Resources.offshore_pump);
			Crafts.AddItem(new oItem("Pumpjack", true, "vanilla"), FactorioOrganizer.Properties.Resources.pumpjack);
			Crafts.AddItem(new oItem("FurnaceStone", true, "vanilla"), FactorioOrganizer.Properties.Resources.stone_furnace);
			Crafts.AddItem(new oItem("FurnaceSteel", true, "vanilla"), FactorioOrganizer.Properties.Resources.steel_furnace);
			Crafts.AddItem(new oItem("FurnaceElectric", true, "vanilla"), FactorioOrganizer.Properties.Resources.electric_furnace);
			Crafts.AddItem(new oItem("GreenCircuit", true, "vanilla"), FactorioOrganizer.Properties.Resources.electronic_circuit);
			Crafts.AddItem(new oItem("RedCircuit", true, "vanilla"), FactorioOrganizer.Properties.Resources.advanced_circuit);
			Crafts.AddItem(new oItem("ProcessingUnit", true, "vanilla"), FactorioOrganizer.Properties.Resources.processing_unit);

			Crafts.AddItem(new oItem("ElectricPole", true, "vanilla"), FactorioOrganizer.Properties.Resources.small_electric_pole);
			Crafts.AddItem(new oItem("MediumElectricPole", true, "vanilla"), FactorioOrganizer.Properties.Resources.medium_electric_pole);
			Crafts.AddItem(new oItem("BigElectricPole", true, "vanilla"), FactorioOrganizer.Properties.Resources.big_electric_pole);
			Crafts.AddItem(new oItem("Pipe", true, "vanilla"), FactorioOrganizer.Properties.Resources.pipe);
			Crafts.AddItem(new oItem("PipeUnderground", true, "vanilla"), FactorioOrganizer.Properties.Resources.pipe_to_ground);
			Crafts.AddItem(new oItem("Pump", true, "vanilla"), FactorioOrganizer.Properties.Resources.pump);

			Crafts.AddItem(new oItem("Inserter", true, "vanilla"), FactorioOrganizer.Properties.Resources.inserter);
			Crafts.AddItem(new oItem("InserterLong", true, "vanilla"), FactorioOrganizer.Properties.Resources.long_handed_inserter);
			Crafts.AddItem(new oItem("InserterFast", true, "vanilla"), FactorioOrganizer.Properties.Resources.fast_inserter);
			Crafts.AddItem(new oItem("InserterFilter", true, "vanilla"), FactorioOrganizer.Properties.Resources.filter_inserter);
			Crafts.AddItem(new oItem("InserterStack", true, "vanilla"), FactorioOrganizer.Properties.Resources.stack_inserter);
			Crafts.AddItem(new oItem("InserterStackFilter", true, "vanilla"), FactorioOrganizer.Properties.Resources.stack_filter_inserter);

			Crafts.AddItem(new oItem("AssemblingMachine", true, "vanilla"), FactorioOrganizer.Properties.Resources.assembling_machine_1);
			Crafts.AddItem(new oItem("AssemblingMachine2", true, "vanilla"), FactorioOrganizer.Properties.Resources.assembling_machine_2);
			Crafts.AddItem(new oItem("AssemblingMachine3", true, "vanilla"), FactorioOrganizer.Properties.Resources.assembling_machine_3);
			Crafts.AddItem(new oItem("OilRefinery", true, "vanilla"), FactorioOrganizer.Properties.Resources.oil_refinery);
			Crafts.AddItem(new oItem("ChemicalPlant", true, "vanilla"), FactorioOrganizer.Properties.Resources.chemical_plant);
			Crafts.AddItem(new oItem("Centrifuge", true, "vanilla"), FactorioOrganizer.Properties.Resources.centrifuge);
			Crafts.AddItem(new oItem("Lab", true, "vanilla"), FactorioOrganizer.Properties.Resources.lab);


			Crafts.AddItem(new oItem("ScienceRed", true, "vanilla"), FactorioOrganizer.Properties.Resources.automation_science_pack);
			Crafts.AddItem(new oItem("ScienceGreen", true, "vanilla"), FactorioOrganizer.Properties.Resources.logistic_science_pack);
			Crafts.AddItem(new oItem("ScienceGrey", true, "vanilla"), FactorioOrganizer.Properties.Resources.military_science_pack);
			Crafts.AddItem(new oItem("ScienceBlue", true, "vanilla"), FactorioOrganizer.Properties.Resources.chemical_science_pack);
			Crafts.AddItem(new oItem("ScienceViolet", true, "vanilla"), FactorioOrganizer.Properties.Resources.production_science_pack);
			Crafts.AddItem(new oItem("ScienceYellow", true, "vanilla"), FactorioOrganizer.Properties.Resources.utility_science_pack);

			Crafts.AddItem(new oItem("Rail", true, "vanilla"), FactorioOrganizer.Properties.Resources.rail);
			Crafts.AddItem(new oItem("TrainStop", true, "vanilla"), FactorioOrganizer.Properties.Resources.train_stop);
			Crafts.AddItem(new oItem("RailSignal", true, "vanilla"), FactorioOrganizer.Properties.Resources.rail_signal);
			Crafts.AddItem(new oItem("RailChainSignal", true, "vanilla"), FactorioOrganizer.Properties.Resources.rail_chain_signal);
			Crafts.AddItem(new oItem("Locomotive", true, "vanilla"), FactorioOrganizer.Properties.Resources.diesel_locomotive);
			Crafts.AddItem(new oItem("CargoWagon", true, "vanilla"), FactorioOrganizer.Properties.Resources.cargo_wagon);
			Crafts.AddItem(new oItem("FluidWagon", true, "vanilla"), FactorioOrganizer.Properties.Resources.fluid_wagon);
			Crafts.AddItem(new oItem("ArtilleryWagon", true, "vanilla"), FactorioOrganizer.Properties.Resources.artillery_wagon);
			Crafts.AddItem(new oItem("Car", true, "vanilla"), FactorioOrganizer.Properties.Resources.car);
			Crafts.AddItem(new oItem("Tank", true, "vanilla"), FactorioOrganizer.Properties.Resources.tank);

			Crafts.AddItem(new oItem("EngineUnit", true, "vanilla"), FactorioOrganizer.Properties.Resources.engine_unit);
			Crafts.AddItem(new oItem("EngineElectricUnit", true, "vanilla"), FactorioOrganizer.Properties.Resources.electric_engine_unit);
			Crafts.AddItem(new oItem("FlyingRobotFrame", true, "vanilla"), FactorioOrganizer.Properties.Resources.flying_robot_frame);
			Crafts.AddItem(new oItem("LogisticRobot", true, "vanilla"), FactorioOrganizer.Properties.Resources.logistic_robot);
			Crafts.AddItem(new oItem("ConstructionRobot", true, "vanilla"), FactorioOrganizer.Properties.Resources.construction_robot);
			Crafts.AddItem(new oItem("RocketPart", true, "vanilla"), FactorioOrganizer.Properties.Resources.rocket_part);
			Crafts.AddItem(new oItem("RocketControlUnit", true, "vanilla"), FactorioOrganizer.Properties.Resources.rocket_control_unit);
			Crafts.AddItem(new oItem("LowDensityStructure", true, "vanilla"), FactorioOrganizer.Properties.Resources.rocket_structure);
			Crafts.AddItem(new oItem("RocketFuel", true, "vanilla"), FactorioOrganizer.Properties.Resources.rocket_fuel);

			Crafts.AddItem(new oItem("Grenade", true, "vanilla"), FactorioOrganizer.Properties.Resources.grenade);
			Crafts.AddItem(new oItem("MagazineFirearm", true, "vanilla"), FactorioOrganizer.Properties.Resources.firearm_magazine);
			Crafts.AddItem(new oItem("MagazinePiercing", true, "vanilla"), FactorioOrganizer.Properties.Resources.piercing_rounds_magazine);
			Crafts.AddItem(new oItem("ShotgunShells", true, "vanilla"), FactorioOrganizer.Properties.Resources.shotgun_shell);
			Crafts.AddItem(new oItem("ShotgunPiercingShells", true, "vanilla"), FactorioOrganizer.Properties.Resources.piercing_shotgun_shell);
			Crafts.AddItem(new oItem("Pistol", true, "vanilla"), FactorioOrganizer.Properties.Resources.pistol);
			Crafts.AddItem(new oItem("SubmachineGun", true, "vanilla"), FactorioOrganizer.Properties.Resources.submachine_gun);
			Crafts.AddItem(new oItem("Shotgun", true, "vanilla"), FactorioOrganizer.Properties.Resources.shotgun);
			Crafts.AddItem(new oItem("CombatShotgun", true, "vanilla"), FactorioOrganizer.Properties.Resources.combat_shotgun);
			Crafts.AddItem(new oItem("LandMine", true, "vanilla"), FactorioOrganizer.Properties.Resources.land_mine);
			Crafts.AddItem(new oItem("Concrete", true, "vanilla"), FactorioOrganizer.Properties.Resources.concrete);
			Crafts.AddItem(new oItem("Wall", true, "vanilla"), FactorioOrganizer.Properties.Resources.wall);
			Crafts.AddItem(new oItem("GunTurret", true, "vanilla"), FactorioOrganizer.Properties.Resources.gun_turret);
			Crafts.AddItem(new oItem("LaserTurret", true, "vanilla"), FactorioOrganizer.Properties.Resources.laser_turret);
			Crafts.AddItem(new oItem("FlamethrowerTurret", true, "vanilla"), FactorioOrganizer.Properties.Resources.flamethrower_turret);
			Crafts.AddItem(new oItem("ArtilleryTurret", true, "vanilla"), FactorioOrganizer.Properties.Resources.artillery_turret);
			Crafts.AddItem(new oItem("Radar", true, "vanilla"), FactorioOrganizer.Properties.Resources.radar);
			Crafts.AddItem(new oItem("RocketSilo", true, "vanilla"), FactorioOrganizer.Properties.Resources.rocket_silo);

			Crafts.AddItem(new oItem("Sulfur", true, "vanilla"), FactorioOrganizer.Properties.Resources.sulfur);
			Crafts.AddItem(new oItem("SolidFuel", true, "vanilla"), FactorioOrganizer.Properties.Resources.solid_fuel);
			Crafts.AddItem(new oItem("Battery", true, "vanilla"), FactorioOrganizer.Properties.Resources.battery);
			Crafts.AddItem(new oItem("Explosives", true, "vanilla"), FactorioOrganizer.Properties.Resources.explosives);
			Crafts.AddItem(new oItem("CliffExplosives", true, "vanilla"), FactorioOrganizer.Properties.Resources.cliff_explosives);
			Crafts.AddItem(new oItem("EmptyBarrel", true, "vanilla"), FactorioOrganizer.Properties.Resources.empty_barrel);

			Crafts.AddItem(new oItem("OilCrude", true, "vanilla"), FactorioOrganizer.Properties.Resources.crude_oil);
			Crafts.AddItem(new oItem("OilLight", true, "vanilla"), FactorioOrganizer.Properties.Resources.light_oil);
			Crafts.AddItem(new oItem("OilHeavy", true, "vanilla"), FactorioOrganizer.Properties.Resources.heavy_oil);
			Crafts.AddItem(new oItem("Lubricant", true, "vanilla"), FactorioOrganizer.Properties.Resources.lubricant);
			Crafts.AddItem(new oItem("PetroleumGas", true, "vanilla"), FactorioOrganizer.Properties.Resources.petroleum_gas);
			Crafts.AddItem(new oItem("SulfuricAcid", true, "vanilla"), FactorioOrganizer.Properties.Resources.sulfuric_acid);
			Crafts.AddItem(new oItem("Water", true, "vanilla"), FactorioOrganizer.Properties.Resources.water);
			Crafts.AddItem(new oItem("Steam", true, "vanilla"), FactorioOrganizer.Properties.Resources.steam);

			//oil processing only :
			Crafts.AddItem(new oItem("BasicOilProcessing", false, "vanilla"), FactorioOrganizer.Properties.Resources.basic_oil_processing);
			Crafts.AddItem(new oItem("AdvancedOilProcessing", false, "vanilla"), FactorioOrganizer.Properties.Resources.advanced_oil_processing);
			Crafts.AddItem(new oItem("HeavyOilCracking", false, "vanilla"), FactorioOrganizer.Properties.Resources.heavy_oil_cracking);
			Crafts.AddItem(new oItem("LightOilCracking", false, "vanilla"), FactorioOrganizer.Properties.Resources.light_oil_cracking);
			Crafts.AddItem(new oItem("CoalLiquefaction", false, "vanilla"), FactorioOrganizer.Properties.Resources.coal_liquefaction);
			Crafts.AddItem(new oItem("SolidFuelFromOilHeavy", false, "vanilla"), FactorioOrganizer.Properties.Resources.solid_fuel_from_heavy_oil);
			Crafts.AddItem(new oItem("SolidFuelFromOilLight", false, "vanilla"), FactorioOrganizer.Properties.Resources.solid_fuel_from_light_oil);
			Crafts.AddItem(new oItem("SolidFuelFromPetroleumGas", false, "vanilla"), FactorioOrganizer.Properties.Resources.solid_fuel_from_petroleum_gas);

			Crafts.AddItem(new oItem("PortableSolarPanel", true, "vanilla"), FactorioOrganizer.Properties.Resources.solar_panel_equipment);
			Crafts.AddItem(new oItem("PortableFusionReactor", true, "vanilla"), FactorioOrganizer.Properties.Resources.fusion_reactor_equipment);
			Crafts.AddItem(new oItem("EnergyShield", true, "vanilla"), FactorioOrganizer.Properties.Resources.energy_shield_equipment);
			Crafts.AddItem(new oItem("EnergyShieldMK2", true, "vanilla"), FactorioOrganizer.Properties.Resources.energy_shield_mk2_equipment);
			Crafts.AddItem(new oItem("PersonalBattery", true, "vanilla"), FactorioOrganizer.Properties.Resources.battery_equipment);
			Crafts.AddItem(new oItem("PersonalBatteryMK2", true, "vanilla"), FactorioOrganizer.Properties.Resources.battery_mk2_equipment);
			Crafts.AddItem(new oItem("PersonalLaserDefense", true, "vanilla"), FactorioOrganizer.Properties.Resources.personal_laser_defense_equipment);
			Crafts.AddItem(new oItem("DischargeDefense", true, "vanilla"), FactorioOrganizer.Properties.Resources.discharge_defense_equipment);
			Crafts.AddItem(new oItem("BeltImmunity", true, "vanilla"), FactorioOrganizer.Properties.Resources.belt_immunity_equipment);
			Crafts.AddItem(new oItem("Exoskeleton", true, "vanilla"), FactorioOrganizer.Properties.Resources.exoskeleton_equipment);
			Crafts.AddItem(new oItem("PersonalRoboport", true, "vanilla"), FactorioOrganizer.Properties.Resources.personal_roboport_equipment);
			Crafts.AddItem(new oItem("PersonalRoboportMK2", true, "vanilla"), FactorioOrganizer.Properties.Resources.personal_roboport_mk2_equipment);
			Crafts.AddItem(new oItem("NightVision", true, "vanilla"), FactorioOrganizer.Properties.Resources.night_vision_equipment);


			//modules :
			Crafts.AddItem(new oItem("Beacon", true, "vanilla"), FactorioOrganizer.Properties.Resources.beacon);
			Crafts.AddItem(new oItem("ModuleSpeed1", true, "vanilla"), FactorioOrganizer.Properties.Resources.speed_module);
			Crafts.AddItem(new oItem("ModuleSpeed2", true, "vanilla"), FactorioOrganizer.Properties.Resources.speed_module_2);
			Crafts.AddItem(new oItem("ModuleSpeed3", true, "vanilla"), FactorioOrganizer.Properties.Resources.speed_module_3);
			Crafts.AddItem(new oItem("ModuleProductivity1", true, "vanilla"), FactorioOrganizer.Properties.Resources.productivity_module);
			Crafts.AddItem(new oItem("ModuleProductivity2", true, "vanilla"), FactorioOrganizer.Properties.Resources.productivity_module_2);
			Crafts.AddItem(new oItem("ModuleProductivity3", true, "vanilla"), FactorioOrganizer.Properties.Resources.productivity_module_3);
			Crafts.AddItem(new oItem("ModuleEfficiency1", true, "vanilla"), FactorioOrganizer.Properties.Resources.effectivity_module);
			Crafts.AddItem(new oItem("ModuleEfficiency2", true, "vanilla"), FactorioOrganizer.Properties.Resources.effectivity_module_2);
			Crafts.AddItem(new oItem("ModuleEfficiency3", true, "vanilla"), FactorioOrganizer.Properties.Resources.effectivity_module_3);




			////create crafts

			//https://wiki.factorio.com/Items

			//Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("TEST"), new sItem[] { Crafts.GetItemFromName("Inserter"), Crafts.GetItemFromName("InserterLong"), Crafts.GetItemFromName("InserterFast"), Crafts.GetItemFromName("InserterFilter") }, , , new sItem[] { Crafts.GetItemFromName("ScienceRed")Crafts.GetItemFromName("ScienceGreen")Crafts.GetItemFromName("ScienceGrey") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("IronPlate"), new oItem[] { Crafts.GetItemFromName("OreIron") }, new oItem[] { Crafts.GetItemFromName("IronPlate") }, true));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("CopperPlate"), new oItem[] { Crafts.GetItemFromName("OreCopper") }, new oItem[] { Crafts.GetItemFromName("CopperPlate") }, true));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SteelPlate"), new oItem[] { Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("SteelPlate") }, true));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Plastic"), new oItem[] { Crafts.GetItemFromName("PetroleumGas"), Crafts.GetItemFromName("Coal") }, new oItem[] { Crafts.GetItemFromName("Plastic") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("StoneBrick"), new oItem[] { Crafts.GetItemFromName("Stone") }, new oItem[] { Crafts.GetItemFromName("StoneBrick") }, true));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("IronGear"), new oItem[] { Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("IronGear") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("IronStick"), new oItem[] { Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("IronStick") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("CopperCable"), new oItem[] { Crafts.GetItemFromName("CopperPlate") }, new oItem[] { Crafts.GetItemFromName("CopperCable") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ElectricPole"), new oItem[] { Crafts.GetItemFromName("CopperCable"), Crafts.GetItemFromName("Wood") }, new oItem[] { Crafts.GetItemFromName("ElectricPole") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("MediumElectricPole"), new oItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronStick"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("MediumElectricPole") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BigElectricPole"), new oItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronStick"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("BigElectricPole") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Pipe"), new oItem[] { Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("Pipe") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PipeUnderground"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("Pipe") }, new oItem[] { Crafts.GetItemFromName("PipeUnderground") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Pump"), new oItem[] { Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("Pump") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ChestWood"), new oItem[] { Crafts.GetItemFromName("Wood") }, new oItem[] { Crafts.GetItemFromName("ChestWood") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ChestIron"), new oItem[] { Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("ChestIron") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ChestSteel"), new oItem[] { Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("ChestSteel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ChestLogistic"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("ChestSteel") }, new oItem[] { Crafts.GetItemFromName("ChestLogistic") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("StorageTank"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("StorageTank") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Belt"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("IronGear") }, new oItem[] { Crafts.GetItemFromName("Belt") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltFast"), new oItem[] { Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Belt") }, new oItem[] { Crafts.GetItemFromName("BeltFast") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltExpress"), new oItem[] { Crafts.GetItemFromName("BeltFast"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Lubricant") }, new oItem[] { Crafts.GetItemFromName("BeltExpress") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltUnderground"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("Belt") }, new oItem[] { Crafts.GetItemFromName("BeltUnderground") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltFastUnderground"), new oItem[] { Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("BeltUnderground") }, new oItem[] { Crafts.GetItemFromName("BeltFastUnderground") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltExpressUnderground"), new oItem[] { Crafts.GetItemFromName("BeltFastUnderground"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Lubricant") }, new oItem[] { Crafts.GetItemFromName("BeltExpressUnderground") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltSplitter"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("Belt") }, new oItem[] { Crafts.GetItemFromName("BeltSplitter") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltFastSplitter"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("BeltSplitter") }, new oItem[] { Crafts.GetItemFromName("BeltFastSplitter") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltExpressSplitter"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("BeltFastSplitter"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Lubricant") }, new oItem[] { Crafts.GetItemFromName("BeltExpressSplitter") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Boiler"), new oItem[] { Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("FurnaceStone") }, new oItem[] { Crafts.GetItemFromName("Boiler") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SteamEngine"), new oItem[] { Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("Pipe") }, new oItem[] { Crafts.GetItemFromName("SteamEngine") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SteamTurbine"), new oItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe") }, new oItem[] { Crafts.GetItemFromName("SteamTurbine") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SolarPanel"), new oItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("SolarPanel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Accumulator"), new oItem[] { Crafts.GetItemFromName("Battery"), Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("Accumulator") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("NuclearReactor"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("Concrete"), Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("NuclearReactor") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("HeatExchanger"), new oItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("HeatExchanger") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("HeatPipe"), new oItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("HeatPipe") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ElectricMiningDrill"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("ElectricMiningDrill") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("OffshorePump"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe") }, new oItem[] { Crafts.GetItemFromName("OffshorePump") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Pumpjack"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("Pumpjack") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("FurnaceStone"), new oItem[] { Crafts.GetItemFromName("Stone") }, new oItem[] { Crafts.GetItemFromName("FurnaceStone") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("FurnaceSteel"), new oItem[] { Crafts.GetItemFromName("StoneBrick"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("FurnaceSteel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("FurnaceElectric"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("SteelPlate"), Crafts.GetItemFromName("StoneBrick") }, new oItem[] { Crafts.GetItemFromName("FurnaceElectric") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("GreenCircuit"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("CopperCable") }, new oItem[] { Crafts.GetItemFromName("GreenCircuit") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RedCircuit"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("Plastic"), Crafts.GetItemFromName("CopperCable") }, new oItem[] { Crafts.GetItemFromName("RedCircuit") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ProcessingUnit"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("SulfuricAcid") }, new oItem[] { Crafts.GetItemFromName("ProcessingUnit") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Inserter"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("GreenCircuit") }, new oItem[] { Crafts.GetItemFromName("Inserter") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("InserterLong"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Inserter") }, new oItem[] { Crafts.GetItemFromName("InserterLong") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("InserterFast"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("Inserter") }, new oItem[] { Crafts.GetItemFromName("InserterFast") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("InserterFilter"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("InserterFast") }, new oItem[] { Crafts.GetItemFromName("InserterFilter") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("InserterStack"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("InserterFast"), Crafts.GetItemFromName("IronGear") }, new oItem[] { Crafts.GetItemFromName("InserterStack") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("InserterStackFilter"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("InserterStack") }, new oItem[] { Crafts.GetItemFromName("InserterStackFilter") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("AssemblingMachine"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("AssemblingMachine") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("AssemblingMachine2"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("AssemblingMachine"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("AssemblingMachine2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("AssemblingMachine3"), new oItem[] { Crafts.GetItemFromName("AssemblingMachine2"), Crafts.GetItemFromName("ModuleSpeed1") }, new oItem[] { Crafts.GetItemFromName("AssemblingMachine3") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("OilRefinery"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("OilRefinery") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ChemicalPlant"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("ChemicalPlant") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Centrifuge"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("Concrete"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("Centrifuge") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Lab"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Belt") }, new oItem[] { Crafts.GetItemFromName("Lab") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ScienceRed"), new oItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronGear") }, new oItem[] { Crafts.GetItemFromName("ScienceRed") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ScienceGreen"), new oItem[] { Crafts.GetItemFromName("Belt"), Crafts.GetItemFromName("Inserter") }, new oItem[] { Crafts.GetItemFromName("ScienceGreen") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ScienceGrey"), new oItem[] { Crafts.GetItemFromName("Grenade"), Crafts.GetItemFromName("MagazinePiercing"), Crafts.GetItemFromName("Wall") }, new oItem[] { Crafts.GetItemFromName("ScienceGrey") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ScienceBlue"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("Sulfur") }, new oItem[] { Crafts.GetItemFromName("ScienceBlue") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ScienceViolet"), new oItem[] { Crafts.GetItemFromName("FurnaceElectric"), Crafts.GetItemFromName("ModuleProductivity1"), Crafts.GetItemFromName("Rail") }, new oItem[] { Crafts.GetItemFromName("ScienceViolet") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ScienceYellow"), new oItem[] { Crafts.GetItemFromName("FlyingRobotFrame"), Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("ProcessingUnit") }, new oItem[] { Crafts.GetItemFromName("ScienceYellow") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Rail"), new oItem[] { Crafts.GetItemFromName("IronStick"), Crafts.GetItemFromName("SteelPlate"), Crafts.GetItemFromName("Stone") }, new oItem[] { Crafts.GetItemFromName("Rail") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("TrainStop"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("IronStick"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("TrainStop") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RailSignal"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("RailSignal") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RailChainSignal"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("RailChainSignal") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Locomotive"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("Locomotive") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("CargoWagon"), new oItem[] { Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("CargoWagon") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("FluidWagon"), new oItem[] { Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate"), Crafts.GetItemFromName("StorageTank") }, new oItem[] { Crafts.GetItemFromName("FluidWagon") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ArtilleryWagon"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("ArtilleryWagon") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Car"), new oItem[] { Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("Car") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Tank"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("Tank") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("EngineUnit"), new oItem[] { Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("EngineUnit") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("EngineElectricUnit"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("Lubricant") }, new oItem[] { Crafts.GetItemFromName("EngineElectricUnit") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("FlyingRobotFrame"), new oItem[] { Crafts.GetItemFromName("Battery"), Crafts.GetItemFromName("EngineElectricUnit"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("FlyingRobotFrame") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("LogisticRobot"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("FlyingRobotFrame") }, new oItem[] { Crafts.GetItemFromName("LogisticRobot") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ConstructionRobot"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("FlyingRobotFrame") }, new oItem[] { Crafts.GetItemFromName("ConstructionRobot") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RocketPart"), new oItem[] { Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("RocketControlUnit"), Crafts.GetItemFromName("RocketFuel") }, new oItem[] { Crafts.GetItemFromName("RocketPart") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RocketControlUnit"), new oItem[] { Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleSpeed1") }, new oItem[] { Crafts.GetItemFromName("RocketControlUnit") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("LowDensityStructure"), new oItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("Plastic"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("LowDensityStructure") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RocketFuel"), new oItem[] { Crafts.GetItemFromName("OilLight"), Crafts.GetItemFromName("SolidFuel") }, new oItem[] { Crafts.GetItemFromName("RocketFuel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Grenade"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("Coal") }, new oItem[] { Crafts.GetItemFromName("Grenade") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("MagazineFirearm"), new oItem[] { Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("MagazineFirearm") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("MagazinePiercing"), new oItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("MagazineFirearm"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("MagazinePiercing") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ShotgunShells"), new oItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("ShotgunShells") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ShotgunPiercingShells"), new oItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("ShotgunShells"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("ShotgunPiercingShells") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Pistol"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("CopperPlate") }, new oItem[] { Crafts.GetItemFromName("Pistol") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SubmachineGun"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronGear") }, new oItem[] { Crafts.GetItemFromName("SubmachineGun") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Shotgun"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Wood") }, new oItem[] { Crafts.GetItemFromName("Shotgun") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("CombatShotgun"), new oItem[] { Crafts.GetItemFromName("SteelPlate"), Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Wood") }, new oItem[] { Crafts.GetItemFromName("CombatShotgun") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("LandMine"), new oItem[] { Crafts.GetItemFromName("Explosives"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("LandMine") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Concrete"), new oItem[] { Crafts.GetItemFromName("OreIron"), Crafts.GetItemFromName("StoneBrick"), Crafts.GetItemFromName("Water") }, new oItem[] { Crafts.GetItemFromName("Concrete") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Wall"), new oItem[] { Crafts.GetItemFromName("StoneBrick") }, new oItem[] { Crafts.GetItemFromName("Wall") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("GunTurret"), new oItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("GunTurret") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("LaserTurret"), new oItem[] { Crafts.GetItemFromName("Battery"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("LaserTurret") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("FlamethrowerTurret"), new oItem[] { Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("FlamethrowerTurret") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ArtilleryTurret"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("Concrete"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("ArtilleryTurret") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Radar"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("IronPlate") }, new oItem[] { Crafts.GetItemFromName("Radar") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RocketSilo"), new oItem[] { Crafts.GetItemFromName("Concrete"), Crafts.GetItemFromName("EngineElectricUnit"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("RocketSilo") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Sulfur"), new oItem[] { Crafts.GetItemFromName("PetroleumGas"), Crafts.GetItemFromName("Water") }, new oItem[] { Crafts.GetItemFromName("Sulfur") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Battery"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("SulfuricAcid") }, new oItem[] { Crafts.GetItemFromName("Battery") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Explosives"), new oItem[] { Crafts.GetItemFromName("Coal"), Crafts.GetItemFromName("Sulfur"), Crafts.GetItemFromName("Water") }, new oItem[] { Crafts.GetItemFromName("Explosives") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("CliffExplosives"), new oItem[] { Crafts.GetItemFromName("EmptyBarrel"), Crafts.GetItemFromName("Explosives"), Crafts.GetItemFromName("Grenade") }, new oItem[] { Crafts.GetItemFromName("CliffExplosives") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("EmptyBarrel"), new oItem[] { Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("EmptyBarrel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Lubricant"), new oItem[] { Crafts.GetItemFromName("OilHeavy") }, new oItem[] { Crafts.GetItemFromName("Lubricant") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SulfuricAcid"), new oItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("Sulfur"), Crafts.GetItemFromName("Water") }, new oItem[] { Crafts.GetItemFromName("SulfuricAcid") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BasicOilProcessing"), new oItem[] { Crafts.GetItemFromName("OilCrude") }, new oItem[] { Crafts.GetItemFromName("PetroleumGas") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("AdvancedOilProcessing"), new oItem[] { Crafts.GetItemFromName("OilCrude"), Crafts.GetItemFromName("Water") }, new oItem[] { Crafts.GetItemFromName("PetroleumGas"), Crafts.GetItemFromName("OilLight"), Crafts.GetItemFromName("OilHeavy") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("HeavyOilCracking"), new oItem[] { Crafts.GetItemFromName("OilHeavy"), Crafts.GetItemFromName("Water") }, new oItem[] { Crafts.GetItemFromName("OilLight") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("LightOilCracking"), new oItem[] { Crafts.GetItemFromName("OilLight"), Crafts.GetItemFromName("Water") }, new oItem[] { Crafts.GetItemFromName("PetroleumGas") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("CoalLiquefaction"), new oItem[] { Crafts.GetItemFromName("Coal"), Crafts.GetItemFromName("OilHeavy"), Crafts.GetItemFromName("Steam") }, new oItem[] { Crafts.GetItemFromName("OilHeavy"), Crafts.GetItemFromName("OilLight"), Crafts.GetItemFromName("PetroleumGas") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SolidFuelFromOilHeavy"), new oItem[] { Crafts.GetItemFromName("OilHeavy") }, new oItem[] { Crafts.GetItemFromName("SolidFuel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SolidFuelFromOilLight"), new oItem[] { Crafts.GetItemFromName("OilLight") }, new oItem[] { Crafts.GetItemFromName("SolidFuel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SolidFuelFromPetroleumGas"), new oItem[] { Crafts.GetItemFromName("PetroleumGas") }, new oItem[] { Crafts.GetItemFromName("SolidFuel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PortableSolarPanel"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("SolarPanel"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("PortableSolarPanel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PortableFusionReactor"), new oItem[] { Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("ProcessingUnit") }, new oItem[] { Crafts.GetItemFromName("PortableFusionReactor") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("EnergyShield"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("EnergyShield") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("EnergyShieldMK2"), new oItem[] { Crafts.GetItemFromName("EnergyShield"), Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("ProcessingUnit") }, new oItem[] { Crafts.GetItemFromName("EnergyShieldMK2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PersonalBattery"), new oItem[] { Crafts.GetItemFromName("Battery"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("PersonalBattery") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PersonalBatteryMK2"), new oItem[] { Crafts.GetItemFromName("PersonalBattery"), Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("ProcessingUnit") }, new oItem[] { Crafts.GetItemFromName("PersonalBatteryMK2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PersonalLaserDefense"), new oItem[] { Crafts.GetItemFromName("LaserTurret"), Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("ProcessingUnit") }, new oItem[] { Crafts.GetItemFromName("PersonalLaserDefense") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("DischargeDefense"), new oItem[] { Crafts.GetItemFromName("LaserTurret"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("DischargeDefense") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltImmunity"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("BeltImmunity") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Exoskeleton"), new oItem[] { Crafts.GetItemFromName("EngineElectricUnit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("Exoskeleton") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PersonalRoboport"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("Battery"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("PersonalRoboport") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PersonalRoboportMK2"), new oItem[] { Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("PersonalRoboport"), Crafts.GetItemFromName("ProcessingUnit") }, new oItem[] { Crafts.GetItemFromName("PersonalRoboportMK2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("NightVision"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("NightVision") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Beacon"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("CopperCable"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("SteelPlate") }, new oItem[] { Crafts.GetItemFromName("Beacon") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleSpeed1"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("RedCircuit") }, new oItem[] { Crafts.GetItemFromName("ModuleSpeed1") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleSpeed2"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleSpeed1") }, new oItem[] { Crafts.GetItemFromName("ModuleSpeed2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleSpeed3"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleSpeed2") }, new oItem[] { Crafts.GetItemFromName("ModuleSpeed3") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleProductivity1"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("RedCircuit") }, new oItem[] { Crafts.GetItemFromName("ModuleProductivity1") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleProductivity2"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleProductivity1") }, new oItem[] { Crafts.GetItemFromName("ModuleProductivity2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleProductivity3"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleProductivity2") }, new oItem[] { Crafts.GetItemFromName("ModuleProductivity3") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleEfficiency1"), new oItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("RedCircuit") }, new oItem[] { Crafts.GetItemFromName("ModuleEfficiency1") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleEfficiency2"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleEfficiency1") }, new oItem[] { Crafts.GetItemFromName("ModuleEfficiency2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleEfficiency3"), new oItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleEfficiency2") }, new oItem[] { Crafts.GetItemFromName("ModuleEfficiency3") }, false));







		}





		//public static event EventHandler TestEvent;
		//public static void Raise_TestEvent()
		//{
		//	if (Crafts.TestEvent != null)
		//	{
		//		Crafts.TestEvent(null, new EventArgs());
		//	}
		//}



	}
}
