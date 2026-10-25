namespace afernandezT3.Views;

[QueryProperty(nameof(TipoId), "TipoId")]
[QueryProperty(nameof(Identificacion), "Identificacion")]
[QueryProperty(nameof(Nombres), "Nombres")]
[QueryProperty(nameof(Apellidos), "Apellidos")]
[QueryProperty(nameof(Fecha), "Fecha")]
[QueryProperty(nameof(Correo), "Correo")]
[QueryProperty(nameof(SalarioStr), "Salario")]
public partial class DetalleContactoPage : ContentPage
{
    private string _tipoId;
    public string TipoId
    {
        get => _tipoId;
        set { _tipoId = value; lblTipoId.Text = value; }
    }

    private string _identificacion;
    public string Identificacion
    {
        get => _identificacion;
        set { _identificacion = value; lblIdentificacion.Text = value; }
    }

    private string _nombres;
    public string Nombres
    {
        get => _nombres;
        set { _nombres = value; ActualizarNombresCompletos(); }
    }

    private string _apellidos;
    public string Apellidos
    {
        get => _apellidos;
        set { _apellidos = value; ActualizarNombresCompletos(); }
    }

    private string _fecha;
    public string Fecha
    {
        get => _fecha;
        set
        {
            _fecha = value;
            if (DateTime.TryParse(value, out DateTime dt))
                lblFecha.Text = dt.ToString("dd/MM/yyyy");
        }
    }

    private string _correo;
    public string Correo
    {
        get => _correo;
        set { _correo = value; lblCorreo.Text = value; }
    }

    private decimal _salario;
    public string SalarioStr
    {
        set
        {
            if (decimal.TryParse(value, out _salario))
            {
                lblSalario.Text = $"${_salario:F2}";
                decimal aporteIESS = _salario * 0.0945m;
                lblIESS.Text = $"${aporteIESS:F2}";
            }
        }
    }

    public DetalleContactoPage()
    {
        InitializeComponent();
    }

    private void ActualizarNombresCompletos()
    {
        lblNombresCompletos.Text = $"{Nombres} {Apellidos}";
    }

    private async void OnExportarClicked(object sender, EventArgs e)
    {
        try
        {
            decimal aporteIESS = _salario * 0.0945m;

            string contenido = "========== DATOS DEL CONTACTO ==========\n" +
                               $"Tipo Documento : {_tipoId}\n" +
                               $"Identificación : {_identificacion}\n" +
                               $"Nombres        : {_nombres}\n" +
                               $"Apellidos      : {_apellidos}\n" +
                               $"Fecha Nac.     : {lblFecha.Text}\n" +
                               $"Correo         : {_correo}\n" +
                               $"Salario        : ${_salario:F2}\n" +
                               $"Aporte IESS    : ${aporteIESS:F2}\n" +
                               "========================================\n" +
                               $"Fecha Registro : {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

            string fileName = $"Contacto_{_identificacion}.txt";
            string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            File.WriteAllText(filePath, contenido);

            await DisplayAlert("Éxito", $"Archivo exportado correctamente en:\n{filePath}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo exportar el archivo: {ex.Message}", "OK");
        }
    }
}