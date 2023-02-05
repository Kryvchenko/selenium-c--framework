using System;

namespace CShartFundus
{
     class Program : EmptyClass
    {
        String name;
        public Program(String name)
        {
            this.name = name;
        }
        public void getName()
        {
            Console.WriteLine($"My name is {this.name}");
        }
        public void getData()
        {
            Console.WriteLine("I am new method");
        }

        static void Main(string[] args)
        {
            
            Program p = new Program("Rahul");
            p.getData();
            p.SetData();
            p.getName();
            Console.WriteLine("Hello, World!");
         
        }
    }
}


