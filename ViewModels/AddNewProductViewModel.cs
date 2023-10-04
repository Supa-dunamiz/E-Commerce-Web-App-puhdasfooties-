namespace PuhdasApp.ViewModels
{
    public class AddNewProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public IFormFile Image { get; set; }
    }
}
