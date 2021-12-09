
public class Land
{
    public string landName{get; set;}
    public int price{get; set;}
    public int numOfBuildings{get; set;}

    public Land(string landName,int price, int numOfBuildings )
    {
        this.landName = landName;
        this.price = price;
        this.numOfBuildings = numOfBuildings;
    }

    

}
