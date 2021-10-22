using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace art_task
{
    class Painting
    {
        private string nameOfPainting;
        private string aftor;
        private int year;
        private int cost;
        private bool isPerform;
        public int Cost // свойство определяющее цену картины(get читает, set записывает )
        {
            get
            {
                return cost;
            }
            set
            {
                if (value < 1000000)
                    cost = 1000000;
                else
                    cost = value;
            }
        }

        public bool IsPerform //читает и возвращает если картина выставлена(по умолчанию выставлена)
        {
            get
            {
                return isPerform;
            }
            set
            {

            }
        }

        public Painting(string name, string a, int y, int c) // конструктор инициализиует объект
        {
            nameOfPainting = name;
            aftor = a;
            year = y;
            cost = c;
            isPerform = true;
        }

        public void Perform() //метод показывающий что картина выставлена
        {
            if (isPerform)
            {
                Console.WriteLine("Картина уже выставлена");
            }
            else
            {
                isPerform = true;
                Console.WriteLine("Картина выставлена");
            }
        }
        public void Remove() //метод убирающий картину
        {
            if (isPerform)
            {
                isPerform = false;
                Console.WriteLine("Картину только что убрали");
            }
            else
            {
                Console.WriteLine("Картина уже была убрана");
            }
        }
        public void GrowInPrice(Critic critic) //метод критика повышать цену картины если он оценивает картину хорошо
        {
            if (critic.Estimate(this) > cost) cost++;
        }
        public void DropInPrice(Critic critic) //метод критика уменьшать цену картины
        {
            if (critic.Estimate(this) < cost) cost--;
        }
        public void Fall()  // метод картины падать
        {
            Console.WriteLine("Картина \"" + nameOfPainting + "\" упала");
        }
    }
    abstract class Human
    {
        protected string firstName;
        protected string lastName;
        protected int numberPainting;
        protected int time;
        protected bool haveMask;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {

            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {

            }
        }
        public int NumberPainting //если значение больше текущего записываем его текущее(число просмотренных каптин уменьшаться не может)
        {
            get
            {
                return numberPainting;
            }
            set
            {
                if (value > numberPainting)
                    numberPainting = value;
            }
        }
        public bool HaveMask // сойство определяющее и выполняющее проверку на наличие маски
        {
            get
            {
                return haveMask;
            }
            set
            {
                if (!haveMask)
                    Console.WriteLine("Наденьте маску, пожалуйста, " + firstName);
                haveMask = HaveMask;
            }
        }
        public void Come() // метод показывающий что люди(и критик, и посетители) могут приходить
        {
            Console.WriteLine("Добро пожаловать, " + firstName + " " + lastName);
        }
        public void Leave() //метод показывающий, что люди могут уходить.
        {
            Console.WriteLine("До свидания, " + firstName + " " + lastName);
        }
        public void viewPainting(Painting p) // метод просмотра картины людьми и показывающий время просмотра картины
        {
            Console.WriteLine("Эта картина на " + Estimate(p));
            numberPainting++;
            Random R = new Random();
            time += R.Next(1, 10);
        }
        public abstract int Estimate(Painting P); // ввели свойство людец оценивать картины в абстрактном классе
        public void Discuss(Human interlocutor, Painting p)
        {
            int estimate = Estimate(p);
            Console.WriteLine("-Я оцениваю эту картину на " + estimate + " - сказал " + firstName + " " + lastName);
            int estimateInterlocutor = interlocutor.Estimate(p);
            if (estimateInterlocutor != estimate)
                Console.WriteLine("-А мне кажется, что эта картина на " + interlocutor.Estimate(p) + " - ответил " + interlocutor.firstName + " " + interlocutor.lastName);
            else
            {
                Console.WriteLine("Я с тобой согласен");
            }
        }
    }
    class Critic : Human
    {
        private List<Visitor> visitors; //он включает в себя список всех посетителей
        bool isDrunk;
        public bool IsDrunk //записывает и читает пьян ли был критик
        {
            get
            {
                return isDrunk;
            }
            set
            {
                if (!isDrunk)
                {
                    isDrunk = value;
                }
            }
        }
        public Critic(string FirstName, string LastName) //конструктор инициализирует критика(включает в себя список всех посетителей)
        {
            firstName = FirstName;
            lastName = LastName;
            numberPainting = 0;
            time = 0;
            haveMask = true;
            isDrunk = false;
            visitors = new List<Visitor>(10);
        }
        public Critic(string FirstName, string LastName, List<Visitor> v) : this(FirstName, LastName)
        {
            visitors = v;
        }
        public override int Estimate(Painting P) //override потому что мы должны его переопределить из абстрактного.
        {
            Random R = new Random();
            int cost = 0;
            foreach (Visitor V in visitors) // он складывает оценки всех посетителей
            {
                cost += V.Estimate(P);
            }
            if (isDrunk) // если пьяный, то увеличивает стоимость картины
                cost += R.Next(500000, 10000000);
            else
                cost += R.Next(0, 1000000);
            cost /= (1 + visitors.Count);  // потому что 1+ это критик мы его тоже учитываем
            return cost;
        }
        public void AddVisitor(Visitor V)
        {
            visitors.Add(V);
        }
        public string WriteAnArticle(Painting p) //свойство писать статьи про картину на основе цены и писать свою ориентировочную оценку
        {
            int cost = Estimate(p);
            if (cost > 10000000) return "Эта картина отличная";
            if (cost < 2000000) return "Эта картина ужасная";
            return "Эта картина на " + cost + " долларов";
        }
        public void Drink() //метод пить за искусство
        {
            Console.WriteLine("Критик решил выпить за искусство");
            isDrunk = true;
        }
        public void ChangePrice(Painting p) // оценка картины 0-100 если оценк больше 50 то растёт
        {
            int cost = p.Cost;
            if (Estimate(p) > 50) p.GrowInPrice(this);
            else p.DropInPrice(this);
        }
    }
    class Visitor : Human
    {
        public Visitor(string name, string LastName, bool mask) //конструктор инициализирующий посетителя
        {
            firstName = name;
            lastName = LastName;
            numberPainting = 0;
            time = 0;
            haveMask = mask;
        }
        public Visitor(string name, string LastName) : this(name, LastName, false) { }
        public override int Estimate(Painting P) // снова переопределяем из абстактного класса
        {
            Random R = new Random();
            int cost = P.Cost + R.Next(-500000, 1000000);
            if (cost < 0)
                cost = 0;
            return cost;
        }
        public void TrySteal(Painting P) // метод позволяющий украсть картину числа чисто на рандоме ни с чем не связанные
        {
            if (P.IsPerform)
            {
                Random R = new Random();
                int x = R.Next(0, 4);
                if (x < 3)
                {
                    Console.WriteLine("Охраних поймал вора");
                }
                else
                {
                    P.Remove();
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader(".txt");
            List<Visitor> visitors = new List<Visitor>(10);
            string[] input = reader.ReadLine().Split();
            visitors.Add(new Visitor(input[0], input[1]));
            Painting SquareMalevich = new Painting("Квадрат Малевича", "Малевич", 1915, 2000000);
            SquareMalevich.Remove();
            SquareMalevich.Perform();
            SquareMalevich.Fall();
            visitors[0].TrySteal(SquareMalevich);
            visitors[0].viewPainting(SquareMalevich);
            Critic Pelevin = new Critic("Олег", "Пелевин", visitors);
            Pelevin.Come();
            Pelevin.AddVisitor(new Visitor("Леонид", "Визель", true));
            Pelevin.Discuss(visitors[1], SquareMalevich);
            Console.WriteLine(Pelevin.WriteAnArticle(SquareMalevich));
            Pelevin.Drink();
            Console.WriteLine(Pelevin.WriteAnArticle(SquareMalevich));
            Pelevin.ChangePrice(SquareMalevich);
        }
    }
}
