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

            // populate listview
            stageItems.Items.Add("bla1");
            stageItems.Items.Add("bla2");
            stageItems.Items.Add("bla3");
            stageItems.Items.Add("bla4");


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
            Vector p = new Vector(0,0,0);

            //we need to solve t
            double t;

            // ray sphere intersection equation
            double a, b, c;

            Vector v;

            Vector Light = new Vector(250, 250, -200);

            // no idea what this is for
            double col = 0.0;


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

                    double red;
                    double green;
                    double blue;

                    double disc = b * b - 4 * a * c;
                    if (disc < 0) // maybe <
                    {
                        col = 0.0; // maybe 0.0 if ^ is <

                        red = (byte)0;
                        green = (byte)0;
                        blue = (byte)0;
                    }
                    else 
                    {
                        col = 1.0; // maybe 1.0 if ^ is >;

                        

                        red = redSlider.Value; // maybe col should be first
                        green = (byte)(greenSlider.Value);
                        blue = (byte)(blueSlider.Value);
                    }
                    t = (-b - Math.Sqrt(disc)) / (2 * a);
                    p = o.add(d.mul(t));
                    Vector Lv = Light.sub(p);
                    Lv.normalise();
                    Vector n = p.sub(cs);
                    n.normalise();
                    double dp=Lv.dot(n);
                    if (dp < 0)
                    {
                        col = 0;
                    }
                    else {
                        col = dp;
                    }
                    if (col > 1) {
                        col = 1;
                    }
                    CustomColour color = new CustomColour((byte)(col * red), (byte)(col * green), (byte)(col * blue));
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

        private void onStageManagerButtonClick(object sender, RoutedEventArgs e)
        {
            sceneCollectionTitle.Visibility = Visibility.Collapsed;
            viewStageManagerButton.Visibility = Visibility.Collapsed;
            propertiesTitle.Visibility = Visibility.Collapsed;
            itemName.Visibility = Visibility.Collapsed;
            itemType.Visibility = Visibility.Collapsed;
            xAxis.Visibility = Visibility.Collapsed;
            yAxis.Visibility = Visibility.Collapsed;
            zAxis.Visibility = Visibility.Collapsed;
            redSlider.Visibility = Visibility.Collapsed;
            greenSlider.Visibility = Visibility.Collapsed;
            blueSlider.Visibility = Visibility.Collapsed;
            sceneObjectsTitle.Visibility = Visibility.Visible;
            addObjectButton.Visibility = Visibility.Visible;
            StageManager.Visibility= Visibility.Visible;
        }

        private void stageItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sceneObjectsTitle.Visibility = Visibility.Collapsed;
            addObjectButton.Visibility = Visibility.Collapsed;
            addObjectButton.Visibility = Visibility.Collapsed;
            StageManager.Visibility = Visibility.Collapsed;
            sceneCollectionTitle.Visibility = Visibility.Visible;
            itemName.Visibility = Visibility.Visible;
            itemType.Visibility = Visibility.Visible;
            viewStageManagerButton.Visibility = Visibility.Visible;
            propertiesTitle.Visibility = Visibility.Visible;
            xAxis.Visibility = Visibility.Visible;
            yAxis.Visibility = Visibility.Visible;
            zAxis.Visibility = Visibility.Visible;
            redSlider.Visibility = Visibility.Visible;
            greenSlider.Visibility = Visibility.Visible;
            blueSlider.Visibility = Visibility.Visible;
        }

        private void showPropertiesWindow() {
            hideStageManagerWindow();

            sceneCollectionTitle.Visibility = Visibility.Visible;
            viewStageManagerButton.Visibility = Visibility.Visible;
            propertiesTitle.Visibility = Visibility.Visible;
            xAxis.Visibility = Visibility.Visible;
            yAxis.Visibility = Visibility.Visible;
            zAxis.Visibility = Visibility.Visible;
            redSlider.Visibility = Visibility.Visible;
            greenSlider.Visibility = Visibility.Visible;
            blueSlider.Visibility = Visibility.Visible;
        }

        private void hidePropertiesWindow() {
            sceneCollectionTitle.Visibility = Visibility.Collapsed;
            viewStageManagerButton.Visibility = Visibility.Collapsed;
            propertiesTitle.Visibility = Visibility.Collapsed;
            xAxis.Visibility = Visibility.Collapsed;
            yAxis.Visibility = Visibility.Collapsed;
            zAxis.Visibility = Visibility.Collapsed;
            redSlider.Visibility = Visibility.Collapsed;
            greenSlider.Visibility = Visibility.Collapsed;
            blueSlider.Visibility = Visibility.Collapsed;

            showStageManagerWindow();
        }

        private void showStageManagerWindow() {
            hidePropertiesWindow();

            sceneObjectsTitle.Visibility = Visibility.Visible;
            addObjectButton.Visibility = Visibility.Visible;
            addObjectButton.Visibility = Visibility.Visible;
        }

        private void hideStageManagerWindow()
        {
            sceneObjectsTitle.Visibility = Visibility.Collapsed;
            addObjectButton.Visibility = Visibility.Collapsed;
            addObjectButton.Visibility = Visibility.Collapsed;

            showPropertiesWindow();
        }

    }
}
