﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using POEApi.Model;
using Procurement.ViewModel;
using Procurement.ViewModel.Filters;
using System.Diagnostics;
using POEApi.Infrastructure;

namespace Procurement.Controls
{
    public partial class StashControl : UserControl
    {
        public int TabNumber { get; set; }
        private bool initialized = false;

        private Dictionary<Tuple<int, int>, Item> stashByLocation;
        private Dictionary<Tuple<int, int>, Border> borderByLocation;
        public List<Item> Stash { get; set; }
        public int FilterResults { get; private set; }

        public IEnumerable<IFilter> Filter
        {
            get { return (IEnumerable<IFilter>)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        public void ForceUpdate()
        {
            if (initialized == false && Stash == null)
                refresh();

            FilterResults = Filter.Count() == 0 ? -1 : 0;

            foreach (var item in Stash)
                updateResult(borderByLocation[Tuple.Create<int, int>(item.X, item.Y)], search(item));

            this.UpdateLayout();
        }

        private void updateResult(Border border, bool isResult)
        {
            if (isResult)
            {
                FilterResults++;
                border.BorderBrush = Brushes.Yellow;
                border.Background = Brushes.Black;
                return;
            }

            border.BorderBrush = Brushes.Transparent;
            border.Background = Brushes.Transparent;
        }

        public void RefreshTab()
        {
            ApplicationState.Stash[ApplicationState.CurrentLeague].RefreshTab(ApplicationState.Model, ApplicationState.CurrentLeague, TabNumber);
            refresh();
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IEnumerable<IFilter>), typeof(StashControl), null);

        public StashControl()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(StashControl_Loaded);
            ApplicationState.LeagueChanged += new System.ComponentModel.PropertyChangedEventHandler(ApplicationState_LeagueChanged);
            stashByLocation = new Dictionary<Tuple<int, int>, Item>();
        }

        void ApplicationState_LeagueChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            initialized = false;
        }

        void StashControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (initialized)
                return;

            refresh();
        }

        Dictionary<string, Item> test = new Dictionary<string, Item>();

        private void refresh()
        {
            this.Stash = ApplicationState.Stash[ApplicationState.CurrentLeague].GetItemsByTab(TabNumber);
            updateStashByLocation();
            render();
        }

        private void updateStashByLocation()
        {

            stashByLocation.Clear();


            foreach (var item in this.Stash)
            {
                var key = Tuple.Create<int, int>(item.X, item.Y);

                if (stashByLocation.ContainsKey(key))
                    continue;

                stashByLocation.Add(key, item);
            }
        }

        private void render()
        {
            const int columns = 12, rows = 12;

            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            grid.Children.Clear();

            borderByLocation = new Dictionary<Tuple<int, int>, Border>();

            for (int i = 0; i < columns; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                for (int j = 0; j < rows; j++)
                {
                    if (i == 0)
                        grid.RowDefinitions.Add(new RowDefinition());

                    Grid childGrid = new Grid();
                    childGrid.Margin = new Thickness(1);

                    Tuple<int, int> currentKey = new Tuple<int, int>(i, j);

                    if (!stashByLocation.ContainsKey(currentKey))
                        continue;

                    Item gearAtLocation = stashByLocation[currentKey];

                    setBackround(childGrid, gearAtLocation);
                    //if (search(gearAtLocation))
                    Border border = getBorder();
                    borderByLocation[currentKey] = border;
                    childGrid.Children.Add(border);

                    childGrid.Children.Add(getImage(gearAtLocation));

                    Grid.SetColumn(childGrid, i);
                    Grid.SetRow(childGrid, j);
                    if (gearAtLocation.H > 1)
                        Grid.SetRowSpan(childGrid, gearAtLocation.H);

                    if (gearAtLocation.W > 1)
                        Grid.SetColumnSpan(childGrid, gearAtLocation.W);

                    grid.Children.Add(childGrid);
                }
            }

            initialized = true;
        }

        private UIElement getImage(Item item)
        {
            return new ItemDisplay() { DataContext = new ItemDisplayViewModel(item) };
        }

        void popup_LostMouseCapture(object sender, MouseEventArgs e)
        {
            (sender as Popup).IsOpen = false;
        }


        private Border getBorder()
        {
            Border b = new Border();
            b.BorderBrush = Brushes.Transparent;
            b.BorderThickness = new Thickness(2);
            return b;
        }

        private void setBackround(Grid childGrid, Item item)
        {
            if (item is Gear && (item as Gear).Rarity != Rarity.Normal && (item as Gear).Explicitmods == null)
                childGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#88001D"));
            else
                childGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#21007F"));



            childGrid.Background.Opacity = 0.3;
        }

        private bool search(Item item)
        {
            if (Filter.Count() == 0)
                return false;

            return Filter.All(filter => filter.Applicable(item));
        }
    }
}
