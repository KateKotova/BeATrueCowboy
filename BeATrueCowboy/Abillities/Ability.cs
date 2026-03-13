using BeATrueCowboy.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BeATrueCowboy.Abillities
{
	public class Ability
	{
		public AbilityType Type { get; set; }
		public SideToggleControl Toggle { get; set; }
		public Label ValueLabel { get; set; }
	} // Ability
} // BeATrueCowboy.Abillities
