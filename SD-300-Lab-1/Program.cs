

Product IceCream = new Product("BaskinRobbins", 5, "qwe");
Product Coke = new Product("Coke-in-can", 2, "asd");
Product Coffee = new Product("Coffee-in-can", 1, "zxc");

VendingMachine LobbyVM = new VendingMachine();

LobbyVM.StockItem(IceCream, 20);
LobbyVM.StockItem(Coke, 10);
LobbyVM.StockItem(IceCream, 5);
LobbyVM.StockFLoat(1, 200);
LobbyVM.StockFLoat(1, 150);
LobbyVM.StockFLoat(5, 120);
//LobbyVM.StockFLoat(10, 100);
//LobbyVM.StockFLoat(20, 90);
//LobbyVM.StockFLoat(50, 50);
//LobbyVM.StockFLoat(100, 50);

List<int> money = new List<int>();
money.Add(10);
money.Add(5);
money.Add(1);

LobbyVM.VendItem("asd", money);

/* This function represents the whole operation of a Purchase in the vending machine, it accepts a vending machine dictionary,
 the price of an item and the money that's been cashed in */
//Dictionary<int, int> Purchase(Dictionary<int, int> vend
//{
//    Dictionary<int, int> changeDenomination = new Dictionary<int, int>();
//    int change = 0;
//    if (cashIn >= price)
//    {
//        change = cashIn - price;
//        int subtractor = change;
//        subtractor = cashIn - price;
//        int number = 0;

//        for (int i = vendingMachine.Count - 1; i >= 0; i--)
//        {
//            int floatCoins = vendingMachine.ElementAt(i).Value;
//            if (vendingMachine.ElementAt(i).Key <= subtractor && floatCoins > 0)
//            {
//                int pieces = 0;

//                if (cashIn / vendingMachine.ElementAt(i).Key >= 2)
//                {
//                    number = subtractor / vendingMachine.ElementAt(i).Key;
//                    pieces += number;
//                }

//                subtractor -= vendingMachine.ElementAt(i).Key * pieces;
//                changeDenomination.Add(vendingMachine.ElementAt(i).Key, pieces);
//                floatCoins -= pieces;
//            }
//        }

//    }
//    else
//    {
//        Console.WriteLine("Cash entered is not enough");
//    }

//    Console.WriteLine($"You have entered {cashIn} for an item that costs {price}");
//    Console.WriteLine($"You'll have a total change of {change}:");
//    foreach (KeyValuePair<int, int> coin in changeDenomination)
//    {
//        Console.WriteLine($"{coin.Value} pc(s) of ${coin.Key}");
//    }

//    return changeDenomination;
//}ingMachine, int price, int cashIn)
//class Department
//{
//    public string Name { get; set; }
//    public ICollection<Student> Students { get; set; }
//    public ICollection<Course> Courses { get; set; }
//    public ICollection<Enrollment> Enrollments { get; set; }
//    public int CourseNumberCount { get; set; } = 1;
//    public int EnrollmentNumberCount { get; set; } = 1;
//    public int StudentId { get; set; } = 1;
//    public Department(string name)
//    {
//        Name = name;
//        Students = new HashSet<Student>();
//        Courses = new HashSet<Course>();
//        Enrollments = new HashSet<Enrollment>();
//    }

//    public Course CreateCourse(string courseTitle)
//    {
//        Course course = new Course(courseTitle, CourseNumberCount);
//        CourseNumberCount++;

//        Courses.Add(course);
//        return course;
//    }

//    public Student CreateStudent(string studentName)
//    {

//        Student newStudent = new Student(studentName, StudentId);
//        StudentId++;

//        Students.Add(newStudent);
//        return newStudent;
//    }

//    public Student RegisterStudent(string name)
//    {
//        Student student = new Student(name, StudentId);
//        Students.Add(student);
//        student.Department = this;

//        return student;
//    }

//    public void DeleteEnrollment(int enrollmentId)
//    {
//        try
//        {
//            //Enrollment enrollment = GetEnrollment(enrollmentId);
//            foreach (Enrollment e in Enrollments)
//            {
//                if (e.RegistrationId.Equals(enrollmentId))
//                {
//                    Enrollments.Remove(e);
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.Message);
//        }
//    }


//    public Student GetStudent(int id)
//    {
//        Student student;
//        foreach (Student s in Students)
//        {
//            if (s.StudentId == id)
//            {
//                student = s;
//                return student;
//            }
//        }
//        throw new Exception("Student not found");
//    }

//    public Course GetCourseId(int id)
//    {
//        Course course;
//        foreach (Course c in Courses)
//        {
//            if (c.CourseNumber.Equals(id))
//            {
//                course = c;
//                return course;
//            }
//        }
//        throw new Exception("Course not found");
//    }

//    public void EnrollStudent(int studentId, int courseId)
//    {
//        try
//        {
//            Student student = GetStudent(studentId);
//            Course course = GetCourseId(courseId);

//            Enrollment newEnrollment = new Enrollment(student, course);
//            Enrollments.Add(newEnrollment);

//            student.Courses.Add(newEnrollment);
//            course.Enrollments.Add(newEnrollment);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.Message);
//        }
//    }
//}

class VendingMachine
{
    public int SerialNumber { get; set; } = 1;
    public Dictionary<int, int> MoneyFloat { get; set; } = new Dictionary<int, int>();
    public Dictionary<Product, int> Inventory { get; set; } = new Dictionary<Product, int>();

    public string StockItem(Product product, int quantity)
    {
        int newValue = 0;
        bool productExists = false;
        // check if product is in the inventory
        foreach(KeyValuePair<Product, int> pair in Inventory)
        {
            // update the quantity if the product already exist in the Inventory
            if(product == pair.Key)
            {
                newValue = pair.Value + quantity;
                Inventory[pair.Key] = newValue;
                productExists = true;
                break;
            } 
        }

        // if the product is not yet in the inventory
        if (!productExists)
        {
            Inventory.Add(product, quantity);
            newValue = quantity;

        }
        Console.WriteLine($"Product: {product.Name}, Code: {product.Code}, Price: {product.Price}, Added Quantity: {quantity}, Total Quantity: {newValue}");
        return $"{product.Name}, {product.Code}, {product.Price}, {newValue}";
    }

    public string StockFLoat(int moneyDenomination, int quantity)
    {
        bool denominationExists = false;
        foreach (KeyValuePair<int, int> pair in MoneyFloat)
        {
            if (moneyDenomination == pair.Key)
            {
                MoneyFloat[pair.Key] = pair.Value + quantity;
                denominationExists = true;
            }
        }
        if (!denominationExists)
        {
            MoneyFloat.Add(moneyDenomination, quantity);
        }

        Console.WriteLine($"New money: {moneyDenomination} added {quantity} pcs.");
        foreach(KeyValuePair<int, int> pair in MoneyFloat)
        {
            Console.WriteLine($"{pair.Key} has {pair.Value} pcs.");
        }
        return $"Great job!";
    }
    //public void ComputeTotalCashIn(List<int> insertedMoney)
    //{
    //    for(insertedMoneyCount)
    //}
    public string VendItem(string code, List<int> insertedMoney)
    {
        int cashIn = insertedMoney[0];
        
        Console.WriteLine("----------------------------------------------------------------");
        // get the total amount of cashed in and the denomination
        for (int i = 0; i < insertedMoney.Count; i++)
        {
            // compute total money cashed in
            if(insertedMoney.Count - 1 == i)
            {
                cashIn = cashIn;
            }
            else {
                cashIn +=  insertedMoney[i + 1];
            }
            Console.WriteLine($"Successfully processed {insertedMoney[i]} total of ${cashIn}");

            foreach (KeyValuePair<int, int> money in MoneyFloat)
            {
                // update the denomination in MoneyFloat
                if (insertedMoney[i] == money.Key)
                {
                    MoneyFloat[money.Key] += 1;
                    Console.WriteLine($"You have inserted {money.Key}");
                }
            }
        }
        int totalChange = 0;
        foreach (KeyValuePair<Product, int> pair in Inventory)
        {
            //Console.WriteLine("sample");
            // if the product exist in the inventory or in-stock
            if (pair.Key.Code == code)
            {

                Console.WriteLine($"This is {pair.Key.Code}");
                Console.WriteLine($"Price is {pair.Key.Price}");
                // updates the inventory
                Inventory[pair.Key] -= 1;
                List<int> change = new List<int>();
                
                foreach (KeyValuePair<int, int> moneyPair in MoneyFloat)
                {
                    //Console.WriteLine($"This is the moneypair loop");

                    // update the money float, vend change
                    if (cashIn > pair.Key.Price)
                    {
                        Console.WriteLine($"So the cashIn is {cashIn} and key price is {pair.Key.Price}");
                        totalChange = cashIn - pair.Key.Price;
                        Console.WriteLine($"This is changeTotal {totalChange}");
                        // list the money denomincation change
                        if (totalChange / moneyPair.Key >= 0)
                        {
                            //Console.WriteLine($"This is the moneypair KEY {moneyPair.Key}");
                            change.Add(moneyPair.Key);
                        }
                        else
                        {
                            Console.WriteLine("Please insert enough amount");
                        }

                        
                        for(int i = 0; i < change.Count; i++)
                        {
                            if (moneyPair.Key == change[i])
                            {
                                MoneyFloat[moneyPair.Key] -= 1;
                                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                                
                            }
                        }
                        
                    }
                    else if(cashIn == pair.Key.Price)
                    {
                        Console.WriteLine($"Thank you for entering the exact amount");
                    }
                }
                Console.WriteLine($"You have received {totalChange} for your change");
            }
        }
        return "Vend complete";
    }
}



class Product
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Code { get; set; }
    public Product(string name, int price, string code)
    {
        Name = name;
        Price = price;
        Code = code;
    }
}

