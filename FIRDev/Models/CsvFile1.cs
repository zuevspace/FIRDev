namespace FIRDev;

public class CsvFile1
{
    public CsvFile1(string row)
    {
        var values = row.Split(',');
        DateLoad = DateTime.Now;
        Id = values[0];
        Date = DateTime.Parse(values[1]);
        Name = values[2];
        Address = values[3];
        Email = values[4];
    }

    public DateTime DateLoad { get; set; }
    public string? Id { get; set; }
    public DateTime Date { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
}
