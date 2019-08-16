using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioOrganizer
{
	
	public class SaveReader
	{
		public List<string> theL;

		public int ActualIndex = 0;
		public string ReadLine()
		{
			string rep = this.theL[this.ActualIndex];
			this.ActualIndex++;
			return rep;
		}

		public bool EndOfStream { get { return this.ActualIndex > this.theL.Count - 1; } }
		

		public SaveReader(List<string> StartL)
		{
			this.theL = StartL;
		}
	}
}
