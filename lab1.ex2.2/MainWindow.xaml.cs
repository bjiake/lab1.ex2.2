using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
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
using System.Collections;
//using LINQtoCSV;//Через это нельзя редактировать :(
using Faker;//Генерация данных

namespace lab1.ex2._2 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        //Путь к файлу csv
        static string pathCsvFile = "Data.csv";
        public MainWindow() {
            InitializeComponent();
        }

        //Ввод только букв
        private void userLastName_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            CheckInput.ChechInputWord(e);
        }

        //Ввод только букв
        private void userCity_PreviewTextInput(object sender, TextCompositionEventArgs e) {
             CheckInput.ChechInputWord(e);
        }

        //Ввод только чисел
        private void userAge_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            CheckInput.IsOnlyDigit(e);
        }
        

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на пустые занчения
            if (String.IsNullOrEmpty(userLastName.Text) || String.IsNullOrEmpty(userAge.Text) || String.IsNullOrEmpty(userCity.Text)) {
                MessageBox.Show(@" Ошибка: Введите данные");
                return;
            }

            string lastName = userLastName.Text.Trim();
            string city = userCity.Text.Trim();
            int age = Convert.ToInt32(userAge.Text.Trim());

            #region Проверка на корректность введенных данных
            if (lastName.Length < 2)
            {
                userLastName.ToolTip = "Это поле введено не корректно!";
                userLastName.Background = Brushes.DarkRed;
            }
            else
            {
                userLastName.ToolTip = null;
                userLastName.Background = Brushes.Transparent;
            }

            if (city.Length < 3)
            {
                userCity.ToolTip = "Это поле введено не корректно!";
                userCity.Background = Brushes.DarkRed;
            }
            else
            {
                userCity.ToolTip = null;
                userCity.Background = Brushes.Transparent;
            }

            if (age > 100 || age < 1)
            {
                userAge.ToolTip = "Это поле введено не корректно!";
                userAge.Background = Brushes.DarkRed;
            }
            else
            {
                userAge.ToolTip = null;
                userAge.Background = Brushes.Transparent;
            }
            #endregion
            //Сохранение данных
            SaveData(lastName, city, age);
        }

        private static void SaveData(string lastName, string city, int age) {
            //Добавление новых данных в файл
            File.AppendAllText(pathCsvFile, $"{lastName},{city},{age}\n");
            #region linqToCsv
            //var userList = new List<User>{
            //    new User { LastName = lastName, City = city, Age = age }
            //};

            //var csvFileDescrition = new CsvFileDescription
            //{
            //    FirstLineHasColumnNames = true,
            //    SeparatorChar = ','
            //};

            //var csvContext = new CsvContext();


            //csvContext.Write(userList, pathCsvFile, csvFileDescrition);
            #endregion
            MessageBox.Show("Данные загружены!");
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e) {
            #region Проверка на пустые занчения
            if (String.IsNullOrEmpty(CityLoad.Text)) {
                MessageBox.Show(@" Ошибка: Введите данные");
                return;
            }
            string city = CityLoad.Text.Trim();

            if (city.Length < 3)
            {
                CityLoad.ToolTip = "Это поле введено не корректно!";
                CityLoad.Background = Brushes.DarkRed;
            }
            else
            {
                userCity.ToolTip = null;
                userCity.Background = Brushes.Transparent;
            }
            #endregion
            #region linqToCsv
            //var csvFileDescrition = new CsvFileDescription {
            //    FirstLineHasColumnNames = true,
            //    IgnoreUnknownColumns = true,
            //    SeparatorChar = ',',
            //    UseFieldIndexForReadingData = false,
            //};

            //var csvContext = new CsvContext();
            //var users = csvContext.Read<User>(pathCsvFile, csvFileDescrition);

            //foreach (var user in users) {
            //    if(user.City == city) {
            //        ResultLabel.Content = ($"{user.LastName} | {user.Age}");
            //    }
            //}
            #endregion
            //Чтение файла
            var lines = File.ReadAllLines(pathCsvFile);
            var users = new List<User>();

            //Получение данных из файла
            foreach (var line in lines) {

                var values = line.Split(',');

                if (values.Length == 3) {
                    var user = new User() { LastName = values[0], City = values[1], Age = Convert.ToInt32(values[2]) };
                    users.Add(user);
                }
            }

            string result = string.Empty;
            ResultListBox.Items.Clear();
            int sumAge = 0;
            int count = 0;

            //Добавление результата в лист
            foreach (var user in users) {
                if(user.City == city){
                    ResultListBox.Items.Add("Фамилия:" + user.LastName + "\tВозраст:" + user.Age);
                    sumAge += user.Age;
                    count++;
                }    
            }
            //Вывод при выявлении жителей в этом городе
            if(count > 0) {
                MessageBox.Show($"Средний возраст жителей этого города равен:{Math.Round((decimal)sumAge / count, 0)}");
            }
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e) {
            //генерация случайных данных с библиотеки
            SaveData(Faker.Name.Last(), Faker.Address.City(), Faker.RandomNumber.Next(1, 100));
        }
    }
    [Serializable]
    public class User {
        //[CsvColumn(Name = "LastName", FieldIndex = 1)]
        public string? LastName { get; set; }
        //[CsvColumn(Name = "City", FieldIndex = 2)]
        public string? City { get; set; }
        //[CsvColumn(Name = "Age", FieldIndex = 3)]
        public int Age { get; set; }    
    }
}
