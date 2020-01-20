using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WinCtar1;
using System.IO;

namespace FactorioOrganizer
{
	//represent a mod
	public class oMod
	{

		public string ModName = "newmod"; //name of the mod


		public class ModItem
		{
			
			public string ItemName; //name of the item

			//return the name of the object, like it would be if it was a oItem object and if we called .Name.
			//we must specify ParentModName, which is the .ModName property of the oMod who contain this ModItem.
			//it will automatically use, or not use, the ParentModName to generate the .Name.
			public string Name(string ParentModName)
			{
				//if it's not an item of an external mod, we use our mod's name
				if (this.ItemModName == "-")
				{
					return ParentModName + "_$_" + this.ItemName;
				}
				
				//for every other cases
				if (this.ItemModName == "vanilla")
				{
					return this.ItemName;
				}
				return this.ItemModName + "_$_" + this.ItemName;
			}


			//name of the mod. if the name is "-", this means it's the string of the name of the mod we are in, whatever its name is.
			//this is also the "official" way to check is this item about another mod.
			public string ItemModName = "-";
			

			//return is this item an external mod/vanilla item. external to this oMod we are in. we simply have to check is mod name = to "-"
			public bool IsExternalItem
			{
				get
				{
					return this.ItemModName != "-";
				}
			}


			public Bitmap Img = null;
			public bool HasImage
			{
				get
				{
					return this.Img != null;
				}
			}
			


			public bool IsBelt = true;

			//IsRecipe is now "computed" when needed : the static class Crafts search if any crafts use it as a recipe.
			//public bool IsRecipe = true;

			public ModItem(string sItemName, string sItemModName = "-", Bitmap sImg = null)
			{
				this.ItemName = sItemName;
				this.ItemModName = sItemModName;
				this.Img = sImg;
			}
		}
		public class ModCraft
		{

			// /!\ /!\ /!\ decrepitated comment :
			//because each crafts are identified by an other item, what define a craft as external is their recipe.

			public refItem Recipe; //name of the item used as a recipe

			public refItem[] Inputs; //names of the inputs
			public refItem[] Outputs; //names of the outputs
			public bool IsMadeInFurnace; //indicate if the item is made inside a furnace
			
			public ModCraft()
			{
				this.Recipe = new refItem("none", "vanilla");
				this.Inputs = new refItem[] { };
				this.Outputs = new refItem[] { };
				this.IsMadeInFurnace = false;
			}
			public ModCraft(refItem sRecipe, refItem[] sInputs, refItem[] sOutputs, bool sIsMadeInFurnace)
			{
				this.Recipe = sRecipe;
				this.Inputs = sInputs;
				this.Outputs = sOutputs;
				this.IsMadeInFurnace = sIsMadeInFurnace;
			}

			public ModCraft GetCopy()
			{
				List<refItem> CopyInputs = new List<refItem>();
				List<refItem> CopyOutputs = new List<refItem>();

				//copy the inputs and output
				foreach (refItem ri in this.Inputs)
				{
					CopyInputs.Add(ri.GetCopy());
				}
				foreach (refItem ri in this.Outputs)
				{
					CopyOutputs.Add(ri.GetCopy());
				}

				ModCraft copy = new ModCraft(this.Recipe.GetCopy(), CopyInputs.ToArray(), CopyOutputs.ToArray(), this.IsMadeInFurnace);
				return copy;
			}
		}


		//lists of everything
		public List<ModItem> listItems = new List<ModItem>();
		public List<ModCraft> listCrafts = new List<ModCraft>();


		//find every reference of an item in the crafts and change it for an other. usefull when the user rename an item
		public void RenameItemReference(string ActualItemName, string ActualModName, string NewItemName, string NewModName)
		{
			//go through every crafts
			foreach (ModCraft mc in this.listCrafts)
			{
				////go through every items of the craft
				//begins with its recipe
				if (mc.Recipe.ItemName == ActualItemName && mc.Recipe.ModName == ActualModName)
				{
					//change it for the new name
					mc.Recipe.ItemName = NewItemName;
					mc.Recipe.ModName = NewModName;
				}

				//inputs
				foreach (refItem ri in mc.Inputs)
				{
					if (ri.ItemName == ActualItemName && ri.ModName == ActualModName)
					{
						ri.ItemName = NewItemName;
						ri.ModName = NewModName;
					}
				}

				//outputs
				foreach (refItem ri in mc.Outputs)
				{
					if (ri.ItemName == ActualItemName && ri.ModName == ActualModName)
					{
						ri.ItemName = NewItemName;
						ri.ModName = NewModName;
					}
				}
				
			}
		}



		//return null if nothing was found.
		//does not replace "-" by this.Name
		public ModItem GetModItemFromStats(string ItemName, string ModName)
		{
			foreach (ModItem mi in this.listItems)
			{
				if (mi.ItemName == ItemName && mi.ItemModName == ModName)
				{
					return mi;
				}
			}
			return null;
		}




		//items, crafts are added at THE END of the lists, no exception. these events are raised when something is added. the ui "managers" know the new thing is at the end.
		public EventHandler<ModItemEventArgs> ItemAdded;
		public EventHandler<ModCraftEventArgs> CraftAdded;
		private void Raise_ItemAdded(ModItem theitem)
		{
			if (this.ItemAdded != null)
			{
				this.ItemAdded(this, new ModItemEventArgs(theitem));
			}
		}
		private void Raise_CraftAdded(ModCraft thecraft)
		{
			if (this.CraftAdded != null)
			{
				this.CraftAdded(this, new ModCraftEventArgs(thecraft));
			}
		}
		public void AddItem(ModItem newitem)
		{
			//add it and raise the event
			this.listItems.Add(newitem);
			this.Raise_ItemAdded(newitem);
		}
		public void AddCraft(ModCraft newcraft)
		{
			//add it and raise the event
			this.listCrafts.Add(newcraft);
			this.Raise_CraftAdded(newcraft);
		}



		
		public oMod()
		{

		}
		public oMod(string filepath)
		{
			////load the archive
			octar1Archive arch = octar1ArchiveSaver.LoadArchive(filepath);


			////load the mod properties written in rootf\\modinfo
			ctar1File ctfModInfo = arch.GetFileFromPath("rootf\\modinfo");
			//create the memory stream to read the file
			MemoryStream wwwms = new MemoryStream(ctfModInfo.Content);
			//read the content of the file
			while (true)
			{
				byte IndicationByte = (byte)(wwwms.ReadByte());

				//if end of the file
				if (IndicationByte == 255) { break; }

				//0x01 is mod name
				if (IndicationByte == 1)
				{
					byte[] byteModName = MemVoid.ReadByteUntilByte(wwwms, 0);
					this.ModName = System.Text.Encoding.UTF8.GetString(byteModName);

				}


			}
			//we have finished reading the mod info file
			wwwms.Dispose();




			////load the items
			ctar1Folder FolderItem = arch.GetFolderFromPath("rootf\\Items"); //get the folder of the items
			foreach (ctar1File f in FolderItem.listSubFile)
			{
				//we create the variable to get the properties values.
				string strItemName = "";
				string strItemModName = "";
				Bitmap img = null;
				bool boolIsBelt = true;

				//create the memory stream used to read the content of the file
				MemoryStream ms = new MemoryStream(f.Content);
				//read the content
				while (true)
				{
					/*
					 * 
					 * 
					 * bytes who describe what property is comming:
					 * -0x01 item name         string
					 * -0x02 mod name          string
					 * -0x03 is belt           bool
					 * -0x04 image file name   string
					 * 
					 * -0xff end of the file, we must stop reading, nothing else is comming after.
					 * 
					 */

					
					//the first byte of each block indicate what's comming next.
					byte IndicationByte = (byte)(ms.ReadByte());

					//255 is the end of the file
					if (IndicationByte == 255) { break; }

					//item name
					if (IndicationByte == 1)
					{
						//get the data as byte[] and then convert it to a string
						byte[] byteStr = MemVoid.ReadByteUntilByte(ms, 0);
						strItemName = System.Text.Encoding.UTF8.GetString(byteStr);
						//Program.wdebug(strItemName + "  :  " + strItemName.Length.ToString());
					}

					//mod name
					if (IndicationByte == 2)
					{
						//get the data as byte[] and then convert it to a string
						byte[] byteStr = MemVoid.ReadByteUntilByte(ms, 0);
						strItemModName = System.Text.Encoding.UTF8.GetString(byteStr);
					}

					//IsBelt
					if (IndicationByte == 3)
					{
						byte byteValue = (byte)(ms.ReadByte());
						boolIsBelt = byteValue == 1;
						//read the null byte that end the bool value
						ms.ReadByte();
					}

					//image file name
					if (IndicationByte == 4)
					{
						//get the data as byte[] and then convert it to a string
						byte[] byteStr = MemVoid.ReadByteUntilByte(ms, 0);
						string strImageFileName = System.Text.Encoding.UTF8.GetString(byteStr);

						//get the file of the image
						ctar1File FileImage = arch.GetFileFromPath("rootf\\Items\\Images\\" + strImageFileName);
						//creates a memory stream for the constructor
						MemoryStream msimage = new MemoryStream(FileImage.Content);
						img = new Bitmap(msimage);
						msimage.Dispose();
						
					}


				}

				//we have finished to read the content
				ms.Dispose();


				//create the ModItem
				ModItem newmi = new ModItem(strItemName, strItemModName, img);
				newmi.IsBelt = boolIsBelt;

				this.listItems.Add(newmi);

			}


			////load the crafts
			ctar1Folder FolderCraft = arch.GetFolderFromPath("rootf\\Crafts"); //get the folder of the crafts
			foreach (ctar1File f in FolderCraft.listSubFile)
			{

				//these variables will be defined during the reading process
				refItem riRecipe = new refItem("", "");
				List<refItem> listInputs = new List<refItem>(); //inputs and outputs will be added during the reading process
				List<refItem> listOutputs = new List<refItem>();
				bool ismadeinfurnace = false;

				//create the memory stream used to read the content
				MemoryStream ms = new MemoryStream(f.Content);

				//read the file
				while (true)
				{

					/*
					 * 
					 * the beginning byte for every type of "block" :
					 * -0x01 recipe                   string string
					 * -0x02 input                    string string
					 * -0x03 output                   string string
					 * -0x04 is made in furnace       bool
					 * 
					 */

					byte IndicationByte = (byte)(ms.ReadByte());

					//if end of the file
					if (IndicationByte == 255) { break; }

					//if recipe
					if (IndicationByte == 1)
					{
						//read the recipe name
						byte[] byteItemName = MemVoid.ReadByteUntilByte(ms, 0); //read until null caracter 0
						riRecipe.ItemName = System.Text.Encoding.UTF8.GetString(byteItemName);

						//read the recipe mod name
						byte[] byteModName = MemVoid.ReadByteUntilByte(ms, 0);
						riRecipe.ModName = System.Text.Encoding.UTF8.GetString(byteModName);

					}

					//if an input
					if (IndicationByte == 2)
					{
						byte[] byteItemName = MemVoid.ReadByteUntilByte(ms, 0);
						byte[] byteModName = MemVoid.ReadByteUntilByte(ms, 0);

						refItem newinput = new refItem();
						newinput.ItemName = System.Text.Encoding.UTF8.GetString(byteItemName);
						newinput.ModName = System.Text.Encoding.UTF8.GetString(byteModName);

						//add the new input to the list
						listInputs.Add(newinput);
					}

					//if an output
					if (IndicationByte == 3)
					{
						byte[] byteItemName = MemVoid.ReadByteUntilByte(ms, 0);
						byte[] byteModName = MemVoid.ReadByteUntilByte(ms, 0);

						refItem newoutput = new refItem();
						newoutput.ItemName = System.Text.Encoding.UTF8.GetString(byteItemName);
						newoutput.ModName = System.Text.Encoding.UTF8.GetString(byteModName);

						//add the new output to the list
						listOutputs.Add(newoutput);
					}

					//if is made in furnace
					if (IndicationByte == 4)
					{
						byte byteValue = (byte)(ms.ReadByte());
						ismadeinfurnace = byteValue == 1;
						//read the null byte that end the bool
						ms.ReadByte();
					}
					
				}

				//we have finished to read the content
				ms.Dispose();


				//create the ModCraft
				ModCraft newcraft = new ModCraft(riRecipe, listInputs.ToArray(), listOutputs.ToArray(), ismadeinfurnace);
				this.listCrafts.Add(newcraft);
				
			}





		}




		//remove extra caracters éèïîêëàç... because they cause problems to ctar1
		private string RemoveAccents(string str)
		{
			string rep = "";
			int index = 0;
			string all = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
			while (index < str.Length)
			{
				string ActualCar = str.Substring(index, 1);

				int subindex = 0;
				while (subindex < all.Length)
				{
					string SubCar = all.Substring(subindex, 1);

					if (SubCar == ActualCar)
					{
						rep += ActualCar;
						break;
					}

					//next iteration
					subindex++;
				}

				//next iteration
				index++;
			}
			return rep;
		}


		private string GetRandomName(Random rnd, int length = 20)
		{
			string rep = "";
			string all = "abcdefghijklmnopqrstuvwxyz";
			for (int i = 0; i < length; i++)
			{
				int rndindex = rnd.Next(0, all.Length);
				rep += all.Substring(rndindex, 1);
			}
			return rep;
		}

		public void Save(string filepath)
		{
			Random rnd = new Random();

			//create a ctar1 archive
			octar1Archive arch = new octar1Archive();



			////write in the file rootf\\version.txt the mod save version. we are in version 1 so it's "v1" in lower case.
			//create the file
			ctar1File ctfVersionFile = arch.CreateFile("rootf\\version.txt");
			//create the memory stream
			MemoryStream wwwms = new MemoryStream();
			MemVoid.WriteStr(wwwms, "v1");
			//get the byte[] data
			ctfVersionFile.Content = wwwms.GetBuffer();
			//dispose it
			wwwms.Dispose();


			////write int the file rootf\\modinfo informatinos about the mod like the mod name
			//create the file
			ctar1File ctfModFile = arch.CreateFile("rootf\\modinfo");
			//create the memory stream
			wwwms = new MemoryStream();
			////wrte every content inside

			/* the save format for this file is the same as for every items. you can read the big comment for the items under this
			 * 
			 * every block ends with a null
			 * 
			 * 0x01 beginning of the mod name. utf8
			 * 
			 * 
			 * 0xff end of the file
			 * 
			 */

			//write the mod name
			wwwms.WriteByte(1);
			MemVoid.WriteStrUTF8(wwwms, this.ModName);
			wwwms.WriteByte(0); //end of the mod name

			//end of the file
			wwwms.WriteByte(255);

			//we've finished generating the file that contains the mod's properties
			ctfModFile.Content = wwwms.GetBuffer();
			wwwms.Dispose();



			////create the folders for saving items
			arch.CreateFolder("rootf\\Items");
			arch.CreateFolder("rootf\\Items\\Images");

			////save the items
			int index = 0; //used for the save file name of the items
			foreach (ModItem mi in this.listItems)
			{
				//the png format is used because it keeps transparency

				string FileImgName = this.RemoveAccents(mi.ItemName) + "_" + this.GetRandomName(rnd, 10) + ".png";
				////save the image of the item
				ctar1File ctfImg = arch.CreateFile("rootf\\Items\\Images\\" + FileImgName);

				//ctar1 files content are byte[].
				//here we save the item's image inside a memory stream and we get the byte[] data from that memory stream.
				MemoryStream ms = new MemoryStream();
				mi.Img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				ctfImg.Content = ms.GetBuffer();
				ms.Dispose(); //we just need it to get the bytes.




				//items must be saved in the same order as they are inside the list this.listItems and must be restored in the same order as they are inside the save, inside that virtual folder.
				//for this reason, every item file name begins with "item[6 digit number index]_".
				//it's not that ctar1 object load the file is alphabetical order, but it's just to be sure.


				////save the item itself
				string FileItemName = "item" + index.ToString().PadLeft(6, "0".ToCharArray()[0]) + "_" + this.RemoveAccents(mi.ItemName) + "_" + this.GetRandomName(rnd, 10) + ".item";
				ctar1File ctfItem = arch.CreateFile("rootf\\Items\\" + FileItemName);

				//create the memory stream (used to generate the byte[]) and write everything inside.
				ms = new MemoryStream();

				/*
				 * in this file/memory stream, we will save multiple thing.
				 * of a ModItem, we have to save the properties ItemName, ModName, IsBelt and the file name of the image previously saved above this comment.
				 * 
				 * most of the properties we have to save are strings. and the bool IsBelt won't be a problem to save.
				 * 
				 * 
				 * how it's saved and how the file must be read in the futur :
				 * 
				 * there's always the first byte that tells what property is comming. for string, you read next bytes until you encounter a null caracter 0x00. for the bool,
				 * you read the next byte and the byte after that one will be a null caracter 0x00.
				 * 
				 * 
				 * string : [byte that describe what property is comming]  then  [multiple bytes who is the content/the string/the property value in utf8]  then  [0x00] ... next thing
				 * bool : [byte that describe what property is comming]  then  [a byte 0x00 or a byte 0x01]  then  [0x00] ... next thing
				 * 
				 * for the string, we use System.Text.Encoding.UTF8 to encode and decode the strings to and from byte[]. so there's no problem for accents éèêë and every letters.
				 * for the bool, if the next byte is 0x00, it's false. if the next byte is 0x01, it's true.
				 * 
				 * 
				 * bytes who describe what property is comming:
				 * -0x01 item name         string
				 * -0x02 mod name          string
				 * -0x03 is belt           bool
				 * -0x04 image file name   string
				 * 
				 * -0xff end of the file, we must stop reading, nothing else is comming after.
				 * 
				 */
				

				//MemVoid is located inside octar1ArchiveSaver.cs at the top of the file.

				////write the item name
				ms.WriteByte(1); //beginning of the item name
				MemVoid.WriteStrUTF8(ms, mi.ItemName); //the item name
				ms.WriteByte(0); //end of the item name

				////write the mod name
				ms.WriteByte(2); //beginning of the mod name
				MemVoid.WriteStrUTF8(ms, mi.ItemModName); //the mod name
				ms.WriteByte(0); //end of the mod name

				////write the file name of the associated image
				ms.WriteByte(4); //beginning of the image file name
				MemVoid.WriteStrUTF8(ms, FileImgName); //the image file name
				ms.WriteByte(0); //end of the image file name

				////write IsBelt. it's a bool property
				ms.WriteByte(3); //beginning of the IsBelt
				//write the bool value
				if (mi.IsBelt)
				{
					//true
					ms.WriteByte(1);
				}
				else
				{
					//false
					ms.WriteByte(0);
				}
				ms.WriteByte(0); //end of the IsBelt


				ms.WriteByte(255); //0xff end of the file


				//get the byte[] data and finish
				ctfItem.Content = ms.GetBuffer();
				ms.Dispose();



				//next iteration
				index++;
			}


			////create the folders for saving crafts
			arch.CreateFolder("rootf\\Crafts");

			////save the crafts
			index = 0; //used for the save file name of the crafts
			foreach (ModCraft mc in this.listCrafts)
			{
				//create a file for the craft

				string SaveFileName = "craft_" + index.ToString().PadLeft(6, "0".ToCharArray()[0]) + ".craft";

				//create the file
				ctar1File ctfCraft = arch.CreateFile("rootf\\Crafts\\" + SaveFileName);

				//create the memory stream
				MemoryStream ms = new MemoryStream();


				//the save system and read system for crafts are identical to the save and read system for items.


				/*
				 * crafts has multiple things to save : recipe, mutiple inputs, multiple output, bool IsMadeInFurnace.
				 * 
				 * recipe, inputs, outputs are all a combinaisons of 2 strings : item name and item's mod name. instead of saving a list structure, we treat every refItem the same way. when reading a file,
				 * when encountering the beginning of a 2 string pair (recipe or input or output), it will be followed by a string, a null, a string, and finally a null.
				 * 
				 * there are 4 type of "block": a recipe, a input, a output, the IsMadeInFurnace.
				 * 
				 * they are encoded this way:
				 * 
				 * recipe : [byte that describe what property is comming]  then  [string utf8 which is the item name]  then  [0x00]  then  [string utf8 which is the item's mod name]  then  [0x00]    ... next thing
				 * input : [byte that describe what property is comming]  then  [string utf8 which is the item name]  then  [0x00]  then  [string utf8 which is the item's mod name]  then  [0x00]    ... next thing
				 * output : [byte that describe what property is comming]  then  [string utf8 which is the item name]  then  [0x00]  then  [string utf8 which is the item's mod name]  then  [0x00]    ... next thing
				 * is made in furnace : [byte that describe what property is comming]  then  [a byte 0x00 or a byte 0x01]  then  [0x00]    ... next thing
				 * 
				 * 
				 * "byte that describe what property is comming" means the one byte that begins every block.
				 * 
				 * the beginning byte for every type of "block" :
				 * -0x01 recipe                   string string
				 * -0x02 input                    string string
				 * -0x03 output                   string string
				 * -0x04 is made in furnace       bool
				 * 
				 * 
				 */
				
				//save the recipe
				ms.WriteByte(1); //the beginning
				MemVoid.WriteStrUTF8(ms, mc.Recipe.ItemName);
				ms.WriteByte(0); //end of the item name
				MemVoid.WriteStrUTF8(ms, mc.Recipe.ModName);
				ms.WriteByte(0); //end of the mod name and end of the recipe

				//save the inputs
				foreach (refItem ri in mc.Inputs)
				{
					ms.WriteByte(2); //beginning of an input
					MemVoid.WriteStrUTF8(ms, ri.ItemName);
					ms.WriteByte(0); //end of the item name
					MemVoid.WriteStrUTF8(ms, ri.ModName);
					ms.WriteByte(0); //end of the mod name and end of the input
				}

				//save the outputs
				foreach (refItem ri in mc.Outputs)
				{
					ms.WriteByte(3); //beginning of an output
					MemVoid.WriteStrUTF8(ms, ri.ItemName);
					ms.WriteByte(0); //end of the item name
					MemVoid.WriteStrUTF8(ms, ri.ModName);
					ms.WriteByte(0); //end of the mod name and end of the output
				}

				//save is made in furnace
				ms.WriteByte(4); //beginning of is made in furnace
				if (mc.IsMadeInFurnace)
				{
					//true
					ms.WriteByte(1);
				}
				else
				{
					//false
					ms.WriteByte(0);
				}
				ms.WriteByte(0); //end of is made in furnace


				//end of the file
				ms.WriteByte(255);

				//put the content inside the file object
				ctfCraft.Content = ms.GetBuffer();
				ms.Dispose(); //we don't need it anymore


				//next iteration
				index++;
			}



			octar1ArchiveSaver.SaveArchive(arch, filepath);


		}



	}
}
