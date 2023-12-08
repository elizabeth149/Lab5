using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp2;


namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        public List<Window> OwnedWindows { get; set; } = new List<Window>();
        public TaskViewModel viewModel;
        public TaskViewModel ViewModel
        {
            get { return viewModel; }
        }
        public ListView GetTaskListView()
        {
            return taskListView;
        }

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new TaskViewModel();
            taskListView.DataContext = viewModel;
            InitializeSampleData();
        }

        public void TaskListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранный элемент
            Task selectedTask = taskListView.SelectedItem as Task;

            if (selectedTask != null)
            {
                // Отобразим расширенную информацию о задаче в вашем интерфейсе
                titleLabel.Text = $"Название: {selectedTask.Title}";
                priorityLabel.Text = $"Приоритет: {selectedTask.Priority}";
                deadlineLabel.Text = $"Дедлайн: {selectedTask.Deadline.ToShortDateString()}";
                completedLabel.Text = $"Выполнено: {selectedTask.IsCompleted}";

                // Здесь вы также можете вызвать метод для отображения дополнительной информации
                // или выполнения других операций, связанных с выбранной задачей.
                // Например: DisplayAdditionalInfo(selectedTask);
            }
        }


        public void ClearTaskDetails()
        {
            // Очищаем информацию о задаче
            titleLabel.Text = string.Empty;
            priorityLabel.Text = string.Empty;
            deadlineLabel.Text = string.Empty;
            completedLabel.Text = string.Empty;
        }

        public void InitializeSampleData()
        {
            // Initialize sample tasks for testing
            viewModel.Tasks = new ObservableCollection<Task>
            {
                new Task { Title = "Task 1", Priority = 1, Deadline = DateTime.Now.AddDays(1), IsCompleted = false },
                new Task { Title = "Task 2", Priority = 2, Deadline = DateTime.Now.AddDays(2), IsCompleted = false },
                new Task { Title = "Task 3", Priority = 3, Deadline = DateTime.Now.AddDays(3), IsCompleted = false }
            };
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(TaskViewModel.Tasks))
                {
                    // Дополнительный код для обработки изменений в списке задач
                }
            };

        }



        public void EditTask_Click(object sender, RoutedEventArgs e)
        {
            // Обработчик EditTask_Click
            var selectedTask = taskListView.SelectedItem as Task;
            if (selectedTask != null)
            {
                // Открываем диалог редактирования, передавая выбранную задачу
                var editDialog = new TaskEditDialog(selectedTask);
                OwnedWindows.Add(editDialog); // Добавить созданное окно в список

                editDialog.ShowDialog();
            }
        }



        public void CreateTask_Click(object sender, RoutedEventArgs e)
        {
            var createDialog = new TaskCreateDialog();
            createDialog.ShowDialog();
            if (createDialog.DialogResult == true)
            {
                var newTask = createDialog.CreatedTask;
                viewModel.Tasks.Add(newTask);

                // Добавьте следующую строку
                viewModel.OnPropertyChanged(nameof(TaskViewModel.Tasks));
            }
        }
        public void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTask = taskListView.SelectedItem as Task;
            if (selectedTask != null)
            {
                var editDialog = new TaskEditDialog(selectedTask);
                editDialog.ShowDialog();

                // После редактирования задачи в диалоговом окне,
                // нужно обновить отображение на главном окне
                if (editDialog.DialogResult == true)
                {
                    // Обновляем отображение
                    taskListView.Items.Refresh();
                }
            }
        }
    }
}

