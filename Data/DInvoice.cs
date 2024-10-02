using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Data
{
    public class DInvoice
    {
        public List<Invoice> ListarFactura(int invoice_id, int customer_id, DateTime date, decimal total, bool active, string numero_de_factura)
        {
            List<Invoice> listaFacturas = new List<Invoice>();

            using (SqlConnection connection = new SqlConnection(AccesoDatos.cadena))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ListarFacturas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@invoice_id", SqlDbType.Int)).Value = invoice_id;
                    command.Parameters.Add(new SqlParameter("@customer_id", SqlDbType.Int)).Value = customer_id;
                    command.Parameters.Add(new SqlParameter("@date", SqlDbType.DateTime)).Value = date;
                    command.Parameters.Add(new SqlParameter("@total", SqlDbType.Decimal)).Value = total;
                    command.Parameters.Add(new SqlParameter("@active", SqlDbType.Bit)).Value = active;
                    command.Parameters.Add(new SqlParameter("@numero_de_factura", SqlDbType.VarChar)).Value = numero_de_factura;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Invoice factura = new Invoice
                        {
                            InvoiceId = Convert.ToInt32(reader["invoice_id"]),
                            CustomerId = Convert.ToInt32(reader["customer_id"]),
                            Date = Convert.ToDateTime(reader["date"]),
                            Total = Convert.ToDecimal(reader["total"]),
                            Active = Convert.ToBoolean(reader["active"]),
                            NumeroDeFactura = Convert.ToString(reader["numero_de_factura"])
                        };

                        listaFacturas.Add(factura);
                    }
                }
            }

            return listaFacturas;
        }
    }
}