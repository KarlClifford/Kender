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
        }

        public void render(CustomBitmap bitmap) 
        {

            //get bitmap size so we can iterate and through every pixel
            int width = bitmap.getWidth();
            int height = bitmap.getHeight();

            //Save slider values
            double red = redSlider.Value;
            double green = greenSlider.Value;
            double blue = blueSlider.Value;

            //Genereate bitmap
            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    byte r = (byte)red;
                    byte g = (byte)green;
                    byte b = (byte)blue;
                    CustomColour color = new(r, g, b);
                    bitmap.setPixel(x, y, color);
                }
            }

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
            render(new CustomBitmap(640, 640));
        }

        private void green_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            //TODO: Implement slider
            Debug.WriteLine($"green Moved: {e.NewValue}");
            render(new CustomBitmap(640, 640));
        }

        private void blue_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            //TODO: Implement slider
            Debug.WriteLine($"blue Moved: {e.NewValue}");
            render(new CustomBitmap(640, 640));
        }


    }
}
