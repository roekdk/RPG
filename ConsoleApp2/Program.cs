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
                            Console.Clear();
                            break;
                        }
                    case 1:
                        {                            
                            ViewStatus();
                            action2 = int.Parse(Console.ReadLine());
                            Console.Clear();
                            ActionCheck();
                            break;
                        }
                    case 2:
                        {                            
                            ViewInventory();
                            action2 = int.Parse(Console.ReadLine());
                            Console.Clear();
                            ActionCheck();
                            break;
                        }
                    case 3:
                        {                           
                            Shop();
                            break;
                        }
                    case 4:
                        {                            
                            ViewDungeon();
                            action2 = int.Parse(Console.ReadLine());
                            Console.Clear();
                            ActionCheck();
                            break;
                        }
                    case 5:
                        {                            
                            ViewINN();
                            action2 = int.Parse(Console.ReadLine());
                            Console.Clear();
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
                Console.WriteLine("4. 던전입장");
                Console.WriteLine("5. 휴식하기");
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
            //void ViewShop()
            //{
            //    Shop();                
            //}
            void ViewDungeon() 
            {   
                Console.WriteLine("4. 던전입장");
                Console.WriteLine("");
                Console.WriteLine("1. 쉬운 던전     | 방어력 5 이상 권장");
                Console.WriteLine("2. 일반 던전     | 방어력 11 이상 권장");
                Console.WriteLine("3. 어려운 던전     | 방어력 17 이상 권장");
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write(">>");
            }
            void ViewINN()
            {
                Console.WriteLine("5. 휴식하기");
                Console.WriteLine("");
                Console.Write("500 G 를 내면 체력을 회복할 수 있습니다.   ");
                Console.WriteLine($"(보유골드: {me.Gold})");
                Console.WriteLine($"(체력: {me.HealthPoint})");
                Console.WriteLine("");
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write(">>");
            }


            void ActionCheck()
            {
                while (true)
                {
                    if (action == 2 && action2 == 1) //2.인벤토리 && 1선택
                    {
                        Console.Clear();
                        ItemNumbering();
                        break;
                    }
                    else if (action == 5 && action2 == 1) //5.휴식하기 && 1선택
                    {
                        INN();                        
                        break;
                    }
                    else if (action == 4 && action2 == 1) //4.던전가기 && 1선택
                    {
                        action2 = 0;
                        Console.Write("1. 쉬운 던전");
                        Dungeon(0);                        
                        break;
                    }
                    else if (action == 4 && action2 == 2) //4.던전가기 && 2선택
                    {
                        action2 = 0;
                        Console.Write("2. 일반 던전");
                        Dungeon(1);                        
                        break;
                    }
                    else if (action == 4 && action2 == 3) //4.던전가기 && 3선택
                    {
                        action2 = 0;
                        Console.Write("3. 어려운 던전");
                        Dungeon(2);
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
                me.Exp = 0; //경험치 == 던전클리어 횟수저장
                me.Power = 10;
                me.TemPower = 0;//장비로 증가될 양
                me.Defense = 5;
                me.TemDefense = 0;//장비로 증가될 양
                me.HealthPoint = 50; //현재체력
                me.MaxHealthPoint = 100; //최대체력
                me.Gold = 1500;               
            }
            void TemStatus()  //장비 업데이트용
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Equipment == "무기" && list[i].IsEquipment)
                    {
                        me.Weapon = list[i].Name;
                        me.TemPower = list[i].ItemValue;
                    }
                    else if (list[i].Equipment == "장비" && list[i].IsEquipment)
                    {
                        me.Equipment = list[i].Name;
                        me.TemDefense = list[i].ItemValue;
                    }
                }
            }

            void StatusChack() //스텟 표현
            {
                Console.Write($"L v . [ {me.Level} ]  ");
                Console.WriteLine($"EXP . [ {me.Exp} ]");
                Console.WriteLine($"Chad ( {me.Job} )");
                Console.WriteLine($"공격력: {me.Power} + {me.TemPower}");
                Console.WriteLine($"방어력: {me.Defense} + {me.TemDefense}");
                Console.WriteLine($"체 력 : {me.HealthPoint} / {me.MaxHealthPoint}");
                Console.WriteLine($"Gold : {me.Gold}");
                Console.WriteLine($"무기 : {me.Weapon}");
                Console.WriteLine($"장비 : {me.Equipment}");
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

                        if (list[i].IsBuy == true && list[i].Equipment!="무역")
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
                    //for (int i = 0; i < list.Count; i++)
                    //{
                    //    if (list[choice - 1].Equipment == list[i].Equipment && list[i].IsEquipment) //선택장비와 가진장비의 장비부위가 같고 && 착용중이라면
                    //    {
                    //        list[i].IsEquipment = !list[i].IsEquipment; //벗어
                    //        list[choice - 1].IsEquipment = !list[choice - 1].IsEquipment; //선택장비 착용 혹은 벗기
                    //        break;
                    //    }
                    //    else if (list[choice - 1].IsEquipment|| !list[choice - 1].IsEquipment) //선택장비를 입고잇다면
                    //    {
                    //        list[choice - 1].IsEquipment = !list[choice - 1].IsEquipment; //선택장비 착용 혹은 벗기
                    //        break;
                    //    }
                    //}
                    list[choice - 1].IsEquipment = !list[choice - 1].IsEquipment;
                    TemStatus();
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
                        ActionCheck();                        
                        break;
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
                    if(list[shopChoice - 1].IsEquipment) 
                    {
                        list[shopChoice - 1].IsEquipment = !list[shopChoice - 1].IsEquipment;
                    }                    
                    me.Gold += (int)(list[shopChoice - 1].Price * 0.85f);
                    Console.Clear();
                }
            }

            void Dungeon(int _num) 
            {
                int num = _num; //난이도
                int DunDefense = 0; //던전 권장방어력
                int DunReward = 0; //던전 보상
                int youDefense = me.Defense + me.TemDefense;  // 내방어력
                float youPower = me.Power + me.TemPower;// 내공격력                                     // 
                int rand1 = new Random().Next(20,36);
                int rand2 = new Random().Next(0, 5); //40퍼 확률
                int rand3 = new Random().Next((int)youPower, (int)youPower *2);// 공격력~공격력* 2 

                switch (num)
                {
                    case 0:
                        {
                            DunDefense = 5;
                            DunReward = 1000;
                            break;
                        }
                    case 1:
                        {
                            DunDefense = 11;
                            DunReward = 1700;
                            break;
                        }
                    case 2:
                        {
                            DunDefense = 17;
                            DunReward = 2500;
                            break;
                        }
                }
                if (youDefense < DunDefense && rand2 < 2)//방어력미달 && 40%걸림
                {
                    me.HealthPoint -= (int)(me.HealthPoint * 0.5f);
                    Console.Write(" 실패 ");
                }
                else //던전 클리어
                {
                    Console.WriteLine(" 성공 ");
                    youDefense -= DunDefense;
                    me.HealthPoint -= rand1 - youDefense;
                    me.Gold += DunReward+ (int)(DunReward*(rand3 * 0.01f));
                    me.Exp ++;
                }
                if (me.Level <= 5 && me.Exp==me.Level) //랩업!
                {
                    Console.WriteLine(" 랩업 ");
                    me.Level++;
                    me.Exp = 0;
                    me.MaxHealthPoint += 10;
                    me.Power += 0.5f;
                    me.Defense += 1;
                }
            }
            void INN()
            {                
                if (me.Gold >= 500) 
                {
                    me.HealthPoint += 100;
                    me.Gold -= 500;
                    if (me.HealthPoint > me.MaxHealthPoint)
                    {
                        me.HealthPoint = me.MaxHealthPoint;
                    }                    
                    Console.WriteLine("회복 되었습니다");
                }
                else { Console.WriteLine("골드가 모자랍니다"); }
                
            }
        }
        class Character
        {
            private string job;
            private int level;
            private int exp;
            private float power;
            private int temPower;
            private int defense;
            private int temDefense;
            private int healthPoint;
            private int maxHealthPoint;
            private int gold;
            private string weapon;
            private string equipment;
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
            public int Exp
            {
                get { return exp; }
                set { exp = value; }
            }
            public float Power
            {
                get { return power; }
                set { power = value; }
            }
            public int TemPower
            {
                get { return temPower; }
                set { temPower = value; }
            }
            public int Defense
            {
                get { return defense; }
                set { defense = value; }
            }
            public int TemDefense
            {
                get { return temDefense; }
                set { temDefense = value; }
            }
            public int HealthPoint
            {
                get { return healthPoint; }
                set { healthPoint = value; }
            }
            public int MaxHealthPoint
            {
                get { return maxHealthPoint; }
                set { maxHealthPoint = value; }
            }
            public int Gold
            {
                get { return gold; }
                set { gold = value; }
            }
            public string Weapon
            {
                get { return weapon; }
                set { weapon = value; }
            }
            public string Equipment
            {
                get { return equipment; }
                set { equipment = value; }
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
