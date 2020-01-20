using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WinCtar1
{

	//create an issue if you need comment translation


	public static class MemVoid
	{

		public static void WriteStr(MemoryStream ms, string str)
		{
			byte[] bytestr = System.Text.Encoding.ASCII.GetBytes(str.ToCharArray());
			ms.Write(bytestr, 0, bytestr.Length);
		}

		public static void WriteStrUTF8(MemoryStream ms, string str)
		{
			byte[] bytestr = System.Text.Encoding.UTF8.GetBytes(str.ToCharArray());
			ms.Write(bytestr, 0, bytestr.Length);
		}

		//read the bytes until it encounter the specified byte. the specified byte is not part of the array returned
		public static byte[] ReadByteUntilByte(MemoryStream ms, byte b)
		{
			List<byte> lista = new List<byte>();
			while (true)
			{
				byte newb = (byte)(ms.ReadByte());
				if (newb == b) { break; }
				else
				{
					lista.Add(newb);
				}
			}
			return lista.ToArray();
		}

	}


	public static class octar1ArchiveSaver
	{
		/*         SYSTEME D'ENCODAGE DES FICHIER .ctar1
		 * tout est concu pour l'ouverture du fichier. un octet specifit dans quel type d'object (fichier ou dossier ou autre) le programme va reconstituer. une fois qu'il
		 * aura terminer, l'octet d'apres va indiquer quel type d'object est le suivant etc...
		 * 
		 * ------------[ les octect indicateur ]---------------
		 * 
		 * 0xff : fin du fichier
		 * 
		 * 0x01 : dossier
		 * 0x02 : fichier
		 * 
		 * 
		 * 
		 * 
		 * ===================================================================================================================================
		 * ----------------[ les dossier ]------------------------
		 * les dossier sont enregistrer comme suit:
		 * a) chemain d'acces du dossier
		 * b) 0x00 : indique la fin du dossier
		 * 
		 * 
		 * ----------------[ les fichier ]------------------------
		 * les fichier commence par le caractere 0x02
		 * a) chemain d'acces du fichier
		 * b) 0x00
		 * c) taille du fichier, en octet, où chaque chiffre decimale occupe un octet
		 * d) 0x00
		 * e) [contenu du fichier]
		 * 
		 * 
		 * 
		 */



		public static void SaveArchive(octar1Archive TheArchive, string SavePath)
		{
			System.IO.MemoryStream ms = new System.IO.MemoryStream();

			List<string> AllFolderPath = octar1ArchiveSaver.GetAllSubFolderPath(TheArchive.rootf);
			List<string> AllFilePath = octar1ArchiveSaver.GetAllSubFilePath(TheArchive.rootf);
			//Program.wdebug("================================================");
			//Program.wdebug("----- FOLDER");
			//foreach (string s in AllFolderPath)
			//{
			//	Program.wdebug(s);
			//}
			//Program.wdebug("----- FILE");
			//foreach (string s in AllFilePath)
			//{
			//	Program.wdebug(s);
			//}


			//ecrit l'entête du fichier
			foreach (string ActualFolderPath in AllFolderPath)
			{
				ms.WriteByte(1); //les dossier commence par 0x01
				MemVoid.WriteStr(ms, ActualFolderPath);
				ms.WriteByte(0); //indique la fin du dossier
				
			}


			//ecrit les fichier
			foreach (string ActualFilePath in AllFilePath)
			{
				ctar1File ActualFile = TheArchive.GetFileFromPath(ActualFilePath);

				ms.WriteByte(2); //les fichier commence par 0x02
				MemVoid.WriteStr(ms, ActualFilePath);
				ms.WriteByte(0); //separation
				MemVoid.WriteStr(ms, ActualFile.Length.ToString());
				ms.WriteByte(0); //separation
				ms.Write(ActualFile.Content, 0, ActualFile.Content.Length);

			}



			//fin
			ms.WriteByte(255);



			byte[] bytecontent = ms.ToArray();
			System.IO.File.WriteAllBytes(SavePath, bytecontent);
			ms.Dispose();

		}
		public static octar1Archive LoadArchive(string RealFilePath)
		{
			octar1Archive NewArchive = new octar1Archive();

			if (System.IO.File.Exists(RealFilePath))
			{
				byte[] bytecontent = System.IO.File.ReadAllBytes(RealFilePath);
				MemoryStream ms = new MemoryStream(bytecontent);
				ms.Position = 0;


				bool CanExit = false;
				while (!CanExit && ms.Position <= ms.Length - 1)
				{
					int bibi = ms.ReadByte();

					//si c'est la fin
					if (bibi == 255)
					{
						CanExit = true;
						break;
					}

					//si c'est un dossier. les dossier commence par 0x01
					if (bibi == 1)
					{
						List<byte> byteActualFolderPath = new List<byte>();
						bool pCanExit = false;
						while (!pCanExit)
						{
							byte b = (byte)(ms.ReadByte());
							//check s'il a ateint la fin du dossier. les dossier finissent par 0x00
							if (b == 0)
							{
								pCanExit = true;
								break;
							}
							byteActualFolderPath.Add(b);
						}

						string NewFolderPath = System.Text.Encoding.ASCII.GetString(byteActualFolderPath.ToArray());
						NewArchive.CreateFolder(NewFolderPath);

					}

					//si c'est un fichier. les fichier comment par 0x02
					if (bibi == 2)
					{
						//optien le chemain d'acces du fichier
						List<byte> byteActualFilePath = new List<byte>();
						bool pCanExit = false;
						while (!pCanExit)
						{
							byte b = (byte)(ms.ReadByte());
							if (b == 0)
							{
								pCanExit = true;
								break;
							}
							byteActualFilePath.Add(b);
						}
						string NewFilePath = System.Text.Encoding.ASCII.GetString(byteActualFilePath.ToArray());
						
						// /!\ /!\ /!\ recyclage de variable
						//optien la longueur du fichier
						while (byteActualFilePath.Count > 0) { byteActualFilePath.RemoveAt(0); }
						pCanExit = false;
						while (!pCanExit)
						{
							byte b = (byte)(ms.ReadByte());
							if (b == 0)
							{
								pCanExit = true;
								break;
							}
							byteActualFilePath.Add(b);
						}
						string strFileLength = System.Text.Encoding.ASCII.GetString(byteActualFilePath.ToArray());
						int FileLength = Convert.ToInt32(strFileLength);

						

						ctar1File NewFile = NewArchive.CreateFile(NewFilePath);
						NewFile.Content = new byte[FileLength];
						long mspos = ms.Position;
						ms.Read(NewFile.Content, 0, FileLength);
						ms.Position = mspos + (long)FileLength; // -1



					}






				}





				//fin
				ms.Dispose();

			}

			return NewArchive;
		}



		//retourne une liste contenant le chemain d'acces de tout les sous-dossier
		//cette liste EXCLUT le dossier entrer en parametre
		public static List<string> GetAllSubFolderPath(ctar1Folder TheFolder)
		{
			List<string> rep = new List<string>();
			foreach (ctar1Folder actualfolder in TheFolder.listSubFolder)
			{
				rep.Add(actualfolder.Path);
				//ajoute les dossier du dossier actuel
				List<string> subfolderpaths = octar1ArchiveSaver.GetAllSubFolderPath(actualfolder);
				rep.AddRange(subfolderpaths);
			}
			return rep;
		}

		//retourne une liste contenant le chemain d'acces de tout les sous-ficheir
		public static List<string> GetAllSubFilePath(ctar1Folder TheFolder)
		{
			List<string> rep = new List<string>();
			//ajoute les fichier
			foreach (ctar1File actualfile in TheFolder.listSubFile)
			{
				rep.Add(actualfile.Path);
			}
			//ajoute les fichier des sous-dossier
			foreach (ctar1Folder actualfolder in TheFolder.listSubFolder)
			{
				List<string> subfilepaths = octar1ArchiveSaver.GetAllSubFilePath(actualfolder);
				rep.AddRange(subfilepaths);
			}
			return rep;
		}



	}
}
