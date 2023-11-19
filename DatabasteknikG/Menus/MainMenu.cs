namespace DatabasteknikG.Menus;

internal class MainMenu
{
    private readonly CustomerMenu _customerMenu;
    private readonly ProductMenu _productMenu;

    public MainMenu(CustomerMenu customerMenu, ProductMenu productMenu)
    {
        _customerMenu = customerMenu;
        _productMenu = productMenu;
    }

    public async Task StartAsync()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Main Meny");
            Console.WriteLine("1. Hantera Kunder");
            Console.WriteLine("2. Hantera Produkter");

            Console.Write("Välj ett alternativ: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await _customerMenu.ManageCustomers();
                    break;

                case "2":
                    await _productMenu.ManageProducts();
                    break;
            }
        }
        while (true);
    }





}