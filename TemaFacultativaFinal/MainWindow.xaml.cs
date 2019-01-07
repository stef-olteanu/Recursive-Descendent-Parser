using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TemaFacultativaFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        int _pageContor = 0;
        ObservableCollection<Page> _pages = new ObservableCollection<Page>();
        public MainWindow()
        {
            InitializeComponent();
            
            _pages.Add(new LoadGrammar());
            _pages.Add(new ModifyGrammar());
            _pages.Add(new CreateDirectingSets());
            _pages.Add(new VerifyDisjoint());
            _pages.Add(new CreateMatrix());
            _pages.Add(new GenerateCode());
            _pages.Add(new Compiler());
            MainFrame.Content = _pages[_pageContor];
            
        }

        private void nextRight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _pageContor++;
            if(_pageContor >= _pages.Count())
            {
                _pageContor = 0;
            }
            MainFrame.Content = _pages[_pageContor];
        }

        private void previousLeft_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _pageContor--;
            if(_pageContor < 0)
            {
                _pageContor = _pages.Count() - 1;
            }
            MainFrame.Content = _pages[_pageContor];
        }

        private void _resetGrammar_Click(object sender, RoutedEventArgs e)
        {
            _pages = new ObservableCollection<Page>();
            _pages.Add(new LoadGrammar());
            _pages.Add(new ModifyGrammar());
            _pages.Add(new CreateDirectingSets());
            _pages.Add(new VerifyDisjoint());
            _pages.Add(new CreateMatrix());
            _pages.Add(new GenerateCode());
            _pages.Add(new Compiler());
            _pageContor = 0;
            MainFrame.Content = _pages[_pageContor];
            

            Grammar.resetGrammar();
            

        }

        private void _openFile_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Environment.CurrentDirectory + "/" + "Grammar" + "/" + "gramatica3.txt");
        }
    }
}
