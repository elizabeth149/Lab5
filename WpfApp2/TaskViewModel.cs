using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Task> tasks;

        public ObservableCollection<Task> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Новый метод для уведомления об изменении свойства
        public void NotifyTaskUpdated(Task task)
        {
            OnPropertyChanged(nameof(Tasks));
        }
    }
}
