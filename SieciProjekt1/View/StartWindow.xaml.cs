using Microsoft.Win32;
using SieciProjekt1.Model;
using SieciProjekt1.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SieciProjekt1.View
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();

            generateCombo();
        }

        private void generateCombo()
        {
            foreach (var item in Enum.GetValues(typeof(ChecksumTypes)))
                checksumComboBox.Items.Add(item);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            withoutRepeatsCheckBox.Visibility = Visibility.Visible;
        }

        private void loadFileButton_Click(object sender, RoutedEventArgs e)
        {
            startButton.Visibility = Visibility.Visible;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            saveFileButton.Visibility = Visibility.Visible;
        }
    }
}
