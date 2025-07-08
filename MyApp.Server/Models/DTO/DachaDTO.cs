namespace MyApp.Server.Models.DTO
{
    public class DachaDTO
    {
        // DTO Files are need to give a data which is needed for the client side.
        // With this we can avoid sending unnecessary data to the client.

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
