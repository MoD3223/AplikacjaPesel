using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace AplikacjaPesel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Byte[] PESELByte = new Byte[11];
        public void UsunLitery()
        {
            String tekst;
            tekst = txtBoxPESEL.Text.ToString();
            tekst = Regex.Replace(tekst, "[^0-9]", "");
            txtBoxPESEL.Text = tekst;
        }

        public static bool PoprawnyPesel(string tekst)
        {
            if (string.IsNullOrEmpty(tekst))
            {
                return false;
            }

            if (tekst.Length != 11)
            {
                return false;
            }

            foreach (char c in tekst)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String tekst;
            tekst = txtBoxPESEL.Text.ToString();
            if (PoprawnyPesel(tekst))
            {

            }
            else
            {

            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UsunLitery();
        }

        private void txtBoxPESEL_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (peselCheckTXTBlock != null)
            {
                UsunLitery();
                txtBoxPESEL.SelectionStart = txtBoxPESEL.Text.Length;


                if (txtBoxPESEL.Text.Length == 11)
                {
                    peselCheckTXTBlock.Visibility = Visibility.Visible;
                    peselCheckTXTBlock.Foreground = new SolidColorBrush(Colors.Green);
                    peselCheckTXTBlock.Text = "PESEL zawiera 11 cyfr";
                }
                else if (txtBoxPESEL.Text.Length == 0)
                {
                    peselCheckTXTBlock.Visibility = Visibility.Hidden;
                }
                else
                {
                    peselCheckTXTBlock.Visibility = Visibility.Visible;
                    peselCheckTXTBlock.Foreground = new SolidColorBrush(Colors.Red);
                    peselCheckTXTBlock.Text = "PESEL nie zawiera 11 cyfr";
                }
            }
        }

        public int getBirthYear()
        {
            int year;
            int month;
            year = 10 * PESELByte[0];
            year += PESELByte[1];
            month = 10 * PESELByte[2];
            month += PESELByte[3];
            if (month > 80 && month < 93)
            {
                year += 1800;
            }
            else if (month > 0 && month < 13)
            {
                year += 1900;
            }
            else if (month > 20 && month < 33)
            {
                year += 2000;
            }
            else if (month > 40 && month < 53)
            {
                year += 2100;
            }
            else if (month > 60 && month < 73)
            {
                year += 2200;
            }
            return year;
        }
    }
}
