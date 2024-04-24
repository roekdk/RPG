using System;

namespace ConsoleApp2
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            int action=0;
            int action2;
            while (true)
            {                
                switch (action)
                {
                    case 0:
                        {   
                            Welcom();
                            action = int.Parse(Console.ReadLine());
                            break;
                        }
                    case 1:
                        {
                            Console.Clear();
                            ViewStatus();
                            action2 = int.Parse(Console.ReadLine());
                            ActionCheck();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            Inventory();
                            action2 = int.Parse(Console.ReadLine());
                            ActionCheck();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            Shop();
                            action2 = int.Parse(Console.ReadLine());                            
                            ActionCheck();
                            break;
                        }
                    default: 
                        {
                            Console.WriteLine("잘못 입력 하였습니다");
                            Console.Write(">>");
                            action = int.Parse(Console.ReadLine());
                            break;
                        }
                }
            }
            void Welcom() 
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("");
                Console.WriteLine("원하는 행동 입력");
                Console.Write(">>");                
            }
            void ViewStatus() 
            {
                Console.WriteLine("1. 상태 보기");
                Status();
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write(">>");
            }
            void Inventory() 
            {
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("구현중");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write(">>");               
            }
            void Shop()
            {
                Console.WriteLine("3. 상점");
                Console.WriteLine("구현중");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write(">>");               
            }
            void ActionCheck()
            {
                while (true)
                {
                    if (action2 != 0)
                    {
                        Console.WriteLine("잘못 입력 하였습니다");
                        Console.Write(">>");
                        action2 = int.Parse(Console.ReadLine());
                    }
                    else 
                    { 
                        action = 0; 
                        Console.Clear(); 
                        break; 
                    }
                }
            }

            void Status()
            {
                Character me = new Character();

                me.Job = "전사";
                me.Level = 1;
                me.Power = 10;
                me.Defense = 5;
                me.HealthPoint = 100;
                me.Gold = 1500;

                Console.WriteLine($"L v . [ {me.Level} ]");
                Console.WriteLine($"Chad ( {me.Job} )");
                Console.WriteLine($"공격력: {me.Power}");
                Console.WriteLine($"방어력: {me.Defense}");
                Console.WriteLine($"체 력 : {me.HealthPoint}");
                Console.WriteLine($"Gold : {me.Gold}");
            }
        }        

        class Character
        {
            private string job;
            private int level;
            private int power;
            private int defense;
            private int healthPoint;
            private int gold;
            public string Job
            {
                get { return job; }
                set { job = value; }
            }
            public int Level
            {
                get { return level; }
                set { level = value; }
            }
            public int Power
            {
                get { return power; }
                set { power = value; }
            }
            public int Defense
            {
                get { return defense; }
                set { defense = value; }
            }
            public int HealthPoint
            {
                get { return healthPoint; }
                set { healthPoint = value; }
            }
            public int Gold
            {
                get { return gold; }
                set { gold = value; }
            }
        }       
    }
}   
