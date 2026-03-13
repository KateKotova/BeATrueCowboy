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
	/// Interaction logic for LabeledImageDataControl.xaml
	/// </summary>
	public partial class LabeledImageDataControl : UserControl
	{
		public static DependencyProperty ImgSourceDP;
		public static DependencyProperty LblContentDP;
		public static DependencyProperty ImdStyleDP;
		public static DependencyProperty LblStyleDP;
		public static DependencyProperty SecondLblContentDP;
		public static DependencyProperty SecondLblStyleDP;
		public static DependencyProperty SecondLblVisabilityDP;

		public event EventHandler ImgSourceChanged;
		public event EventHandler LblContentChanged;
		public event EventHandler ImgStyleChanged;
		public event EventHandler LblStyleChanged;
		public event EventHandler SecondLblContentChanged;
		public event EventHandler SecondLblStyleChanged;
		public event EventHandler SecondLblVisabilityChanged;

		public LabeledImageDataControl( )
		{
			InitializeComponent( );
		} // LabeledImageDataControl

		static LabeledImageDataControl( )
		{
			// Registration of propeties dependencies.
			ImgSourceDP = DependencyProperty.Register(
				nameof( ImageSource ),
				typeof( ImageSource ),
				typeof( LabeledImageDataControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnImgSourceChanged )
				)
			);
			LblContentDP = DependencyProperty.Register(
				nameof( LabelContent ),
				typeof( Object ),
				typeof( LabeledImageDataControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnLblContentChanged )
				)
			);

			ImdStyleDP = DependencyProperty.Register(
				nameof( ImageStyle ),
				typeof( Style ),
				typeof( LabeledImageDataControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnImgStyleChanged )
				)
			);
			LblStyleDP = DependencyProperty.Register(
				nameof( LabelStyle ),
				typeof( Style ),
				typeof( LabeledImageDataControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnLblStyleChanged )
				)
			);

			SecondLblContentDP = DependencyProperty.Register(
				nameof( SecondLabelContent ),
				typeof( Object ),
				typeof( LabeledImageDataControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnSecondLblContentChanged )
				)
			);
			SecondLblStyleDP = DependencyProperty.Register(
				nameof( SecondLabelStyle ),
				typeof( Style ),
				typeof( LabeledImageDataControl ),
				new FrameworkPropertyMetadata(
					null,
					new PropertyChangedCallback( OnSecondLblStyleChanged )
				)
			);
			SecondLblVisabilityDP = DependencyProperty.Register(
				nameof( SecondLabelVisability ),
				typeof( Visibility ),
				typeof( LabeledImageDataControl ),
				new FrameworkPropertyMetadata(
					Visibility.Collapsed,
					new PropertyChangedCallback( OnSecondLblVisabilityChanged )
				)
			);
	} // LabeledImageDataControl

		private static void OnImgSourceChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			ImageSource newImageSource = ( ImageSource ) parArgs.NewValue;
			LabeledImageDataControl control
				= ( LabeledImageDataControl ) parSender;
			control.ImageSource = newImageSource;
			control.ImgSourceChanged?.Invoke( control, EventArgs.Empty );
		} // OnImgSourceChanged

		private static void OnLblContentChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Object newLabelContent = ( Object ) parArgs.NewValue;
			LabeledImageDataControl control
				= ( LabeledImageDataControl ) parSender;
			control.LabelContent = newLabelContent;
			control.LblContentChanged?.Invoke( control, EventArgs.Empty );
		} // OnLblContentChanged

		private static void OnImgStyleChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Style newImageStyle = ( Style ) parArgs.NewValue;
			LabeledImageDataControl control
				= ( LabeledImageDataControl ) parSender;
			control.ImageStyle = newImageStyle;
			control.ImgStyleChanged?.Invoke( control, EventArgs.Empty );
		} // OnImgStyleChanged

		private static void OnLblStyleChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Style newLabelStyle = ( Style ) parArgs.NewValue;
			LabeledImageDataControl control
				= ( LabeledImageDataControl ) parSender;
			control.LabelStyle = newLabelStyle;
			control.LblStyleChanged?.Invoke( control, EventArgs.Empty );
		} // OnLblStyleChanged

		private static void OnSecondLblContentChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Object newSecondLabelContent = ( Object ) parArgs.NewValue;
			LabeledImageDataControl control
				= ( LabeledImageDataControl ) parSender;
			control.SecondLabelContent = newSecondLabelContent;
			control.SecondLblContentChanged?.Invoke( control, EventArgs.Empty );
		} // OnSecondLblContentChanged

		private static void OnSecondLblStyleChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Style newSecondLabelStyle = ( Style ) parArgs.NewValue;
			LabeledImageDataControl control
				= ( LabeledImageDataControl ) parSender;
			control.SecondLabelStyle = newSecondLabelStyle;
			control.SecondLblStyleChanged?.Invoke( control, EventArgs.Empty );
		} // OnSecondLblStyleChanged

		private static void OnSecondLblVisabilityChanged(
			DependencyObject parSender,
			DependencyPropertyChangedEventArgs parArgs )
		{
			Visibility newSecondLabelVisability = ( Visibility ) parArgs.NewValue;
			LabeledImageDataControl control
				= ( LabeledImageDataControl ) parSender;
			control.SecondLabelVisability = newSecondLabelVisability;
			control.SecondLblVisabilityChanged?.Invoke
				( control, EventArgs.Empty );
		} // OnSecondLblVisabilityChanged

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

		public Style ImageStyle
		{
			get
			{
				return ( Style ) GetValue( ImdStyleDP );
			}
			set
			{
				SetValue( ImdStyleDP, value );
			}
		} // ImageStyle

		public Style LabelStyle
		{
			get
			{
				return ( Style ) GetValue( LblStyleDP );
			}
			set
			{
				SetValue( LblStyleDP, value );
			}
		} // LabelStyle

		public Object SecondLabelContent
		{
			get
			{
				return ( Object ) GetValue( SecondLblContentDP );
			}
			set
			{
				SetValue( SecondLblContentDP, value );
			}
		} // SecondLabelContent

		public Style SecondLabelStyle
		{
			get
			{
				return ( Style ) GetValue( SecondLblStyleDP );
			}
			set
			{
				SetValue( SecondLblStyleDP, value );
			}
		} // SecondLabelStyle

		public Visibility SecondLabelVisability
		{
			get
			{
				return ( Visibility ) GetValue( SecondLblVisabilityDP );
			}
			set
			{
				SetValue( SecondLblVisabilityDP, value );
			}
		} // SecondLabelVisability

	} // LabeledImageDataControl
} // Controls
