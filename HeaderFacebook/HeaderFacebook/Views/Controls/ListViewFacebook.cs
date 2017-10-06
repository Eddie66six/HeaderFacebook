using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace HeaderFacebook.Views.Controls
{
    public class ListViewFacebook : ScrollView
    {
        public ListViewFacebook()
        {
            Scrolled += (sender, e) => Parallax();
        }
        public static readonly BindableProperty ParallaxViewProperty =
            BindableProperty.Create(nameof(ListViewFacebook), typeof(View), typeof(ListViewFacebook), null);

        public View ParallaxView
        {
            get { return (View)GetValue(ParallaxViewProperty); }
            set { SetValue(ParallaxViewProperty, value); }
        }

        public static readonly BindableProperty HeaderViewProperty =
            BindableProperty.Create(nameof(ListViewFacebook), typeof(View), typeof(ListViewFacebook), null);

        public View HeaderView
        {
            get { return (View)GetValue(HeaderViewProperty); }
            set { SetValue(HeaderViewProperty, value); }
        }

        double height, maxPositionHeader;
        public void Parallax()
        {
            if (ParallaxView == null)
                return;

            if (height <= 0)
                height = ParallaxView.Height;

            var y = -(int)((float)ScrollY / 2.5f);
            if (y < 0)
            {
                //Move a imagem no eixo Y em uma fração da posição Y do ScrollView.
                ParallaxView.Scale = 1;
                ParallaxView.TranslationY = y;
                if (HeaderView.TranslationY <= 0)
                    HeaderView.TranslationY = y /1.7;
            }
            else if (Device.OS == TargetPlatform.iOS)
            {
                //Calcula uma escala que iguala a altura x scroll.
                double newHeight = height + (ScrollY * -1);
                ParallaxView.Scale = newHeight / height;
                ParallaxView.TranslationY = -(ScrollY / 2);
            }
            else
            {
                ParallaxView.Scale = 1;
                ParallaxView.TranslationY = 0;
            }
        }
    }
}
