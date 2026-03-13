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

namespace BeATrueCowboy.Controls
{
	/// <summary>
	/// Interaction logic for LowerBandToggleControl.xaml
	/// </summary>
	public partial class LowerBandToggleControl : UserControl
	{
		public static DependencyProperty ImageSourceProperty;
		public static DependencyProperty LabelContentProperty;
		public static DependencyProperty LabelIsTightProperty;
		public static DependencyProperty IsCheckedProperty;
		public static DependencyProperty AssociatedUIElementProperty;
		public static DependencyProperty GroupIndexProperty;

		public const String HIT_AREA_TIGHT_LABEL_STYLE_NAME
			= "HitAreaTightLabelStyle";
		public const String HIT_AREA_NORMAL_LABEL_STYLE_NAME
			= "HitAreaNormalLabelStyle";

		public static readonly RoutedEvent CheckedEvent;

		public LowerBandToggleControl( )
		{
			InitializeComponent( );
			this.ToggleObject.IsCheckedChanged
				+= this.OnToggleObjectIsChecked;
		} // LowerBandToggleControl

		private void OnToggleObjectIsChecked(
			Object parSender, EventArgs parArgs )
		{
			this.IsChecked = this.ToggleObject.IsChecked;
		} // OnToggleObjectIsChecked

		static LowerBandToggleControl( )
		{
			// Registration of routed event.
			CheckedEvent = EventManager.RegisterRoutedEvent(
				nameof( Checked ),
				RoutingStrategy.Bubble,
				typeof( RoutedEventHandler ),
				typeof( LowerBandToggleControl )
			);
			// Registration of propeties dependencies.
			ImageSourceProperty = DependencyProperty.Register(
				nameof( ImageSource ),
				typeof( ImageSource ),
				typeof( LowerBandToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnImageSourceChanged )
				)
			);
			LabelContentProperty = DependencyProperty.Register(
				nameof( LabelContent ),
				typeof( Object ),
				typeof( LowerBandToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnLabelContentChanged )
				)
			);
			LabelIsTightProperty = DependencyProperty.Register(
				nameof( LabelIsTight ),
				typeof( Boolean ),
				typeof( LowerBandToggleControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnLabelIsTightChanged )
				)
			);

			IsCheckedProperty = DependencyProperty.Register(
				nameof( IsChecked ),
				typeof( Boolean ),
				typeof( LowerBandToggleControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnIsCheckedChanged )
				)
			);
			AssociatedUIElementProperty = DependencyProperty.Register(
				nameof( AssociatedUIElement ),
				typeof( UIElement ),
				typeof( LowerBandToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnAssociatedUIElementChanged )
				)
			);
			GroupIndexProperty = DependencyProperty.Register(
				nameof( GroupIndex ),
				typeof( Int32 ),
				typeof( LowerBandToggleControl ),
				new FrameworkPropertyMetadata(
					SimpleToggleControl.UNDEFINED_GROUP_INDEX,
					new PropertyChangedCallback( OnGroupIndexChanged )
				)
			);
		} // LowerBandToggleControl

		private static void OnImageSourceChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			ImageSource newImageSource = ( ImageSource ) parArgs.NewValue;
			LowerBandToggleControl control
				= ( LowerBandToggleControl ) parSender;
			control.ImageSource = newImageSource;
			control.HitArea.ImageSource = newImageSource;
		} // OnImageSourceChanged

		private static void OnLabelContentChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Object newLabelContent = ( Object ) parArgs.NewValue;
			LowerBandToggleControl control
				= ( LowerBandToggleControl ) parSender;
			control.LabelContent = newLabelContent;
			control.HitArea.LabelContent = newLabelContent;
		} // OnLabelContentChanged

		private static void OnLabelIsTightChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newLabelIsTight = ( Boolean ) parArgs.NewValue;
			LowerBandToggleControl control
				= ( LowerBandToggleControl ) parSender;
			control.LabelIsTight = newLabelIsTight;

			String labelStyleName = control.LabelIsTight
				? HIT_AREA_TIGHT_LABEL_STYLE_NAME
				: HIT_AREA_NORMAL_LABEL_STYLE_NAME;
			control.HitArea.LabelStyle = ( Style )
				control.Resources[ labelStyleName ];
		} // OnLabelIsTightChanged

		private static void OnIsCheckedChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newIsChecked = ( Boolean ) parArgs.NewValue;
			LowerBandToggleControl control
				= ( LowerBandToggleControl ) parSender;
			control.IsChecked = newIsChecked;
			control.ToggleObject.IsChecked = newIsChecked;
		} // OnIsCheckedChanged

		private static void OnAssociatedUIElementChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			UIElement newAssociatedUIElement = ( UIElement ) parArgs.NewValue;
			LowerBandToggleControl control
				= ( LowerBandToggleControl ) parSender;
			control.AssociatedUIElement = newAssociatedUIElement;
			control.ToggleObject.AssociatedUIElement = newAssociatedUIElement;
		} // OnAssociatedUIElementChanged

		private static void OnGroupIndexChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Int32 newGroupIndex = ( Int32 ) parArgs.NewValue;
			LowerBandToggleControl control
				= ( LowerBandToggleControl ) parSender;
			control.GroupIndex = newGroupIndex;
			control.ToggleObject.GroupIndex = newGroupIndex;
		} // OnGroupIndexChanged

		public event RoutedEventHandler Checked
		{
			add { AddHandler( CheckedEvent, value ); }
			remove { RemoveHandler( CheckedEvent, value ); }
		} // Checked

		private void RaiseCheckedEvent( )
		{
			this.RaiseEvent( new RoutedEventArgs
				( LowerBandToggleControl.CheckedEvent ) );
		} // RaiseCheckedEvent

		private void OnToggleObjectChecked( Object parSender,
			RoutedEventArgs parArgs )
		{
			this.RaiseCheckedEvent( );
		} // OnToggleObjectChecked

		public ImageSource ImageSource
		{
			get
			{
				return ( ImageSource ) GetValue( ImageSourceProperty );
			}
			set
			{
				SetValue( ImageSourceProperty, value );
			}
		} // ImageSource

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

		public Boolean LabelIsTight
		{
			get
			{
				return ( Boolean ) GetValue( LabelIsTightProperty );
			}
			set
			{
				SetValue( LabelIsTightProperty, value );
			}
		} // LabelIsTight

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

		public UIElement AssociatedUIElement
		{
			get
			{
				return ( UIElement ) GetValue( AssociatedUIElementProperty );
			}
			set
			{
				SetValue( AssociatedUIElementProperty, value );
			}
		} // IsChAssociatedUIElementecked

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
	} // LowerBandToggleControl
} // BeATrueCowboy.Controls
