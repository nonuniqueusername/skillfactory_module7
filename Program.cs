internal class Program
{
    private static void Main(string[] args)
    {

    }

    sealed class OrderCollection<TDelivery, TCustomer, TOrder>
        where TDelivery : Delivery
        where TCustomer : Customer
        where TOrder : Order<TDelivery, int, TCustomer>
    {
        private readonly List<TOrder> orderList;
        public OrderCollection()
        {
            orderList = new List<TOrder>();
        }

        public TOrder this[int index]
        {
            get
            {
                if (index >= 0 && index < orderList.Count)
                {
                    return orderList[index];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }


        public void AddOrder(TOrder order) { }

        public void RemoveOrder(TOrder order) { }

        public List<TOrder> GetOrdersByCustomer<TCustomer>(TCustomer customer) where TCustomer : Customer
        {
            return orderList.Where(e => e.Customer == customer).ToList();
        }
    }

    class Order<TDelivery, TProductID, TCustomer>
        where TDelivery : Delivery
        where TCustomer : Customer
    {
        static int numberCounter = 0;
        public TDelivery Delivery;
        public int Number;
        public string Description;
        public Product<TProductID>[] Products;
        public TCustomer Customer;
        public DateTime Date;

        public Order(TDelivery delivery, string description, Product<TProductID>[] products, TCustomer customer, DateTime date)
        {
            Delivery = delivery;
            Description = description;
            Products = products;
            Customer = customer;
            Date = date;
            Number = ++numberCounter;
        }

        public void DisplayAddress()
        {
            Console.WriteLine(Delivery.Address);
        }
        public void ChangeDeliveryType(TDelivery delivery)
        {
            Delivery = delivery;
        }
    }

    abstract class Product<TID>
    {
        public string Name;
        public TID ID;
        public string Price;

        public Product(string name, TID iD, string price)
        {
            Name = name;
            ID = iD;
            Price = price;
        }
    }

    class Grecha<TID> : Product<TID>
    {
        public float Mass;

        public Grecha(string name, TID iD, string price, float mass) : base(name, iD, price)
        {
            Mass = mass;
        }
    }


    abstract class Customer
    {
        public string Name;
        public string LastName;
        public string FullName;
        protected string PhoneNumber
        {
            set
            {
                PhoneNumber = Validator.ValidatePhonenumber(value);
            }
            get
            {
                return PhoneNumber;
            }
        }
        protected string Email
        {
            set
            {
                Email = Validator.ValidateEmail(value);
            }
            get
            {
                return Email;
            }
        }

        public Customer(string name, string lastName, string phoneNumber, string email)
        {
            Name = name;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            FullName = (LastName + Name).Transliterate();
        }

        public virtual void Notify()
        {
            Notifier.SendMail(Email);
        }
    }

    class IndividualCustomer : Customer
    {
        public IndividualCustomer(string name, string lastName, string phoneNumber, string email) : base(name, lastName, phoneNumber, email)
        {

        }

        public override void Notify()
        {
            base.Notify();
            Notifier.SendSMS(PhoneNumber);
        }
    }

    class LegalCustomer : Customer
    {
        public string BIC;
        public string ITN;
        public LegalCustomer(string name, string lastName, string phoneNumber, string email, string bic, string itn) : base(name, lastName, phoneNumber, email)
        {
            BIC = bic;
            ITN = itn;
        }
    }


    abstract class Delivery
    {
        public string Address;
    }

    class HomeDelivery : Delivery { }

    class PickPointDelivery : Delivery { }

    class ShopDelivery : Delivery { }

    static class Validator
    {
        static private bool isValid(string value)
        {
            return true;
        }

        public static string ValidatePhonenumber(string value)
        {
            if (IsPhonenumberValid(value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public static string ValidateEmail(string value)
        {
            if (IsEmailValid(value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        private static bool IsPhonenumberValid(string value)
        {
            return isValid(value);
        }
        private static bool IsEmailValid(string value)
        {
            return isValid(value);
        }
    }


    static class Notifier
    {
        static public void SendSMS(string phoneNumber)
        {
            //
        }
        static public void SendMail(string mail)
        {
            //
        }
    }



}
static class StringHelper
{
    static public string Transliterate(this string str)
    {
        return str;
    }
}
