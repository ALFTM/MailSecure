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

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour FlatWindow.xaml
    /// </summary>
    public partial class FlatWindow : Window
    {
        public FlatWindow()
        {
            InitializeComponent();
            DataContext = new WindowViewModel(this);
        }
    }
}
