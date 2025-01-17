﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace IntellectWorkout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            // Starts this application!
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

        // MATH BUTTON
        private void Math_Button_Click(object sender, RoutedEventArgs e)
        {
            // OPEN THE MATH WINDOW
            mathWindow maWindow = new mathWindow();
            maWindow.Show();
            this.Close();
        }

        // MEMORY BUTTON
        private void Memory_Button_Click(object sender, RoutedEventArgs e)
        {
            memoryWindow mw = new();
            mw.Show();
            Hide();
        }

        // REASONING BUTTON
        private void Reasoning_Button_Click(object sender, RoutedEventArgs e)
        {
            reasoningWindow rw = new();
            rw.Show();
            Hide();
        }

        // SPATIAL BUTTON
        private void Spatial_Button_Click(object sender, RoutedEventArgs e)
        {
            spatialWindow sw = new();
            sw.Show();
            Close();
        }

        // LINGUISTIC BUTTON
        private void Linguistic_Button_Click(object sender, RoutedEventArgs e)
        {
            volcabWindow vc = new();
            vc.Show();
            Close();
        }

        // KINESTHETIC BUTTON
        private void Kinesthetic_Button_Click(object sender, RoutedEventArgs e)
        {
            awarnessWindow aw = new();
            aw.Show();
            Close();
        }

        // TO HELP DRAG THE APP
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // TO MAKE MY OWN EXIT BUTTON
        private void Exit_Button_Home(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
