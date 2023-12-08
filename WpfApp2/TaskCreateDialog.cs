using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace WpfApp2
{
    public partial class TaskCreateDialog : DialogBase
    {
        public Task CreatedTask { get; private set; }

        // Добавлены элементы управления для ввода свойств задачи
        public TextBox TitleTextBox { get; private set; }
        public TextBox PriorityTextBox { get; private set; }
        public DatePicker DeadlineDatePicker { get; private set; }
        public CheckBox IsCompletedCheckBox { get; private set; }

        public TaskCreateDialog()
        {
            InitializeComponent();
            CreatedTask = new Task();

            // Инициализация элементов управления
            TitleTextBox = new TextBox();
            PriorityTextBox = new TextBox();
            DeadlineDatePicker = new DatePicker();
            IsCompletedCheckBox = new CheckBox();
             CreateTaskButton = new Button // Изменение: добавлено создание объекта кнопки
            {
                Content = "Create Task"
            };

            // Добавление элементов управления на форму
            Content = new StackPanel
            {
                Children =
                {
                    new Label { Content = "Title:" },
                    TitleTextBox,
                    new Label { Content = "Priority:" },
                    PriorityTextBox,
                    new Label { Content = "Deadline:" },
                    DeadlineDatePicker,
                    new Label { Content = "Is Completed:" },
                    IsCompletedCheckBox,
                    CreateTaskButton // Переиспользуем кнопку CreateTaskButton из базового класса
                }
            };
        }

        private void CreateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Заполнение свойств задачи значениями из элементов управления
            CreatedTask.Title = TitleTextBox.Text;
            int priority;
            if (int.TryParse(PriorityTextBox.Text, out priority))
            {
                CreatedTask.Priority = priority;
            }
            CreatedTask.Deadline = DeadlineDatePicker.SelectedDate ?? DateTime.Now; // Если не выбрана дата, устанавливаем текущую
            CreatedTask.IsCompleted = IsCompletedCheckBox.IsChecked ?? false;

            DialogResult = true; // Устанавливаем DialogResult в true при нажатии кнопки "Create Task"
            Close(); // Закрываем диалог
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow?.ViewModel?.OnPropertyChanged(nameof(TaskViewModel.Tasks));

        }
    }
}
