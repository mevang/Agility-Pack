using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Xml;
using HtmlAgilityPack;
using System.Xml.XPath;


namespace AgilityPack_v10
{
    class Program
    {
        static void Main(string[] args)
        {
            var Url = @"C:\Users\mev1_\Downloads\Temp\Test.html";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlWeb().Load(Url);

            Console.WriteLine("Parse Errors : {0}", doc.ParseErrors.Count());

            string xpath = "//html/body/table";
               
            HtmlNodeCollection Nodes = doc.DocumentNode.SelectNodes(xpath);

            

            Console.WriteLine("Number of Nodes in HtmlNodeCollection Selected for xpath string {0}: {1}", xpath, Nodes.Count());
            Console.WriteLine("\nIterating over {0} Nodes ", Nodes.Count());





            //Using Recusion
            List<Tuple<int, string>> DomStruct = new List<Tuple<int, string>>();
            List<int> DOM_Level = new List<int>();
            List<string> DOM_Node = new List<string>();

            int level = -1;
            RecursiveChildSearch(level, Nodes, ref DomStruct, ref DOM_Level, ref DOM_Node);

            foreach (var item in DomStruct)
            {
                Console.WriteLine("Level :{0} {1}", item.Item1, item.Item2);
            }
            Console.ReadKey();
            MakeXpathExpressions(ref DOM_Level, ref DOM_Node);
        }


        static void RecursiveChildSearch(int level, HtmlNodeCollection NodeCollection, ref List<Tuple<int, string>> DomStruct, ref List<int> DOM_Level, ref List<string> DOM_Node)
        {
            foreach (var node in NodeCollection.Where(x => x.GetType() != typeof(HtmlTextNode)))
            //  foreach (var node in NodeCollection)
            {
                level++;
                Console.WriteLine("Level : {0}  NodeName: {1} NodeType: {2} #ChildNodes {3}  ", level, node.Name, node.GetType() == typeof(HtmlTextNode), node.ChildNodes.Count);
                DomStruct.Add(new Tuple<int, string>(level, node.Name));
                DOM_Level.Add(level);
                DOM_Node.Add(node.Name);
                
                if (node.HasChildNodes)
                {
                    RecursiveChildSearch(level, node.ChildNodes, ref DomStruct, ref DOM_Level, ref DOM_Node);
                }

                level--;
            }

        }

        static void MakeXpathExpressions(ref List<int> DOM_Level, ref List<string> DOM_Node)

        {
            Console.WriteLine("MakeXpath Code");
            Console.ReadKey();



            List<string> XpathList = new List<string>();
            int index = -1;
            foreach (int level in DOM_Level)
            {
                index++;
                
                XpathList.Insert(level, DOM_Node[index]);
                XpathList.RemoveRange(level + 1, XpathList.Count - level - 1);

                string xpathexpression = "";
                foreach (var k in XpathList)
                {
                    xpathexpression = xpathexpression + "/" + k;
                }
                //Print List

                Console.WriteLine(xpathexpression);
            }

        }


    }
}