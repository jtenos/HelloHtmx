using System;
namespace HelloHtmx.DataModels;

public record class Person(long PersonID, string Name, long Age)
{
    public static Person Generate()
    {
        string name = Faker.Name.FullName();
        int age = Faker.RandomNumber.Next(min: 0, max: 100);
        return new(PersonID: -1, Name: name, Age: age);
    }
}
