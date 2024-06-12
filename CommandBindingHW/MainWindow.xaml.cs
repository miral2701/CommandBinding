using System.ComponentModel;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;

namespace CommandBindingHW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyCounter counter= new MyCounter();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = counter;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();


        }
        
        void timer_Tick(object sender, EventArgs e)
        {
            if (counter.Flag)
            {
                counter.Counter += 1;

            }
            else
            {
                counter.Counter -= 1;

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("При нажатии на стрелку вверх счетчик увеличивается\nПри нажатии на стрелку внис счетчик уменьшается","",MessageBoxButton.OK,MessageBoxImage.Information);
        }
    }

    class MyCounter : INotifyPropertyChanged
    {
        private readonly Command upCommand;
        private readonly Command downCommand;

        public ICommand UpCommand => upCommand;
        public ICommand DownCommand => downCommand;
        public MyCounter()
        {
           
            upCommand = new DelegateCommand(
            () => Flag=true
            );
            downCommand = new DelegateCommand(
            () => Flag=false
            );
            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals(nameof(Flag)))
                {
                    upCommand.RaiseCanExecuteChanged();
                    downCommand.RaiseCanExecuteChanged();
                }
            };
        }

        private int counter = 0;
        private bool flag = true;

        public int Counter
        {
            get { return counter; }
            set { counter = value; NotifyPropertyChanged(); }
        }

        public bool Flag
        {
            get { return flag; }
            set { flag = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}