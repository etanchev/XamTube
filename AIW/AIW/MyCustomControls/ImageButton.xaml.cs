using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AIW.MyCustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageButton : ContentView 
    {


        public static readonly BindableProperty ButtonTextProperty =
            BindableProperty.Create("ButtonText", typeof(string), typeof(ImageButton), default(string));

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

       

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ImageButton), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
           BindableProperty.Create("CommandParameter", typeof(object), typeof(ImageButton), null);

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty);  }
            set { SetValue(CommandParameterProperty, value); }
        }


        public static readonly BindableProperty ImageSoruceProperty =
             BindableProperty.Create("Source", typeof(ImageSource), typeof(ImageButton), default(ImageSource));

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(ImageSoruceProperty); }
            set { SetValue(ImageSoruceProperty, value); }
        }


        public ImageButton()
        {
            InitializeComponent();

            innerLable.SetBinding(Label.TextProperty, new Binding("ButtonText", source: this));
            innerImage.SetBinding(Image.SourceProperty, new Binding("Source", source: this));


        }
    }
}