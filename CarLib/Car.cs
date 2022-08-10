namespace CarLib;


//Compares Model
public class CarModelComparer : IComparer<Car>
{
    public int Compare(Car? x, Car? y) => x.Model.CompareTo(y.Model);
}
//--------------



//Compares Year
public class CarYearComparer : IComparer<Car>
{
    public int Compare(Car? x, Car? y) => x.Year.CompareTo(y.Year);
}
//------------



public class Car
{
    private int _year;
    private int _msrp;
    public string? Model { get; set; }
    public string? Make { get; set; }
    public int Year
    {
        get { return _year; }
        set
        {
            if (value < 1886) //year first car was produced
            {

                throw new InvalidYearException("Age must be greater than 1886!");
            }

            if (value > 2025) //nothing newer than 2025 as of now
            {
                //Invalid 
                throw new InvalidYearException("Age must be less than 2025!");
            }
            _year = value;
        }
    }

    public int MSRP
    {

        get { return _msrp; }
        set
        {
            if (value < 1) //Cannot be less than 1
            {

                throw new InvalidMSRPException("The MSRP cannot be less than or equal to 1");
            }

            if (value > 100000) //$100,000 cuttoff mark
            {
                throw new InvalidMSRPException("The MRP cannot be greater than $100,000.");
            }
            _msrp = value;
        }
    }


    //retruns the data in CSV format 
    public string ToCSV()
    {
        return $"{Model}, {Make}, {Year}, {MSRP}";
    }
}