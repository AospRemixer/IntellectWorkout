﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Text.RegularExpressions;
using System.Windows.Ink;

namespace IntellectWorkout
{
    /// <summary>
    /// Interaction logic for spatialQuestions.xaml
    /// </summary>
    public partial class spatialQuestions : Window
    {
        // Global Variables...
        public static int fsM = 0;

        // Starts this window!
        public spatialQuestions()
        {
            InitializeComponent();
            // THIS IS THE TIMER PART
            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick;
            LiveTime.Start();
            // FUNCTION FOR THE TIMER
            void timer_Tick(object sender, EventArgs e)
            {
                LiveTimeLabel.Content = DateTime.Now.ToString("HH:mm");
            }
        }

        // To Close the app!
        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // To Go back a window!
        private void Back_Button_ClickM(object sender, RoutedEventArgs e)
        {
            spatialWindow sw = new();
            sw.Show();
            Close();
        }

        // TO HELP DRAG MOVE THE APP
        private void spatialQuestions_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // FullScreen Mode Button
        private void FullscreenButton_Click(object sender, RoutedEventArgs e)
        {
            if (fsM == 0)
            {
                this.WindowState = WindowState.Maximized;
                fsText.Text = "Minimize";
                fsM = 1;
            }
            else if (fsM == 1)
            {
                this.WindowState = WindowState.Normal;
                fsText.Text = "Fullscreen";
                fsM = 0;
            }
        }

        // TO DOWNLOAD YOUR DRAWING
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            string downloadsFolder = GlobalVars.KnownFolders.GetPath(GlobalVars.KnownFolder.Downloads);
            MessageBox.Show("Saved To Downloads Folder!");
            Rect bounds = VisualTreeHelper.GetDescendantBounds(draw);
            double dpi = 96d;

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(draw);
                dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }
            rtb.Render(dv);

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                pngEncoder.Save(ms);
                System.IO.File.WriteAllBytes(downloadsFolder + @"\download.png", ms.ToArray());
            }
            exportText.Text = "Save";
        }

        // Switch To Normal Brush.
        private void BrushSettings_click(object sender, RoutedEventArgs e)
        {
            this.draw.EditingMode = InkCanvasEditingMode.Ink;
        }

        // Save Brush Color
        public void saveS()
        {
            draw.DefaultDrawingAttributes.Color = ConvertStringToColor(GlobalVars.brushClr);
        }

        // Convert Hex to COLOR.
        public Color ConvertStringToColor(String hex)
        {
            //remove the # at the front
            hex = hex.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            int start = 0;

            //handle ARGB strings (8 characters long)
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                start = 2;
            }

            //convert RGB characters to bytes
            r = byte.Parse(hex.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return System.Windows.Media.Color.FromArgb(a, r, g, b);
        }
        
        // To Clear Drawing!
        private void ClearBtn_click(object sender, RoutedEventArgs e)
        {
            if(draw.Strokes.Count != 0)
            {
                while (draw.Strokes.Count > 0)
                {
                    draw.Strokes.RemoveAt(draw.Strokes.Count - 1);
                }
            }
            else
            {

            }
            
        }

        // To Use highlighter!
        private void HighlightBtn_click(object sender, RoutedEventArgs e)
        {
            if (draw.DefaultDrawingAttributes.IsHighlighter)
            {
                draw.DefaultDrawingAttributes.IsHighlighter = false;
            }
            else
            {
                draw.DefaultDrawingAttributes.IsHighlighter = true;
            }
        }

        // To Change brush size!
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // do nothing is size text is changed!
        private void sizeTxt_change(object sender, TextChangedEventArgs e)
        {
            
        }

        // to activated eraser mode!
        private void EraserBtn_click(object sender, RoutedEventArgs e)
        {
            this.draw.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }

        // To activate select mode!
        private void SelectBtn_click(object sender, RoutedEventArgs e)
        {
            this.draw.EditingMode = InkCanvasEditingMode.Select;
        }

        // To apply brush size!
        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            double a = Convert.ToDouble(sizeTxt.Text);
            draw.DefaultDrawingAttributes.Width = a;
            draw.DefaultDrawingAttributes.Height= a;
        }

        // To Undo!
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (draw.Strokes.Count > 0)
            {
                draw.Strokes.RemoveAt(draw.Strokes.Count - 1);
            }
            else
            {
                // Else Do Nothing.
            }
        }

        // To save the chosen color!
        private void cp_SelectedColorChanged_1(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            draw.DefaultDrawingAttributes.Color = ConvertStringToColor(cp.SelectedColor.Value.ToString());
        }

    }
}
