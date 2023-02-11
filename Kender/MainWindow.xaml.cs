// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Kender
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            ExtendsContentIntoTitleBar = true;

            this.InitializeComponent();

            // Set default RGB slider values
            redSlider.Value = 255;
            greenSlider.Value = 0;
            blueSlider.Value = 0;

            Render(new CustomBitmap(500, 500));
        }

        public void Render(CustomBitmap bitmap) 
        {

            //get bitmap size so we can iterate and through every pixel
            int width = bitmap.getWidth();
            int height = bitmap.getHeight();

            

            // generate ray tracer image

            // origin point
            Vector o = new Vector(0, 0, 0);

            //ray direction
            Vector d = new Vector(0, 0, 1);

            //ray center of sphere
            Vector cs = new Vector(0, 0, 0);

            //sphere radius
            double r = 100;

            //point of intersection
            Vector p;

            //we need to solve t
            double t;

            // ray sphere intersection equation
            double a, b, c;

            Vector v;


            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {

                    o.x = x - 250;
                    o.y = y - 250;
                    o.z = -200;

                    v = o.sub(cs);
                    a = d.dot(d);
                    b = 2 * v.dot(d);
                    c = v.dot(v) - r * r;

                    byte red = (byte)0;
                    byte green = (byte)0;
                    byte blue = (byte)0;

                    double disc = b * b - 4 * a * c;
                    if (disc > 0)
                    {
                        red = (byte)redSlider.Value;
                        green = (byte)greenSlider.Value;
                        blue = (byte)blueSlider.Value;
                    }
                    else 
                    {
                        red = (byte)0;
                        green = (byte)0;
                        blue = (byte)0;
                    }
                    CustomColour color = new CustomColour(red, green, blue);
                    bitmap.setPixel(x, y, color);
                }
            }

            //Genereate bitmap
            //for(int x = 0; x < width; x++)
            //{
            //for (int y = 0; y < height; y++)
            //{
            //byte r = (byte)red;
            //byte g = (byte)green;
            //byte b = (byte)blue;
            //CustomColour color = new(r, g, b);
            //bitmap.setPixel(x, y, color);
            //}
            //}

            //Save the generated bitmap
            bitmap.Save(@"C:\Users\khscl\Downloads\render.bmp");

            //Update the UI to display the new bitmap
            //renderImage.Source = new BitmapImage(new Uri("render.bmp"), UriKind.Relative);
            BitmapImage bitmapImage= new BitmapImage();
            bitmapImage.UriSource = new Uri(@"C:\Users\khscl\Downloads\render.bmp", UriKind.Relative);
            renderImage.Source = null;
            renderImage.Source = bitmapImage;
        }

        private void xAxis_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            //TODO: Implement slider
            Debug.WriteLine($"X Moved: {e.NewValue}");

        }

        private void yAxis_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            //TODO: Implement slider
            Debug.WriteLine($"Y Moved: {e.NewValue}");
        }

        private void zAxis_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            //TODO: Implement slider
            Debug.WriteLine($"Z Moved: {e.NewValue}");
        }

        private void red_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            //TODO: Implement slider
            Debug.WriteLine($"red Moved: {e.NewValue}");
            Render(new CustomBitmap(500, 500));
        }

        private void green_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            //TODO: Implement slider
            Debug.WriteLine($"green Moved: {e.NewValue}");
            Render(new CustomBitmap(500, 500));
        }

        private void blue_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            //TODO: Implement slider
            Debug.WriteLine($"blue Moved: {e.NewValue}");
            Render(new CustomBitmap(500, 500));
        }


    }
}
