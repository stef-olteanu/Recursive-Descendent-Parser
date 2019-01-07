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
    /// Interaction logic for Compiler.xaml
    /// </summary>
    public partial class Compiler : Page
    {
        public Compiler()
        {
            InitializeComponent();
        }

        private void compile_Click(object sender, RoutedEventArgs e)
        {
            _compilerTxtb.Text = "The code has been compiled and executed!";
            BackCompiler compiler = new BackCompiler(Grammar._code);
            compiler.Compile();
        }
    }
}
