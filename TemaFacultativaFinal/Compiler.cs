using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaFacultativaFinal
{
    class BackCompiler
    {
        string _code;
        CSharpCodeProvider _provider = new CSharpCodeProvider();
        CompilerParameters _parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, "generated.exe", true);

        public BackCompiler(String Code)
        {
            _parameters.GenerateInMemory = true;
            _parameters.GenerateExecutable = true;
            _code = Code;
        }

        public void Compile()
        {
            CompilerResults _results = _provider.CompileAssemblyFromSource(_parameters, _code);
            if (_results.Errors.HasErrors)
            {

            }
            else
            {
                Process.Start(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/" + "generated.exe");
            }
        }
    }
}
