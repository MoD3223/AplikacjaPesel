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

        private void WykrytoBlad()
        {
            txtBlockCalosc.Visibility = Visibility.Visible;
            txtBlockCalosc.Foreground = new SolidColorBrush(Colors.Red);
            txtBlockCalosc.Text = "Znaleziono bledy! Sprawdz swoj PESEL";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String tekst;
            tekst = txtBoxPESEL.Text.ToString();
            if (PoprawnyPesel(tekst))
            {
                for (int i = 0; i < 11; i++)
                {
                    PESELByte[i] = byte.Parse(tekst.Substring(i, 1));
                }
                if (!checkDay() || !checkMonth())
                {
                    WykrytoBlad();

                }
                if (getSex() == "---")
                {
                    WykrytoBlad();
                }
                if (!checkSum())
                {
                    WykrytoBlad();
                }


                if (checkDay() & checkMonth() & getSex() != "---" & checkSum())
                {

                    txtBlockRok.Visibility = Visibility.Visible;
                    txtBlockRok.Foreground = new SolidColorBrush(Colors.Green);
                    txtBlockRok.Text = $"Twoja data urodzenia to {getBirthYear()}-{getBirthMonth()}-{getBirthDay()} (Format yyyy-mm-dd)";


                    txtBlockGender.Visibility = Visibility.Visible;
                    txtBlockGender.Foreground = new SolidColorBrush(Colors.Green);
                    txtBlockGender.Text = $"Twoja plec to {getSex()}";

                    txtBlockSuma.Visibility = Visibility.Visible;
                    txtBlockSuma.Foreground = new SolidColorBrush(Colors.Green);
                    txtBlockSuma.Text = "Poprawna suma kontrolna";


                    txtBlockCalosc.Visibility = Visibility.Visible;
                    txtBlockCalosc.Foreground = new SolidColorBrush(Colors.Green);
                    txtBlockCalosc.Text = "Wpisano poprawny PESEL";
                }
                else
                {
                    WykrytoBlad();
                }

            }

        }


        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UsunLitery();
        }

        private void txtBoxPESEL_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (txtBlockRok != null & txtBlockGender != null & txtBlockSuma != null & txtBlockCalosc != null)
            {

                txtBlockRok.Visibility = Visibility.Hidden;
                txtBlockGender.Visibility = Visibility.Hidden;
                txtBlockSuma.Visibility = Visibility.Hidden;
                txtBlockCalosc.Visibility = Visibility.Hidden;

            }
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

        public int getBirthMonth()
        {
            int month;
            month = 10 * PESELByte[2];
            month += PESELByte[3];
            if (month > 80 && month < 93)
            {
                month -= 80;
            }
            else if (month > 20 && month < 33)
            {
                month -= 20;
            }
            else if (month > 40 && month < 53)
            {
                month -= 40;
            }
            else if (month > 60 && month < 73)
            {
                month -= 60;
            }
            return month;
        }
        public int getBirthDay()
        {
            int day;
            day = 10 * PESELByte[4];
            day += PESELByte[5];
            return day;
        }

        public String getSex()
        {
                if (PESELByte[9] % 2 == 1)
                {
                    return "Mezczyzna";
                }
                else if (PESELByte[9] % 2 == 0)
                {
                    return "Kobieta";
                }
                else
                {
                return "---";
                }
        }

        private bool checkSum()
        {
            int sum = 1 * PESELByte[0] +
            3 * PESELByte[1] +
            7 * PESELByte[2] +
            9 * PESELByte[3] +
            1 * PESELByte[4] +
            3 * PESELByte[5] +
            7 * PESELByte[6] +
            9 * PESELByte[7] +
            1 * PESELByte[8] +
            3 * PESELByte[9];
            sum %= 10;
            sum = 10 - sum;
            sum %= 10;

            if (sum == PESELByte[10])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkMonth()
        {
            int month = getBirthMonth();
            int day = getBirthDay();
            if (month > 0 && month < 13)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkDay()
        {
            int year = getBirthYear();
            int month = getBirthMonth();
            int day = getBirthDay();
            if ((day > 0 && day < 32) &&
            (month == 1 || month == 3 || month == 5 ||
            month == 7 || month == 8 || month == 10 ||
            month == 12))
            {
                return true;
            }
            else if ((day > 0 && day < 31) &&
            (month == 4 || month == 6 || month == 9 ||
            month == 11))
            {
                return true;
            }
            else if ((day > 0 && day < 30 && leapYear(year)) ||
            (day > 0 && day < 29 && !leapYear(year)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool leapYear(int year)
        {
            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                return true;
            else
                return false;
        }
    }
}
