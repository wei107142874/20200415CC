using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfTest
{

    public class Person : INotifyPropertyChanged
    {
        private int age;

        public string Name { get; set; }

        public bool Gender { get; set; }

        public int Age
        {
            get => age;
            set
            {
                age = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Age"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Person person = new Person()
        {
            Age = 12,
            Name = "张三"
        };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Content = "水域为是猪";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Person> ps = new List<Person>();
            ps.Add(new Person { Age=22,Name="这是", Gender=false });
            ps.Add(new Person { Age = 12, Name = "这是1", Gender=true });
            dg.AutoGenerateColumns = false;//禁止自动创建列
            dg.IsReadOnly = true;//只读
            dg.CanUserAddRows = false;//不允许添加行
            dg.ItemsSource = ps;


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
