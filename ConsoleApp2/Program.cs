using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            int action = 0;
            int action2 = 0;
            List<Item> list = new List<Item>();
            ItemInitialize(); //1회 불러옴
            Character me = new Character();
            Status(); // 1회 불러옴

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
                            ViewInventory();
                            action2 = int.Parse(Console.ReadLine());
                            ActionCheck();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            ViewShop();
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
                Console.WriteLine("");
                StatusChack();
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write(">>");
            }
            void ViewInventory()
            {
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("");
                Console.WriteLine("[아이템목록]");
                Console.WriteLine("");
                Inventory();
                Console.WriteLine("");
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write(">>");
            }
            void ViewShop()
            {
                Shop();
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write(">>");
            }
            void ActionCheck()
            {
                while (true)
                {
                    if (action == 2 && action2 == 1)
                    {
                        Console.Clear();
                        ItemNumbering();
                        break;
                    }
                    else if (action2 != 0)
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

            void Status() //캐릭터 스팩 구현
            {
                me.Job = "전사";
                me.Level = 1;
                me.Power = 10;
                me.Defense = 5;
                me.HealthPoint = 100;
                me.Gold = 1500;
            }

            void StatusChack() 
            {
                Console.WriteLine($"L v . [ {me.Level} ]");
                Console.WriteLine($"Chad ( {me.Job} )");
                Console.WriteLine($"공격력: {me.Power}");
                Console.WriteLine($"방어력: {me.Defense}");
                Console.WriteLine($"체 력 : {me.HealthPoint}");
                Console.WriteLine($"Gold : {me.Gold}");
            }

            void ItemInitialize() //인게임모든 아이템구현
            {
                list.Add(new Item(1,"낡은 검    ","무기", false, "공격력", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 100, true));
                list.Add(new Item(2,"청동 도끼  ", "무기", false, "공격력", 5, "쉽게 볼 수 있는 낡은 검 입니다.", 100, false));
                list.Add(new Item(3,"스파르타의 창", "무기", true, "공격력", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3500, true));
                list.Add(new Item(4,"수련자 갑옷 ", "장비", false, "방어력", 5, "수련에 도움을 주는 갑옷입니다.", 1000, false));
                list.Add(new Item(5,"무쇠갑옷    ", "장비", true, "방어력", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000, true));
                list.Add(new Item(6,"스파르타의 갑옷", "장비", false, "방어력", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false));
                list.Add(new Item(7,"구매테스트용", "무역", false, "방어력", 1, "무역품.", 100, false));
                list.Add(new Item(8,"판매테스트용", "무역", false, "방어력", 1, "무역품.", 100, true));

            }
            void Inventory() //내가 가진것 
            {                
                for (int i = 0; i < list.Count; i++)
                {
                    string isEquipment = list[i].IsEquipment ? "E" : "X"; //착용여부 true/false >>> "E" / "X"
                    if (list[i].IsBuy == true) 
                    {
                        Console.WriteLine($"[{isEquipment}]{list[i].Name} | {list[i].Equipment} | {list[i].Type} +{list[i].ItemValue} | {list[i].Info} ");
                    }
                }
            }
            void ItemNumbering() //장착하기 시 나옴
            {
                while (true)
                {
                    Console.WriteLine("장착관리 ");
                    for (int i = 0; i < list.Count; i++)
                    {
                        string isEquipment = list[i].IsEquipment ? "E" : "X";

                        if (list[i].IsBuy == true)
                        {
                            list[i].Num = i;
                            Console.WriteLine($"{i + 1} [{isEquipment}]{list[i].Name} | {list[i].Equipment} | {list[i].Type} +{list[i].ItemValue} | {list[i].Info} ");
                        }
                    }
                    Console.WriteLine("");
                    Console.WriteLine("0. 나가기");
                    Console.Write(">>");
                    int choice = int.Parse(Console.ReadLine());
                    if (choice == 0)
                    {
                        action2 = 0;
                        ActionCheck();
                        break;
                    }                    
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[choice - 1].Name!=list[choice - 1].Name && list[choice - 1].Equipment == list[i].Equipment && list[i].IsEquipment)
                        {                            
                                list[i].IsEquipment = !list[i].IsEquipment;
                        }
                    }
                    list[choice - 1].IsEquipment = !list[choice - 1].IsEquipment;

                    //Console.WriteLine("착용 할수 없습니다");
                }
            }

            void Shop()
            {
                string isBuyChack = "";
                while (true)
                {
                    Console.WriteLine("3. 상점");
                    Console.WriteLine("");
                    Console.WriteLine("[보유골드]:" + me.Gold);
                    Console.WriteLine("");
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].IsBuy == true)
                        {
                            isBuyChack = "[구매함]";
                        }
                        else
                        {
                            isBuyChack = list[i].Price.ToString();
                        }
                        Console.WriteLine($"{list[i].Name} | {list[i].Equipment} | {list[i].Type} +{list[i].ItemValue} | {list[i].Info} | = {isBuyChack}");
                    }
                    Console.WriteLine("");
                    Console.WriteLine("1. 구매하기");
                    Console.WriteLine("2. 판매하기");
                    Console.WriteLine("0. 나가기");
                    Console.Write(">>");
                    int ShopTap = int.Parse(Console.ReadLine());
                    if (ShopTap == 0)
                    {
                        action2 = 0;
                        Console.Clear();
                        ActionCheck();
                    }
                    else if (ShopTap == 1)
                    {
                        Console.Clear();
                        ShopBuy();
                    }
                    else if (ShopTap == 2)
                    {
                        Console.Clear();
                        ShopSell();
                    }
                    else { Console.Clear(); Console.WriteLine("잘못 선택하섰습니다"); }
                }
            }

            void ShopBuy()
            {
                int shopPrice = 0;
                
                while (true)
                {
                    Console.WriteLine("3. 구매하기");
                    Console.WriteLine("");
                    Console.WriteLine("[보유골드]:" + me.Gold);
                    Console.WriteLine("");
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (!list[i].IsBuy)
                        {
                            list[i].Num = i;
                            shopPrice = list[i].Price;
                            Console.WriteLine($"{list[i].Num+1} {list[i].Name} | {list[i].Equipment} | {list[i].Type} +{list[i].ItemValue} | {list[i].Info} | = {list[i].Price}");
                        }
                    }
                    Console.WriteLine("");
                    Console.WriteLine("0. 나가기");
                    Console.Write(">>");
                    int shopChoice = int.Parse(Console.ReadLine());
                    if (shopChoice == 0)
                    {
                        Shop();
                        break;
                    }
                    list[shopChoice - 1].IsBuy = !list[shopChoice - 1].IsBuy;
                    me.Gold = me.Gold - list[shopChoice - 1].Price;
                    Console.Clear();
                }
            }

            void ShopSell()
            {
                float sellPrice = 0;
                
                while (true)
                {
                    Console.WriteLine("3. 판매하기");
                    Console.WriteLine("");
                    Console.WriteLine("[보유골드]:" + me.Gold);
                    Console.WriteLine("");
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].IsBuy == true)
                        {
                            sellPrice = (float)list[i].Price *0.8f;
                            Console.WriteLine($"{i + 1} {list[i].Name} | {list[i].Equipment} | {list[i].Type} +{list[i].ItemValue} | {list[i].Info} | = {sellPrice}");
                        }
                    }
                    Console.WriteLine("");
                    Console.WriteLine("0. 나가기");
                    Console.Write(">>");
                    int shopChoice = int.Parse(Console.ReadLine());
                    if (shopChoice == 0)
                    {
                        Shop();
                        break;
                    }
                    list[shopChoice - 1].IsBuy = !list[shopChoice - 1].IsBuy;
                    list[shopChoice - 1].IsEquipment = !list[shopChoice - 1].IsEquipment;
                    me.Gold = me.Gold + (int)(list[shopChoice - 1].Price * 0.85f);
                    Console.Clear();
                }
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
        class Item
        {
            public int num;
            private string name;
            private string equipment;
            private bool isEquipment;
            private string type;
            private int itemValue;
            private string info;
            private int price;
            private bool isBuy;


            public Item(int _num, string _itemName, string _equipment, bool _isEquipment, string _type, int _itemValue, string _info, int _price,bool _isBuy) 
            {
                num= _num; //장비 번호
                name = _itemName; //장비이름
                isEquipment = _isEquipment; //장비 착용여부
                equipment = _equipment; //장비 장착 부위(1.무기,2.장비...3.소모품?)
                type = _type;  //아이템 능력(공격역,방어력)
                itemValue = _itemValue; // 아이템능력치
                info = _info;   // 소개
                price = _price; //가격
                isBuy = _isBuy; //구매 여부
            }
            public int Num
            {
                get { return num; }
                set { num = value; }
            }
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public bool IsEquipment
            {
                get { return isEquipment; }
                set { isEquipment = value; }
            }
            public string Equipment
            {
                get { return equipment; }
                set { equipment = value; }
            }
            public string Type
            {
                get { return type; }
                set { type = value; }
            }
            public int ItemValue
            {
                get { return itemValue; }
                set { itemValue = value; }
            }
            public string Info
            {
                get { return info; }
                set { info = value; }
            }
            public int Price
            {
                get { return price; }
                set { price = value; }
            }
            public bool IsBuy
            {
                get { return isBuy; }
                set { isBuy = value; }
            }

        }
    }
}   
