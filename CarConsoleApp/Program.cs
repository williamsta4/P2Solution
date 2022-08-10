using CarLib;


//Using CRUD Create Read Update Delete
ICarRepository repo = new CSVCarRepository("Cars.csv");
List<Car> cars;
IComparer<Car> comparer;
string compareHow = "By Name";



//------Prompt------
Console.WriteLine(new string('-', 80));
Console.WriteLine("This program uses the CRUD principles to help you put forth a list of vehicles.");
Console.WriteLine("The first automobile was produced in 1886 by the Mercedez Benz corporation.");
Console.WriteLine("Therefore, a vehicle cannot be older than 1886, nor can it be newer than 2024");
Console.WriteLine("If your car's age is older than 25 year, you can apply for antique tags.");
Console.WriteLine("It will also tell you what today's price would be multiplied by 1/4.");
//------------------


string answer;
do
{
    cars = repo.ReadAll();//Shows all cars from file
    if (compareHow == "By Model") comparer = new CarModelComparer(); //sorts by model
    else comparer = new CarYearComparer(); //sorts by year
    cars.Sort(comparer);//compares

    ShowAllCars(cars);//shows all cars from file
    Console.WriteLine(new string('-', 80));
    Console.WriteLine();
    ShowMenu(); //shows menu

    //Begninng of swtich
    answer = ReadString("Enter your choice: ");
    switch (answer.ToUpper())
    {
        case "C":
            repo.Create(ReadCar());
            break;

        case "R":
            ViewCar(repo);
            break;

        case "U":
            UpdateCar(repo);
            break;

        case "D":
            DeleteCar(repo);
            break;

        case "M":
            compareHow = "By Model";
            break;

        case "Y":
            compareHow = "By Year";
            break;
    }
    //End of switch
} while (answer.ToUpper() != "X");
//-----------



//Show menu
static void ShowMenu()
{
    Console.WriteLine("C = Create Car,\nR= Read Car,\nU = Update Car,\nD = Delete Car," +
                       "\nM = Order by Model \nY= Order by Year \nX = Exit");
}
//-----------



//ShowAllCars 
static void ShowAllCars(List<Car> cars)
{
    Console.WriteLine(new String('-', 80));
    Console.WriteLine($"{"Model: ",-15} {"Make: ",-15} {"Year: ",-3} {"MSRP: "} Today's Price: Antique Tags: ");
    Console.WriteLine(new String('-', 80));
    foreach (Car car in cars) { Console.WriteLine($"{car.Model,-15}{car.Make,-15} {car.Year,-3} {"\t" + "$" + car.MSRP,-3} \t ${TodaysPrice(car)}{"\t\t" + ApplyForAntiqueTags(car)}"); }

}
//-----------



//Read Car
static Car ReadCar()
{
    Car car = new();
    car.Model = ReadString("Enter your car's model: ");
    car.Make = ReadString("Enter your car's make: ");
    ReadYear(car);
    ReadMSRP(car);
    return car;
}
//-----------



//View Car
static void ViewCar(ICarRepository repo)
{
    var carModel = ReadString("Enter your car's model: ");
    var car = repo.Read(carModel);
    if (car != null)
    {
        Console.WriteLine();
        Console.WriteLine($"Model:{car.Model} Make:{car.Make} Year:{car.Year} MSRP:${car.MSRP} Today's Price: ${TodaysPrice(car)}");
    }

    else { Console.WriteLine($"{carModel} was not found!"); }
}
//-----------



//Update Car
static void UpdateCar(ICarRepository repo)
{
    var carModel = ReadString("Enter your car's model: ");
    var car = repo.Read(carModel);
    if (car != null)
    {
        Car newCar = ReadCar();
        repo.Update(carModel, newCar);
        Console.WriteLine($"{carModel} was updated");
    }
    else { Console.WriteLine($"{carModel} was not found!"); }
}
//-----------



//Delete Car
static void DeleteCar(ICarRepository repo)
{
    var carModel = ReadString("Enter your car's model: ");
    var car = repo.Read(carModel);
    if (car != null)
    {
        //Confirmation for deleting a car
        string confirm;
        confirm = ReadString("Are you sure you want to delete? (Enter Yes to delete)");
        if (confirm == "Yes")
        {
            repo.Delete(carModel);
            Console.WriteLine($"{carModel} was removed");
        }
        else { Console.WriteLine($"{carModel} was not removed."); }

    }
    else { Console.WriteLine($"{carModel} was not found!"); }
}
//-----------



//Read String
static string ReadString(string prompt)
{
    string value = "";
    Console.WriteLine(prompt);
    string? valueStr = Console.ReadLine();

    if (valueStr != null) { value = valueStr.Trim(); }
    return value;
}
//-----------



//Read Year
static void ReadYear(Car car)
{
    do
    {
        int year = ReadInteger("Please enter your cars year: ");
        try { car.Year = year; break; }
        catch (InvalidYearException ex) { Console.WriteLine(ex.Message); }
    } while (true);
}
//-----------



//ReadMSRP
static void ReadMSRP(Car car)
{
    do
    {
        int msrp = ReadInteger("Enter your car's MSRP: ");
        try { car.MSRP = msrp; break; }
        catch (InvalidMSRPException ex) { Console.WriteLine(ex.Message); }
    } while (true);
}
//-----------



//Read Integer
static int ReadInteger(string prompt)
{
    int value;
    do
    {
        Console.WriteLine(prompt);
        string? valueStr = Console.ReadLine(); //creates a variable to store the inputted value in
        if (valueStr != null)
        {
            try
            {
                value = int.Parse(valueStr); //parses inputted valueStr to age
                Console.WriteLine($"You entered: {value}");
                break; //breaks the loop
            }

            catch (FormatException ex) { Console.WriteLine(ex.Message); }
        }
    } while (true);
    return value;
}
//-----------



//Apply For Antique Tags
static string ApplyForAntiqueTags(Car car)
{
    int result = (2022 - car.Year); //2022 is current year
    string resultStr = ("");

    if (result < 25) //if current year - car's year is less than 25 
    {

        resultStr = "No";
    }

    else //if a car is over 25 years old
    {
        resultStr = "Yes";
    }
    return resultStr; ;
}
//-----------



//Todays Price
static int TodaysPrice(Car car)
{
    int todaysPrice = ((car.MSRP * 1 / 4) + car.MSRP); //multipies MSRP by .25
    return todaysPrice;
}
//-----------
//END OF Program.cs