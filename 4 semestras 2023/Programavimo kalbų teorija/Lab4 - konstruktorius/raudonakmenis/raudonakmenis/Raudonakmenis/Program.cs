//using System;
//using System.IO;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Antlr4.Runtime;
//using Antlr4.Runtime.Tree;

//namespace Raudonakmenis
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            StreamReader aStreamReader = new StreamReader(args[0]);

//            AntlrInputStream antlrInputStream = new AntlrInputStream(aStreamReader);

//            GrammarLexer lexer = new GrammarLexer(antlrInputStream);

//            CommonTokenStream tokens = new CommonTokenStream(lexer);

//            GrammarParser parser = new GrammarParser(tokens);

//            IParseTree tree = parser.compileUnit();

//            Console.WriteLine(tree.ToStringTree());
//        }
//    }
//}
