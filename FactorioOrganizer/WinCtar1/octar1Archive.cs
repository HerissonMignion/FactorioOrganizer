using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WinCtar1
{

	//create an issue if you need comment translation

	public class ctar1Folder
	{
		private ctar1Folder zzzParent;
		private bool zzzHasParent = false;
		public bool HasParent { get { return this.zzzHasParent; } }
		public ctar1Folder Parent
		{
			get
			{
				return this.zzzParent;
			}
			set
			{
				if (!this.zzzHasParent)
				{
					this.zzzHasParent = true;
					this.zzzParent = value;
				}
			}
		}

		public string Name = "";
		public string ParentPath
		{
			get
			{
				string rep = "";
				if (this.HasParent)
				{
					rep = this.Parent.Path;
				}
				else
				{
					rep = this.Name;
				}
				return rep;
			}
		}
		//le path inclut le nom du dossier et SANS le \ a la fin
		public string Path
		{
			get
			{
				string rep = "";
				if (this.HasParent)
				{
					rep = this.ParentPath + "\\" + this.Name;
				}
				else
				{
					rep = this.Name;
				}
				return rep;
			}
		}

		public List<ctar1Folder> listSubFolder = new List<ctar1Folder>();
		public List<ctar1File> listSubFile = new List<ctar1File>();
		public ctar1Folder GetSubFolderFromName(string foldername)
		{
			ctar1Folder rep = null;
			foreach (ctar1Folder actualfolder in this.listSubFolder)
			{
				if (actualfolder.Name == foldername)
				{
					rep = actualfolder;
					break;
				}
			}
			return rep;
		}
		public ctar1File GetSubFileFromName(string filename)
		{
			ctar1File rep = null;
			foreach (ctar1File actualfile in this.listSubFile)
			{
				if (actualfile.Name == filename)
				{
					rep = actualfile;
					break;
				}
			}
			return rep;
		}

		public ctar1Folder(ctar1Folder StartParent)
		{
			this.zzzHasParent = true;
			this.zzzParent = StartParent;
			StartParent.listSubFolder.Add(this);
		}
		//cette void new() est pour si le dossier est la racine du fichier ctar1
		public ctar1Folder()
		{

		}
		
	}
	public class ctar1File
	{
		private ctar1Folder zzzParent;
		public ctar1Folder Parent { get { return this.zzzParent; } }

		public string Name = "";
		public string ParentPath
		{
			get
			{
				string rep = "";
				rep = this.Parent.Path;
				return rep;
			}
		}
		//le path inclut le nom du ficher et SANS le \ a la fin
		public string Path
		{
			get
			{
				string rep = "";
				rep = this.ParentPath + "\\" + this.Name;
				return rep;
			}
		}

		public byte[] Content;
		public int Length { get { return this.Content.Length; } }
		public long LongLength { get { return this.Content.LongLength; } }

		public ctar1File(ctar1Folder StartParent)
		{
			this.zzzParent = StartParent;
			StartParent.listSubFile.Add(this);
			
			this.Content = new byte[] { };
		}
	}

	public class octar1Archive
	{

		public ctar1Folder rootf;

		//fonction de manipulation des fichier/dossier
		public ctar1Folder GetFolderFromPath(string folderpath)
		{
			ctar1Folder rep = null;
			if (folderpath == this.rootf.Path) { return this.rootf; }
			
			//i didn't wanted to use recursion, for fun

			//cree la liste de tout les dossier parent
			List<string> AllParentFolder = new List<string>();
			string ActualParentFolderPath = folderpath;
			while (true)
			{
				//monte d'un niveau dans la liste des dossier
				ActualParentFolderPath = System.IO.Path.GetDirectoryName(ActualParentFolderPath);
				if (ActualParentFolderPath.Length > 0)
				{
					string ActualFolderName = System.IO.Path.GetFileName(ActualParentFolderPath);
					AllParentFolder.Insert(0, ActualFolderName);
				}
				else
				{
					break;
				}
			}

			////parcour tout les dossier de la liste pour retrouver l'object desirer

			if (AllParentFolder[0] == this.rootf.Name)
			{
				ctar1Folder ActualFolder = this.rootf; //a la fin du processus, ceci est le dossier parent du dossier demander
				AllParentFolder.RemoveAt(0); //delete rootf

				while (AllParentFolder.Count > 0)
				{
					ctar1Folder NextFolder = ActualFolder.GetSubFolderFromName(AllParentFolder[0]);
					AllParentFolder.RemoveAt(0);

					//si le dossier n'exist pas, il retourne null car c'est impossible qu'il puisse exister plus tard
					if (NextFolder == null)
					{
						return null;
					}

					//next iteration
					ActualFolder = NextFolder;
				}

				rep = ActualFolder.GetSubFolderFromName(System.IO.Path.GetFileName(folderpath));
				
			}
			
			return rep;
		}
		public ctar1File GetFileFromPath(string filepath)
		{
			ctar1File rep = null;
			string ParentFolderPath = System.IO.Path.GetDirectoryName(filepath);
			ctar1Folder ParentFolder = this.GetFolderFromPath(ParentFolderPath);
			if (ParentFolder == null) { return null; }
			rep = ParentFolder.GetSubFileFromName(System.IO.Path.GetFileName(filepath));
			return rep;
		}
		public bool IsFolderExist(string path)
		{
			ctar1Folder thef = this.GetFolderFromPath(path);
			if (thef != null) { return true; } else { return false; }
		}
		public bool IsFileExist(string path)
		{
			ctar1File thef = this.GetFileFromPath(path);
			if (thef != null) { return true; } else { return false; }
		}

		public void CreateFolder(string path)
		{
			if (IsFolderExist(path))
			{
				throw new Exception("Folder already exist.");
			}
			else
			{
				string ParentPath = Path.GetDirectoryName(path);
				bool IsParentExist = this.IsFolderExist(ParentPath);
				if (!IsParentExist) { this.zzzCreateFolderNoCheck(ParentPath); }

				ctar1Folder ParentFolder = this.GetFolderFromPath(ParentPath);
				ctar1Folder NewFolder = new ctar1Folder(ParentFolder);
				NewFolder.Name = Path.GetFileName(path);
				
			}
		}
		private void zzzCreateFolderNoCheck(string path)
		{
			string ParentPath = Path.GetDirectoryName(path);
			bool IsParentExist = this.IsFolderExist(ParentPath);
			if (!IsParentExist) { this.zzzCreateFolderNoCheck(ParentPath); }

			ctar1Folder ParentFolder = this.GetFolderFromPath(ParentPath);
			ctar1Folder NewFolder = new ctar1Folder(ParentFolder);
			NewFolder.Name = Path.GetFileName(path);

		}

		public ctar1File CreateFile(string path)
		{
			string ParentPath = Path.GetDirectoryName(path);
			if (!this.IsFolderExist(ParentPath))
			{
				this.CreateFolder(ParentPath);
			}
			ctar1Folder ParentFolder = this.GetFolderFromPath(ParentPath);
			ctar1File NewFile = new ctar1File(ParentFolder);
			NewFile.Name = Path.GetFileName(path);
			return NewFile;
		}



		//void new()
		public octar1Archive()
		{
			this.rootf = new ctar1Folder();
			this.rootf.Name = "rootf"; // root
			
		}


		public static ctar1File LoadRealFileFromPath(string filepath, ctar1Folder ParentFolder)
		{
			ctar1File newfile = new ctar1File(ParentFolder);
			newfile.Name = System.IO.Path.GetFileName(filepath);

			//load le contenue du fichier
			byte[] content = System.IO.File.ReadAllBytes(filepath);
			newfile.Content = content;
			
			return newfile;
		}
		public static ctar1Folder LoadRealFolderFromPath(string folderpath, ctar1Folder ParentFolder)
		{
			ctar1Folder newfolder = new ctar1Folder(ParentFolder);
			newfolder.Name = System.IO.Path.GetFileName(folderpath);

			//load les sous dossier
			string[] AllSubFolder = System.IO.Directory.GetDirectories(folderpath);
			foreach (string subfolderpath in AllSubFolder)
			{
				octar1Archive.LoadRealFolderFromPath(subfolderpath, newfolder);
			}
			
			//load les sous fichier
			string[] AllSubFile = System.IO.Directory.GetFiles(folderpath);
			foreach (string filepath in AllSubFile)
			{
				octar1Archive.LoadRealFileFromPath(filepath, newfolder);
			}
			
			return newfolder;
		}

		public static void SaveFileAtRealPath(ctar1File thefile, string filesavepath)
		{
			System.IO.File.WriteAllBytes(filesavepath, thefile.Content);
		}
		public static void SaveFolderAtRealPath(ctar1Folder thefolder, string foldersavepath)
		{
			//si le dossier existe deja, il le delete puis en cree un nouveau pour que le contenue soit vide
			if (System.IO.Directory.Exists(foldersavepath))
			{
				System.IO.Directory.Delete(foldersavepath);
			}
			System.IO.Directory.CreateDirectory(foldersavepath);

			//save tout ses sous dossier
			foreach (ctar1Folder subfolder in thefolder.listSubFolder)
			{
				string subfoldersavepath = foldersavepath + "\\" + subfolder.Name;
				octar1Archive.SaveFolderAtRealPath(subfolder, subfoldersavepath);
			}
			
			//save tout ses sous fichier
			foreach (ctar1File subfile in thefolder.listSubFile)
			{
				string filesavepath = foldersavepath + "\\" + subfile.Name;
				octar1Archive.SaveFileAtRealPath(subfile, filesavepath);
			}
			

		}


	}
}
