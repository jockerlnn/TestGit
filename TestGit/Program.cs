using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGit
{
    public class SuperMath_
    {
        public void Hello(string hello)
        {
            Console.WriteLine($"HEllo, {hello}!!!");
        }
    }

    public class Math_
    {
        private int m_tmp = 0;
        private List<string> m_list_ = new List<string>
        {
            "asd",
            "zcx",
            "qwe"
        };

        public int Tmp { get => m_tmp; set => m_tmp = value; }
        public SuperMath_ Sm { get => m_sm; set => m_sm = value; }
        public List<string> List_ { get => m_list_; set => m_list_ = value; }

        public int Add(int a, int b) => a + b;

        private SuperMath_ m_sm = new SuperMath_();
    }

    class Program
    {
        

        static void Main(string[] args)
        {
            int i = 1;
            bool factor = false;

            bool check(int x,bool f) => x == 0 || f;

            if (check(i,factor))
                Console.WriteLine("TRUE");
            else
                Console.WriteLine("FALSE");
            
            var p = Python.CreateEngine();
            
            Math_ m = new Math_();

            var paths = p.GetSearchPaths();
            paths.Add(@"D:\IronPython-2.7.7\Lib");

            p.SetSearchPaths(paths);

            Dictionary<string, object> scope = new Dictionary<string, object>
            {
                {"a",1 },
                {"b",2 },
                {"m",m }
            };

            ScriptSource ss = p.CreateScriptSourceFromString("");

            ScriptScope pyScope = p.CreateScope(scope);
            
            p.Execute(@"
                import copy
                import clr
                import System
                #Комментарий 
                print(m.Add(a,b)) 
                m.Tmp = 10        
                m.Sm.Hello('Tom')
                if m.Tmp >= 10:
                    m.Sm.Hello('TRUE')
                #попробовать сделать вывод списка
                for str in m.List_:
                    print(str)
                m.List_.Add('qaz')
                users1 = ['Tom', 'Bob', 'Alice']
                users2 = copy.deepcopy(users1)
                users2.append('Sam')
                # пееменные users1 и users2 указывают на разные списки
                print(users1)   # ['Tom', 'Bob', 'Alice']
                print(users2)   # ['Tom', 'Bob', 'Alice', 'Sam']
                l = System.Collections.Generic.List[int]()
                l.Add(1)
                l.Add(2)
                l.Add(3)
                for li in l:
                    print(li)
                print(l)
                result = 'result!!!'
                print('END')".Replace("                ",""), pyScope);

            Console.WriteLine(m.Tmp);

            Console.WriteLine(pyScope.GetVariable("result"));

            //ScriptScope sss = p.GetSysModule();



            //foreach (string name in sss.GetVariableNames())
            //{
            //    try
            //    {
            //        Console.WriteLine($"{name} = {sss.GetVariable(name).ToString()}");
            //    }
            //    catch { }
            //}
            
            Console.WriteLine("Press Any Key...");
            Console.ReadKey();
        }
    }
}
