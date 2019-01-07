using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaFacultativaFinal
{
    class ReadFromFile

    {
        private int _lineCounter { get; set; }
        private List<String> _lines = new List<string>();
        private System.IO.StreamReader _file;

        public ReadFromFile()
        {
            _lineCounter = 0;
            string fasda = Environment.CurrentDirectory + "/" + "Grammar" + "/" + "gramatica3.txt";
            //_file = new System.IO.StreamReader(Environment.CurrentDirectory + "/" + "Grammar" + "/" + "gramatica3.txt");
            _file = new System.IO.StreamReader(Environment.CurrentDirectory + "/" + "Grammar" + "/" + "gramatica3.txt");
            if (_file == null)
            {
                
            }
        }

        ~ReadFromFile()
        {
            _file.Close();
        }


        public List<String> ReadLines()
        {
            String _line;
            while ((_line = _file.ReadLine()) != null)
            {
                _lines.Add(_line);
            }
            return _lines;
        }

    }
}

