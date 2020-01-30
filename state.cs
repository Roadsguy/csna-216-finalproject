using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
	public class state
	{
		private string nameString;
		private string abbreviationString;
		/// <summary>
		/// used to display the states in 3 different forms
		/// Name ("Pennsylvania"), Abbreviation ("PA") , 
		/// name and abbreviation ("Pennsylvania (PA) ")
		/// </summary>
		/// <param name="nameArg"></param>
		/// <param name="abbreviationArg"></param>
		public state(ref string nameArg, ref string abbreviationArg)
		{
			abbreviationString = abbreviationArg;
			nameString = nameArg;
		}

		public string Name
		{
			get
			{
				return nameString;
			}
		}

		public string Abbreviation
		{
			get
			{
				return abbreviationString;
			}
		}

		public string FullAndAbbrev
		{
			get
			{
				return nameString + " (" + abbreviationString + ")";
			}
		}
	}
}
