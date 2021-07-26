using Xamarin.Forms;
using AIW.CustomRenderer;
using AIW.Droid.CutosmRenderers;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics;

[assembly: ExportRenderer(typeof(MyButton), typeof(MyButtonRenderer))]

namespace AIW.Droid.CutosmRenderers
{

    class MyButtonRenderer : ButtonRenderer 
    {
        public MyButtonRenderer(Context context) : base(context)
        {
            


        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);



                //icon red emty squre
                
                //Path path = new Path();
                //path.AddCircle(10,10,10, Path.Direction.Cw);
                //ShapeDrawable icon = new ShapeDrawable(new PathShape(path,50,50)) ;
                ShapeDrawable icon = new ShapeDrawable(new  RectShape() ) ;
                icon.Paint.Color = Android.Graphics.Color.SaddleBrown;
                icon.SetIntrinsicHeight(50);
                icon.SetIntrinsicWidth(50);
                icon.SetBounds(0, 0, icon.IntrinsicHeight + 50, icon.IntrinsicWidth + 50);
                icon.Paint.StrokeWidth = 4;
                icon.Paint.SetStyle(Paint.Style.Fill);
                icon.Alpha = 10;
                
                

                //oval shape background
                ShapeDrawable oval = new ShapeDrawable(new OvalShape());
                oval.Paint.Color = Android.Graphics.Color.SaddleBrown;
                oval.Paint.StrokeWidth = 5;
                oval.Paint.SetStyle(Paint.Style.Stroke);
                //oval.SetIntrinsicWidth(50);
                //oval.SetIntrinsicHeight(50);


                Paint paint = new Paint
                {
                    Color = Android.Graphics.Color.Aqua
                };

                Image image = new Image();
                image.Source = ImageSource.FromFile("splash_log.png");

               
                Canvas canvas = new Canvas();
                canvas.DrawText("Hello", 0, 0,paint);

                Picture pic = new Picture();
                pic.Draw(canvas);

                Drawable backgound = new PictureDrawable(pic);  // PictureDrawable(new Picture());
                backgound.SetAlpha(50);

                

                //RoundRectShape
                RectF rectF = new RectF(4, 2, 4, 2); //offset betwin innner and outer 
                float[] inner = new float[] { 90, 90, 90, 90, 90, 90, 90, 90 }; 
                float[] outer = new float[] { 90, 90, 90, 90, 90, 90, 90, 90 };
                ShapeDrawable roundRect = new ShapeDrawable(new RoundRectShape(outer,rectF, inner )); ;
                roundRect.Paint.Color = Android.Graphics.Color.SandyBrown;
                roundRect.Paint.StrokeWidth = 0;
                roundRect.Paint.SetStyle(Paint.Style.FillAndStroke);

                Control.SetBackground(roundRect);
                

                Control.SetCompoundDrawables(icon, null, null, null);

            }






        }
    }
}