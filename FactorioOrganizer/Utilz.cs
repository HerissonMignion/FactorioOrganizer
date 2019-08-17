using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FactorioOrganizer
{
	public static class Utilz
	{


		public static List<FOType> GetListOfAllFOType()
		{
			return Enum.GetValues(typeof(FOType)).Cast<FOType>().ToList();
		}
		public static FOType GetFOTypeAssociatedToString(string str)
		{
			List<FOType> allft = Utilz.GetListOfAllFOType();
			foreach (FOType ft in allft)
			{
				if (ft.ToString() == str) { return ft; }
			}
			return FOType.none;
		}
		
		
		public static Bitmap GetAssociatedIcon(FOType ft)
		{
			if (ft == FOType.TEST) { return FactorioOrganizer.Properties.Resources.assembling_machine_0; }

			if (ft == FOType.OreIron) { return FactorioOrganizer.Properties.Resources.iron_ore; }
			if (ft == FOType.OreCopper) { return FactorioOrganizer.Properties.Resources.copper_ore; }
			if (ft == FOType.IronPlate) { return FactorioOrganizer.Properties.Resources.iron_plate; }
			if (ft == FOType.CopperPlate) { return FactorioOrganizer.Properties.Resources.copper_plate; }
			if (ft == FOType.Coal) { return FactorioOrganizer.Properties.Resources.coal_dark_background; } // coal
			if (ft == FOType.Stone) { return FactorioOrganizer.Properties.Resources.stone; }
			if (ft == FOType.Wood) { return FactorioOrganizer.Properties.Resources.wood; }
			if (ft == FOType.SteelPlate) { return FactorioOrganizer.Properties.Resources.steel_plate; }
			if (ft == FOType.Plastic) { return FactorioOrganizer.Properties.Resources.plastic_bar; }
			if (ft == FOType.StoneBrick) { return FactorioOrganizer.Properties.Resources.stone_brick; }

			if (ft == FOType.IronGear) { return FactorioOrganizer.Properties.Resources.iron_gear_wheel; }
			if (ft == FOType.IronStick) { return FactorioOrganizer.Properties.Resources.iron_stick; }
			if (ft == FOType.CopperCable) { return FactorioOrganizer.Properties.Resources.copper_cable; }

			if (ft == FOType.ChestWood) { return FactorioOrganizer.Properties.Resources.wooden_chest; }
			if (ft == FOType.ChestIron) { return FactorioOrganizer.Properties.Resources.iron_chest; }
			if (ft == FOType.ChestSteel) { return FactorioOrganizer.Properties.Resources.steel_chest; }
			if (ft == FOType.ChestLogistic) { return FactorioOrganizer.Properties.Resources.logistic_chest_buffer; }
			if (ft == FOType.StorageTank) { return FactorioOrganizer.Properties.Resources.storage_tank; }

			if (ft == FOType.Belt) { return FactorioOrganizer.Properties.Resources.transport_belt; }
			if (ft == FOType.BeltFast) { return FactorioOrganizer.Properties.Resources.fast_transport_belt; }
			if (ft == FOType.BeltExpress) { return FactorioOrganizer.Properties.Resources.express_transport_belt; }
			if (ft == FOType.BeltUnderground) { return FactorioOrganizer.Properties.Resources.underground_belt; }
			if (ft == FOType.BeltFastUnderground) { return FactorioOrganizer.Properties.Resources.fast_underground_belt; }
			if (ft == FOType.BeltExpressUnderground) { return FactorioOrganizer.Properties.Resources.express_underground_belt; }
			if (ft == FOType.BeltSplitter) { return FactorioOrganizer.Properties.Resources.splitter; }
			if (ft == FOType.BeltFastSplitter) { return FactorioOrganizer.Properties.Resources.fast_splitter; }
			if (ft == FOType.BeltExpressSplitter) { return FactorioOrganizer.Properties.Resources.express_splitter; }

			if (ft == FOType.Boiler) { return FactorioOrganizer.Properties.Resources.boiler; }
			if (ft == FOType.SteamEngine) { return FactorioOrganizer.Properties.Resources.steam_engine; }
			if (ft == FOType.SteamTurbine) { return FactorioOrganizer.Properties.Resources.steam_turbine; }
			if (ft == FOType.SolarPanel) { return FactorioOrganizer.Properties.Resources.solar_panel; }
			if (ft == FOType.Accumulator) { return FactorioOrganizer.Properties.Resources.accumulator; }
			if (ft == FOType.NuclearReactor) { return FactorioOrganizer.Properties.Resources.nuclear_reactor; }
			if (ft == FOType.HeatExchanger) { return FactorioOrganizer.Properties.Resources.heat_boiler; }
			if (ft == FOType.HeatPipe) { return FactorioOrganizer.Properties.Resources.heat_pipe; }
			if (ft == FOType.ElectricMiningDrill) { return FactorioOrganizer.Properties.Resources.electric_mining_drill; }
			if (ft == FOType.OffshorePump) { return FactorioOrganizer.Properties.Resources.offshore_pump; }
			if (ft == FOType.Pumpjack) { return FactorioOrganizer.Properties.Resources.pumpjack; }
			if (ft == FOType.FurnaceStone) { return FactorioOrganizer.Properties.Resources.stone_furnace; }
			if (ft == FOType.FurnaceSteel) { return FactorioOrganizer.Properties.Resources.steel_furnace; }
			if (ft == FOType.FurnaceElectric) { return FactorioOrganizer.Properties.Resources.electric_furnace; }
			if (ft == FOType.GreenCircuit) { return FactorioOrganizer.Properties.Resources.electronic_circuit; }
			if (ft == FOType.RedCircuit) { return FactorioOrganizer.Properties.Resources.advanced_circuit; }
			if (ft == FOType.ProcessingUnit) { return FactorioOrganizer.Properties.Resources.processing_unit; }

			if (ft == FOType.ElectricPole) { return FactorioOrganizer.Properties.Resources.small_electric_pole; }
			if (ft == FOType.MediumElectricPole) { return FactorioOrganizer.Properties.Resources.medium_electric_pole; }
			if (ft == FOType.BigElectricPole) { return FactorioOrganizer.Properties.Resources.big_electric_pole; }
			if (ft == FOType.Pipe) { return FactorioOrganizer.Properties.Resources.pipe; }
			if (ft == FOType.PipeUnderground) { return FactorioOrganizer.Properties.Resources.pipe_to_ground; }
			if (ft == FOType.Pump) { return FactorioOrganizer.Properties.Resources.pump; }

			if (ft == FOType.Inserter) { return FactorioOrganizer.Properties.Resources.inserter; }
			if (ft == FOType.InserterLong) { return FactorioOrganizer.Properties.Resources.long_handed_inserter; }
			if (ft == FOType.InserterFast) { return FactorioOrganizer.Properties.Resources.fast_inserter; }
			if (ft == FOType.InserterFilter) { return FactorioOrganizer.Properties.Resources.filter_inserter; }
			if (ft == FOType.InserterStack) { return FactorioOrganizer.Properties.Resources.stack_inserter; }
			if (ft == FOType.InserterStackFilter) { return FactorioOrganizer.Properties.Resources.stack_filter_inserter; }

			if (ft == FOType.AssemblingMachine) { return FactorioOrganizer.Properties.Resources.assembling_machine_1; }
			if (ft == FOType.AssemblingMachine2) { return FactorioOrganizer.Properties.Resources.assembling_machine_2; }
			if (ft == FOType.AssemblingMachine3) { return FactorioOrganizer.Properties.Resources.assembling_machine_3; }
			if (ft == FOType.OilRefinery) { return FactorioOrganizer.Properties.Resources.oil_refinery; }
			if (ft == FOType.ChemicalPlant) { return FactorioOrganizer.Properties.Resources.chemical_plant; }
			if (ft == FOType.Centrifuge) { return FactorioOrganizer.Properties.Resources.centrifuge; }
			if (ft == FOType.Lab) { return FactorioOrganizer.Properties.Resources.lab; }

			if (ft == FOType.ScienceRed) { return FactorioOrganizer.Properties.Resources.automation_science_pack; }
			if (ft == FOType.ScienceGreen) { return FactorioOrganizer.Properties.Resources.logistic_science_pack; }
			if (ft == FOType.ScienceGrey) { return FactorioOrganizer.Properties.Resources.military_science_pack; }
			if (ft == FOType.ScienceBlue) { return FactorioOrganizer.Properties.Resources.chemical_science_pack; }
			if (ft == FOType.ScienceViolet) { return FactorioOrganizer.Properties.Resources.production_science_pack; }
			if (ft == FOType.ScienceYellow) { return FactorioOrganizer.Properties.Resources.utility_science_pack; }

			if (ft == FOType.Rail) { return FactorioOrganizer.Properties.Resources.rail; }
			if (ft == FOType.TrainStop) { return FactorioOrganizer.Properties.Resources.train_stop; }
			if (ft == FOType.RailSignal) { return FactorioOrganizer.Properties.Resources.rail_signal; }
			if (ft == FOType.RailChainSignal) { return FactorioOrganizer.Properties.Resources.rail_chain_signal; }
			if (ft == FOType.Locomotive) { return FactorioOrganizer.Properties.Resources.diesel_locomotive; }
			if (ft == FOType.CargoWagon) { return FactorioOrganizer.Properties.Resources.cargo_wagon; }
			if (ft == FOType.FluidWagon) { return FactorioOrganizer.Properties.Resources.fluid_wagon; }
			if (ft == FOType.ArtilleryWagon) { return FactorioOrganizer.Properties.Resources.artillery_wagon; }
			if (ft == FOType.Car) { return FactorioOrganizer.Properties.Resources.car; }
			if (ft == FOType.Tank) { return FactorioOrganizer.Properties.Resources.tank; }

			if (ft == FOType.EngineUnit) { return FactorioOrganizer.Properties.Resources.engine_unit; }
			if (ft == FOType.EngineElectricUnit) { return FactorioOrganizer.Properties.Resources.electric_engine_unit; }
			if (ft == FOType.FlyingRobotFrame) { return FactorioOrganizer.Properties.Resources.flying_robot_frame; }
			if (ft == FOType.LogisticRobot) { return FactorioOrganizer.Properties.Resources.logistic_robot; }
			if (ft == FOType.ConstructionRobot) { return FactorioOrganizer.Properties.Resources.construction_robot; }
			if (ft == FOType.RocketControlUnit) { return FactorioOrganizer.Properties.Resources.rocket_control_unit; }
			if (ft == FOType.LowDensityStructure) { return FactorioOrganizer.Properties.Resources.rocket_structure; }
			if (ft == FOType.RocketFuel) { return FactorioOrganizer.Properties.Resources.rocket_fuel; }

			if (ft == FOType.Grenade) { return FactorioOrganizer.Properties.Resources.grenade; }
			if (ft == FOType.MagazineFirearm) { return FactorioOrganizer.Properties.Resources.firearm_magazine; }
			if (ft == FOType.MagazinePiercing) { return FactorioOrganizer.Properties.Resources.piercing_rounds_magazine; }
			if (ft == FOType.ShotgunShells) { return FactorioOrganizer.Properties.Resources.shotgun_shell; }
			if (ft == FOType.ShotgunPiercingShells) { return FactorioOrganizer.Properties.Resources.piercing_shotgun_shell; }
			if (ft == FOType.Pistol) { return FactorioOrganizer.Properties.Resources.pistol; }
			if (ft == FOType.SubmachineGun) { return FactorioOrganizer.Properties.Resources.submachine_gun; }
			if (ft == FOType.Shotgun) { return FactorioOrganizer.Properties.Resources.shotgun; }
			if (ft == FOType.CombatShotgun) { return FactorioOrganizer.Properties.Resources.combat_shotgun; }
			if (ft == FOType.LandMine) { return FactorioOrganizer.Properties.Resources.land_mine; }
			if (ft == FOType.Concrete) { return FactorioOrganizer.Properties.Resources.concrete; }
			if (ft == FOType.Wall) { return FactorioOrganizer.Properties.Resources.wall; }
			if (ft == FOType.GunTurret) { return FactorioOrganizer.Properties.Resources.gun_turret; }
			if (ft == FOType.LaserTurret) { return FactorioOrganizer.Properties.Resources.laser_turret; }
			if (ft == FOType.FlamethrowerTurret) { return FactorioOrganizer.Properties.Resources.flamethrower_turret; }
			if (ft == FOType.ArtilleryTurret) { return FactorioOrganizer.Properties.Resources.artillery_turret; }
			if (ft == FOType.Radar) { return FactorioOrganizer.Properties.Resources.radar; }
			if (ft == FOType.RocketSilo) { return FactorioOrganizer.Properties.Resources.rocket_silo; }

			if (ft == FOType.Sulfur) { return FactorioOrganizer.Properties.Resources.sulfur; }
			if (ft == FOType.SolidFuel) { return FactorioOrganizer.Properties.Resources.solid_fuel; }
			if (ft == FOType.Battery) { return FactorioOrganizer.Properties.Resources.battery; }
			if (ft == FOType.Explosives) { return FactorioOrganizer.Properties.Resources.explosives; }
			if (ft == FOType.CliffExplosives) { return FactorioOrganizer.Properties.Resources.cliff_explosives; }
			if (ft == FOType.EmptyBarrel) { return FactorioOrganizer.Properties.Resources.empty_barrel; }

			if (ft == FOType.OilCrude) { return FactorioOrganizer.Properties.Resources.crude_oil; }
			if (ft == FOType.OilLight) { return FactorioOrganizer.Properties.Resources.light_oil; }
			if (ft == FOType.OilHeavy) { return FactorioOrganizer.Properties.Resources.heavy_oil; }
			if (ft == FOType.Lubricant) { return FactorioOrganizer.Properties.Resources.lubricant; }
			if (ft == FOType.PetroleumGas) { return FactorioOrganizer.Properties.Resources.petroleum_gas; }
			if (ft == FOType.SulfuricAcid) { return FactorioOrganizer.Properties.Resources.sulfuric_acid; }
			if (ft == FOType.Water) { return FactorioOrganizer.Properties.Resources.water; }
			if (ft == FOType.Steam) { return FactorioOrganizer.Properties.Resources.steam; }
			if (ft == FOType.BasicOilProcessing) { return FactorioOrganizer.Properties.Resources.basic_oil_processing; }
			if (ft == FOType.AdvancedOilProcessing) { return FactorioOrganizer.Properties.Resources.advanced_oil_processing; }
			if (ft == FOType.HeavyOilCracking) { return FactorioOrganizer.Properties.Resources.heavy_oil_cracking; }
			if (ft == FOType.LightOilCracking) { return FactorioOrganizer.Properties.Resources.light_oil_cracking; }
			if (ft == FOType.CoalLiquefaction) { return FactorioOrganizer.Properties.Resources.coal_liquefaction; }
			if (ft == FOType.SolidFuelFromOilHeavy) { return FactorioOrganizer.Properties.Resources.solid_fuel_from_heavy_oil; }
			if (ft == FOType.SolidFuelFromOilLight) { return FactorioOrganizer.Properties.Resources.solid_fuel_from_light_oil; }
			if (ft == FOType.SolidFuelFromPetroleumGas) { return FactorioOrganizer.Properties.Resources.solid_fuel_from_petroleum_gas; }

			if (ft == FOType.PortableSolarPanel) { return FactorioOrganizer.Properties.Resources.solar_panel_equipment; }
			if (ft == FOType.PortableFusionReactor) { return FactorioOrganizer.Properties.Resources.fusion_reactor_equipment; }
			if (ft == FOType.EnergyShield) { return FactorioOrganizer.Properties.Resources.energy_shield_equipment; }
			if (ft == FOType.EnergyShieldMK2) { return FactorioOrganizer.Properties.Resources.energy_shield_mk2_equipment; }
			if (ft == FOType.PersonalBattery) { return FactorioOrganizer.Properties.Resources.battery_equipment; }
			if (ft == FOType.PersonalBatteryMK2) { return FactorioOrganizer.Properties.Resources.battery_mk2_equipment; }
			if (ft == FOType.PersonalLaserDefense) { return FactorioOrganizer.Properties.Resources.personal_laser_defense_equipment; }
			if (ft == FOType.DischargeDefense) { return FactorioOrganizer.Properties.Resources.discharge_defense_equipment; }
			if (ft == FOType.BeltImmunity) { return FactorioOrganizer.Properties.Resources.belt_immunity_equipment; }
			if (ft == FOType.Exoskeleton) { return FactorioOrganizer.Properties.Resources.exoskeleton_equipment; }
			if (ft == FOType.PersonalRoboport) { return FactorioOrganizer.Properties.Resources.personal_roboport_equipment; }
			if (ft == FOType.PersonalRoboportMK2) { return FactorioOrganizer.Properties.Resources.personal_roboport_mk2_equipment; }
			if (ft == FOType.NightVision) { return FactorioOrganizer.Properties.Resources.night_vision_equipment; }

			if (ft == FOType.Beacon) { return FactorioOrganizer.Properties.Resources.beacon; }
			if (ft == FOType.ModuleSpeed1) { return FactorioOrganizer.Properties.Resources.speed_module; }
			if (ft == FOType.ModuleSpeed2) { return FactorioOrganizer.Properties.Resources.speed_module_2; }
			if (ft == FOType.ModuleSpeed3) { return FactorioOrganizer.Properties.Resources.speed_module_3; }
			if (ft == FOType.ModuleProductivity1) { return FactorioOrganizer.Properties.Resources.productivity_module; }
			if (ft == FOType.ModuleProductivity2) { return FactorioOrganizer.Properties.Resources.productivity_module_2; }
			if (ft == FOType.ModuleProductivity3) { return FactorioOrganizer.Properties.Resources.productivity_module_3; }
			if (ft == FOType.ModuleEfficiency1) { return FactorioOrganizer.Properties.Resources.effectivity_module; }
			if (ft == FOType.ModuleEfficiency2) { return FactorioOrganizer.Properties.Resources.effectivity_module_2; }
			if (ft == FOType.ModuleEfficiency3) { return FactorioOrganizer.Properties.Resources.effectivity_module_3; }

			return FactorioOrganizer.Properties.Resources.assembling_machine_0;
		}


		//return true if this element is made inside a furnace ////    return true si c'est élément est fabriqué à l'intérieur d'un four
		public static bool IsRecipeMadeInFurnace(FOType Recipe)
		{
			if (Recipe == FOType.IronPlate) { return true; }
			if (Recipe == FOType.CopperPlate) { return true; }

			if (Recipe == FOType.SteelPlate) { return true; }
			if (Recipe == FOType.StoneBrick) { return true; }
			return false;
		}

		//return every inputs of a recipe ////    return tout les inputs de la recette
		public static FOType[] GetRecipeInputs(FOType Recipe)
		{
			if (Recipe == FOType.TEST) { return new FOType[] { FOType.Inserter, FOType.InserterLong, FOType.InserterFast, FOType.InserterFilter }; }

			// https://wiki.factorio.com/Items

			//for the sake of the universe keep that somewhat ordered
			
			if (Recipe == FOType.IronGear) { return new FOType[] { FOType.IronPlate }; }
			else if (Recipe == FOType.IronPlate) { return new FOType[] { FOType.OreIron }; }
			else if (Recipe == FOType.CopperPlate) { return new FOType[] { FOType.OreCopper }; }
			else if (Recipe == FOType.IronStick) { return new FOType[] { FOType.IronPlate }; }
			else if (Recipe == FOType.CopperCable) { return new FOType[] { FOType.CopperPlate }; }

			else if (Recipe == FOType.ChestWood) { return new FOType[] { FOType.Wood }; }
			else if (Recipe == FOType.ChestIron) { return new FOType[] { FOType.IronPlate }; }
			else if (Recipe == FOType.ChestSteel) { return new FOType[] { FOType.SteelPlate }; }
			else if (Recipe == FOType.ChestLogistic) { return new FOType[] { FOType.RedCircuit, FOType.GreenCircuit, FOType.ChestSteel }; }
			else if (Recipe == FOType.StorageTank) { return new FOType[] { FOType.IronPlate, FOType.SteelPlate }; }

			else if (Recipe == FOType.Belt) { return new FOType[] { FOType.IronPlate, FOType.IronGear }; }
			else if (Recipe == FOType.BeltFast) { return new FOType[] { FOType.IronGear, FOType.Belt }; }
			else if (Recipe == FOType.BeltExpress) { return new FOType[] { FOType.BeltFast, FOType.IronGear, FOType.Lubricant }; }
			else if (Recipe == FOType.BeltUnderground) { return new FOType[] { FOType.IronPlate, FOType.Belt }; }
			else if (Recipe == FOType.BeltFastUnderground) { return new FOType[] { FOType.IronGear, FOType.BeltUnderground }; }
			else if (Recipe == FOType.BeltExpressUnderground) { return new FOType[] { FOType.BeltFastUnderground, FOType.IronGear, FOType.Lubricant }; }
			else if (Recipe == FOType.BeltSplitter) { return new FOType[] { FOType.GreenCircuit, FOType.IronPlate, FOType.Belt }; }
			else if (Recipe == FOType.BeltFastSplitter) { return new FOType[] { FOType.GreenCircuit, FOType.IronGear, FOType.BeltSplitter }; }
			else if (Recipe == FOType.BeltExpressSplitter) { return new FOType[] { FOType.RedCircuit, FOType.BeltFastSplitter, FOType.IronGear, FOType.Lubricant }; }

			else if (Recipe == FOType.Boiler) { return new FOType[] { FOType.Pipe, FOType.FurnaceStone }; }
			else if (Recipe == FOType.SteamEngine) { return new FOType[] { FOType.IronGear, FOType.IronPlate, FOType.Pipe }; }
			else if (Recipe == FOType.SteamTurbine) { return new FOType[] { FOType.CopperPlate, FOType.IronGear, FOType.Pipe }; }
			else if (Recipe == FOType.SolarPanel) { return new FOType[] { FOType.CopperPlate, FOType.GreenCircuit, FOType.SteelPlate }; }
			else if (Recipe == FOType.Accumulator) { return new FOType[] { FOType.Battery, FOType.IronPlate }; }
			else if (Recipe == FOType.NuclearReactor) { return new FOType[] { FOType.RedCircuit, FOType.Concrete, FOType.CopperPlate, FOType.SteelPlate }; }
			else if (Recipe == FOType.HeatExchanger) { return new FOType[] { FOType.CopperPlate, FOType.Pipe, FOType.SteelPlate }; }
			else if (Recipe == FOType.HeatPipe) { return new FOType[] { FOType.CopperPlate, FOType.SteelPlate }; }
			else if (Recipe == FOType.ElectricMiningDrill) { return new FOType[] { FOType.GreenCircuit, FOType.IronGear, FOType.IronPlate }; }
			else if (Recipe == FOType.OffshorePump) { return new FOType[] { FOType.GreenCircuit, FOType.IronGear, FOType.Pipe }; }
			else if (Recipe == FOType.Pumpjack) { return new FOType[] { FOType.GreenCircuit, FOType.IronGear, FOType.Pipe, FOType.SteelPlate }; }
			else if (Recipe == FOType.FurnaceStone) { return new FOType[] { FOType.Stone }; }
			else if (Recipe == FOType.FurnaceSteel) { return new FOType[] { FOType.StoneBrick, FOType.SteelPlate }; }
			else if (Recipe == FOType.FurnaceElectric) { return new FOType[] { FOType.RedCircuit, FOType.SteelPlate, FOType.StoneBrick }; }
			else if (Recipe == FOType.GreenCircuit) { return new FOType[] { FOType.IronPlate, FOType.CopperCable }; }
			else if (Recipe == FOType.RedCircuit) { return new FOType[] { FOType.GreenCircuit, FOType.Plastic, FOType.CopperCable }; }
			else if (Recipe == FOType.ProcessingUnit) { return new FOType[] { FOType.GreenCircuit, FOType.RedCircuit, FOType.SulfuricAcid }; }

			else if (Recipe == FOType.ElectricPole) { return new FOType[] { FOType.CopperCable, FOType.Wood }; }
			else if (Recipe == FOType.MediumElectricPole) { return new FOType[] { FOType.CopperPlate, FOType.IronStick, FOType.SteelPlate }; }
			else if (Recipe == FOType.BigElectricPole) { return new FOType[] { FOType.CopperPlate, FOType.IronStick, FOType.SteelPlate }; }
			else if (Recipe == FOType.Pipe) { return new FOType[] { FOType.IronPlate }; }
			else if (Recipe == FOType.PipeUnderground) { return new FOType[] { FOType.IronPlate, FOType.Pipe }; }
			else if (Recipe == FOType.Pump) { return new FOType[] { FOType.EngineUnit, FOType.Pipe, FOType.SteelPlate }; }

			else if (Recipe == FOType.SteelPlate) { return new FOType[] { FOType.IronPlate }; }
			else if (Recipe == FOType.Plastic) { return new FOType[] { FOType.PetroleumGas, FOType.Coal }; }
			else if (Recipe == FOType.StoneBrick) { return new FOType[] { FOType.Stone }; }

			else if (Recipe == FOType.Inserter) { return new FOType[] { FOType.IronPlate, FOType.IronGear, FOType.GreenCircuit }; }
			else if (Recipe == FOType.InserterLong) { return new FOType[] { FOType.IronPlate, FOType.IronGear, FOType.Inserter }; }
			else if (Recipe == FOType.InserterFast) { return new FOType[] { FOType.IronPlate, FOType.GreenCircuit, FOType.Inserter }; }
			else if (Recipe == FOType.InserterFilter) { return new FOType[] { FOType.GreenCircuit, FOType.InserterFast }; }
			else if (Recipe == FOType.InserterStack) { return new FOType[] { FOType.RedCircuit, FOType.GreenCircuit, FOType.InserterFast, FOType.IronGear }; }
			else if (Recipe == FOType.InserterStackFilter) { return new FOType[] { FOType.GreenCircuit, FOType.InserterStack }; }

			else if (Recipe == FOType.AssemblingMachine) { return new FOType[] { FOType.GreenCircuit, FOType.IronGear, FOType.IronPlate }; }
			else if (Recipe == FOType.AssemblingMachine2) { return new FOType[] { FOType.GreenCircuit, FOType.IronGear, FOType.AssemblingMachine, FOType.SteelPlate }; }
			else if (Recipe == FOType.AssemblingMachine3) { return new FOType[] { FOType.AssemblingMachine2, FOType.ModuleSpeed1 }; }
			else if (Recipe == FOType.OilRefinery) { return new FOType[] { FOType.GreenCircuit, FOType.IronGear, FOType.Pipe, FOType.SteelPlate }; }
			else if (Recipe == FOType.ChemicalPlant) { return new FOType[] { FOType.GreenCircuit, FOType.IronGear, FOType.Pipe, FOType.SteelPlate }; }
			else if (Recipe == FOType.Centrifuge) { return new FOType[] { FOType.RedCircuit, FOType.Concrete, FOType.IronGear, FOType.SteelPlate }; }
			else if (Recipe == FOType.Lab) { return new FOType[] { FOType.GreenCircuit, FOType.IronGear, FOType.Belt }; }

			else if (Recipe == FOType.ScienceRed) { return new FOType[] { FOType.CopperPlate, FOType.IronGear }; }
			else if (Recipe == FOType.ScienceGreen) { return new FOType[] { FOType.Belt, FOType.Inserter }; }
			else if (Recipe == FOType.ScienceGrey) { return new FOType[] { FOType.Grenade, FOType.MagazinePiercing, FOType.Wall }; }
			else if (Recipe == FOType.ScienceBlue) { return new FOType[] { FOType.RedCircuit, FOType.EngineUnit, FOType.Sulfur }; }
			else if (Recipe == FOType.ScienceViolet) { return new FOType[] { FOType.FurnaceElectric, FOType.ModuleProductivity1, FOType.Rail }; }
			else if (Recipe == FOType.ScienceYellow) { return new FOType[] { FOType.FlyingRobotFrame, FOType.LowDensityStructure, FOType.ProcessingUnit }; }

			else if (Recipe == FOType.Rail) { return new FOType[] { FOType.IronStick, FOType.SteelPlate, FOType.Stone }; }
			else if (Recipe == FOType.TrainStop) { return new FOType[] { FOType.GreenCircuit, FOType.IronPlate, FOType.IronStick, FOType.SteelPlate }; }
			else if (Recipe == FOType.RailSignal) { return new FOType[] { FOType.GreenCircuit, FOType.IronPlate }; }
			else if (Recipe == FOType.RailChainSignal) { return new FOType[] { FOType.GreenCircuit, FOType.IronPlate }; }
			else if (Recipe == FOType.Locomotive) { return new FOType[] { FOType.GreenCircuit, FOType.EngineUnit, FOType.SteelPlate }; }
			else if (Recipe == FOType.CargoWagon) { return new FOType[] { FOType.IronGear, FOType.IronPlate, FOType.SteelPlate }; }
			else if (Recipe == FOType.FluidWagon) { return new FOType[] { FOType.IronGear, FOType.Pipe, FOType.SteelPlate, FOType.StorageTank }; }
			else if (Recipe == FOType.ArtilleryWagon) { return new FOType[] { FOType.RedCircuit, FOType.EngineUnit, FOType.IronGear, FOType.Pipe, FOType.SteelPlate }; }
			else if (Recipe == FOType.Car) { return new FOType[] { FOType.EngineUnit, FOType.IronPlate, FOType.SteelPlate }; }
			else if (Recipe == FOType.Tank) { return new FOType[] { FOType.RedCircuit, FOType.EngineUnit, FOType.IronGear, FOType.SteelPlate }; }

			else if (Recipe == FOType.EngineUnit) { return new FOType[] { FOType.IronGear, FOType.Pipe, FOType.SteelPlate }; }
			else if (Recipe == FOType.EngineElectricUnit) { return new FOType[] { FOType.GreenCircuit, FOType.EngineUnit, FOType.Lubricant }; }
			else if (Recipe == FOType.FlyingRobotFrame) { return new FOType[] { FOType.Battery, FOType.EngineElectricUnit, FOType.GreenCircuit, FOType.SteelPlate }; }
			else if (Recipe == FOType.LogisticRobot) { return new FOType[] { FOType.RedCircuit, FOType.FlyingRobotFrame }; }
			else if (Recipe == FOType.ConstructionRobot) { return new FOType[] { FOType.GreenCircuit, FOType.FlyingRobotFrame }; }
			else if (Recipe == FOType.RocketControlUnit) { return new FOType[] { FOType.ProcessingUnit, FOType.ModuleSpeed1 }; }
			else if (Recipe == FOType.LowDensityStructure) { return new FOType[] { FOType.CopperPlate, FOType.Plastic, FOType.SteelPlate }; }
			else if (Recipe == FOType.RocketFuel) { return new FOType[] { FOType.OilLight, FOType.SolidFuel }; }

			else if (Recipe == FOType.Grenade) { return new FOType[] { FOType.IronPlate, FOType.Coal }; }
			else if (Recipe == FOType.MagazineFirearm) { return new FOType[] { FOType.IronPlate }; }
			else if (Recipe == FOType.MagazinePiercing) { return new FOType[] { FOType.CopperPlate, FOType.MagazineFirearm, FOType.SteelPlate }; }
			else if (Recipe == FOType.ShotgunShells) { return new FOType[] { FOType.CopperPlate, FOType.IronPlate }; }
			else if (Recipe == FOType.ShotgunPiercingShells) { return new FOType[] { FOType.CopperPlate, FOType.ShotgunShells, FOType.SteelPlate }; }
			else if (Recipe == FOType.Pistol) { return new FOType[] { FOType.IronPlate, FOType.CopperPlate }; }
			else if (Recipe == FOType.SubmachineGun) { return new FOType[] { FOType.IronPlate, FOType.CopperPlate, FOType.IronGear }; }
			else if (Recipe == FOType.Shotgun) { return new FOType[] { FOType.IronPlate, FOType.CopperPlate, FOType.IronGear, FOType.Wood }; }
			else if (Recipe == FOType.CombatShotgun) { return new FOType[] { FOType.SteelPlate, FOType.CopperPlate, FOType.IronGear, FOType.Wood }; }
			else if (Recipe == FOType.LandMine) { return new FOType[] { FOType.Explosives, FOType.SteelPlate }; }
			else if (Recipe == FOType.Concrete) { return new FOType[] { FOType.OreIron, FOType.StoneBrick, FOType.Water }; }
			else if (Recipe == FOType.Wall) { return new FOType[] { FOType.StoneBrick }; }
			else if (Recipe == FOType.GunTurret) { return new FOType[] { FOType.CopperPlate, FOType.IronGear, FOType.IronPlate }; }
			else if (Recipe == FOType.LaserTurret) { return new FOType[] { FOType.Battery, FOType.GreenCircuit, FOType.SteelPlate }; }
			else if (Recipe == FOType.FlamethrowerTurret) { return new FOType[] { FOType.EngineUnit, FOType.IronGear, FOType.Pipe, FOType.SteelPlate }; }
			else if (Recipe == FOType.ArtilleryTurret) { return new FOType[] { FOType.RedCircuit, FOType.Concrete, FOType.IronGear, FOType.SteelPlate }; }
			else if (Recipe == FOType.Radar) { return new FOType[] { FOType.GreenCircuit, FOType.IronGear, FOType.IronPlate }; }
			else if (Recipe == FOType.RocketSilo) { return new FOType[] { FOType.Concrete, FOType.EngineElectricUnit, FOType.Pipe, FOType.ProcessingUnit, FOType.SteelPlate }; }

			else if (Recipe == FOType.Sulfur) { return new FOType[] { FOType.PetroleumGas, FOType.Water }; }
			else if (Recipe == FOType.Battery) { return new FOType[] { FOType.IronPlate, FOType.CopperPlate, FOType.SulfuricAcid }; }
			else if (Recipe == FOType.Explosives) { return new FOType[] { FOType.Coal, FOType.Sulfur, FOType.Water }; }
			else if (Recipe == FOType.CliffExplosives) { return new FOType[] { FOType.EmptyBarrel, FOType.Explosives, FOType.Grenade }; }
			else if (Recipe == FOType.EmptyBarrel) { return new FOType[] { FOType.SteelPlate }; }

			else if (Recipe == FOType.Lubricant) { return new FOType[] { FOType.OilHeavy }; }
			else if (Recipe == FOType.SulfuricAcid) { return new FOType[] { FOType.IronPlate, FOType.Sulfur, FOType.Water }; }
			else if (Recipe == FOType.BasicOilProcessing) { return new FOType[] { FOType.OilCrude }; }
			else if (Recipe == FOType.AdvancedOilProcessing) { return new FOType[] { FOType.OilCrude, FOType.Water }; }
			else if (Recipe == FOType.HeavyOilCracking) { return new FOType[] { FOType.OilHeavy, FOType.Water }; }
			else if (Recipe == FOType.LightOilCracking) { return new FOType[] { FOType.OilLight, FOType.Water }; }
			else if (Recipe == FOType.CoalLiquefaction) { return new FOType[] { FOType.Coal, FOType.OilHeavy, FOType.Steam }; }
			else if (Recipe == FOType.SolidFuelFromOilHeavy) { return new FOType[] { FOType.OilHeavy }; }
			else if (Recipe == FOType.SolidFuelFromOilLight) { return new FOType[] { FOType.OilLight }; }
			else if (Recipe == FOType.SolidFuelFromPetroleumGas) { return new FOType[] { FOType.PetroleumGas }; }

			else if (Recipe == FOType.PortableSolarPanel) { return new FOType[] { FOType.RedCircuit, FOType.SolarPanel, FOType.SteelPlate }; }
			else if (Recipe == FOType.PortableFusionReactor) { return new FOType[] { FOType.LowDensityStructure, FOType.ProcessingUnit }; }
			else if (Recipe == FOType.EnergyShield) { return new FOType[] { FOType.RedCircuit, FOType.SteelPlate }; }
			else if (Recipe == FOType.EnergyShieldMK2) { return new FOType[] { FOType.EnergyShield, FOType.LowDensityStructure, FOType.ProcessingUnit }; }
			else if (Recipe == FOType.PersonalBattery) { return new FOType[] { FOType.Battery, FOType.SteelPlate }; }
			else if (Recipe == FOType.PersonalBatteryMK2) { return new FOType[] { FOType.PersonalBattery, FOType.LowDensityStructure, FOType.ProcessingUnit }; }
			else if (Recipe == FOType.PersonalLaserDefense) { return new FOType[] { FOType.LaserTurret, FOType.LowDensityStructure, FOType.ProcessingUnit }; }
			else if (Recipe == FOType.DischargeDefense) { return new FOType[] { FOType.LaserTurret, FOType.ProcessingUnit, FOType.SteelPlate }; }
			else if (Recipe == FOType.BeltImmunity) { return new FOType[] { FOType.RedCircuit, FOType.SteelPlate }; }
			else if (Recipe == FOType.Exoskeleton) { return new FOType[] { FOType.EngineElectricUnit, FOType.ProcessingUnit, FOType.SteelPlate }; }
			else if (Recipe == FOType.PersonalRoboport) { return new FOType[] { FOType.RedCircuit, FOType.Battery, FOType.IronGear, FOType.SteelPlate }; }
			else if (Recipe == FOType.PersonalRoboportMK2) { return new FOType[] { FOType.LowDensityStructure, FOType.PersonalRoboport, FOType.ProcessingUnit }; }
			else if (Recipe == FOType.NightVision) { return new FOType[] { FOType.RedCircuit, FOType.SteelPlate }; }

			else if (Recipe == FOType.Beacon) { return new FOType[] { FOType.RedCircuit, FOType.CopperCable, FOType.GreenCircuit, FOType.SteelPlate }; }
			else if (Recipe == FOType.ModuleSpeed1) { return new FOType[] { FOType.GreenCircuit, FOType.RedCircuit }; }
			else if (Recipe == FOType.ModuleSpeed2) { return new FOType[] { FOType.RedCircuit, FOType.ProcessingUnit, FOType.ModuleSpeed1 }; }
			else if (Recipe == FOType.ModuleSpeed3) { return new FOType[] { FOType.RedCircuit, FOType.ProcessingUnit, FOType.ModuleSpeed2 }; }
			else if (Recipe == FOType.ModuleProductivity1) { return new FOType[] { FOType.GreenCircuit, FOType.RedCircuit }; }
			else if (Recipe == FOType.ModuleProductivity2) { return new FOType[] { FOType.RedCircuit, FOType.ProcessingUnit, FOType.ModuleProductivity1 }; }
			else if (Recipe == FOType.ModuleProductivity3) { return new FOType[] { FOType.RedCircuit, FOType.ProcessingUnit, FOType.ModuleProductivity2 }; }
			else if (Recipe == FOType.ModuleEfficiency1) { return new FOType[] { FOType.GreenCircuit, FOType.RedCircuit }; }
			else if (Recipe == FOType.ModuleEfficiency2) { return new FOType[] { FOType.RedCircuit, FOType.ProcessingUnit, FOType.ModuleEfficiency1 }; }
			else if (Recipe == FOType.ModuleEfficiency3) { return new FOType[] { FOType.RedCircuit, FOType.ProcessingUnit, FOType.ModuleEfficiency2 }; }

			else { return new FOType[] { }; }
		}

		//return every outputs of a recipe ////    retourne tout les output de la recette
		public static FOType[] GetRecipeOutputs(FOType Recipe)
		{
			if (Recipe == FOType.TEST) { return new FOType[] { FOType.ScienceRed, FOType.ScienceGreen, FOType.ScienceGrey }; }

			if (Recipe == FOType.BasicOilProcessing) { return new FOType[] { FOType.PetroleumGas }; }
			if (Recipe == FOType.AdvancedOilProcessing) { return new FOType[] { FOType.PetroleumGas, FOType.OilLight, FOType.OilHeavy }; }
			if (Recipe == FOType.HeavyOilCracking) { return new FOType[] { FOType.OilLight }; }
			if (Recipe == FOType.LightOilCracking) { return new FOType[] { FOType.PetroleumGas }; }
			if (Recipe == FOType.CoalLiquefaction) { return new FOType[] { FOType.OilHeavy, FOType.OilLight, FOType.PetroleumGas }; }
			if (Recipe == FOType.SolidFuelFromOilHeavy) { return new FOType[] { FOType.SolidFuel }; }
			if (Recipe == FOType.SolidFuelFromOilLight) { return new FOType[] { FOType.SolidFuel }; }
			if (Recipe == FOType.SolidFuelFromPetroleumGas) { return new FOType[] { FOType.SolidFuel }; }

			return new FOType[] { Recipe }; //most objects ////    la plupart des object
		}
		
		//a recipe is never an array. every recipe are designed from an object of the game.
		//inputs and outputs are always arrays that can contain one or more elements.
		//a recipe specify what are the inputs and outputs. a recipe isn't always possible as a belt for exemple : oil processing things
		//most object of the game are considered as their own recipe.

		
		//return if the specified object can exist as belt. if false, it can only be a recipe in machine ////    retourne si l'object spécifié peut exister en version belt. si false, il n'est possible uniquement dans une machine
		public static bool IsBeltable(FOType ft)
		{
			if (ft == FOType.TEST) { return false; }
			
			if (ft == FOType.BasicOilProcessing) { return false; }
			if (ft == FOType.AdvancedOilProcessing) { return false; }
			if (ft == FOType.HeavyOilCracking) { return false; }
			if (ft == FOType.LightOilCracking) { return false; }
			if (ft == FOType.CoalLiquefaction) { return false; }
			if (ft == FOType.SolidFuelFromOilHeavy) { return false; }
			if (ft == FOType.SolidFuelFromOilLight) { return false; }
			if (ft == FOType.SolidFuelFromPetroleumGas) { return false; }

			return true;
		}

		//return if the specified object is concidered a recipe. ////    return si l'object spécifié est considéré comme une recette. c'est à dire qu'il peut exister comme recette de machine
		public static bool IsRecipe(FOType ft)
		{

			if (ft == FOType.OreIron) { return false; }
			if (ft == FOType.OreCopper) { return false; }
			if (ft == FOType.Coal) { return false; }
			if (ft == FOType.Stone) { return false; }
			if (ft == FOType.Wood) { return false; }

			if (ft == FOType.SolidFuel) { return false; }

			if (ft == FOType.OilCrude) { return false; }
			if (ft == FOType.OilLight) { return false; }
			if (ft == FOType.OilHeavy) { return false; }
			if (ft == FOType.PetroleumGas) { return false; }
			if (ft == FOType.Water) { return false; }
			if (ft == FOType.Steam) { return false; }

			return true;
		}



	}
}
