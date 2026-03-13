using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
	/// <summary>
	/// Interaction logic for AddServiceAccountSimpleToggle.xaml
	/// </summary>
	public partial class AddServiceAccountSimpleToggle : UserControl
	{
		public static DependencyProperty IsCheckedDP;

		public static readonly RoutedEvent CheckedEvent;

		public AddServiceAccountSimpleToggle( )
		{
			InitializeComponent( );
		} // AddServiceAccountSimpleToggle

		static AddServiceAccountSimpleToggle( )
		{
			// Registration of routed event.
			CheckedEvent = EventManager.RegisterRoutedEvent(
				nameof( Checked ),
				RoutingStrategy.Bubble,
				typeof( RoutedEventHandler ),
				typeof( AddServiceAccountSimpleToggle )
			);
			// Registration of propeties dependencies.
			IsCheckedDP = DependencyProperty.Register(
				nameof( IsChecked ),
				typeof( Boolean ),
				typeof( AddServiceAccountSimpleToggle ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnIsCheckedChanged )
				)
			);
		} // AddServiceAccountSimpleToggle

		private static void OnIsCheckedChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newIsChecked = ( Boolean ) parArgs.NewValue;
			AddServiceAccountSimpleToggle control
				= ( AddServiceAccountSimpleToggle ) parSender;
			control.IsChecked = newIsChecked;
			control.AddServiceAccountToggle.IsChecked = newIsChecked;
		} // OnIsCheckedChanged

		public event RoutedEventHandler Checked
		{
			add { AddHandler( CheckedEvent, value ); }
			remove { RemoveHandler( CheckedEvent, value ); }
		} // Checked

		private void RaiseCheckedEvent( )
		{
			this.RaiseEvent( new RoutedEventArgs
				( AddServiceAccountSimpleToggle.CheckedEvent ) );
		} // RaiseCheckedEvent

		private void OnAddServiceAccountToggleChecked( Object parSender,
			RoutedEventArgs parArgs )
		{
			this.IsChecked = true;
			this.RaiseCheckedEvent( );
		} // OnAddServiceAccountToggleChecked

		public Boolean IsChecked
		{
			get
			{
				return ( Boolean ) GetValue( IsCheckedDP );
			}
			set
			{
				SetValue( IsCheckedDP, value );
			}
		} // IsChecked
	} // AddServiceAccountSimpleToggle
} // Controls
