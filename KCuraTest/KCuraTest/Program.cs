using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;



public class Lad
{
    public string EndPoint;
    public string TestCase;
    public List<TestMethods> SampleArr;
    
}

public class TestMethods
{
    public string id;
    public string Name;
}
class Program
{
    static void Main()
    {
        var obj = new Lad
        {
            EndPoint = "Hardship",
            TestCase = "Employment",
            SampleArr = new List<TestMethods>()
            {
                new TestMethods
                {
                    id = "1",
                    Name = "Test Method Name"
                },
                new TestMethods
                {
                    id="2",
                    Name="Test Method Sample 2"
                }
            }
        };

        var json = new JavaScriptSerializer().Serialize(obj);

        using(var sw=new StreamWriter())
        {

        }


        Console.WriteLine(json);
        Console.ReadLine();





    }
}