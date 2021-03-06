﻿using Inevent.Models;
using Inevent.ViewModels;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inevent.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private Tag[] favourites;
        public HomeView()
        {
            InitializeComponent();
            LoadTags();
            DataContext = new DashboardModel();

            DashboardButton.Background = (Brush)(new BrushConverter().ConvertFrom("#f7fafc"));
            DashboardButton.Foreground = (Brush)(new BrushConverter().ConvertFrom("#1976d2"));
        }

        public async void LoadTags()
        {
            try
            {
                Tag[] req = await Tags.GetAllTags();
                AllTags.ItemsSource = req;
                favourites = await Tags.GetUserTags(Properties.Settings.Default.id);
                if (favourites.Length > 0 )
                {
                    Favourites.ItemsSource = favourites;
                }
                else
                {
                    IfEmpty.Text = "Nie wybrałeś ulubionych tagów. Edytuj profil, aby to zrobić!";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ClearButtons()
        {
            DashboardButton.Background = null;
            DashboardButton.Foreground = Brushes.Black;
            ProfileEditButton.Background = null;
            ProfileEditButton.Foreground = Brushes.Black;
            CreateEventButton.Background = null;
            CreateEventButton.Foreground = Brushes.Black;
            CreatedButton.Background = null;
            CreatedButton.Foreground = Brushes.Black;
            SignedButton.Background = null;
            SignedButton.Foreground = Brushes.Black;
            MatchedButton.Background = null;
            MatchedButton.Foreground = Brushes.Black;
        }

        public void Dashboard_click(object sender, EventArgs e)
        {
            Content = new HomeModel();
        }

        public void TagButton_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Properties.Settings.Default.currentTag = Convert.ToInt32(btn.Tag);
            DataContext = new EventsByTagModel();
            EventsByTagView x = new EventsByTagView();
            Control.DataContext = x;
            x.Refresh();
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
           ClearButtons();
           ProfileEditButton.Background = (Brush)(new BrushConverter().ConvertFrom("#f7fafc"));
           ProfileEditButton.Foreground = (Brush)(new BrushConverter().ConvertFrom("#1976d2"));
           DataContext = new EditProfileModel();
        }

        public void Refresh()
        {
            LoadTags();
        }

        private void CreateEvent_Click(object sender, RoutedEventArgs e)
        {
            ClearButtons();
            CreateEventButton.Background = (Brush)(new BrushConverter().ConvertFrom("#f7fafc"));
            CreateEventButton.Foreground = (Brush)(new BrushConverter().ConvertFrom("#1976d2"));
            DataContext = new EventCreatorModel();
        }

        private void Created_Click(object sender, RoutedEventArgs e)
        {
            ClearButtons();
            CreatedButton.Background = (Brush)(new BrushConverter().ConvertFrom("#f7fafc"));
            CreatedButton.Foreground = (Brush)(new BrushConverter().ConvertFrom("#1976d2"));
            DataContext = new CreatedEventsModel();
        }

        private void Signed_Click(object sender, RoutedEventArgs e)
        {
            ClearButtons();
            SignedButton.Background = (Brush)(new BrushConverter().ConvertFrom("#f7fafc"));
            SignedButton.Foreground = (Brush)(new BrushConverter().ConvertFrom("#1976d2"));
            DataContext = new SignedEventsModel();
        }
        private void Matched_Click(object sender, RoutedEventArgs e)
        {
            ClearButtons();
            MatchedButton.Background = (Brush)(new BrushConverter().ConvertFrom("#f7fafc"));
            MatchedButton.Foreground = (Brush)(new BrushConverter().ConvertFrom("#1976d2"));
            DataContext = new MatchedEventsModel();
        }
    }
}
