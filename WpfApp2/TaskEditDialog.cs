using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    public partial class TaskEditDialog : DialogBase
    {
        public Task EditedTask { get; private set; }

        public TaskEditDialog(Task taskToEdit)
        {
            InitializeComponent();
            EditedTask = taskToEdit; // Инициализируем задачу для редактирования при создании диалога
            DataContext = EditedTask; // Устанавливаем контекст данных для привязки к элементам управления в XAML
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true; // Устанавливаем DialogResult в true при нажатии кнопки "Save Changes"
            Close(); // Закрываем диалог
        }
    }
}
