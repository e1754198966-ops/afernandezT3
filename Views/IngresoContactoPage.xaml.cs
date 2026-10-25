using System.Text.RegularExpressions;
using afernandezT3.Views;

namespace afernandezT3.Views;

public partial class IngresoContactoPage : ContentPage
{
    public IngresoContactoPage()
    {
        InitializeComponent();
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        // 1. Validar Tipo de Identificación
        if (pickerTipoId.SelectedIndex == -1)
        {
            await DisplayAlert("Error", "Debe seleccionar un tipo de identificación.", "OK");
            return;
        }

        string tipoId = pickerTipoId.SelectedItem.ToString();
        string id = txtIdentificacion.Text?.Trim() ?? "";

        // Validaciones según el tipo de ID
        if (tipoId == "CI")
        {
            if (id.Length != 10 || !Regex.IsMatch(id, @"^\d+$"))
            {
                await DisplayAlert("Error de Validación", "La Cédula de Identidad (CI) debe tener exactamente 10 dígitos numéricos.", "OK");
                return;
            }
        }
        else if (tipoId == "RUC")
        {
            if (id.Length != 13 || !Regex.IsMatch(id, @"^\d+$"))
            {
                await DisplayAlert("Error de Validación", "El RUC debe tener exactamente 13 dígitos numéricos.", "OK");
                return;
            }
        }
        else if (tipoId == "Pasaporte")
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                await DisplayAlert("Error de Validación", "Ingrese un número de pasaporte válido.", "OK");
                return;
            }
        }

        // 2. Validar Nombres y Apellidos
        if (string.IsNullOrWhiteSpace(txtNombres.Text) || string.IsNullOrWhiteSpace(txtApellidos.Text))
        {
            await DisplayAlert("Error", "Debe ingresar nombres y apellidos.", "OK");
            return;
        }

        // 3. Validar Correo Electrónico
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (string.IsNullOrWhiteSpace(txtCorreo.Text) || !Regex.IsMatch(txtCorreo.Text.Trim(), emailPattern))
        {
            await DisplayAlert("Error", "Ingrese un correo electrónico válido.", "OK");
            return;
        }

        // 4. Validar Salario
        if (!decimal.TryParse(txtSalario.Text, out decimal salario) || salario <= 0)
        {
            await DisplayAlert("Error", "Ingrese un salario numérico válido mayor a 0.", "OK");
            return;
        }
        // Empaquetar los datos de forma individual en un Diccionario hacia la ruta de navegación
        var parametros = new Dictionary<string, object>
        {
            { "TipoId", tipoId },
            { "Identificacion", id },
            { "Nombres", txtNombres.Text.Trim() },
            { "Apellidos", txtApellidos.Text.Trim() },
            { "Fecha", $"{dtpFecha.Date:yyyy-MM-dd}" },
            { "Correo", txtCorreo.Text.Trim() },
            { "Salario", salario.ToString() }
        };
        await Shell.Current.GoToAsync(nameof(DetalleContactoPage), parametros);
    }
}