using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Search.UserControls
{
    public sealed partial class ArticleControl : UserControl
    {
        public ArticleControl()
        {
            this.InitializeComponent();
        }

        public ImageSource Source
        {
            get
            {
                return (ImageSource)GetValue(sourceProperty);
            }
            set
            {
                SetValue(sourceProperty, value);
                image.Source = Source;
            }
        }
        public static readonly DependencyProperty sourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(ArticleControl), null);

        public string Price
        {
            get
            {
                return (string)GetValue(priceProperty);
            }
            set
            {
                SetValue(priceProperty, value);
                PriceText.Text = value;
            }
        }
        public static readonly DependencyProperty priceProperty = DependencyProperty.Register("Price", typeof(string), typeof(ArticleControl), null);

        public string Brand
        {
            get
            {
                return (string)GetValue(brandProperty);
            }
            set
            {
                SetValue(brandProperty, value);
                BrandText.Text = value;
            }
        }
        public static readonly DependencyProperty brandProperty = DependencyProperty.Register("Brand", typeof(string), typeof(ArticleControl), null);

        public string ArticleName
        {
            get
            {
                return (string)GetValue(articleNameProperty);
            }
            set
            {
                SetValue(articleNameProperty, value);
                NameText.Text = value;
            }
        }
        public static readonly DependencyProperty articleNameProperty = DependencyProperty.Register("ArticleName", typeof(string), typeof(ArticleControl), null);


    }
}
