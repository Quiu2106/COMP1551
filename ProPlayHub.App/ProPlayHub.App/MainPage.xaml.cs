using System;
using Microsoft.Maui.Controls;

namespace ProPlayHub.App
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        // Correct signature expected by the XAML Clicked event:
        // void MethodName(object sender, EventArgs e)
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            if (CounterBtn != null)
            {
                CounterBtn.Text = $"Clicked {count} time{(count == 1 ? "" : "s")}";
            }
        }
    }
}