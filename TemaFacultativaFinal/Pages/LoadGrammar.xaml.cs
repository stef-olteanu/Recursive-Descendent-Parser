using System;
using System.Collections.Generic;
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
    /// Interaction logic for LoadGrammar.xaml
    /// </summary>
    public partial class LoadGrammar : Page
    {
        public LoadGrammar()
        {
            InitializeComponent();
           
        }

        private void loadGrammar_Click(object sender, RoutedEventArgs e)
        {
            ReadFromFile _filereader = new ReadFromFile();
            List<String> _fileGrammar = new List<String>();
            _fileGrammar = _filereader.ReadLines();
            if(!Grammar.isCreated)
                Grammar.CreateGrammar(_fileGrammar);
            _loadGrammarTxtb.Text =  Grammar.ShowGrammar();
        }
    }
}
