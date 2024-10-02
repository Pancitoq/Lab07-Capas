using Data;
using Entity;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo07
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnListarFacturas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DInvoice dInvoice = new DInvoice();

                // Captura de parámetros desde los controles
                int invoiceId = int.TryParse(txtInvoiceId.Text, out var tempInvoiceId) ? tempInvoiceId : 0;
                int customerId = int.TryParse(txtCustomerId.Text, out var tempCustomerId) ? tempCustomerId : 0;
                DateTime date = dpDate.SelectedDate.HasValue ? dpDate.SelectedDate.Value : DateTime.Now; // Usa la fecha actual si no se selecciona nada
                decimal total = decimal.TryParse(txtTotal.Text, out var tempTotal) ? tempTotal : 0;
                bool active = chkActive.IsChecked.HasValue && chkActive.IsChecked.Value;
                string numeroDeFactura = txtNumeroFactura.Text;

                // Llamada al método para listar facturas
                List<Invoice> facturas = dInvoice.ListarFactura(invoiceId, customerId, date, total, active, numeroDeFactura);

                // Asignación de la lista de facturas al DataGrid
                dgvFacturas.ItemsSource = facturas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}");
            }
        }
    }
}