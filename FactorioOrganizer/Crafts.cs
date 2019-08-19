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

		public static List<sItem> listItems = (new sItem[] { new sItem("none", true, true, "vanilla") }).ToList(); //this list contains every existing items. (belt & recipe) + belt only + recipe only
		public static List<oCraft> listCrafts = new List<oCraft>(); //this list contains every existing crafts

		public static List<Bitmap> listIcons = new List<Bitmap>(); //this list constains every existing icons of items.
		public static Bitmap GetAssociatedIcon(sItem i)
		{
			//sItems contain a number who indicate the index of their icon. we don't have to use a foreach or a while.
			if (i.IconIndex >= 0 && i.IconIndex < Crafts.listIcons.Count)
			{
				return Crafts.listIcons[i.IconIndex];
			}
			return FactorioOrganizer.Properties.Resources.assembling_machine_0;
		}


		//get a craft from the item used as recipe
		public static oCraft GetCraftFromRecipe(sItem recipe)
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


		/*
		 * yes, the new system can be a little bit annoying because we often have to use GetItemFromName to get anything, but expendability is necessary for mods
		 * 
		 */


		//.Name property. its internal name.
		//if Strict==false, it ToLower the names
		public static sItem GetItemFromName(string name, bool Strict = false)
		{
			//search the item
			if (Strict)
			{
				foreach (sItem i in Crafts.listItems)
				{
					if (i.Name == name) { return i; }
				}
			}
			else
			{
				string tlname = name.ToLower();
				foreach (sItem i in Crafts.listItems)
				{
					if (i.Name.ToLower() == tlname) { return i; }
				}
			}
			//the none item is the first element of the list
			return Crafts.listItems[0];
		}


		//for both items and crafts (especialy crafts), if it already exist, we override the aleady existing one. this would allow us to make a backup object list inside the program if the user don't have any files. the next version of FO, the one with mod support, will have a file with it, a .fomod file who contains vanilla items/crafts. if the user don't have it, he will have the defaults items/crafts.
		//overriding already existing items/crafts, combined with mod load order, will allow mods to override other mod's items.


		public static void AddItem(sItem newitem, Bitmap ItemIcon)
		{
			bool AlreadyExist = false;
			//search if that item already exist. if so, it overrides it
			int index = 0;
			while (index < Crafts.listItems.Count)
			{
				//check if it's the same item
				if (Crafts.listItems[index].Name == newitem.Name)
				{
					AlreadyExist = true;

					int iconindex = Crafts.listItems[index].IconIndex; //save the index
					newitem.IconIndex = iconindex; //define its icon index
					Crafts.listItems[index] = newitem; //we place it at its location. sItems are structure, we can't edit structures properties if they are inside a list.

					//override the icon, if it had one
					if (iconindex >= 0 && iconindex < Crafts.listIcons.Count) //check it's in bound
					{
						Crafts.listIcons[iconindex] = ItemIcon;
					}

					break; //we don't have to check for other items if we already found it
				}
				//next iteration
				index++;
			}
			//if the item didn't already exist, we add it
			if (!AlreadyExist)
			{
				//we must not forget that sItem is a structure, not a class
				newitem.IconIndex = Crafts.listIcons.Count; //must be done before adding it to the list
				Crafts.listItems.Add(newitem);
				Crafts.listIcons.Add(ItemIcon);
			}
		}

		public static void AddCraft(oCraft newcraft)
		{
			bool AlreadyExist = false;
			//search if that craft already exist. if so, it overrides it
			int index = 0;
			while (index < Crafts.listCrafts.Count)
			{
				//check if it's the same craft. each craft are supposed to be uniquely identified by their recipe. so if the recipes names are identical, it's the same craft and we override it
				if (Crafts.listCrafts[index].Recipe.Name == newcraft.Recipe.Name)
				{
					AlreadyExist = true;

					Crafts.listCrafts[index] = newcraft;

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






		//in future, Form1 will have a TabControl. there will be one uiToolBox per TabPage per mod, and by default a tabpage for the vanilla items
		//when a mod will be added, Form1 will make a new tab and a new uiToolBox for the items of the mod.
		//Form1 will be listening to this event. yes, it's possible to achive this without events, but i'll do it with events. change my mind :)

		public static event EventHandler<ModEventArgs> ModAdded;

		//import the content of a mod
		public static void AddMod(oMod newmod)
		{



			//raise the event
			if (Crafts.ModAdded != null)
			{
				Crafts.ModAdded(null, new ModEventArgs(newmod));
			}
		}










		public static void CreateDefualtVanillaItems()
		{

			Crafts.AddItem(new sItem("none", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.assembling_machine_0);
			//Crafts.AddItem(new sItem("TEST", false, true, "vanilla"), imghere);

			Crafts.AddItem(new sItem("OreIron", true, false, "vanilla"), FactorioOrganizer.Properties.Resources.iron_ore);
			Crafts.AddItem(new sItem("OreCopper", true, false, "vanilla"), FactorioOrganizer.Properties.Resources.copper_ore);
			Crafts.AddItem(new sItem("IronPlate", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.iron_plate);
			Crafts.AddItem(new sItem("CopperPlate", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.copper_plate);
			Crafts.AddItem(new sItem("Coal", true, false, "vanilla"), FactorioOrganizer.Properties.Resources.coal_dark_background);
			Crafts.AddItem(new sItem("Stone", true, false, "vanilla"), FactorioOrganizer.Properties.Resources.stone);
			Crafts.AddItem(new sItem("Wood", true, false, "vanilla"), FactorioOrganizer.Properties.Resources.wood);
			Crafts.AddItem(new sItem("SteelPlate", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.steel_plate);
			Crafts.AddItem(new sItem("Plastic", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.plastic_bar);
			Crafts.AddItem(new sItem("StoneBrick", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.stone_brick);

			Crafts.AddItem(new sItem("IronGear", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.iron_gear_wheel);
			Crafts.AddItem(new sItem("IronStick", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.iron_stick);
			Crafts.AddItem(new sItem("CopperCable", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.copper_cable);

			Crafts.AddItem(new sItem("ChestWood", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.wooden_chest);
			Crafts.AddItem(new sItem("ChestIron", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.iron_chest);
			Crafts.AddItem(new sItem("ChestSteel", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.steel_chest);
			Crafts.AddItem(new sItem("ChestLogistic", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.logistic_chest_buffer);
			Crafts.AddItem(new sItem("StorageTank", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.storage_tank);

			Crafts.AddItem(new sItem("Belt", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.transport_belt);
			Crafts.AddItem(new sItem("BeltUnderground", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.underground_belt);
			Crafts.AddItem(new sItem("BeltSplitter", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.splitter);
			Crafts.AddItem(new sItem("BeltFast", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.fast_transport_belt);
			Crafts.AddItem(new sItem("BeltFastUnderground", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.fast_underground_belt);
			Crafts.AddItem(new sItem("BeltFastSplitter", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.fast_splitter);
			Crafts.AddItem(new sItem("BeltExpress", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.express_transport_belt);
			Crafts.AddItem(new sItem("BeltExpressUnderground", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.express_underground_belt);
			Crafts.AddItem(new sItem("BeltExpressSplitter", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.express_splitter);

			Crafts.AddItem(new sItem("Boiler", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.boiler);
			Crafts.AddItem(new sItem("SteamEngine", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.steam_engine);
			Crafts.AddItem(new sItem("SteamTurbine", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.steam_turbine);
			Crafts.AddItem(new sItem("SolarPanel", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.solar_panel);
			Crafts.AddItem(new sItem("Accumulator", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.accumulator);
			Crafts.AddItem(new sItem("NuclearReactor", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.nuclear_reactor);
			Crafts.AddItem(new sItem("HeatExchanger", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.heat_boiler);
			Crafts.AddItem(new sItem("HeatPipe", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.heat_pipe);
			Crafts.AddItem(new sItem("ElectricMiningDrill", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.electric_mining_drill);
			Crafts.AddItem(new sItem("OffshorePump", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.offshore_pump);
			Crafts.AddItem(new sItem("Pumpjack", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.pumpjack);
			Crafts.AddItem(new sItem("FurnaceStone", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.stone_furnace);
			Crafts.AddItem(new sItem("FurnaceSteel", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.steel_furnace);
			Crafts.AddItem(new sItem("FurnaceElectric", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.electric_furnace);
			Crafts.AddItem(new sItem("GreenCircuit", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.electronic_circuit);
			Crafts.AddItem(new sItem("RedCircuit", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.advanced_circuit);
			Crafts.AddItem(new sItem("ProcessingUnit", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.processing_unit);

			Crafts.AddItem(new sItem("ElectricPole", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.small_electric_pole);
			Crafts.AddItem(new sItem("MediumElectricPole", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.medium_electric_pole);
			Crafts.AddItem(new sItem("BigElectricPole", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.big_electric_pole);
			Crafts.AddItem(new sItem("Pipe", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.pipe);
			Crafts.AddItem(new sItem("PipeUnderground", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.pipe_to_ground);
			Crafts.AddItem(new sItem("Pump", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.pump);

			Crafts.AddItem(new sItem("Inserter", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.inserter);
			Crafts.AddItem(new sItem("InserterLong", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.long_handed_inserter);
			Crafts.AddItem(new sItem("InserterFast", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.fast_inserter);
			Crafts.AddItem(new sItem("InserterFilter", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.filter_inserter);
			Crafts.AddItem(new sItem("InserterStack", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.stack_inserter);
			Crafts.AddItem(new sItem("InserterStackFilter", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.stack_filter_inserter);

			Crafts.AddItem(new sItem("AssemblingMachine", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.assembling_machine_1);
			Crafts.AddItem(new sItem("AssemblingMachine2", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.assembling_machine_2);
			Crafts.AddItem(new sItem("AssemblingMachine3", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.assembling_machine_3);
			Crafts.AddItem(new sItem("OilRefinery", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.oil_refinery);
			Crafts.AddItem(new sItem("ChemicalPlant", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.chemical_plant);
			Crafts.AddItem(new sItem("Centrifuge", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.centrifuge);
			Crafts.AddItem(new sItem("Lab", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.lab);

			
			Crafts.AddItem(new sItem("ScienceRed", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.automation_science_pack);
			Crafts.AddItem(new sItem("ScienceGreen", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.logistic_science_pack);
			Crafts.AddItem(new sItem("ScienceGrey", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.military_science_pack);
			Crafts.AddItem(new sItem("ScienceBlue", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.chemical_science_pack);
			Crafts.AddItem(new sItem("ScienceViolet", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.production_science_pack);
			Crafts.AddItem(new sItem("ScienceYellow", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.utility_science_pack);

			Crafts.AddItem(new sItem("Rail", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.rail);
			Crafts.AddItem(new sItem("TrainStop", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.train_stop);
			Crafts.AddItem(new sItem("RailSignal", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.rail_signal);
			Crafts.AddItem(new sItem("RailChainSignal", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.rail_chain_signal);
			Crafts.AddItem(new sItem("Locomotive", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.diesel_locomotive);
			Crafts.AddItem(new sItem("CargoWagon", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.cargo_wagon);
			Crafts.AddItem(new sItem("FluidWagon", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.fluid_wagon);
			Crafts.AddItem(new sItem("ArtilleryWagon", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.artillery_wagon);
			Crafts.AddItem(new sItem("Car", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.car);
			Crafts.AddItem(new sItem("Tank", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.tank);

			Crafts.AddItem(new sItem("EngineUnit", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.engine_unit);
			Crafts.AddItem(new sItem("EngineElectricUnit", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.electric_engine_unit);
			Crafts.AddItem(new sItem("FlyingRobotFrame", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.flying_robot_frame);
			Crafts.AddItem(new sItem("LogisticRobot", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.logistic_robot);
			Crafts.AddItem(new sItem("ConstructionRobot", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.construction_robot);
			Crafts.AddItem(new sItem("RocketPart", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.rocket_part);
			Crafts.AddItem(new sItem("RocketControlUnit", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.rocket_control_unit);
			Crafts.AddItem(new sItem("LowDensityStructure", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.rocket_structure);
			Crafts.AddItem(new sItem("RocketFuel", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.rocket_fuel);

			Crafts.AddItem(new sItem("Grenade", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.grenade);
			Crafts.AddItem(new sItem("MagazineFirearm", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.firearm_magazine);
			Crafts.AddItem(new sItem("MagazinePiercing", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.piercing_rounds_magazine);
			Crafts.AddItem(new sItem("ShotgunShells", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.shotgun_shell);
			Crafts.AddItem(new sItem("ShotgunPiercingShells", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.piercing_shotgun_shell);
			Crafts.AddItem(new sItem("Pistol", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.pistol);
			Crafts.AddItem(new sItem("SubmachineGun", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.submachine_gun);
			Crafts.AddItem(new sItem("Shotgun", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.shotgun);
			Crafts.AddItem(new sItem("CombatShotgun", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.combat_shotgun);
			Crafts.AddItem(new sItem("LandMine", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.land_mine);
			Crafts.AddItem(new sItem("Concrete", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.concrete);
			Crafts.AddItem(new sItem("Wall", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.wall);
			Crafts.AddItem(new sItem("GunTurret", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.gun_turret);
			Crafts.AddItem(new sItem("LaserTurret", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.laser_turret);
			Crafts.AddItem(new sItem("FlamethrowerTurret", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.flamethrower_turret);
			Crafts.AddItem(new sItem("ArtilleryTurret", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.artillery_turret);
			Crafts.AddItem(new sItem("Radar", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.radar);
			Crafts.AddItem(new sItem("RocketSilo", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.rocket_silo);

			Crafts.AddItem(new sItem("Sulfur", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.sulfur);
			Crafts.AddItem(new sItem("SolidFuel", true, false, "vanilla"), FactorioOrganizer.Properties.Resources.solid_fuel);
			Crafts.AddItem(new sItem("Battery", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.battery);
			Crafts.AddItem(new sItem("Explosives", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.explosives);
			Crafts.AddItem(new sItem("CliffExplosives", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.cliff_explosives);
			Crafts.AddItem(new sItem("EmptyBarrel", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.empty_barrel);

			Crafts.AddItem(new sItem("OilCrude", true, false, "vanilla"), FactorioOrganizer.Properties.Resources.crude_oil);
			Crafts.AddItem(new sItem("OilLight", true, false, "vanilla"), FactorioOrganizer.Properties.Resources.light_oil);
			Crafts.AddItem(new sItem("OilHeavy", true, false, "vanilla"), FactorioOrganizer.Properties.Resources.heavy_oil);
			Crafts.AddItem(new sItem("Lubricant", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.lubricant);
			Crafts.AddItem(new sItem("PetroleumGas", true, false, "vanilla"), FactorioOrganizer.Properties.Resources.petroleum_gas);
			Crafts.AddItem(new sItem("SulfuricAcid", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.sulfuric_acid);
			Crafts.AddItem(new sItem("Water", true, false, "vanilla"), FactorioOrganizer.Properties.Resources.water);
			Crafts.AddItem(new sItem("Steam", true, false, "vanilla"), FactorioOrganizer.Properties.Resources.steam);

			//oil processing only :
			Crafts.AddItem(new sItem("BasicOilProcessing", false, true, "vanilla"), FactorioOrganizer.Properties.Resources.basic_oil_processing);
			Crafts.AddItem(new sItem("AdvancedOilProcessing", false, true, "vanilla"), FactorioOrganizer.Properties.Resources.advanced_oil_processing);
			Crafts.AddItem(new sItem("HeavyOilCracking", false, true, "vanilla"), FactorioOrganizer.Properties.Resources.heavy_oil_cracking);
			Crafts.AddItem(new sItem("LightOilCracking", false, true, "vanilla"), FactorioOrganizer.Properties.Resources.light_oil_cracking);
			Crafts.AddItem(new sItem("CoalLiquefaction", false, true, "vanilla"), FactorioOrganizer.Properties.Resources.coal_liquefaction);
			Crafts.AddItem(new sItem("SolidFuelFromOilHeavy", false, true, "vanilla"), FactorioOrganizer.Properties.Resources.solid_fuel_from_heavy_oil);
			Crafts.AddItem(new sItem("SolidFuelFromOilLight", false, true, "vanilla"), FactorioOrganizer.Properties.Resources.solid_fuel_from_light_oil);
			Crafts.AddItem(new sItem("SolidFuelFromPetroleumGas", false, true, "vanilla"), FactorioOrganizer.Properties.Resources.solid_fuel_from_petroleum_gas);

			Crafts.AddItem(new sItem("PortableSolarPanel", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.solar_panel_equipment);
			Crafts.AddItem(new sItem("PortableFusionReactor", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.fusion_reactor_equipment);
			Crafts.AddItem(new sItem("EnergyShield", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.energy_shield_equipment);
			Crafts.AddItem(new sItem("EnergyShieldMK2", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.energy_shield_mk2_equipment);
			Crafts.AddItem(new sItem("PersonalBattery", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.battery_equipment);
			Crafts.AddItem(new sItem("PersonalBatteryMK2", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.battery_mk2_equipment);
			Crafts.AddItem(new sItem("PersonalLaserDefense", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.personal_laser_defense_equipment);
			Crafts.AddItem(new sItem("DischargeDefense", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.discharge_defense_equipment);
			Crafts.AddItem(new sItem("BeltImmunity", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.belt_immunity_equipment);
			Crafts.AddItem(new sItem("Exoskeleton", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.exoskeleton_equipment);
			Crafts.AddItem(new sItem("PersonalRoboport", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.personal_roboport_equipment);
			Crafts.AddItem(new sItem("PersonalRoboportMK2", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.personal_roboport_mk2_equipment);
			Crafts.AddItem(new sItem("NightVision", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.night_vision_equipment);


			//modules :
			Crafts.AddItem(new sItem("Beacon", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.beacon);
			Crafts.AddItem(new sItem("ModuleSpeed1", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.speed_module);
			Crafts.AddItem(new sItem("ModuleSpeed2", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.speed_module_2);
			Crafts.AddItem(new sItem("ModuleSpeed3", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.speed_module_3);
			Crafts.AddItem(new sItem("ModuleProductivity1", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.productivity_module);
			Crafts.AddItem(new sItem("ModuleProductivity2", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.productivity_module_2);
			Crafts.AddItem(new sItem("ModuleProductivity3", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.productivity_module_3);
			Crafts.AddItem(new sItem("ModuleEfficiency1", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.effectivity_module);
			Crafts.AddItem(new sItem("ModuleEfficiency2", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.effectivity_module_2);
			Crafts.AddItem(new sItem("ModuleEfficiency3", true, true, "vanilla"), FactorioOrganizer.Properties.Resources.effectivity_module_3);




			////create crafts
			
			//https://wiki.factorio.com/Items

			//Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("TEST"), new sItem[] { Crafts.GetItemFromName("Inserter"), Crafts.GetItemFromName("InserterLong"), Crafts.GetItemFromName("InserterFast"), Crafts.GetItemFromName("InserterFilter") }, , , new sItem[] { Crafts.GetItemFromName("ScienceRed")Crafts.GetItemFromName("ScienceGreen")Crafts.GetItemFromName("ScienceGrey") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("IronPlate"), new sItem[] { Crafts.GetItemFromName("OreIron") }, new sItem[] { Crafts.GetItemFromName("IronPlate") }, true));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("CopperPlate"), new sItem[] { Crafts.GetItemFromName("OreCopper") }, new sItem[] { Crafts.GetItemFromName("CopperPlate") }, true));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SteelPlate"), new sItem[] { Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("SteelPlate") }, true));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Plastic"), new sItem[] { Crafts.GetItemFromName("PetroleumGas"), Crafts.GetItemFromName("Coal") }, new sItem[] { Crafts.GetItemFromName("Plastic") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("StoneBrick"), new sItem[] { Crafts.GetItemFromName("Stone") }, new sItem[] { Crafts.GetItemFromName("StoneBrick") }, true));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("IronGear"), new sItem[] { Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("IronGear") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("IronStick"), new sItem[] { Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("IronStick") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("CopperCable"), new sItem[] { Crafts.GetItemFromName("CopperPlate") }, new sItem[] { Crafts.GetItemFromName("CopperCable") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ElectricPole"), new sItem[] { Crafts.GetItemFromName("CopperCable"), Crafts.GetItemFromName("Wood") }, new sItem[] { Crafts.GetItemFromName("ElectricPole") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("MediumElectricPole"), new sItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronStick"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("MediumElectricPole") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BigElectricPole"), new sItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronStick"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("BigElectricPole") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Pipe"), new sItem[] { Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("Pipe") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PipeUnderground"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("Pipe") }, new sItem[] { Crafts.GetItemFromName("PipeUnderground") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Pump"), new sItem[] { Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("Pump") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ChestWood"), new sItem[] { Crafts.GetItemFromName("Wood") }, new sItem[] { Crafts.GetItemFromName("ChestWood") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ChestIron"), new sItem[] { Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("ChestIron") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ChestSteel"), new sItem[] { Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("ChestSteel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ChestLogistic"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("ChestSteel") }, new sItem[] { Crafts.GetItemFromName("ChestLogistic") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("StorageTank"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("StorageTank") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Belt"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("IronGear") }, new sItem[] { Crafts.GetItemFromName("Belt") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltFast"), new sItem[] { Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Belt") }, new sItem[] { Crafts.GetItemFromName("BeltFast") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltExpress"), new sItem[] { Crafts.GetItemFromName("BeltFast"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Lubricant") }, new sItem[] { Crafts.GetItemFromName("BeltExpress") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltUnderground"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("Belt") }, new sItem[] { Crafts.GetItemFromName("BeltUnderground") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltFastUnderground"), new sItem[] { Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("BeltUnderground") }, new sItem[] { Crafts.GetItemFromName("BeltFastUnderground") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltExpressUnderground"), new sItem[] { Crafts.GetItemFromName("BeltFastUnderground"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Lubricant") }, new sItem[] { Crafts.GetItemFromName("BeltExpressUnderground") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltSplitter"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("Belt") }, new sItem[] { Crafts.GetItemFromName("BeltSplitter") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltFastSplitter"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("BeltSplitter") }, new sItem[] { Crafts.GetItemFromName("BeltFastSplitter") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltExpressSplitter"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("BeltFastSplitter"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Lubricant") }, new sItem[] { Crafts.GetItemFromName("BeltExpressSplitter") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Boiler"), new sItem[] { Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("FurnaceStone") }, new sItem[] { Crafts.GetItemFromName("Boiler") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SteamEngine"), new sItem[] { Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("Pipe") }, new sItem[] { Crafts.GetItemFromName("SteamEngine") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SteamTurbine"), new sItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe") }, new sItem[] { Crafts.GetItemFromName("SteamTurbine") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SolarPanel"), new sItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("SolarPanel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Accumulator"), new sItem[] { Crafts.GetItemFromName("Battery"), Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("Accumulator") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("NuclearReactor"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("Concrete"), Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("NuclearReactor") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("HeatExchanger"), new sItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("HeatExchanger") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("HeatPipe"), new sItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("HeatPipe") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ElectricMiningDrill"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("ElectricMiningDrill") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("OffshorePump"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe") }, new sItem[] { Crafts.GetItemFromName("OffshorePump") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Pumpjack"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("Pumpjack") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("FurnaceStone"), new sItem[] { Crafts.GetItemFromName("Stone") }, new sItem[] { Crafts.GetItemFromName("FurnaceStone") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("FurnaceSteel"), new sItem[] { Crafts.GetItemFromName("StoneBrick"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("FurnaceSteel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("FurnaceElectric"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("SteelPlate"), Crafts.GetItemFromName("StoneBrick") }, new sItem[] { Crafts.GetItemFromName("FurnaceElectric") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("GreenCircuit"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("CopperCable") }, new sItem[] { Crafts.GetItemFromName("GreenCircuit") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RedCircuit"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("Plastic"), Crafts.GetItemFromName("CopperCable") }, new sItem[] { Crafts.GetItemFromName("RedCircuit") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ProcessingUnit"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("SulfuricAcid") }, new sItem[] { Crafts.GetItemFromName("ProcessingUnit") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Inserter"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("GreenCircuit") }, new sItem[] { Crafts.GetItemFromName("Inserter") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("InserterLong"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Inserter") }, new sItem[] { Crafts.GetItemFromName("InserterLong") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("InserterFast"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("Inserter") }, new sItem[] { Crafts.GetItemFromName("InserterFast") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("InserterFilter"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("InserterFast") }, new sItem[] { Crafts.GetItemFromName("InserterFilter") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("InserterStack"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("InserterFast"), Crafts.GetItemFromName("IronGear") }, new sItem[] { Crafts.GetItemFromName("InserterStack") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("InserterStackFilter"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("InserterStack") }, new sItem[] { Crafts.GetItemFromName("InserterStackFilter") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("AssemblingMachine"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("AssemblingMachine") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("AssemblingMachine2"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("AssemblingMachine"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("AssemblingMachine2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("AssemblingMachine3"), new sItem[] { Crafts.GetItemFromName("AssemblingMachine2"), Crafts.GetItemFromName("ModuleSpeed1") }, new sItem[] { Crafts.GetItemFromName("AssemblingMachine3") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("OilRefinery"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("OilRefinery") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ChemicalPlant"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("ChemicalPlant") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Centrifuge"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("Concrete"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("Centrifuge") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Lab"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Belt") }, new sItem[] { Crafts.GetItemFromName("Lab") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ScienceRed"), new sItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronGear") }, new sItem[] { Crafts.GetItemFromName("ScienceRed") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ScienceGreen"), new sItem[] { Crafts.GetItemFromName("Belt"), Crafts.GetItemFromName("Inserter") }, new sItem[] { Crafts.GetItemFromName("ScienceGreen") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ScienceGrey"), new sItem[] { Crafts.GetItemFromName("Grenade"), Crafts.GetItemFromName("MagazinePiercing"), Crafts.GetItemFromName("Wall") }, new sItem[] { Crafts.GetItemFromName("ScienceGrey") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ScienceBlue"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("Sulfur") }, new sItem[] { Crafts.GetItemFromName("ScienceBlue") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ScienceViolet"), new sItem[] { Crafts.GetItemFromName("FurnaceElectric"), Crafts.GetItemFromName("ModuleProductivity1"), Crafts.GetItemFromName("Rail") }, new sItem[] { Crafts.GetItemFromName("ScienceViolet") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ScienceYellow"), new sItem[] { Crafts.GetItemFromName("FlyingRobotFrame"), Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("ProcessingUnit") }, new sItem[] { Crafts.GetItemFromName("ScienceYellow") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Rail"), new sItem[] { Crafts.GetItemFromName("IronStick"), Crafts.GetItemFromName("SteelPlate"), Crafts.GetItemFromName("Stone") }, new sItem[] { Crafts.GetItemFromName("Rail") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("TrainStop"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("IronStick"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("TrainStop") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RailSignal"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("RailSignal") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RailChainSignal"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("RailChainSignal") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Locomotive"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("Locomotive") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("CargoWagon"), new sItem[] { Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("CargoWagon") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("FluidWagon"), new sItem[] { Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate"), Crafts.GetItemFromName("StorageTank") }, new sItem[] { Crafts.GetItemFromName("FluidWagon") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ArtilleryWagon"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("ArtilleryWagon") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Car"), new sItem[] { Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("Car") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Tank"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("Tank") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("EngineUnit"), new sItem[] { Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("EngineUnit") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("EngineElectricUnit"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("Lubricant") }, new sItem[] { Crafts.GetItemFromName("EngineElectricUnit") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("FlyingRobotFrame"), new sItem[] { Crafts.GetItemFromName("Battery"), Crafts.GetItemFromName("EngineElectricUnit"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("FlyingRobotFrame") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("LogisticRobot"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("FlyingRobotFrame") }, new sItem[] { Crafts.GetItemFromName("LogisticRobot") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ConstructionRobot"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("FlyingRobotFrame") }, new sItem[] { Crafts.GetItemFromName("ConstructionRobot") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RocketPart"), new sItem[] { Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("RocketControlUnit"), Crafts.GetItemFromName("RocketFuel") }, new sItem[] { Crafts.GetItemFromName("RocketPart") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RocketControlUnit"), new sItem[] { Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleSpeed1") }, new sItem[] { Crafts.GetItemFromName("RocketControlUnit") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("LowDensityStructure"), new sItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("Plastic"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("LowDensityStructure") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RocketFuel"), new sItem[] { Crafts.GetItemFromName("OilLight"), Crafts.GetItemFromName("SolidFuel") }, new sItem[] { Crafts.GetItemFromName("RocketFuel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Grenade"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("Coal") }, new sItem[] { Crafts.GetItemFromName("Grenade") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("MagazineFirearm"), new sItem[] { Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("MagazineFirearm") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("MagazinePiercing"), new sItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("MagazineFirearm"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("MagazinePiercing") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ShotgunShells"), new sItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("ShotgunShells") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ShotgunPiercingShells"), new sItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("ShotgunShells"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("ShotgunPiercingShells") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Pistol"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("CopperPlate") }, new sItem[] { Crafts.GetItemFromName("Pistol") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SubmachineGun"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronGear") }, new sItem[] { Crafts.GetItemFromName("SubmachineGun") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Shotgun"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Wood") }, new sItem[] { Crafts.GetItemFromName("Shotgun") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("CombatShotgun"), new sItem[] { Crafts.GetItemFromName("SteelPlate"), Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Wood") }, new sItem[] { Crafts.GetItemFromName("CombatShotgun") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("LandMine"), new sItem[] { Crafts.GetItemFromName("Explosives"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("LandMine") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Concrete"), new sItem[] { Crafts.GetItemFromName("OreIron"), Crafts.GetItemFromName("StoneBrick"), Crafts.GetItemFromName("Water") }, new sItem[] { Crafts.GetItemFromName("Concrete") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Wall"), new sItem[] { Crafts.GetItemFromName("StoneBrick") }, new sItem[] { Crafts.GetItemFromName("Wall") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("GunTurret"), new sItem[] { Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("GunTurret") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("LaserTurret"), new sItem[] { Crafts.GetItemFromName("Battery"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("LaserTurret") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("FlamethrowerTurret"), new sItem[] { Crafts.GetItemFromName("EngineUnit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("FlamethrowerTurret") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ArtilleryTurret"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("Concrete"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("ArtilleryTurret") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Radar"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("IronPlate") }, new sItem[] { Crafts.GetItemFromName("Radar") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("RocketSilo"), new sItem[] { Crafts.GetItemFromName("Concrete"), Crafts.GetItemFromName("EngineElectricUnit"), Crafts.GetItemFromName("Pipe"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("RocketSilo") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Sulfur"), new sItem[] { Crafts.GetItemFromName("PetroleumGas"), Crafts.GetItemFromName("Water") }, new sItem[] { Crafts.GetItemFromName("Sulfur") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Battery"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("CopperPlate"), Crafts.GetItemFromName("SulfuricAcid") }, new sItem[] { Crafts.GetItemFromName("Battery") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Explosives"), new sItem[] { Crafts.GetItemFromName("Coal"), Crafts.GetItemFromName("Sulfur"), Crafts.GetItemFromName("Water") }, new sItem[] { Crafts.GetItemFromName("Explosives") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("CliffExplosives"), new sItem[] { Crafts.GetItemFromName("EmptyBarrel"), Crafts.GetItemFromName("Explosives"), Crafts.GetItemFromName("Grenade") }, new sItem[] { Crafts.GetItemFromName("CliffExplosives") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("EmptyBarrel"), new sItem[] { Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("EmptyBarrel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Lubricant"), new sItem[] { Crafts.GetItemFromName("OilHeavy") }, new sItem[] { Crafts.GetItemFromName("Lubricant") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SulfuricAcid"), new sItem[] { Crafts.GetItemFromName("IronPlate"), Crafts.GetItemFromName("Sulfur"), Crafts.GetItemFromName("Water") }, new sItem[] { Crafts.GetItemFromName("SulfuricAcid") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BasicOilProcessing"), new sItem[] { Crafts.GetItemFromName("OilCrude") }, new sItem[] { Crafts.GetItemFromName("PetroleumGas") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("AdvancedOilProcessing"), new sItem[] { Crafts.GetItemFromName("OilCrude"), Crafts.GetItemFromName("Water") }, new sItem[] { Crafts.GetItemFromName("PetroleumGas"), Crafts.GetItemFromName("OilLight"), Crafts.GetItemFromName("OilHeavy") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("HeavyOilCracking"), new sItem[] { Crafts.GetItemFromName("OilHeavy"), Crafts.GetItemFromName("Water") }, new sItem[] { Crafts.GetItemFromName("OilLight") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("LightOilCracking"), new sItem[] { Crafts.GetItemFromName("OilLight"), Crafts.GetItemFromName("Water") }, new sItem[] { Crafts.GetItemFromName("PetroleumGas") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("CoalLiquefaction"), new sItem[] { Crafts.GetItemFromName("Coal"), Crafts.GetItemFromName("OilHeavy"), Crafts.GetItemFromName("Steam") }, new sItem[] { Crafts.GetItemFromName("OilHeavy"), Crafts.GetItemFromName("OilLight"), Crafts.GetItemFromName("PetroleumGas") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SolidFuelFromOilHeavy"), new sItem[] { Crafts.GetItemFromName("OilHeavy") }, new sItem[] { Crafts.GetItemFromName("SolidFuel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SolidFuelFromOilLight"), new sItem[] { Crafts.GetItemFromName("OilLight") }, new sItem[] { Crafts.GetItemFromName("SolidFuel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("SolidFuelFromPetroleumGas"), new sItem[] { Crafts.GetItemFromName("PetroleumGas") }, new sItem[] { Crafts.GetItemFromName("SolidFuel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PortableSolarPanel"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("SolarPanel"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("PortableSolarPanel") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PortableFusionReactor"), new sItem[] { Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("ProcessingUnit") }, new sItem[] { Crafts.GetItemFromName("PortableFusionReactor") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("EnergyShield"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("EnergyShield") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("EnergyShieldMK2"), new sItem[] { Crafts.GetItemFromName("EnergyShield"), Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("ProcessingUnit") }, new sItem[] { Crafts.GetItemFromName("EnergyShieldMK2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PersonalBattery"), new sItem[] { Crafts.GetItemFromName("Battery"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("PersonalBattery") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PersonalBatteryMK2"), new sItem[] { Crafts.GetItemFromName("PersonalBattery"), Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("ProcessingUnit") }, new sItem[] { Crafts.GetItemFromName("PersonalBatteryMK2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PersonalLaserDefense"), new sItem[] { Crafts.GetItemFromName("LaserTurret"), Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("ProcessingUnit") }, new sItem[] { Crafts.GetItemFromName("PersonalLaserDefense") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("DischargeDefense"), new sItem[] { Crafts.GetItemFromName("LaserTurret"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("DischargeDefense") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("BeltImmunity"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("BeltImmunity") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Exoskeleton"), new sItem[] { Crafts.GetItemFromName("EngineElectricUnit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("Exoskeleton") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PersonalRoboport"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("Battery"), Crafts.GetItemFromName("IronGear"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("PersonalRoboport") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("PersonalRoboportMK2"), new sItem[] { Crafts.GetItemFromName("LowDensityStructure"), Crafts.GetItemFromName("PersonalRoboport"), Crafts.GetItemFromName("ProcessingUnit") }, new sItem[] { Crafts.GetItemFromName("PersonalRoboportMK2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("NightVision"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("NightVision") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("Beacon"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("CopperCable"), Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("SteelPlate") }, new sItem[] { Crafts.GetItemFromName("Beacon") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleSpeed1"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("RedCircuit") }, new sItem[] { Crafts.GetItemFromName("ModuleSpeed1") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleSpeed2"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleSpeed1") }, new sItem[] { Crafts.GetItemFromName("ModuleSpeed2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleSpeed3"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleSpeed2") }, new sItem[] { Crafts.GetItemFromName("ModuleSpeed3") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleProductivity1"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("RedCircuit") }, new sItem[] { Crafts.GetItemFromName("ModuleProductivity1") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleProductivity2"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleProductivity1") }, new sItem[] { Crafts.GetItemFromName("ModuleProductivity2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleProductivity3"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleProductivity2") }, new sItem[] { Crafts.GetItemFromName("ModuleProductivity3") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleEfficiency1"), new sItem[] { Crafts.GetItemFromName("GreenCircuit"), Crafts.GetItemFromName("RedCircuit") }, new sItem[] { Crafts.GetItemFromName("ModuleEfficiency1") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleEfficiency2"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleEfficiency1") }, new sItem[] { Crafts.GetItemFromName("ModuleEfficiency2") }, false));
			Crafts.AddCraft(new oCraft(Crafts.GetItemFromName("ModuleEfficiency3"), new sItem[] { Crafts.GetItemFromName("RedCircuit"), Crafts.GetItemFromName("ProcessingUnit"), Crafts.GetItemFromName("ModuleEfficiency2") }, new sItem[] { Crafts.GetItemFromName("ModuleEfficiency3") }, false));







		}

	}
}
