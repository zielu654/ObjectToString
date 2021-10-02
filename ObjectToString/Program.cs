//Person person = new Person() { Name = "ala", Id = 1, Address = new Adderss() { City = new City() { Id = 2, Name= "Wroclaw"}, State = "State" }, LastName = "Kowalska"};
Person person = new Person() { Name = "ala", Id = 1, Address = new Adderss() { City = "Wroclaw", State = "State" }, LastName = "Kowalska"};
//Person person = new Person() { Name = "ala", Id = 1, Address = "Adress" };
string s = ObjectToString.ObjToStr(person, "=;");
Console.WriteLine(s);
Person p2 = ObjectToString.StrToObj<Person>(s, "=;");
Console.WriteLine(p2.Name);

public class Person
{
    public int Id {  get; set; }
    public string Name {  get; set; }
    public Adderss Address {  get; set; }
    public string LastName {  get; set; }
}
public class Adderss
{
    public string City {  get; set; }
    public string State {  get; set; }
}

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
}