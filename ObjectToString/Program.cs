Person person = new Person() { Name = "ala", Id = 1, Address = new Addrss() { City = new City() { Id = 2, Name= "Wroclaw"}, State = new State(){ Id = 3, Name = "Dolnosloskie" } }, LastName = "Kowalska"};
//Person person = new Person() { Name = "ala", Id = 1, Address = new Addrss() { City = "Wroclaw", State = "State" }, LastName = "Kowalska"};
//Person person = new Person() { Name = "ala", Id = 1, Address = "Adress" };
string s = ObjectToString.ObjToStr(person, "=;");
Console.WriteLine(s);
Person p2 = ObjectToString.StrToObj<Person>(s, "=;");
Console.WriteLine(p2.Name);

public class Person
{
    public int Id {  get; set; }
    public string Name {  get; set; }
    public Addrss Address {  get; set; }
    public string LastName {  get; set; }
}
public class Addrss
{
    public City City {  get; set; }
    public State State {  get; set; }
}

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class State
{
    public int Id { get; set; }
    public string Name { get; set; }
}