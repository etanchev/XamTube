using AIW.Models;
using AIW.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AIW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JsonViewCell : ViewCell
	{
        //initialize new tapgesture
        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
        SwipeGestureRecognizer swipeGesture = new SwipeGestureRecognizer();
        JsonViewModel jsonViewModel = new JsonViewModel();
        ModelSearchJson.Snippet ms = new ModelSearchJson.Snippet();
        ModelSearchJsonExtraction msje = new ModelSearchJsonExtraction();

        public Color myBeige = Color.FromHex("#d6a66d");

        public JsonViewCell ()
		{
			//InitializeComponent();

            var imageAndId = ImageAndId();
            var labelsLayout = LabelsLayout();
            labelsLayout.SetBinding(StackLayout.TranslationXProperty, new Binding(){ Mode = BindingMode.OneWay, Path = "TranslateX", Source = jsonViewModel });
            labelsLayout.SetBinding(StackLayout.BackgroundColorProperty, new Binding() { Mode = BindingMode.OneWay, Path = "BackGroundColor", Source = jsonViewModel });

            var viewLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                
                Children = { imageAndId, labelsLayout, },
            };

            View = viewLayout;

            tapGestureRecognizer.Tapped +=  (sender, arg) =>
            {

                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>");
                //await objectToAnimate.TranslateTo(200, 0, 1000, Easing.Linear);
                //await View.TranslateTo(200, 0, 1000, Easing.Linear);
                //View.TranslationY = 40;

                viewLayout.MinimumHeightRequest = 400;
                jsonViewModel.BackGroundColor = Color.FromRgb(new Random().Next(255), new Random().Next(255), new Random().Next(255));
                //jsonViewModel.TranslateX = 10;
            };

            swipeGesture.Direction = SwipeDirection.Right;
            swipeGesture.Swiped += async (sender, arg) =>
            {
                View.Animate("animate", new Animation(), 16, 250, Easing.BounceOut, null, null);
                await View.TranslateTo(0, 200, 250, Easing.Linear);
            };

            viewLayout.GestureRecognizers.Add(tapGestureRecognizer);
           // viewLayout.GestureRecognizers.Add(swipeGesture);
        }

       

        public StackLayout LabelsLayout()
        {
            var nameLable = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = myBeige,


                // FontSize = 15,
            };

            //nameLable.BindingContext = ms;
            // nameLable.SetBinding(Label.TextProperty, new Binding() { Source = msje, Mode = BindingMode.OneWay, Path = "Title" });
            nameLable.SetBinding<ModelSearchJsonExtraction>(Label.TextProperty, pname => pname.Title);
           // nameLable.SetBinding<ModelSearchJson.Item>(Label.TextProperty, pname => pname.snippet.title);



            var channelID = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                // FontSize = 15,
                TextColor = myBeige,

            };
            //channelID.SetBinding(Label.TextColorProperty, new Binding() { Source = msje, Mode = BindingMode.OneWay, Path = "ChanelId" });
            channelID.SetBinding<ModelSearchJsonExtraction>(Label.TextProperty, pname => pname.ChanelId);
           // channelID.SetBinding<ModelSearchJson.Item>(Label.TextProperty, pname => pname.snippet.channelId);

            var nameLayout = new StackLayout()
            {

                HorizontalOptions = LayoutOptions.End,
                Orientation = StackOrientation.Vertical,

                Children = { nameLable, channelID, },
            };
            return nameLayout;
        }

        public StackLayout ImageAndId()
        {
            var image = new Image
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                //HorizontalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = 90,
                HeightRequest = 60,
            };
            //image.Source = ImageSource.FromUri(new Uri("https://xamarin.com/content/images/pages/forms/example-app.png"));
            // image.SetBinding(Image.SourceProperty, new Binding() { Source = msje, Mode = BindingMode.OneWay, Path = "ImageURL" });
            image.SetBinding<ModelSearchJsonExtraction>(Image.SourceProperty, pname => pname.ImageURL);
           // image.SetBinding<ModelSearchJson.Item>(Image.SourceProperty, pname => pname.snippet.thumbnails.@default.url);

            var videoId = new Label
            {
                // HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                MinimumWidthRequest = 100,
                TextColor = myBeige,
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center,
            };
            // videoId.SetBinding(Label.TextProperty, new Binding() { Source = msje ,  Mode = BindingMode.OneWay , Path ="ID" });
            videoId.SetBinding<ModelSearchJsonExtraction>(Label.TextProperty, pname => pname.VideoId);
            //videoId.SetBinding<ModelSearchJson.Item>(Label.TextProperty, pname => pname.id.videoId);

            var imageandId = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Start,
                //VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { image, videoId },
            };
            return imageandId;
        }
    }
}