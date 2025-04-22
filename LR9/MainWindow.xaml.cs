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
using static LR9.MainWindow;

namespace LR9
{
    public partial class MainWindow : Window
    {
        //для хранения данных формы
        public class FormData
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string BirthDate { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public string ApartmentNumber { get; set; }
        }

        public static FormData Data = new FormData();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Page1());
        }

        //1
        public class Page1 : Page
        {
            public Page1()
            {
                var stack = new StackPanel { Margin = new Thickness(20) };

                stack.Children.Add(new TextBlock { Text = "Личные данные", FontSize = 20, Margin = new Thickness(0, 0, 0, 20) });

                stack.Children.Add(new Label { Content = "Имя:" });
                var firstName = new TextBox();
                stack.Children.Add(firstName);

                stack.Children.Add(new Label { Content = "Фамилия:", Margin = new Thickness(0, 10, 0, 0) });
                var lastName = new TextBox();
                stack.Children.Add(lastName);

                stack.Children.Add(new Label { Content = "Дата рождения:", Margin = new Thickness(0, 10, 0, 0) });
                var birthDate = new DatePicker();
                stack.Children.Add(birthDate);

                var nextBtn = new Button { Content = "Далее", Margin = new Thickness(0, 20, 0, 0) };
                nextBtn.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(firstName.Text) ||
                        string.IsNullOrWhiteSpace(lastName.Text) ||
                        birthDate.SelectedDate == null)
                    {
                        MessageBox.Show("Заполните все поля!");
                        return;
                    }

                    Data.FirstName = firstName.Text;
                    Data.LastName = lastName.Text;
                    Data.BirthDate = birthDate.SelectedDate.Value.ToShortDateString();

                    NavigationService.Navigate(new Page2());
                };
                stack.Children.Add(nextBtn);

                Content = stack;
            }
        }
        //2
        public class Page2 : Page
        {
            public Page2()
            {
                var stack = new StackPanel { Margin = new Thickness(20) };

                stack.Children.Add(new TextBlock { Text = "Контактные данные", FontSize = 20, Margin = new Thickness(0, 0, 0, 20) });

                stack.Children.Add(new Label { Content = "Email:" });
                var email = new TextBox();
                stack.Children.Add(email);

                stack.Children.Add(new Label { Content = "Телефон:", Margin = new Thickness(0, 10, 0, 0) });
                var phone = new TextBox();
                stack.Children.Add(phone);

                var backBtn = new Button { Content = "Назад", Margin = new Thickness(0, 20, 0, 0) };
                backBtn.Click += (s, e) => NavigationService.GoBack();
                stack.Children.Add(backBtn);

                var nextBtn = new Button { Content = "Далее", Margin = new Thickness(0, 10, 0, 0) };
                nextBtn.Click += (s, e) =>
                {
                    if (!Regex.IsMatch(email.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") ||
                        string.IsNullOrWhiteSpace(phone.Text))
                    {
                        MessageBox.Show("Некорректный email или телефон!");
                        return;
                    }

                    Data.Email = email.Text;
                    Data.Phone = phone.Text;

                    NavigationService.Navigate(new Page3());
                };
                stack.Children.Add(nextBtn);

                Content = stack;
            }
        }
        // Страница 3: Адрес
        public class Page3 : Page
        {
            public Page3()
            {
                var stack = new StackPanel { Margin = new Thickness(20) };

                stack.Children.Add(new TextBlock { Text = "Адрес", FontSize = 20, Margin = new Thickness(0, 0, 0, 20) });

                stack.Children.Add(new Label { Content = "Город:" });
                var city = new TextBox();
                stack.Children.Add(city);

                stack.Children.Add(new Label { Content = "Улица:", Margin = new Thickness(0, 10, 0, 0) });
                var street = new TextBox();
                stack.Children.Add(street);

                stack.Children.Add(new Label { Content = "Дом:", Margin = new Thickness(0, 10, 0, 0) });
                var house = new TextBox();
                stack.Children.Add(house);

                stack.Children.Add(new Label { Content = "Квартира (необязательно):", Margin = new Thickness(0, 10, 0, 0) });
                var apartment = new TextBox();
                stack.Children.Add(apartment);

                var backBtn = new Button { Content = "Назад", Margin = new Thickness(0, 20, 0, 0) };
                backBtn.Click += (s, e) => NavigationService.GoBack();
                stack.Children.Add(backBtn);

                var submitBtn = new Button { Content = "Отправить", Margin = new Thickness(0, 10, 0, 0) };
                submitBtn.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(city.Text) ||
                        string.IsNullOrWhiteSpace(street.Text) ||
                        string.IsNullOrWhiteSpace(house.Text))
                    {
                        MessageBox.Show("Заполните город, улицу и дом!");
                        return;
                    }

                    Data.City = city.Text;
                    Data.Street = street.Text;
                    Data.HouseNumber = house.Text;
                    Data.ApartmentNumber = apartment.Text;

                    MessageBox.Show(
                        $"Данные:\n\n" +
                        $"Имя: {Data.FirstName}\n" +
                        $"Фамилия: {Data.LastName}\n" +
                        $"Дата рождения: {Data.BirthDate}\n" +
                        $"Email: {Data.Email}\n" +
                        $"Телефон: {Data.Phone}\n" +
                        $"Адрес: {Data.City}, {Data.Street}, д.{Data.HouseNumber}" +
                        (string.IsNullOrEmpty(Data.ApartmentNumber) ? "" : $", кв.{Data.ApartmentNumber}"),
                        "Спасибо!"
                    );
                };
                stack.Children.Add(submitBtn);

                Content = stack;
            }
        }
    }
}
