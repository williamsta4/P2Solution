using CarLib;

namespace CarTestProject;

[TestClass]
public class ACar
{
    [TestMethod]
    public void CanHaveAModel()
    {
        var sut = new Car();
        sut.Model = "Mustang";
        Assert.AreEqual("Mustang", sut.Model);
    }

    [TestMethod]
    public void CanHaveAMake()
    {
        var sut = new Car();
        sut.Make = "Ford";
        Assert.AreEqual("Ford", sut.Make);
    }

    [TestMethod]
    public void CanHaveAYear()
    {
        var sut = new Car();
        sut.Year = 1989;
        Assert.AreEqual(1989, sut.Year);
    }

    [TestMethod]
    public void CanHaveAMSRP()
    {
        var sut = new Car();
        sut.MSRP = 5000;
        Assert.AreEqual(5000,sut.MSRP);
    }
}