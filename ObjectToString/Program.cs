using System;
Order order = new Order()
{
    orderID = 1,
    address = new Address()
    {
        city = "Wroclaw",
        postalCode = 1214,
        street = "Harcerska"
    },
    person = new Person()
    {
        name = "Michal",
        lastName = "Zielinski",
        address = new Address()
        {
            city = "Wroclaw",
            postalCode = 1214,
            street = "Harcerska"
        }
    }
};
string a = "michal";
string s = ObjectToString.ObjToStr(order);
Console.WriteLine(s);
Order o = ObjectToString.StrToObj<Order>(s);
Console.WriteLine();
public class Order
{
    public int orderID {  get; set; }
    public Person person {  get; set; }
    public Address address {  get; set; }
}
public class Person
{
    public string name {  get; set; }
    public string lastName {  get; set; }
    public Address address {  get; set; }
}
public class Address
{
    public string street { get; set; }
    public string city {  get; set; }
    public int postalCode {  get; set; }
}