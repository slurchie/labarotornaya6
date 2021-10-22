using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace methodichka
{
    class BankAccount
    { 
    enum account { saving, current,incorrect };
    private int number;
    private account type;
    private decimal balance;
    private static int num = 1;
    public void PutItOnTheAccount()
    {
        Console.WriteLine("Введите сумму, которую хотите положить на счет");
        bool result = Int32.TryParse(Console.ReadLine(), out int temp);
        if (result)
        {
            balance += temp;
        }
        else
        {
            Console.WriteLine("Ошибка при вводе баланса");
        }
    }
    public void WithdrawFromTheAccount()
    {
        Console.WriteLine("Введите сумму, которую хотите cнять с счета");
        bool result = Int32.TryParse(Console.ReadLine(), out int temp);
        if (result)
        {
            if (temp <= balance)
            {
                balance -= temp;
            }
            else if (temp > balance)
            {
                Console.WriteLine("Недостаточно средств");
            }
        }
    }
    public void IncreaseNum()
    {
        number = num++;
    }
    public void Print()
    {
        Console.WriteLine($"Account number: {number} \n balance: {balance} \n type: {type}");
    }
        public void GetBank_Account()
        {
            Console.WriteLine("Введите баланс:");
            bool result = decimal.TryParse(Console.ReadLine(), out decimal temp1);
            if (result)
            {
                balance = temp1;
            }
            Console.WriteLine("Выберите тип счета: saving или current \nВведите 1 или 2");
            result = Int32.TryParse(Console.ReadLine(), out int temp);
            switch (temp)
            {
                case 1:
                    type = account.saving;
                    break;
                case 2:
                    type = account.current;
                    break;
                default:
                    Console.WriteLine("Нужно вводить только 1 или 2");
                    break;
            } 
        }

        static void Main(string[] args)
        {

            BankAccount account = new BankAccount();
            account.GetBank_Account();
            account.IncreaseNum();
            account.PutItOnTheAccount();
            account.WithdrawFromTheAccount();
            account.Print();
        }       
    }    
}
    
