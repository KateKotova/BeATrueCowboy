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
	/// Interaction logic for LowerBandToggleControl.xaml
	/// </summary>
	public partial class LowerBandToggleControl : UserControl
	{
		public static DependencyProperty ImgSourceDP;
		public static DependencyProperty LblContentDP;
		public static DependencyProperty LblIsTightDP;
		public static DependencyProperty IsCheckedDP;
		public static DependencyProperty AssociatedUIElemDP;
		public static DependencyProperty GroupIndexDP;

		public const String HIT_AREA_TIGHT_LBL_STYLE_NAME
			= "HitAreaTightLabelStyle";
		public const String HIT_AREA_LBL_STYLE_NAME
			= "HitAreaLabelStyle";

		public static readonly RoutedEvent CheckedEvent;

		public LowerBandToggleControl( )
		{
			InitializeComponent( );
			this.LowerBandToggle.IsCheckedChanged
				+= this.OnLowerBandToggleIsChecked;
		} // LowerBandToggleControl

		private void OnLowerBandToggleIsChecked(
			Object parSender, EventArgs parArgs )
		{
			this.IsChecked = this.LowerBandToggle.IsChecked;
		} // OnLowerBandToggleIsChecked

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
			ImgSourceDP = DependencyProperty.Register(
				nameof( ImageSource ),
				typeof( ImageSource ),
				typeof( LowerBandToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnImgSourceChanged )
				)
			);
			LblContentDP = DependencyProperty.Register(
				nameof( LabelContent ),
				typeof( Object ),
				typeof( LowerBandToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnLblContentChanged )
				)
			);
			LblIsTightDP = DependencyProperty.Register(
				nameof( LabelIsTight ),
				typeof( Boolean ),
				typeof( LowerBandToggleControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnLabelIsTightChanged )
				)
			);

			IsCheckedDP = DependencyProperty.Register(
				nameof( IsChecked ),
				typeof( Boolean ),
				typeof( LowerBandToggleControl ),
				new FrameworkPropertyMetadata(
					false,
					new PropertyChangedCallback( OnIsCheckedChanged )
				)
			);
			AssociatedUIElemDP = DependencyProperty.Register(
				nameof( AssociatedUIElement ),
				typeof( UIElement ),
				typeof( LowerBandToggleControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnAssociatedUIElemChanged )
				)
			);
			GroupIndexDP = DependencyProperty.Register(
				nameof( GroupIndex ),
				typeof( Int32 ),
				typeof( LowerBandToggleControl ),
				new FrameworkPropertyMetadata(
					SimpleToggle.UNDEFINED_GROUP_INDEX,
					new PropertyChangedCallback( OnGroupIndexChanged )
				)
			);
		} // LowerBandToggleControl

		private static void OnImgSourceChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			ImageSource newImageSource = ( ImageSource ) parArgs.NewValue;
			LowerBandToggleControl control
				= ( LowerBandToggleControl ) parSender;
			control.ImageSource = newImageSource;
			control.HitArea.ImageSource = newImageSource;
		} // OnImgSourceChanged

		private static void OnLblContentChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Object newLabelContent = ( Object ) parArgs.NewValue;
			LowerBandToggleControl control
				= ( LowerBandToggleControl ) parSender;
			control.LabelContent = newLabelContent;
			control.HitArea.LabelContent = newLabelContent;
		} // OnLblContentChanged

		private static void OnLabelIsTightChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Boolean newLabelIsTight = ( Boolean ) parArgs.NewValue;
			LowerBandToggleControl control
				= ( LowerBandToggleControl ) parSender;
			control.LabelIsTight = newLabelIsTight;

			String labelStyleName = control.LabelIsTight
				? HIT_AREA_TIGHT_LBL_STYLE_NAME
				: HIT_AREA_LBL_STYLE_NAME;
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
			control.LowerBandToggle.IsChecked = newIsChecked;
		} // OnIsCheckedChanged

		private static void OnAssociatedUIElemChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			UIElement newAssociatedUIElement = ( UIElement ) parArgs.NewValue;
			LowerBandToggleControl control
				= ( LowerBandToggleControl ) parSender;
			control.AssociatedUIElement = newAssociatedUIElement;
			control.LowerBandToggle.AssociatedUIElement = newAssociatedUIElement;
		} // OnAssociatedUIElemChanged

		private static void OnGroupIndexChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Int32 newGroupIndex = ( Int32 ) parArgs.NewValue;
			LowerBandToggleControl control
				= ( LowerBandToggleControl ) parSender;
			control.GroupIndex = newGroupIndex;
			control.LowerBandToggle.GroupIndex = newGroupIndex;
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

		private void OnSimpleToggleChecked( Object parSender,
			RoutedEventArgs parArgs )
		{
			this.RaiseCheckedEvent( );
		} // OnSimpleToggleChecked

		public ImageSource ImageSource
		{
			get
			{
				return ( ImageSource ) GetValue( ImgSourceDP );
			}
			set
			{
				SetValue( ImgSourceDP, value );
			}
		} // ImageSource

		public Object LabelContent
		{
			get
			{
				return ( Object ) GetValue( LblContentDP );
			}
			set
			{
				SetValue( LblContentDP, value );
			}
		} // LabelContent

		public Boolean LabelIsTight
		{
			get
			{
				return ( Boolean ) GetValue( LblIsTightDP );
			}
			set
			{
				SetValue( LblIsTightDP, value );
			}
		} // LabelIsTight

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

		public UIElement AssociatedUIElement
		{
			get
			{
				return ( UIElement ) GetValue( AssociatedUIElemDP );
			}
			set
			{
				SetValue( AssociatedUIElemDP, value );
			}
		} // IsChAssociatedUIElementecked

		public Int32 GroupIndex
		{
			get
			{
				return ( Int32 ) GetValue( GroupIndexDP );
			}
			set
			{
				SetValue( GroupIndexDP, value );
			}
		} // GroupIndex
	} // LowerBandToggleControl
} // Controls
