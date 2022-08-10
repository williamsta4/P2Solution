namespace CarLib;

/// <summary>
/// interface fpr CRRUD
/// </summary>
public interface ICarRepository
{
    Car Create(Car car);
    List<Car> ReadAll();
    Car? Read(string id); //id is model 

    void Update(string oldID, Car car); //oldID is model
    void Delete(string id); //id is model

}
