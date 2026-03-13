using BeATrueCowboy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BeATrueCowboy
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		void app_Startup( object sender, StartupEventArgs e )
		{
			MainWindow window = new MainWindow( );
			window.Show( );
		}
	} // App
} // BeATrueCowboy
