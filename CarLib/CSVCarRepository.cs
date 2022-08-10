namespace CarLib;

public class CSVCarRepository : ICarRepository
{
    private string _filePath;

    public CSVCarRepository(string filePath)
    {
        _filePath = filePath;
    }

    //creates Car 
    public Car Create(Car car)
    {
        try
        {
            StreamWriter writer = new StreamWriter(_filePath, append: true); //creates instance of file, appends if does not exists
            writer.WriteLine(car.ToCSV());
            writer.Close();
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return car;
    }



    public Car? Read(string id)
    {
        Car? car = null;
        try
        {
            string? record;
            StreamReader reader = new(_filePath);
            record = reader.ReadLine();

            //Check for End Of File Character (EO
            while (record != null)
            {
                string[] fields = record.Split(',');
                if (fields[0] == id)
                {
                    car = new()
                    {
                        Model = fields[0],
                        Make = fields[1],
                        Year = int.Parse(fields[2]),
                        MSRP = int.Parse(fields[3]),//EOF
                    };
                    break; //exits while loop
                }
                record = reader.ReadLine();
            }
            reader.Close();
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        return car;
    }



    public List<Car> ReadAll()
    {
        List<Car> cars = new();
        try
        {
            string? record;
            StreamReader reader = new(_filePath);
            record = reader.ReadLine();

            //Check for End Of File Character (EO
            while (record != null)
            {
                string[] fields = record.Split(',');
                Car car = new()
                {
                    Model = fields[0],
                    Make = fields[1],
                    Year = int.Parse(fields[2]),
                    MSRP = int.Parse(fields[3]),//EOF
                };
                cars.Add(car);
                record = reader.ReadLine();
            }
            reader.Close();
        }

        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        return cars;
    }

    public void Update(string oldID, Car car)
    {
        try
        {
            //creates temp file
            string tempFileName = _filePath + ".temp";
            StreamWriter writer = new(tempFileName);

            //opens file for reading the main file
            string? record;
            StreamReader reader = new(_filePath);
            record = reader.ReadLine();

            while (record != null)
            {
                string[] fields = record.Split(',');
                if (fields[0] != oldID)
                {
                    writer.WriteLine(record);
                }

                else
                {
                    writer.WriteLine(car.ToCSV());
                }
                record = reader.ReadLine();
            }

            reader.Close();
            writer.Close();

            File.Delete(_filePath);
            File.Move(tempFileName, _filePath); //Renames the file 
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Delete(string id)
    {
        try
        {
            //creates temp file
            string tempFileName = _filePath + ".temp";
            StreamWriter writer = new(tempFileName);

            //opens file for reading the main file
            string? record;
            StreamReader reader = new(_filePath);
            record = reader.ReadLine();

            while (record != null)
            {
                string[] fields = record.Split(',');
                if (fields[0] != id)
                {
                    writer.WriteLine(record);

                }
                record = reader.ReadLine();
            }


            reader.Close();
            writer.Close();

            File.Delete(_filePath);
            File.Move(tempFileName, _filePath); //Renames the file 

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

}

