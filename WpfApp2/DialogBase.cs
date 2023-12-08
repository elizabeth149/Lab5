using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    public class DialogBase : Window
    {
        public Button CreateTaskButton { get; protected set; }
        public Button SaveChangesButton { get; protected set; } // Добавлено свойство кнопки для сохранения изменений

        public DialogBase()
        {
            if (Application.Current == null)
            {
                new Application();
            }
            CreateTaskButton = new Button
            {
                Content = "OK"
            };
            SaveChangesButton = new Button // Добавлено создание объекта кнопки для сохранения изменений
            {
                Content = "Save Changes"
            };

            InitializeComponent();
            CreateTaskButton.Click += CreateTaskButton_Click;
            SaveChangesButton.Click += SaveChangesButton_Click; // Добавлено событие нажатия для кнопки сохранения изменений
            Content = new StackPanel
            {
                Children =
                {
                    // ...
                    SaveChangesButton // Добавлена кнопка для сохранения изменений
                }
            };
        }

        public void InitializeComponent()
        {
            if (Application.Current != null)
            {
                ApplyTemplate();
                OnInitialized(EventArgs.Empty);
            }
        }

        private void CreateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Добавлен метод для обработки нажатия кнопки сохранения изменений
        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            // Метод может быть пустым или содержать логику сохранения изменений, в зависимости от потребностей
        }
    }

}