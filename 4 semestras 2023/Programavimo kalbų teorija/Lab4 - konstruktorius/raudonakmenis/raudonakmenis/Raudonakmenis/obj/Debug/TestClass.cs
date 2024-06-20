using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.IO;

namespace Raudonakmenis.obj.Debug
{
    class TestClass
    {
        static void Main(string[] args)
        {
            string inputPath = "C:\\Users\\Lenovo\\Desktop\\Redsotne\\raudonakmenis\\raudonakmenis\\Raudonakmenis\\obj\\Debug\\testas.txt";
            string inputContent = File.ReadAllText(inputPath);
            var input = new AntlrInputStream(inputContent);
            var lexer = new GrammarLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new GrammarParser(tokens);

            // Parse the input and obtain the parse tree
            IParseTree tree = parser.program();
            SymbolTable table = new SymbolTable();
            // Create the visitor
            RedstoneVisitor visitor = new RedstoneVisitor(table);

            // Traverse the parse tree with the visitor
            var output = visitor.Visit(tree);

            Console.WriteLine(output);
            Console.ReadKey();
        }
    }
}
