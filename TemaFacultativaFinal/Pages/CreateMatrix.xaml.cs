using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for CreateMatrix.xaml
    /// </summary>
    public partial class CreateMatrix : Page
    {
        public CreateMatrix()
        {
            InitializeComponent();
        }

        private void createMatrix_Click(object sender, RoutedEventArgs e)
        {
            if(!Grammar.isMatrixCreated)
            {
                if (Grammar.isGrammarDisjoint)
                {
                    Grammar.createMatrix();
                    _matrixTxtb.Text = Grammar.showMatrix();

                    
                        
                }
                else
                {
                    _matrixTxtb.Text = "The sets are not disjoint! Please reset and load another Grammar!";
                }
            }
        }
    }
}
