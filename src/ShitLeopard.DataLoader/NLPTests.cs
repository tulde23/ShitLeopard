using System;
using java.io;
using edu.stanford.nlp.process;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.trees;
using edu.stanford.nlp.parser.lexparser;
using Console = System.Console;
using System.Reflection;
using System.IO;

namespace ShitLeopard.DataLoader
{
    class NLPTests
    {
        public static void Test()
        {
            var jarRoot = @"C:\Development\ShitLeopard\src\ShitLeopard.Api\stanford-parser-full-2016-10-31\models\";
            var modelsDirectory = jarRoot + @"\edu\stanford\nlp\models";

            var asm = Assembly.GetExecutingAssembly();
         
            var ms = new MemoryStream(System.IO.File.ReadAllBytes(modelsDirectory + @"\lexparser\englishPCFG.ser.gz"));

            var inp = new ikvm.io.InputStreamWrapper(ms);
            var x = new java.io.ObjectInputStream(inp);
            // Loading english PCFG parser from file
            var lp = LexicalizedParser.loadModel(x);

            // This sample shows parsing a list of correctly tokenized words
            var sent = new[] { "This", "is", "an", "easy", "sentence", "." };
            var rawWords = SentenceUtils.toCoreLabelList(sent);
            var tree = lp.apply(rawWords);
            tree.pennPrint();

            // This option shows loading and using an explicit tokenizer
            var sent2 = "This is another sentence.";
            var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
            var sent2Reader = new java.io.StringReader(sent2);
            var rawWords2 = tokenizerFactory.getTokenizer(sent2Reader).tokenize();
            sent2Reader.close();
            var tree2 = lp.apply(rawWords2);

            // Extract dependencies from lexical tree
            var tlp = new PennTreebankLanguagePack();
            var gsf = tlp.grammaticalStructureFactory();
            var gs = gsf.newGrammaticalStructure(tree2);
            var tdl = gs.typedDependenciesCCprocessed();
            Console.WriteLine("\n{0}\n", tdl);

            // Extract collapsed dependencies from parsed tree
            var tp = new TreePrint("penn,typedDependenciesCollapsed");
            tp.printTree(tree2);
        }
    }
}
