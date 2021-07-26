
using Android.App;
using Android.Content.PM;
using Android.Content;
using Android.OS;
using System.IO;
using Acr.UserDialogs;
using Android.Widget;
using System;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;
using System.Threading.Tasks;

namespace AIW.Droid
{
    [Activity(Label = "AIW", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { Intent.ActionSend }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = "text/plain")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity 
    {
        internal static MainActivity Instance { get; private set; }

        const int REQUEST_WRITE_ACCESS = 1234;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            Current = this;

            //runtime permision check
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) == (int)Permission.Granted)
            {
                var requiredPermissions = new String[] { Manifest.Permission.WriteExternalStorage };
                // We have permission, go ahead and use the camera.
              
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteExternalStorage },  REQUEST_WRITE_ACCESS);
            }

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            string directory = Android.OS.Environment.ExternalStorageDirectory + "/MyUTD/";


            UserDialogs.Init(() => this);

            //create download directory if do not exist
            if (Directory.Exists(directory)) {  }
            else { Directory.CreateDirectory(directory); }


            // Get intent, action and MIME type
            string action = Intent.Action;
            string type = Intent.Type;

            //get intent youtube url and subject from the bundle 
            var bundle = Intent.Extras;
            var shareURL = bundle?.GetString(Intent.ExtraText);
            var videoTitle = bundle?.GetString(Intent.ExtraSubject);


            //handle intents from other apps " Youtube"  ect...
            if (Intent.ActionSend.Equals(action) && type != null)
            {

                if ("text/plain".Equals(type))
                {
                    //Toast.MakeText(this, videoTitle, ToastLength.Long).Show();
                    Xamarin.Forms.MessagingCenter.Send<string>(videoTitle, "FromAndroid");
                    


                }
            }
            else
            {
                //Handle other intents, such as being started from the home screen
                // PopulateList("https://www.youtube.com/watch?v=JhpbIbJTqCs".Substring("https://www.youtube.com/watch?v=JhpbIbJTqCs".IndexOf("=") + 1));

            }



            Xamarin.Forms.MessagingCenter.Subscribe<string>(this, "Share", (filename) => {

               
                Intent filesystemActivity = new Intent(Intent.ActionSend);
                // filesystemActivity.SetDataAndType(directoryUri, "*/*");
                 filesystemActivity.PutExtra(Intent.ExtraText, directory + filename); //text 
                //filesystemActivity.PutExtra(Intent.ExtraStream, "file://" +  directory + filename);
                //filesystemActivity.PutExtra(Intent.ExtraStream, new Uri("file://" + directory + filename));

                filesystemActivity.SetType("text/plain"); //for strings
                //filesystemActivity.SetType("video/mp4"); //for videos
                
                //filesystemActivity.SetType("image/jpg"); //for images
                //filesystemActivity.SetType("application/pdf");

                Intent shareIntent = Intent.CreateChooser(filesystemActivity, "Share");
                StartActivity(shareIntent);

            });


        }

        protected override void OnStart()
        {
            base.OnStart();


        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == REQUEST_WRITE_ACCESS)
            {
                // Received permission result for camera permission.
                // Check if the only required permission has been granted
                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    // Location permission has been granted, okay to retrieve the location of the device.
                    Toast.MakeText(this,"Permision granted", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Permision not granted application will exit", ToastLength.Short).Show();

                    Finish();
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }




        //Video Player  prop and handlers
        public static MainActivity Current { private set; get; }

        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<string> PickImageTaskCompletionSource { set; get; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (data != null))
                {
                    // Set the filename as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(data.DataString);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }
    }
}