using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab1.ex2._2 {
    internal class CheckInput {
        //Защита от дурака
        public static void ChechInputWord(TextCompositionEventArgs e) {
            foreach (char c in e.Text) {
                if (
                    c < 'A' || c > 'Z' &&
                    c < 'a' || c > 'z' &&
                    c < 'А' || c > 'Я' &&
                    c < 'а' || c > 'я'
                   ) {
                    e.Handled = true;
                    break;
                }
            }
        }

        public static void IsOnlyDigit(TextCompositionEventArgs e) {
            if (!Char.IsDigit(e.Text, 0)) {
                e.Handled = true;
            }
        }
    }
}
