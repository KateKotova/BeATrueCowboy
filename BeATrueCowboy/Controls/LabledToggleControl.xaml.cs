using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeATrueCowboy.Controls
{
	/// <summary>
	/// Interaction logic for LabledToggleControl.xaml
	/// </summary>
	public partial class LabledToggleControl : UserControl
	{
		public static DependencyProperty IsCheckedProperty;
		public static DependencyProperty LabelContentProperty;
		public static DependencyProperty ActiveImageSourceProperty;
		public static DependencyProperty InactiveImageSourceProperty;
		public static DependencyProperty GroupIndexProperty;

		public static readonly RoutedEvent CheckedEvent;

		static LabledToggleControl( )
		{
			// Registration of routed event.
			CheckedEvent = EventManager.RegisterRoutedEvent(
				nameof( Checked ),
				RoutingStrategy.Bubble,
				typeof( RoutedEventHandler ),
				typeof( LabledToggleControl )
			);
			// Registration of propeties dependencies.
			IsCheckedProperty = DependencyProperty.Register(
				nameof( IsChecked ),
				typeof( Boolean ),
				typeof( LabledToggleControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnIsCheckedChanged )
				)
			);
			LabelContentProperty = DependencyProperty.Register(
				nameof( LabelContent ),
				typeof( Object ),
				typeof( LabledToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnLabelContentChanged )
				)
			);
			ActiveImageSourceProperty = DependencyProperty.Register(
				nameof( ActiveImageSource ),
				typeof( ImageSource ),
				typeof( LabledToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnActiveImageSourceChanged )
				)
			);
			InactiveImageSourceProperty = DependencyProperty.Register(
				nameof( InactiveImageSource ),
				typeof( ImageSource ),
				typeof( LabledToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnInactiveImageSourceChanged )
				)
			);
			GroupIndexProperty = DependencyProperty.Register(
				nameof( GroupIndex ),
				typeof( Int32 ),
				typeof( LabledToggleControl ),
				new FrameworkPropertyMetadata(
					SimpleToggleControl.UNDEFINED_GROUP_INDEX,
					new PropertyChangedCallback( OnGroupIndexChanged )
				)
			);
		} // LabledToggleControl

		public LabledToggleControl( )
		{
			InitializeComponent( );
			this.ToggleObject.GroupIndexChanged += this.OnGroupIndexChanged;
		} // LabledToggleControl

		private static void OnIsCheckedChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newIsChecked = ( Boolean ) parArgs.NewValue;
			LabledToggleControl control
				= ( LabledToggleControl ) parSender;
			control.IsChecked = newIsChecked;
			control.ToggleObject.IsChecked = newIsChecked;
		} // OnIsCheckedChanged

		private static void OnLabelContentChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Object newLabelContent = ( Object ) parArgs.NewValue;
			LabledToggleControl control
				= ( LabledToggleControl ) parSender;
			control.LabelContent = newLabelContent;
			control.LabelObject.Content = newLabelContent;
		} // OnLabelContentChanged

		private static void OnActiveImageSourceChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			ImageSource newActiveImageSource = ( ImageSource ) parArgs.NewValue;
			LabledToggleControl control = ( LabledToggleControl ) parSender;
			control.ActiveImageSource = newActiveImageSource;
			control.ActiveToggle.Source = newActiveImageSource;
		} // OnActiveImageSourceChanged

		private static void OnInactiveImageSourceChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			ImageSource newInactiveImageSource = ( ImageSource ) parArgs.NewValue;
			LabledToggleControl control = ( LabledToggleControl ) parSender;
			control.InactiveImageSource = newInactiveImageSource;
			control.InactiveToggle.Source = newInactiveImageSource;
		} // OnInactiveImageSourceChanged

		private static void OnGroupIndexChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Int32 newGroupIndex = ( Int32 ) parArgs.NewValue;
			LabledToggleControl control = ( LabledToggleControl ) parSender;
			control.ToggleObject.GroupIndex = newGroupIndex;
			control.GroupIndex = control.ToggleObject.GroupIndex;
		} // OnGroupIndexChanged

		public event RoutedEventHandler Checked
		{
			add { AddHandler( CheckedEvent, value ); }
			remove { RemoveHandler( CheckedEvent, value ); }
		} // Checked

		private void RaiseCheckedEvent( )
		{
			this.RaiseEvent( new RoutedEventArgs
				( LabledToggleControl.CheckedEvent ) );
		} // RaiseCheckedEvent

		private void OnToggleObjectChecked( Object parSender,
			RoutedEventArgs parArgs )
		{
			this.IsChecked = true;
			this.RaiseCheckedEvent( );
		} // OnToggleObjectChecked

		private void OnGroupIndexChanged(
			Object parSender, EventArgs parArgs )
		{
			this.GroupIndex = this.ToggleObject.GroupIndex;
		} // OnGroupIndexChanged

		public Boolean IsChecked
		{
			get
			{
				return ( Boolean ) GetValue( IsCheckedProperty );
			}
			set
			{
				SetValue( IsCheckedProperty, value );
			}
		} // IsChecked

		public Object LabelContent
		{
			get
			{
				return ( Object ) GetValue( LabelContentProperty );
			}
			set
			{
				SetValue( LabelContentProperty, value );
			}
		} // LabelContent

		public ImageSource ActiveImageSource
		{
			get
			{
				return ( ImageSource ) GetValue( ActiveImageSourceProperty );
			}
			set
			{
				SetValue( ActiveImageSourceProperty, value );
			}
		} // ActiveImageSource

		public ImageSource InactiveImageSource
		{
			get
			{
				return ( ImageSource ) GetValue( InactiveImageSourceProperty );
			}
			set
			{
				SetValue( InactiveImageSourceProperty, value );
			}
		} // InactiveImageSource

		public Int32 GroupIndex
		{
			get
			{
				return ( Int32 ) GetValue( GroupIndexProperty );
			}
			set
			{
				SetValue( GroupIndexProperty, value );
			}
		} // GroupIndex
	} // LabledToggleControl
} // BeATrueCowboy.Controls