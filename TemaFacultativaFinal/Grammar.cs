using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaFacultativaFinal
{
    public class Grammar
    {

        #region Zona declarari

        internal static String _startSymbol;
        internal static List<String> _terminals = new List<String>();
        internal static List<String> _nonTerminals = new List<String>();
        internal static List<String> v = new List<String>();
        internal static int _productionRulesCount;
        internal static List<List<String>> _productionRules = new List<List<String>>();
        internal static List<List<String>> _directingSymbols = new List<List<String>>();
        internal static int colNumber;
        internal static int lineNumber;
        internal static string[,] _matrix;
        internal static string _code;
        internal static string followNotRepeat;
        internal static string followRule;

        internal static bool isCreated = false;
        internal static bool isModified = false;
        internal static bool areSetsCreated = false;
        internal static bool isDisjointVerified = false;
        internal static bool isMatrixCreated = false;
        internal static bool isCodeGenerated = false;
        internal static bool isGrammarDisjoint = false;

        #endregion

        internal static void resetGrammar()
        {
            _startSymbol = "";
            _terminals = new List<String>();
            _nonTerminals = new List<String>();
            v = new List<String>();
            _productionRulesCount = 0;
            _productionRules = new List<List<String>>();
            _directingSymbols = new List<List<String>>();
            colNumber = 0;
            lineNumber = 0;
            _matrix = new string[lineNumber, colNumber];
            _code = "";
            Grammar.isCodeGenerated = false;
            Grammar.isCreated = false;
            Grammar.isModified = false;
            Grammar.areSetsCreated = false;
            Grammar.isDisjointVerified = false;
            Grammar.isGrammarDisjoint = false;
            Grammar.isMatrixCreated = false;

        }


        internal static void CreateGrammar(List<String> _grammarFile)
        {
            isCreated = true;
            int count = 0;
            foreach (string element in _grammarFile)
            {
                if (count == 0)
                {
                    _startSymbol = element;
                }

                if (count == 1)
                {
                    int elementCount = 0;
                    while (elementCount != element.Length)
                    {
                        if (element[elementCount] != ' ')
                        {
                            _nonTerminals.Add(element[elementCount].ToString());
                        }
                        elementCount++;
                    }
                }

                if (count == 2)
                {
                    int elementCount = 0;
                    string[] terminals = element.Split(' ');
                    while (elementCount != terminals.Length)
                    {
                        _terminals.Add(terminals[elementCount]);
                        elementCount++;
                    }
                }

                if (count == 3)
                {
                    _productionRulesCount = Int32.Parse(element);
                }

                if (count >= 4)
                {
                    int elementCount = 0;
                    List<String> _productionRule = new List<String>();
                    String[] prodRule = element.Split(' ');
                    while (elementCount != prodRule.Length)
                    {
                        if (prodRule[elementCount] != ":")
                            _productionRule.Add(prodRule[elementCount]);

                        elementCount++;
                    }
                    if(_productionRule[0]!="")
                         _productionRules.Add(_productionRule);
                }
                count++;
            }
            foreach (string _terminal in _terminals)
            {
                v.Add(_terminal);
            }
            foreach (string _nonterminal in _nonTerminals)
            {
                v.Add(_nonterminal);
            }
        }

        internal static string ShowGrammar()
        {
            string _grammar;
            _grammar = "Start symbol: " + _startSymbol + "\n";
            _grammar = _grammar + "Nonterminals: ";
            foreach (string element in _nonTerminals)
            {
                _grammar = _grammar + element + " ";
            }
            _grammar = _grammar + "\n";
            _grammar = _grammar + "Terminals: ";
            foreach (string element in _terminals)
            {
                _grammar = _grammar + element + " ";
            }
            _grammar = _grammar + "\n";
            _grammar = _grammar + "Number of production rules: ";
            _grammar = _grammar + _productionRulesCount.ToString() + "\n";
            _grammar = _grammar + "Production rules: " + "\n";
            foreach (List<String> productionRule in _productionRules)
            {
                foreach (string element in productionRule)
                {
                    _grammar = _grammar + element + " ";
                }
                _grammar = _grammar + "\n";
            }
            return _grammar;

        }

        internal static string showDirectingSet()
        {
            int contor = 0;
            string _setsDir = "";
            foreach (List<String> set in _directingSymbols)
            {
                contor++;
                _setsDir = _setsDir + contor.ToString() + ".";
                foreach (String symbol in set)
                {
                    _setsDir = _setsDir + symbol + " ";
                }
                _setsDir = _setsDir + "\n";
            }
            return _setsDir;
        }


        internal static void ModifyGrammar()
        {
            isModified = true;
            SecondRule();
            FirstRule();
            _productionRules = _productionRules.Distinct().ToList();
        }

        internal static void directingSets()
        {
            areSetsCreated = true;
            foreach (List<String> prodRule in _productionRules)
            {
                if (prodRule[1] != "eps")
                {
                    _directingSymbols.Add(First(prodRule[1]));
                }
                else
                {
                    followNotRepeat = prodRule[0];
                    followRule = prodRule[0];
                    _directingSymbols.Add(Follow(prodRule[0]));
                }
            }
        }

        internal static void FirstRule()
        {
            int loopContor = 0;
            List<List<String>> _productionRuleToDelete = new List<List<String>>();
            while (loopContor != _nonTerminals.Count())   //parcurge fiecare neterminal
            {
                string _nonterminal = _nonTerminals[loopContor];
                List<List<String>> _productionRuleForNonTerminal = new List<List<String>>();
                foreach (List<String> _productionRule in _productionRules)  //scoatem fiecare regula pentru neterminal
                {
                    if (_productionRule.First() == _nonterminal)
                        _productionRuleForNonTerminal.Add(_productionRule);
                }

                bool verif = false;
                foreach (List<String> _productionRule in _productionRuleForNonTerminal)
                {
                    if (_productionRule.First() == _productionRule[1])  //daca regula are recursivitate stanga
                    {
                        verif = true;
                    }
                }

                if (verif == true)  // pentru regulile care au recursivitate stanga
                {
                    String _newNonTerminal;
                    _productionRulesCount++;
                    _newNonTerminal = _nonterminal + _productionRulesCount;  //cream un nou neterminal
                    _nonTerminals.Add(_newNonTerminal); //il adaugam in lista de neterminale
                    foreach (List<String> _productionRule in _productionRuleForNonTerminal)
                    {
                        if (_productionRule.First() == _productionRule[1])  //pentru regula care avea recursivitate stanga din regulile de productie pentru neterminal
                        {
                            List<String> _newProdRule = new List<String>();
                            _newProdRule.Add(_newNonTerminal); //cream noua regula de productie
                            int contor = 2;
                            while (contor != _productionRule.Count()) //punem in regula noua tot ce se afla dupa neterminalul din partea stanga
                            {
                                _newProdRule.Add(_productionRule[contor]);
                                contor++;
                            }
                            _newProdRule.Add(_newNonTerminal); //la final adaugam noul neterminal

                            _productionRules.Add(_newProdRule); //adaugam regula de productie 

                            List<String> _epsProdRule = new List<String>();
                            _epsProdRule.Add(_newNonTerminal); //adaugam o regula de productie vida
                            _epsProdRule.Add("eps");
                            //if(!_productionRules.Contains(_epsProdRule))
                                _productionRules.Add(_epsProdRule);
                            _productionRules.Remove(_productionRule);

                        }
                        else
                            _productionRule.Add(_newNonTerminal); //altfel daca nu are recursivitate stanga pentru restul adaugam nou neterminal la final



                    }
                }
                loopContor++;
            }
        }

        internal static void SecondRule()
        {
            int loopContor = 0;

            while (loopContor != _nonTerminals.Count())
            {
                string _nonterminal = _nonTerminals[loopContor];
                List<List<String>> _productionRuleForNonTerminal = new List<List<String>>();
                foreach (List<String> _productionRule in _productionRules)
                {
                    if (_productionRule.First() == _nonterminal)
                    {
                        _productionRuleForNonTerminal.Add(_productionRule);   //luam regulilele de productie pentru fiecare neterminal
                    }
                }
                if (_productionRuleForNonTerminal.Count() > 1) //daca exista mai mult de una
                {
                    
                    foreach (string element in v)
                    {
                        List<List<String>> _rulesToModify = new List<List<String>>();
                        foreach (List<String> _productionRule in _productionRuleForNonTerminal) //cautam regulile de productie care contin acelasi prim element
                        {
                            if (_productionRule[1] == element)
                                _rulesToModify.Add(_productionRule);
                        }
                        if (_rulesToModify.Count() > 1) //daca acele reguli sunt cel putin 2
                        {
                            int _elementContor = 1;
                            int contor2 = 1;
                            bool verif = true;
                            while (_elementContor + 1 < _rulesToModify[0].Count()) //cat timp putem parcurge prima regula 
                            {
                                _elementContor++; //contorul pentru interiorul regulilor
                                while (contor2 != _rulesToModify.Count()) //parcurgem celelalte reguli
                                {
                                    if (_rulesToModify[contor2].Count() == _elementContor) //daca se termina a doua regula
                                    {
                                       // verif = false;
                                        break;
                                    }
                                    if (_elementContor < _rulesToModify[0].Count()) 
                                    {
                                        if (_rulesToModify[contor2][_elementContor] != _rulesToModify[0][_elementContor]) //daca gasim un element diferit
                                        {
                                            verif = false;
                                            break;
                                        }
                                    }
                                    contor2++;
                                }
                                if (verif == false)
                                    break;
                                contor2 = 1;
                            }
                            //if (_rulesToModify[0].Count!=2)
                            //_elementContor--;
                            if (verif == false) //revenim la ultimul element la fel
                                _elementContor--;
                            List<String> _newProdRule = new List<String>();
                            _productionRulesCount++;
                            String _newNonTerminal = _productionRuleForNonTerminal[0][0] + _productionRulesCount;
                            _nonTerminals.Add(_newNonTerminal);
                            _newProdRule.Add(_productionRuleForNonTerminal[0][0]);
                            int count = 1;
                  
                            while (count <= _elementContor) //pentru neterminalul curent adaugam pana la ultimul element gasit
                            {
                               // if(count<_rulesToModify[0].Count)
                                _newProdRule.Add(_rulesToModify[0][count]);
                                count++;
                            }
                            _newProdRule.Add(_newNonTerminal);
                            if(_newProdRule[0] != "")
                                 _productionRules.Add(_newProdRule);


                            //_productionRulesCount--;
                            //_newNonTerminal = _productionRuleForNonTerminal[0][0] + _productionRulesCount;
                            //_nonTerminals.Add(_newNonTerminal);
                            
                            foreach (List<String> _rule in _rulesToModify)
                            {
                                _newProdRule = new List<string>();
                                _newProdRule.Add(_newNonTerminal);
                                _productionRulesCount++;
                                count = _elementContor + 1;

                                if (count == _rule.Count()) //daca s-a terminat regula de productie dar celelalte mai au adaugam regula cu epsilon
                                {
                                    _newProdRule.Add("eps");
                                    if (_newProdRule[0] != "" && !_productionRules.Contains(_newProdRule))
                                    {
                                        _productionRules.Add(_newProdRule);
                                    }
                                    continue;
                                }
                                 
                                    while (count != _rule.Count()) //daca nu adaugam ce era dupa ultimul element gasit
                                    {
                                        if(count < _rule.Count())
                                            _newProdRule.Add(_rule[count]);
                                        count++;
                                    }
                                if (_newProdRule[0] != "")
                                    _productionRules.Add(_newProdRule);
                            }

                            List<List<String>> _toRemove = new List<List<String>>();
                            foreach (List<String> _prodRule in _rulesToModify)
                            {
                                _productionRules.Remove(_prodRule);
                                _productionRulesCount--;
                                
                            }
                            
                        }


                    }
                }
                loopContor++;
            }

        }

        internal static bool IsTerminal(String _terminal)
        {
            foreach (String element in _terminals)
            {
                if (_terminal == element)
                    return true;

            }
            return false;
        }

        internal static bool IsNonTerminal(String _nonTerminal)
        {
            foreach (String element in _nonTerminals)
            {
                if (element == _nonTerminal)
                    return true;
            }
            return false;
        }

        internal static List<String> First(String symbol)
        {
            List<String> _directingLot = new List<String>();
            if (IsTerminal(symbol)) //daca simbolul este terminal este adaugat direct in multime
            {
                if (!_directingLot.Contains(symbol))
                    _directingLot.Add(symbol);
                return _directingLot;
            }
            else
                foreach (List<String> _productionRule in _productionRules)
                {
                    if (_productionRule.First() == symbol)
                    {
                        if (IsTerminal(_productionRule[1])) 
                        {
                            if (!_directingLot.Contains(_productionRule[1]))
                                _directingLot.Add(_productionRule[1]);
                        }

                        if (_productionRule[1] == "eps" && _productionRule[0] != followNotRepeat)
                        {
                            List<String> _followList = new List<String>();
                            string aux = followNotRepeat;
                            followNotRepeat = symbol;
                            _followList = Follow(symbol);
                            followNotRepeat = aux;
                            foreach (string element in _followList)
                            {
                                if (!_directingLot.Contains(element))
                                    _directingLot.Add(element);
                            }
                        }

                        if (IsNonTerminal(_productionRule[1]))
                        {
                            List<String> _firstList = new List<String>();
                            _firstList = First(_productionRule[1]);
                            foreach (string element in _firstList)
                            {
                                if (!_directingLot.Contains(element))
                                    _directingLot.Add(element);
                            }
                        }

                    }
                }

            return _directingLot;
        }

        internal static List<String> Follow(String _nonTerminal)
        {
            
            List<String> followList = new List<String>();
            if (_nonTerminal == _startSymbol)
            {
                followList.Add("$");
            }
            foreach (List<String> _prodRule in _productionRules)
            {
                int count = 1;
                while (count != _prodRule.Count())
                {
                    if (_nonTerminal == _prodRule[count])
                    {
                        if (count + 1 == _prodRule.Count() || _prodRule[count+1]=="")
                        {
                            List<String> follow = new List<String>();
                            if (_prodRule[0] != _nonTerminal && _prodRule[0]!=followNotRepeat && _prodRule[0]!=followRule)
                            {
                                followRule = _prodRule[_prodRule.Count-1];
                                follow = Follow(_prodRule[0]);
                                foreach (String element in follow)
                                {
                                    if (!followList.Contains(element))
                                        followList.Add(element);
                                }
                            }

                        }

                        if (count + 1 != _prodRule.Count())
                        {
                            if (IsTerminal(_prodRule[count + 1]))
                            {
                                followList.Add(_prodRule[count + 1]);
                            }
                        }

                        if (count + 1 != _prodRule.Count())
                        {
                            if (IsNonTerminal(_prodRule[count + 1]))
                            {
                                List<String> follow = new List<String>();
                                follow = First(_prodRule[count + 1]);
                                foreach (String element in follow)
                                {
                                    if (!followList.Contains(element))
                                        followList.Add(element);
                                }
                            }
                        }

                    }
                    count++;
                }
            }


            return followList;
        }

        internal static bool verifyDisjoint()
        {
            isDisjointVerified = true;
            foreach (String _nonterminal in _nonTerminals)
            {
                List<int> _indices = new List<int>();
                foreach (List<String> _prodRule in _productionRules)
                {
                    if (_prodRule[0] == _nonterminal)
                    {
                        _indices.Add(_productionRules.IndexOf(_prodRule));
                    }
                }
                if (_indices.Count() > 1)
                {
                    int count = 0;
                    List<List<String>> _toVerifiy = new List<List<String>>();
                    foreach (int index in _indices)
                    {
                        count = 0;
                        foreach (List<String> element in _directingSymbols)
                        {
                            if (index == count)
                                _toVerifiy.Add(element);
                            count++;
                        }
                    }

                    count = 0;
                    int count1 = 0;
                    while (count != _toVerifiy.Count())
                    {
                        count1 = count + 1;
                        while (count1 != _toVerifiy.Count())
                        {
                            if (_toVerifiy[count].Intersect(_toVerifiy[count1]).Any())
                            {
                                return false;
                            }

                            count1++;
                        }
                        count++;
                    }
                }
            }
            return true;
        }

        internal static int getRuleNumber(String nonTerminal, String _terminal)
        {
            int ruleCount = 0;
            int directCount = 0;
            foreach (List<String> _prodRule in _productionRules)
            {
                ruleCount++;
                directCount = 0;
                if (_prodRule.First() == nonTerminal)
                {
                    foreach (List<String> _set in _directingSymbols)
                    {
                        directCount++;
                        if (_set.Contains(_terminal) && directCount == ruleCount)
                        {
                            return directCount;
                        }
                    }
                }
            }
            return 0;
        }

        internal static void createMatrix()
        {
            colNumber = _terminals.Count() + 2;

            lineNumber = _terminals.Count() + _nonTerminals.Count() + 2;
            _matrix = new string[lineNumber, colNumber];
            _matrix[0, 0] = "#";
            int terminalCount = 0;
            int nonTerminalCount = 0;
            int line = 0;
            int column;
            for (column = 1; column < colNumber - 1; column++)
            {
                if (line == 0)
                {
                    _matrix[line, column] = _terminals[terminalCount];
                    terminalCount++;

                }
            }
            _matrix[line, column] = "$";
            column = 0;
            terminalCount = 0;
            for (line = 1; line < lineNumber - 1; line++)
            {
                if (nonTerminalCount != _nonTerminals.Count())
                {
                    _matrix[line, column] = _nonTerminals[nonTerminalCount];
                    nonTerminalCount++;
                }
                else
                {
                    _matrix[line, column] = _terminals[terminalCount];
                    terminalCount++;
                }
            }
            _matrix[line, column] = "$";
            int ruleNumber;
            for (line = 1; line < lineNumber; line++)
            {
                for (column = 1; column < colNumber; column++)
                {
                    ruleNumber = getRuleNumber(_matrix[line, 0], _matrix[0, column]);
                    if (ruleNumber != 0)
                    {
                        _matrix[line, column] = ruleNumber.ToString();
                    }
                    else
                        _matrix[line, column] = " ";
                    if (_matrix[line, 0] == _matrix[0, column] && _matrix[line, 0] != "$")
                    {
                        _matrix[line, column] = "P";
                    }
                    if (_matrix[line, 0] == "$" && _matrix[0, column] == "$")
                    {
                        _matrix[line, column] = "A";
                    }
                }
            }

        }


        internal static string showMatrix()
        {
            string _string = "\n";
            _string = _string + "The rule matrix is: " + "\n";
            for (int i = 0; i < lineNumber; i++)
            {
                for (int j = 0; j < colNumber; j++)
                {
                    _string = _string + _matrix[i, j] + "     ";
                }
                _string = _string + "\n"; 
            }
            return _string;
        }

        internal static void GenerateCode()
        {
            isCodeGenerated = true;
            _code = "using System; " +
                   "using System.Collections.Generic; " +
                   "using System.Linq; using System.Text; " +
                   "using System.Threading.Tasks;" +
                   "namespace GeneratedCode" +
                   "{" +
                     "class Program" +
                     "{" +
                         "static int _inputContor = 0;" +
                          "static List<String> _input = new List<String>();" +
                         "static void Main(string[] args)" +
                         "{" +
                            "string _read = Console.ReadLine().ToString();" +

                            "while(_read != \"$\")" +
                            "{" +
                                "_input.Add(_read);" +
                                "_read = Console.ReadLine().ToString();" +
                            "}" +
                            "_input.Add(\"$\");" +
                            _startSymbol + "();" +
                            "if(_input[_inputContor] == \"$\")" +
                            "{" +
                                "Console.WriteLine(\"Propozitia este corecta\");" +
                            "}" +
                            "else" +
                            "{" +
                                "Console.WriteLine(\"Propozitia este incorecta\");" +
                            "}" +
                            "Console.ReadLine();" +
                        "}";

            _code = _code + "public static void error(){ Console.WriteLine(\"Propozitia este incorecta\"); Console.ReadKey(); Environment.Exit(1);}";

            foreach (String _nonTerminal in _nonTerminals)
            {
                _code = _code + "public static void " + _nonTerminal + "(){";
                int ruleCount = 0;
                List<int> _usedRules = new List<int>();


                for (int line = 0; line < lineNumber; line++)
                {
                    for (int column = 0; column < colNumber; column++)
                    {
                        if (_matrix[line, 0] == _nonTerminal)
                        {
                            if (_matrix[line, column] != " " && column != 0)
                            {
                                int _matrixRule = Int32.Parse(_matrix[line, column].ToString());
                                bool verif = false;
                                foreach (int number in _usedRules)
                                {
                                    if (number == _matrixRule)
                                    {
                                        verif = true;
                                    }
                                }

                                if (!verif)
                                {
                                    int ruleContor = 0;

                                    foreach (List<String> rule in _productionRules)
                                    {
                                        ruleContor++;
                                        if (ruleContor == _matrixRule)
                                        {
                                            int open = 0;
                                            if (rule[1] == "eps")
                                            {
                                                //if (_matrix[0, column] == "$")
                                                //{
                                                    _code = _code + "if(_input[_inputContor]==" + "\"" + _matrix[0, column] + "\"){ return;}";
                                                    //_code = _code + "else { error(); }";
                                                //}
                                                //else
                                                //{
                                                //    _code = _code + "if(_input[_inputContor]==" + "\"" + _matrix[0, column] + "\"){ return;}";
                                                //    //_code = _code + "else { error(); }";
                                                //}
                                            }
                                            else
                                            {
                                                _code = _code + "if(_input[_inputContor]==" + "\"" + _matrix[0, column] + "\"){";
                                                open++;
                                                int contor = 1;
                                                while (contor != rule.Count())
                                                {
                                                    if (IsNonTerminal(rule[contor]))
                                                    {
                                                        _code = _code + rule[contor] + "();";
                                                    }
                                                    if (IsTerminal(rule[contor]))
                                                    {
                                                        _code = _code + "if(_input[_inputContor]==" + "\"" + rule[contor] + "\"){";
                                                        open++;
                                                        _code = _code + "_inputContor++;";
                                                    }
                                                    contor++;
                                                }
                                                int count = 0;
                                                if (open == 1)
                                                {
                                                    while (count != open)
                                                    {
                                                        _code = _code + "return;";
                                                        _code = _code + "}";
                                                        count++;
                                                    }
                                                }
                                                else
                                                {
                                                    while (count != open)
                                                    {
                                                        if (count != open - 1)
                                                        {
                                                            _code = _code + "}";
                                                            _code = _code + "else { error(); }";

                                                        }
                                                        if (count == open - 1)
                                                        {
                                                            _code = _code + "return;";
                                                            _code = _code + "}";
                                                        }
                                                        count++;
                                                    }
                                                }
                                            }


                                        }
                                    }
                                }

                            }

                        }
                    }
                    if (HasRules(_nonTerminal))
                    {
                        
                        if (_matrix[line, 0] == _nonTerminal)
                        {
                            _code = _code + "if(";
                            for (int column = 1; column < colNumber; column++)
                            {
                                if (_matrix[line, column] != " ")
                                {
                                    _code = _code + "_input[_inputContor] != " + "\"" + _matrix[0, column] + "\"" + "&&";

                                }
                            }

                            _code = _code.Substring(0, _code.Length - 2);
                            _code = _code + ")";
                            _code = _code + "{ error(); }";
                        }
                    }
                }
                _code = _code + "}";
            }
            _code = _code + "}" + "}";

        }

        internal static bool HasRules(string _nonTerminal)
        {
            for(int line= 0; line < lineNumber; line++)
                for(int col=0; col < colNumber; col++)
                {
                    if (_matrix[line,0] == _nonTerminal)
                    {
                        if(col!=0)
                        {
                            if (_matrix[line, col] != " ")
                                return true;
                        }
                    }
                }
            return false;
        }

        internal static string PrintCode()
        {
           
            _code = CSharpSyntaxTree.ParseText(_code).GetRoot().NormalizeWhitespace().ToFullString();
            //Console.WriteLine(_code);
            //System.IO.File.WriteAllText(@"D:\ATM\Compilatoare\TemaFacultativaConsoleApp\TemaFacultativaConsoleApp\code.txt", _code);
            return _code;

        }

        internal static string getCode()
        {
            return _code;
        }
    }
}
