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
			
			this.CreateNewButtonBoth(Crafts.GetItemFromName("OreIron"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("OreCopper"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("IronPlate"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("CopperPlate"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Coal"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Stone"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Wood"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("SteelPlate"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Plastic"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("StoneBrick"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("IronGear"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("IronStick"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("CopperCable"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("ChestWood"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ChestIron"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ChestSteel"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ChestLogistic"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("StorageTank"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("Belt"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("BeltUnderground"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("BeltSplitter"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("BeltFast"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("BeltFastUnderground"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("BeltFastSplitter"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("BeltExpress"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("BeltExpressUnderground"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("BeltExpressSplitter"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("Boiler"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("SteamEngine"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("SteamTurbine"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("SolarPanel"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Accumulator"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("NuclearReactor"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("HeatExchanger"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("HeatPipe"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ElectricMiningDrill"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("OffshorePump"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Pumpjack"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("FurnaceStone"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("FurnaceSteel"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("FurnaceElectric"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("GreenCircuit"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("RedCircuit"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ProcessingUnit"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("ElectricPole"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("MediumElectricPole"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("BigElectricPole"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Pipe"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("PipeUnderground"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Pump"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("Inserter"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("InserterLong"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("InserterFast"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("InserterFilter"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("InserterStack"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("InserterStackFilter"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("AssemblingMachine"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("AssemblingMachine2"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("AssemblingMachine3"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("OilRefinery"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ChemicalPlant"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Centrifuge"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Lab"));


			this.CreateNewButtonBoth(Crafts.GetItemFromName("ScienceRed"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ScienceGreen"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ScienceGrey"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ScienceBlue"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ScienceViolet"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ScienceYellow"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("Rail"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("TrainStop"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("RailSignal"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("RailChainSignal"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Locomotive"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("CargoWagon"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("FluidWagon"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ArtilleryWagon"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Car"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Tank"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("EngineUnit"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("EngineElectricUnit"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("FlyingRobotFrame"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("LogisticRobot"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ConstructionRobot"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("RocketPart"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("RocketControlUnit"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("LowDensityStructure"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("RocketFuel"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("Grenade"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("MagazineFirearm"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("MagazinePiercing"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ShotgunShells"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ShotgunPiercingShells"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Pistol"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("SubmachineGun"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Shotgun"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("CombatShotgun"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("LandMine"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Concrete"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Wall"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("GunTurret"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("LaserTurret"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("FlamethrowerTurret"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ArtilleryTurret"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Radar"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("RocketSilo"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("Sulfur"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("SolidFuel"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Battery"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Explosives"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("CliffExplosives"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("EmptyBarrel"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("OilCrude"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("OilLight"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("OilHeavy"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Lubricant"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("PetroleumGas"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("SulfuricAcid"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Water"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Steam"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("BasicOilProcessing"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("AdvancedOilProcessing"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("HeavyOilCracking"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("LightOilCracking"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("CoalLiquefaction"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("SolidFuelFromOilHeavy"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("SolidFuelFromOilLight"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("SolidFuelFromPetroleumGas"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("PortableSolarPanel"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("PortableFusionReactor"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("EnergyShield"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("EnergyShieldMK2"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("PersonalBattery"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("PersonalBatteryMK2"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("PersonalLaserDefense"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("DischargeDefense"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("BeltImmunity"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("Exoskeleton"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("PersonalRoboport"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("PersonalRoboportMK2"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("NightVision"));

			this.CreateNewButtonBoth(Crafts.GetItemFromName("Beacon"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ModuleSpeed1"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ModuleSpeed2"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ModuleSpeed3"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ModuleProductivity1"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ModuleProductivity2"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ModuleProductivity3"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ModuleEfficiency1"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ModuleEfficiency2"));
			this.CreateNewButtonBoth(Crafts.GetItemFromName("ModuleEfficiency3"));

			
		}
		private void CreateNewButtonBelt(Bitmap img, sItem i)
		{
			Button newb = new Button();
			newb.Parent = this.panele;
			newb.Image = img;
			newb.ImageAlign = ContentAlignment.MiddleCenter;
			this.listButtonBelt.Add(newb);
			//newb.Click += new EventHandler(this.AnyButton_Click);
			newb.MouseDown += new MouseEventHandler(this.AnyButton_MosueDown);
			newb.Tag = new object[] { MOType.Belt, i };
		}
		private void CreateNewButtonMachine(Bitmap img, sItem i)
		{
			Button newb = new Button();
			newb.Parent = this.panele;
			newb.Image = img;
			newb.ImageAlign = ContentAlignment.MiddleCenter;
			this.listButtonMachine.Add(newb);
			//newb.Click += new EventHandler(this.AnyButton_Click);
			newb.MouseDown += new MouseEventHandler(this.AnyButton_MosueDown);
			newb.Tag = new object[] { MOType.Machine, i };
		}
		private void CreateNewButtonBoth(sItem i)
		{
			Bitmap img = Crafts.GetAssociatedIcon(i);
			this.CreateNewButtonBelt(img, i);
			this.CreateNewButtonMachine(img, i);
		}
		private void AnyButton_MosueDown(object sender, MouseEventArgs e)
		{
			Button btn = (Button)sender;
			btn.Focus();
			MOType mt = (MOType)(((object[])(btn.Tag))[0]);
			sItem i = (sItem)(((object[])(btn.Tag))[1]);

			if (e.Button == MouseButtons.Left)
			{
				if (mt == MOType.Belt)
				{
					if (i.IsBelt) //we set addmode only if the item can be a belt
					{
						MapObject newmo = new MapObject(MOType.Belt, i);
						this.Editer.StartAddMode(newmo);
					}
				}
				if (mt == MOType.Machine)
				{
					if (i.IsRecipe) //we set addmode only if the item can be a machine
					{
						MapObject newmo = new MapObject(MOType.Machine, i);
						this.Editer.StartAddMode(newmo);
					}
				}
			}
			if (e.Button == MouseButtons.Right)
			{
				//if this button is as machine, i represents the recipe
				oCraft c = Crafts.GetCraftFromRecipe(i);
				oRightClick3 rc = new oRightClick3();
				rc.AddChoice(i.Name);
				if (c != null)
				{
					rc.AddSeparator();
					rc.AddSeparator();
					//add every outputs and inputs for the user
					rc.AddChoice("Outputs :");
					foreach (sItem subi in c.Outputs)
					{
						rc.AddChoice("-" + subi.Name);
					}
					rc.AddChoice("");
					rc.AddChoice("Inputs :");
					foreach (sItem subi in c.Inputs)
					{
						rc.AddChoice("-" + subi.Name);
					}
				}
				string rep = rc.ShowDialog();
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
				sItem i = (sItem)(((object[])(b.Tag))[1]);
				b.Left = actualleft;
				b.Top = 1;
				b.Size = buttonsize;

				//back color ////    couleur d'arrière plan
				b.BackColor = Color.Gainsboro;
				bool isbelt = i.IsBelt;
				if (!isbelt) { b.BackColor = Color.Crimson; }


				//next iteration
				actualleft += b.Width + 1;
			}

			actualleft = StartLeft;
			foreach (Button b in this.listButtonMachine)
			{
				MOType mt = (MOType)(((object[])(b.Tag))[0]);
				sItem i = (sItem)(((object[])(b.Tag))[1]);
				b.Left = actualleft;
				b.Top = 1 + buttonsize.Height + 2;
				b.Size = buttonsize;

				//back color ////    couleur d'arrière plan
				b.BackColor = Color.Gainsboro;
				bool ismachine = i.IsRecipe;
				if (!ismachine) { b.BackColor = Color.Crimson; }

				//next iteration
				actualleft += b.Width + 1;
			}


		}


	}
}
