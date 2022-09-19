using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace lab1.ex2._2 {
    internal class CheckInput {
        //Защита от дурака
        public static void ChechInputWordLastName(TextCompositionEventArgs e, object sender) {
            var textBox = sender as TextBox;
            Regex regex = new Regex("^[^А-Яа-яa-zA-Z\\-\\`]+$");
            if (
                ( textBox.Text.Contains('-') && e.Text == "-" ) ||
                ( textBox.Text == "" && e.Text == "-" ) &&
                ( textBox.Text.Contains('`') && e.Text == "`" ) ||
                ( textBox.Text == "" && e.Text == "`" )
            ) {
                e.Handled = true;
            } else {
                e.Handled = regex.IsMatch(e.Text);
            }
        }
        public static void ChechInputWordCity(TextCompositionEventArgs e, object sender) {
            var textBox = sender as TextBox;
            Regex regex = new Regex("^[^А-Яа-яa-zA-Z\\-]+$");
            if (
                ( textBox.Text.Contains('-') && e.Text == "-" ) ||
                ( textBox.Text == "" && e.Text == "-" )
            ) {
                e.Handled = true;
            } else {
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        public static void IsOnlyDigit(TextCompositionEventArgs e) {
            if (!Char.IsDigit(e.Text, 0)) {
                e.Handled = true;
            }
        }
    }
}
