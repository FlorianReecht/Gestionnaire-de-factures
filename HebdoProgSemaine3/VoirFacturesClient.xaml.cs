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

namespace HebdoProgSemaine3
{
    /// <summary>
    /// Logique d'interaction pour VoirFacturesClient.xaml
    /// </summary>
    public partial class VoirFacturesClient : Window
    {
        public VoirFacturesClient()
        {
            InitializeComponent();
        }
        private void goBackToMain(object sender, RoutedEventArgs e)
        {
            new ChoixClient().Show();
            this.Close();
        }

    }
}
