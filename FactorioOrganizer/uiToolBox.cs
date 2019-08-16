using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FactorioOrganizer
{
	public class uiToolBox
	{
		public uiMapEditer Editer;

		private Panel panele;
		private Label TextBelt;
		private Label TextAssembler;


		public int Top
		{
			get { return this.panele.Top; }
			set { this.panele.Top = value; }
		}
		public int Left
		{
			get { return this.panele.Left; }
			set { this.panele.Left = value; }
		}
		public int Width
		{
			get { return this.panele.Width; }
			set { this.panele.Width = value; }
		}
		public int Height
		{
			get { return this.panele.Height; }
			set { this.panele.Height = value; }
		}
		public Control Parent
		{
			get { return this.panele.Parent; }
			set { this.panele.Parent = value; }
		}
		public DockStyle Dock
		{
			get { return this.panele.Dock; }
			set { this.panele.Dock = value; }
		}
		public AnchorStyles Anchor
		{
			get { return this.panele.Anchor; }
			set { this.panele.Anchor = value; }
		}

		

		public uiToolBox(uiMapEditer StartEditer)
		{
			this.Editer = StartEditer;

			this.panele = new Panel();
			this.panele.BorderStyle = BorderStyle.FixedSingle;
			this.panele.AutoScroll = true;
			this.panele.BackColor = Color.FromArgb(32, 32, 32);

			this.TextBelt = new Label();
			this.TextBelt.Parent = this.panele;
			this.TextBelt.Text = "Belt :";
			this.TextBelt.ForeColor = Color.White;

			this.TextAssembler = new Label();
			this.TextAssembler.Parent = this.panele;
			this.TextAssembler.Text = "Assembler :";
			this.TextAssembler.ForeColor = Color.White;
			


			this.CreateControls();
			this.RefreshSize();
		}



		private List<Button> listButtonBelt = new List<Button>();
		private List<Button> listButtonMachine = new List<Button>();
		

		private void CreateControls()
		{

			//this.CreateNewButtonBelt(FactorioOrganizer.Properties.Resources.iron_plate, FOType.IronPlate);
			//this.CreateNewButtonBelt(FactorioOrganizer.Properties.Resources.copper_plate, FOType.CopperPlate);
			//this.CreateNewButtonBelt(FactorioOrganizer.Properties.Resources.iron_gear_wheel, FOType.IronGear);
			//this.CreateNewButtonBelt(FactorioOrganizer.Properties.Resources.copper_cable, FOType.CopperCable);
			//this.CreateNewButtonBelt(FactorioOrganizer.Properties.Resources.transport_belt, FOType.Belt);
			//this.CreateNewButtonBelt(FactorioOrganizer.Properties.Resources.electronic_circuit, FOType.GreenCircuit);

			//this.CreateNewButtonMachine(FactorioOrganizer.Properties.Resources.iron_gear_wheel, FOType.IronGear);
			//this.CreateNewButtonMachine(FactorioOrganizer.Properties.Resources.copper_cable, FOType.CopperCable);
			//this.CreateNewButtonMachine(FactorioOrganizer.Properties.Resources.transport_belt, FOType.Belt);
			//this.CreateNewButtonMachine(FactorioOrganizer.Properties.Resources.electronic_circuit, FOType.GreenCircuit);



			//this.CreateNewButonBoth(FOType.TEST);
			this.CreateNewButtonBoth(FOType.OreIron);
			this.CreateNewButtonBoth(FOType.OreCopper);
			this.CreateNewButtonBoth(FOType.IronPlate);
			this.CreateNewButtonBoth(FOType.CopperPlate);
			this.CreateNewButtonBoth(FOType.Coal);
			this.CreateNewButtonBoth(FOType.Stone);
			this.CreateNewButtonBoth(FOType.Wood);
			this.CreateNewButtonBoth(FOType.SteelPlate);
			this.CreateNewButtonBoth(FOType.Plastic);
			this.CreateNewButtonBoth(FOType.StoneBrick);

			this.CreateNewButtonBoth(FOType.IronGear);
			this.CreateNewButtonBoth(FOType.IronStick);
			this.CreateNewButtonBoth(FOType.CopperCable);

			this.CreateNewButtonBoth(FOType.ChestWood);
			this.CreateNewButtonBoth(FOType.ChestIron);
			this.CreateNewButtonBoth(FOType.ChestSteel);
			this.CreateNewButtonBoth(FOType.ChestLogistic);
			this.CreateNewButtonBoth(FOType.StorageTank);

			this.CreateNewButtonBoth(FOType.Belt);
			this.CreateNewButtonBoth(FOType.BeltUnderground);
			this.CreateNewButtonBoth(FOType.BeltSplitter);
			this.CreateNewButtonBoth(FOType.BeltFast);
			this.CreateNewButtonBoth(FOType.BeltFastUnderground);
			this.CreateNewButtonBoth(FOType.BeltFastSplitter);
			this.CreateNewButtonBoth(FOType.BeltExpress);
			this.CreateNewButtonBoth(FOType.BeltExpressUnderground);
			this.CreateNewButtonBoth(FOType.BeltExpressSplitter);

			this.CreateNewButtonBoth(FOType.Boiler);
			this.CreateNewButtonBoth(FOType.SteamEngine);
			this.CreateNewButtonBoth(FOType.SteamTurbine);
			this.CreateNewButtonBoth(FOType.SolarPanel);
			this.CreateNewButtonBoth(FOType.Accumulator);
			this.CreateNewButtonBoth(FOType.NuclearReactor);
			this.CreateNewButtonBoth(FOType.HeatExchanger);
			this.CreateNewButtonBoth(FOType.HeatPipe);
			this.CreateNewButtonBoth(FOType.ElectricMiningDrill);
			this.CreateNewButtonBoth(FOType.OffshorePump);
			this.CreateNewButtonBoth(FOType.Pumpjack);
			this.CreateNewButtonBoth(FOType.FurnaceStone);
			this.CreateNewButtonBoth(FOType.FurnaceSteel);
			this.CreateNewButtonBoth(FOType.FurnaceElectric);
			this.CreateNewButtonBoth(FOType.GreenCircuit);
			this.CreateNewButtonBoth(FOType.RedCircuit);
			this.CreateNewButtonBoth(FOType.ProcessingUnit);

			this.CreateNewButtonBoth(FOType.ElectricPole);
			this.CreateNewButtonBoth(FOType.MediumElectricPole);
			this.CreateNewButtonBoth(FOType.BigElectricPole);
			this.CreateNewButtonBoth(FOType.Pipe);
			this.CreateNewButtonBoth(FOType.PipeUnderground);
			this.CreateNewButtonBoth(FOType.Pump);

			this.CreateNewButtonBoth(FOType.Inserter);
			this.CreateNewButtonBoth(FOType.InserterLong);
			this.CreateNewButtonBoth(FOType.InserterFast);
			this.CreateNewButtonBoth(FOType.InserterFilter);
			this.CreateNewButtonBoth(FOType.InserterStack);
			this.CreateNewButtonBoth(FOType.InserterStackFilter);

			this.CreateNewButtonBoth(FOType.AssemblingMachine);
			this.CreateNewButtonBoth(FOType.AssemblingMachine2);
			this.CreateNewButtonBoth(FOType.AssemblingMachine3);
			this.CreateNewButtonBoth(FOType.OilRefinery);
			this.CreateNewButtonBoth(FOType.ChemicalPlant);
			this.CreateNewButtonBoth(FOType.Centrifuge);
			this.CreateNewButtonBoth(FOType.Lab);


			this.CreateNewButtonBoth(FOType.ScienceRed);
			this.CreateNewButtonBoth(FOType.ScienceGreen);
			this.CreateNewButtonBoth(FOType.ScienceGrey);
			this.CreateNewButtonBoth(FOType.ScienceBlue);
			this.CreateNewButtonBoth(FOType.ScienceViolet);
			this.CreateNewButtonBoth(FOType.ScienceYellow);

			this.CreateNewButtonBoth(FOType.Rail);
			this.CreateNewButtonBoth(FOType.TrainStop);
			this.CreateNewButtonBoth(FOType.RailSignal);
			this.CreateNewButtonBoth(FOType.RailChainSignal);
			this.CreateNewButtonBoth(FOType.Locomotive);
			this.CreateNewButtonBoth(FOType.CargoWagon);
			this.CreateNewButtonBoth(FOType.FluidWagon);
			this.CreateNewButtonBoth(FOType.ArtilleryWagon);
			this.CreateNewButtonBoth(FOType.Car);
			this.CreateNewButtonBoth(FOType.Tank);

			this.CreateNewButtonBoth(FOType.EngineUnit);
			this.CreateNewButtonBoth(FOType.EngineElectricUnit);
			this.CreateNewButtonBoth(FOType.FlyingRobotFrame);
			this.CreateNewButtonBoth(FOType.LogisticRobot);
			this.CreateNewButtonBoth(FOType.ConstructionRobot);
			this.CreateNewButtonBoth(FOType.RocketControlUnit);
			this.CreateNewButtonBoth(FOType.LowDensityStructure);
			this.CreateNewButtonBoth(FOType.RocketFuel);

			this.CreateNewButtonBoth(FOType.Grenade);
			this.CreateNewButtonBoth(FOType.MagazineFirearm);
			this.CreateNewButtonBoth(FOType.MagazinePiercing);
			this.CreateNewButtonBoth(FOType.ShotgunShells);
			this.CreateNewButtonBoth(FOType.ShotgunPiercingShells);
			this.CreateNewButtonBoth(FOType.Pistol);
			this.CreateNewButtonBoth(FOType.SubmachineGun);
			this.CreateNewButtonBoth(FOType.Shotgun);
			this.CreateNewButtonBoth(FOType.CombatShotgun);
			this.CreateNewButtonBoth(FOType.LandMine);
			this.CreateNewButtonBoth(FOType.Concrete);
			this.CreateNewButtonBoth(FOType.Wall);
			this.CreateNewButtonBoth(FOType.GunTurret);
			this.CreateNewButtonBoth(FOType.LaserTurret);
			this.CreateNewButtonBoth(FOType.FlamethrowerTurret);
			this.CreateNewButtonBoth(FOType.ArtilleryTurret);
			this.CreateNewButtonBoth(FOType.Radar);
			this.CreateNewButtonBoth(FOType.RocketSilo);

			this.CreateNewButtonBoth(FOType.Sulfur);
			this.CreateNewButtonBoth(FOType.SolidFuel);
			this.CreateNewButtonBoth(FOType.Battery);
			this.CreateNewButtonBoth(FOType.Explosives);
			this.CreateNewButtonBoth(FOType.CliffExplosives);
			this.CreateNewButtonBoth(FOType.EmptyBarrel);

			this.CreateNewButtonBoth(FOType.OilCrude);
			this.CreateNewButtonBoth(FOType.OilLight);
			this.CreateNewButtonBoth(FOType.OilHeavy);
			this.CreateNewButtonBoth(FOType.Lubricant);
			this.CreateNewButtonBoth(FOType.PetroleumGas);
			this.CreateNewButtonBoth(FOType.SulfuricAcid);
			this.CreateNewButtonBoth(FOType.Water);
			this.CreateNewButtonBoth(FOType.Steam);
			this.CreateNewButtonBoth(FOType.BasicOilProcessing);
			this.CreateNewButtonBoth(FOType.AdvancedOilProcessing);
			this.CreateNewButtonBoth(FOType.HeavyOilCracking);
			this.CreateNewButtonBoth(FOType.LightOilCracking);
			this.CreateNewButtonBoth(FOType.CoalLiquefaction);
			this.CreateNewButtonBoth(FOType.SolidFuelFromOilHeavy);
			this.CreateNewButtonBoth(FOType.SolidFuelFromOilLight);
			this.CreateNewButtonBoth(FOType.SolidFuelFromPetroleumGas);

			this.CreateNewButtonBoth(FOType.PortableSolarPanel);
			this.CreateNewButtonBoth(FOType.PortableFusionReactor);
			this.CreateNewButtonBoth(FOType.EnergyShield);
			this.CreateNewButtonBoth(FOType.EnergyShieldMK2);
			this.CreateNewButtonBoth(FOType.PersonalBattery);
			this.CreateNewButtonBoth(FOType.PersonalBatteryMK2);
			this.CreateNewButtonBoth(FOType.PersonalLaserDefense);
			this.CreateNewButtonBoth(FOType.DischargeDefense);
			this.CreateNewButtonBoth(FOType.BeltImmunity);
			this.CreateNewButtonBoth(FOType.Exoskeleton);
			this.CreateNewButtonBoth(FOType.PersonalRoboport);
			this.CreateNewButtonBoth(FOType.PersonalRoboportMK2);
			this.CreateNewButtonBoth(FOType.NightVision);

			this.CreateNewButtonBoth(FOType.Beacon);
			this.CreateNewButtonBoth(FOType.ModuleSpeed1);
			this.CreateNewButtonBoth(FOType.ModuleSpeed2);
			this.CreateNewButtonBoth(FOType.ModuleSpeed3);
			this.CreateNewButtonBoth(FOType.ModuleProductivity1);
			this.CreateNewButtonBoth(FOType.ModuleProductivity2);
			this.CreateNewButtonBoth(FOType.ModuleProductivity3);
			this.CreateNewButtonBoth(FOType.ModuleEfficiency1);
			this.CreateNewButtonBoth(FOType.ModuleEfficiency2);
			this.CreateNewButtonBoth(FOType.ModuleEfficiency3);


		}
		private void CreateNewButtonBelt(Bitmap img, FOType ft)
		{
			Button newb = new Button();
			newb.Parent = this.panele;
			newb.Image = img;
			newb.ImageAlign = ContentAlignment.MiddleCenter;
			this.listButtonBelt.Add(newb);
			//newb.Click += new EventHandler(this.AnyButton_Click);
			newb.MouseDown += new MouseEventHandler(this.AnyButton_MosueDown);
			newb.Tag = new object[] { MOType.Belt, ft };
		}
		private void CreateNewButtonMachine(Bitmap img, FOType ft)
		{
			Button newb = new Button();
			newb.Parent = this.panele;
			newb.Image = img;
			newb.ImageAlign = ContentAlignment.MiddleCenter;
			this.listButtonMachine.Add(newb);
			//newb.Click += new EventHandler(this.AnyButton_Click);
			newb.MouseDown += new MouseEventHandler(this.AnyButton_MosueDown);
			newb.Tag = new object[] { MOType.Machine, ft };
		}
		private void CreateNewButtonBoth(FOType ft)
		{
			Bitmap img = Utilz.GetAssociatedIcon(ft);
			this.CreateNewButtonBelt(img, ft);
			this.CreateNewButtonMachine(img, ft);
		}
		private void AnyButton_MosueDown(object sender, MouseEventArgs e)
		{
			Button btn = (Button)sender;
			btn.Focus();
			MOType mt = (MOType)(((object[])(btn.Tag))[0]);
			FOType ft = (FOType)(((object[])(btn.Tag))[1]);

			if (e.Button == MouseButtons.Left)
			{
				if (mt == MOType.Belt)
				{
					if (Utilz.IsBeltable(ft))
					{
						MapObject newmo = new MapObject(mt, ft);
						this.Editer.StartAddMode(newmo);
					}
				}
				if (mt == MOType.Machine)
				{
					if (Utilz.IsRecipe(ft))
					{
						MapObject newmo = new MapObject(mt, ft);
						this.Editer.StartAddMode(newmo);
					}
				}
			}
			if (e.Button == MouseButtons.Right)
			{
				FOType[] arrayOutputs = Utilz.GetRecipeOutputs(ft);
				FOType[] arrayInputs = Utilz.GetRecipeInputs(ft);

				oRightClick3 rc = new oRightClick3();
				//rc.Width = 200;
				rc.AddChoice(ft.ToString());
				rc.AddSeparator();
				rc.AddSeparator();
				rc.AddChoice("Outputs :");
				foreach (FOType subft in arrayOutputs)
				{
					rc.AddChoice("-" + subft.ToString());
				}
				rc.AddChoice("");
				rc.AddChoice("Inputs :");
				foreach (FOType subft in arrayInputs)
				{
					rc.AddChoice("-" + subft.ToString());
				}
				rc.ShowDialog();
			}

		}



		private void RefreshSize()
		{
			this.TextBelt.Top = 15; // 15
			this.TextBelt.Left = 5; // 5
			this.TextBelt.AutoSize = true;

			this.TextAssembler.Top = 55; // 55
			this.TextAssembler.Left = 5; // 5
			this.TextAssembler.AutoSize = true;
			


			int StartLeft = 70; // 70
			Size buttonsize = new Size(40, 40); // 40 40

			int actualleft = StartLeft;
			foreach (Button b in this.listButtonBelt)
			{
				MOType mt = (MOType)(((object[])(b.Tag))[0]);
				FOType ft = (FOType)(((object[])(b.Tag))[1]);
				b.Left = actualleft;
				b.Top = 1;
				b.Size = buttonsize;

				//back color ////    couleur d'arrière plan
				b.BackColor = Color.Gainsboro;
				bool isbelt = Utilz.IsBeltable(ft);
				if (!isbelt) { b.BackColor = Color.Crimson; }


				//next iteration
				actualleft += b.Width + 1;
			}

			actualleft = StartLeft;
			foreach (Button b in this.listButtonMachine)
			{
				MOType mt = (MOType)(((object[])(b.Tag))[0]);
				FOType ft = (FOType)(((object[])(b.Tag))[1]);
				b.Left = actualleft;
				b.Top = 1 + buttonsize.Height + 2;
				b.Size = buttonsize;

				//back color ////    couleur d'arrière plan
				b.BackColor = Color.Gainsboro;
				bool ismachine = Utilz.IsRecipe(ft);
				if (!ismachine) { b.BackColor = Color.Crimson; }

				//next iteration
				actualleft += b.Width + 1;
			}


		}


	}
}
