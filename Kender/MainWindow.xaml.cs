using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kender
{
    /// <summary>
    /// The home screen of the application. 
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        /// <summary>
        /// Contains all the objects that will be rendered.
        /// </summary>
        static readonly List<Vector> SCENE_OBJECTS = new();

        //HACK
        // use that to get the current item.
        //SCENE_OBJECTS[stageItems.SelectedIndex];



        public MainWindow()
        {
            ExtendsContentIntoTitleBar = true;

            this.InitializeComponent();

            SetDefaults();

            Render(new CustomBitmap(500, 500));
        }

        /// <summary>
        /// Sets up the default project scene.
        /// </summary>
        private void SetDefaults()
        {

            // Add the default objects so we have a viewable scene when we first open the appliation.
            SCENE_OBJECTS.Add(new Light(250, 250, -200));
            SCENE_OBJECTS.Add(new Sphere(0, 0, 0, 100, new CustomColour(255, 0, 0)));

            /* 
             * Add the name of our scene objects to the stage items listview,
             * this will allow the user to select one of the default
             * objects when they first open the app so the can
             * edit its properties.
             */
            SCENE_OBJECTS.ForEach(obj =>
            {
                // Use a combination of the type of item and its current index to produce a unique ID.
                string item = obj.GetType().Name + " " + stageItems.Items.Count;
                stageItems.Items.Add(item);
            });
        }

        /// <summary>
        /// Preforms ray intersection to calculate a colour for each pixel in a bitmap of
        /// a custom size.
        /// </summary>
        /// <param name="bitmap">
        /// The bitmap we will use to render this image.
        /// </param>
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
            Vector p = new Vector(0, 0, 0);

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
                    double dp = Lv.dot(n);
                    if (dp < 0)
                    {
                        col = 0;
                    }
                    else
                    {
                        col = dp;
                    }
                    if (col > 1)
                    {
                        col = 1;
                    }
                    CustomColour color = new CustomColour((byte)(col * red), (byte)(col * green), (byte)(col * blue));
                    bitmap.setPixel(x, y, color);
                }
            }

            // Save the generated bitmap.
            bitmap.Save(@"C:\Users\khscl\Downloads\render.bmp");


            UpdateViewport();
        }

        /// <summary>
        /// Gets the latest rendered image to display to the screen.
        /// </summary>
        private void UpdateViewport()
        {
            // Get the new image.
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.UriSource = new Uri(@"C:\Users\khscl\Downloads\render.bmp", UriKind.Relative);
            // Remove the current image from the screen.
            renderImage.Source = null;
            // Set the new image.
            renderImage.Source = bitmapImage;
        }

        /// <summary>
        /// Called when the X Axis slider has changed.
        /// </summary>
        /// <param name="sender">The controller that raised the event.</param>
        /// <param name="e">The new slider value.</param>
        private void XAxis_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            
            Debug.WriteLine($"X Moved: {e.NewValue}");
            SCENE_OBJECTS[stageItems.SelectedIndex].x = (float)e.NewValue;
            Render(new CustomBitmap(500, 500));
        }

        /// <summary>
        /// Called when the Y Axis slider has changed.
        /// </summary>
        /// <param name="sender">The controller that raised the event.</param>
        /// <param name="e">The new slider value.</param>
        private void YAxis_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            
            Debug.WriteLine($"Y Moved: {e.NewValue}");
            SCENE_OBJECTS[stageItems.SelectedIndex].y = (float)e.NewValue;
            Render(new CustomBitmap(500, 500));
        }

        /// <summary>
        /// Called when the Z Axis slider has changed.
        /// </summary>
        /// <param name="sender">The controller that raised the event.</param>
        /// <param name="e">The new slider value.</param>
        private void ZAxis_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Debug.WriteLine($"Z Moved: {e.NewValue}");
            SCENE_OBJECTS[stageItems.SelectedIndex].z = (float)e.NewValue;
            Render(new CustomBitmap(500, 500));
        }

        /// <summary>
        /// Called when the radius slider has changed.
        /// </summary>
        /// <param name="sender">The controller that raised the event.</param>
        /// <param name="e">The new slider value.</param>
        private void radius_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Debug.WriteLine($"red Moved: {e.NewValue}");
            // Get the sphere to edit the RGB values.
            Sphere sphere = (Sphere)SCENE_OBJECTS[stageItems.SelectedIndex];
            // Set the new colour value.
            sphere.Radius = (float)e.NewValue;
            // Replace the old sphere with the new sphere.
            SCENE_OBJECTS[stageItems.SelectedIndex] = sphere;
            Render(new CustomBitmap(500, 500));
        }

        /// <summary>
        /// Called when the red Axis slider has changed.
        /// </summary>
        /// <param name="sender">The controller that raised the event.</param>
        /// <param name="e">The new slider value.</param>
        private void Red_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Debug.WriteLine($"red Moved: {e.NewValue}");
            // Get the sphere to edit the RGB values.
            Sphere sphere = (Sphere)SCENE_OBJECTS[stageItems.SelectedIndex];
            // Set the new colour value.
            sphere.Colour = new CustomColour((byte)e.NewValue, sphere.Colour.greenColour, sphere.Colour.blueColour);
            // Replace the old sphere with the new sphere.
            SCENE_OBJECTS[stageItems.SelectedIndex] = sphere;
            Render(new CustomBitmap(500, 500));
        }

        /// <summary>
        /// Called when the green Axis slider has changed.
        /// </summary>
        /// <param name="sender">The controller that raised the event.</param>
        /// <param name="e">The new slider value.</param>
        private void Green_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Debug.WriteLine($"green Moved: {e.NewValue}");
            // Get the sphere to edit the RGB values.
            Sphere sphere = (Sphere)SCENE_OBJECTS[stageItems.SelectedIndex];
            // Set the new colour value.
            sphere.Colour = new CustomColour(sphere.Colour.redColour, (byte)e.NewValue, sphere.Colour.blueColour);
            // Replace the old sphere with the new sphere.
            SCENE_OBJECTS[stageItems.SelectedIndex] = sphere;
            Render(new CustomBitmap(500, 500));
        }

        /// <summary>
        /// Called when the blue Axis slider has changed.
        /// </summary>
        /// <param name="sender">The controller that raised the event.</param>
        /// <param name="e">The new slider value.</param>
        private void Blue_SliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Debug.WriteLine($"blue Moved: {e.NewValue}");
            // Get the sphere to edit the RGB values.
            Sphere sphere = (Sphere)SCENE_OBJECTS[stageItems.SelectedIndex];
            // Set the new colour value.
            sphere.Colour = new CustomColour(sphere.Colour.redColour, sphere.Colour.greenColour, (byte)e.NewValue);
            // Replace the old sphere with the new sphere.
            SCENE_OBJECTS[stageItems.SelectedIndex] = sphere;
            Render(new CustomBitmap(500, 500));
        }

        /// <summary>
        /// Called when a new light source is added to the scene.
        /// </summary>
        /// <param name="sender">The controller that raised the event.</param>
        private void OnAddLightClick(object _, RoutedEventArgs e)
        {
            SCENE_OBJECTS.Add(new Light());
            stageItems.Items.Add("Light " + stageItems.Items.Count);
        }

        /// <summary>
        /// Called when a new sphere is added to the scene.
        /// </summary>
        /// <param name="sender">The controller that raised the event.</param>
        private void OnAddSphereClick(object _, RoutedEventArgs e)
        {
            // Add sphere to scene.
            SCENE_OBJECTS.Add(new Sphere());
            // Add sphere to UI so the user can interact with it.
            stageItems.Items.Add("Sphere " + stageItems.Items.Count);
        }

        /// <summary>
        /// Switches the window to the stage manager.
        /// </summary>
        private void OnStageManagerButtonClick(object _, RoutedEventArgs __)
        {
            // Set visibility of widgets to represent the stage manager UI.
            sceneCollectionTitle.Visibility = Visibility.Collapsed;
            viewStageManagerButton.Visibility = Visibility.Collapsed;
            propertiesTitle.Visibility = Visibility.Collapsed;
            itemName.Visibility = Visibility.Collapsed;
            itemType.Visibility = Visibility.Collapsed;
            xAxis.Visibility = Visibility.Collapsed;
            yAxis.Visibility = Visibility.Collapsed;
            zAxis.Visibility = Visibility.Collapsed;
            radiusSlider.Visibility = Visibility.Collapsed;
            redSlider.Visibility = Visibility.Collapsed;
            greenSlider.Visibility = Visibility.Collapsed;
            blueSlider.Visibility = Visibility.Collapsed;
            sceneObjectsTitle.Visibility = Visibility.Visible;
            addObjectButton.Visibility = Visibility.Visible;
            StageManager.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Switches the window to the properties manager.
        /// </summary>
        private void StageItemSelectionChanged(object _, SelectionChangedEventArgs __)
        {
            // Set visibility of widgets to represent the properties manager UI.
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
            radiusSlider.Visibility = Visibility.Visible;
            redSlider.Visibility = Visibility.Visible;
            greenSlider.Visibility = Visibility.Visible;
            blueSlider.Visibility = Visibility.Visible;

            // Update property fields with name and type data.
            itemName.Text = "Name: " + stageItems.SelectedItem.ToString();
            itemType.Text = "Type: " + SCENE_OBJECTS[stageItems.SelectedIndex].GetType().Name;

            // Update sliders depending on which type of object has been selected by the user
            if (SCENE_OBJECTS[stageItems.SelectedIndex].GetType().Name == "Sphere")
            {
                // Get the sphere from the scene objects vector array and store it as a sphere so we can access the colour properties.
                Sphere sphere = (Sphere)SCENE_OBJECTS[stageItems.SelectedIndex];

                // Update RGB sliders.
                redSlider.Value = sphere.Colour.redColour;
                greenSlider.Value = sphere.Colour.greenColour;
                blueSlider.Value = sphere.Colour.blueColour;

                // Update radius slider.
                radiusSlider.Value = sphere.Radius;

                // Update XYZ sliders.
                xAxis.Value = sphere.x;
                yAxis.Value = sphere.y;
                zAxis.Value = sphere.z;
            }
            else {
                // Must be a light object, We currently are not storing a radius or RGB value for lights so we might as well hide radius and the RGB sliders.
                radiusSlider.Visibility = Visibility.Collapsed;
                redSlider.Visibility = Visibility.Collapsed;
                greenSlider.Visibility = Visibility.Collapsed;
                blueSlider.Visibility = Visibility.Collapsed;

                // Get the light object from the scene objects vector array and store it as a light so we can access its xyz properties.
                Light light = (Light)SCENE_OBJECTS[stageItems.SelectedIndex];

                // Update XYZ sliders.
                xAxis.Value = light.x;
                yAxis.Value = light.y;
                zAxis.Value = light.z;
            }
        }

    }
}
