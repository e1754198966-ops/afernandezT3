using afernandezT3.Views;

namespace afernandezT3;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(DetalleContactoPage), typeof(DetalleContactoPage));
    }
}