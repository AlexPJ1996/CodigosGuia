using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace [Proyecto]
{	
	// Comentarios multi-linea
	/*
	
	*/
	
	public partial class [Form] : Form
	{
		// Procedimiento
		void [Procedimiento](){
			
		}
		
		// Obtener información de ensamblado de aplicación/Proyecto
		void AppInfo()
        {
            string AppTit = Application.ProductName;
            string AppDev = Application.CompanyName;
            string AppVer = Application.ProductVersion;
        }
		
		// MessageBox.Show
		void MessageBoxShow()
		{
			MessageBox.Show("");
			// MessageBox multilinea
			MessageBox.Show([Linea 1] + Environment.NewLine + [Linea 2] + Environment.NewLine + [Linea N]);
			// If MessageBox.Show
			if (MessageBox.Show("", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				
			}
			else
			{
				
			}
		}
		
		// Instrucción Try Catch
		void TryCatch()
		{
			try
			{
				
			}
			catch (Exception ex)
			{
				// Cuadro de mensaje (WinForms)
				MessageBox.Show(ex.Message);
				// Alamcenar en variable para uso posterior
				string [Variable] = ex.Message;
			}
		}
		
		/*
		Instrucción If Else
		&& = AND
		|| = OR
		== es = (Igual a...)
		!= es <> (Diferente de...)
		*/
		void IfElse()
		{
			// Forma 1
			if ([Operacion logica])
			{
				
			}
			else
			{
				
			}
			// Forma 2
			if ([Operacion logica])
			{
				
			}
			else if ([Operacion logica])
			{
				
			}
		}
		
		// Instrucción Switch (Select Case)
		void SelCase()
		{
			switch ([Variable])
			{
				case [Value1]:
				    [Instrucción1]
					break;
				case [Value2]:
				    [Instrucción2]
					break;
				case [ValueN]:
				    [Instrucción3]
					break;
				default:
				    [Instrucción]
					break;
			}
		}
		
		// Formatos
		void TxtFormt()
		{
			// MaxLength
			[TextBox].MaxLength = [Longitud];
			// TextBox PasswordChar
			[TextBox].UseSystemPasswordChar = [true/false];
			// TextAlign
			[TextBox].TextAlign = HorizontalAlignment.[Center/Right/Left] ;
			// CharacterCasing
			
		}
		
		// Llamar clases
		void CallClass()
		{
			[Clase] [Variable] = new [Clase]();
			/*
			.Net 5.0 en adelante
			[Clase] [Variable] = new();
			*/
		}
		
		// Instrucción Show para mostrar Forms, similar al llamdo de clase
		void ShowForms()
		{
			[Form] [Variable] = new [Form]();
			[Variable].Show();
			// Mostrar Form unida con instrucción frm_closing
			[Variable].FormClosing += Frm_Closing;
		}
		
		// Cerrar/Ocultar Forms
		void EndProj()
		{
			// Equivalente End
			Environment.Exit(0);
			// Equivalente Me.Close()
			this.Close();
			// Ocultar
			this.Hide();
		}
		
		// Mostrar Form al cerrar otro Form sin necesidad de un evento FormClosed
		private void Frm_Closing(object sender, FormClosingEventArgs e)
		{
			this.Show();
		}
		
		// Crear variable String multi-linea
		void StringML()
		{
			StringBuilder [Variable] = new StringBuilder();
			// StringBuilder [Variable] = new();
			// Linea 1
			[Variable].AppendLine("");
			// Linea 2
			[Variable].AppendLine("");
			// Linea N
			[Variable].AppendLine("");
		}
		
		// Unir cadenas string &
		void UnirCadenas()
		{
			[String] = String.Concat([String 1] , [String 2]),... [String N];
		}
		
		// Solo recibir numeros en un TextBox (int 0), mediante evento KeyPress
		public void OnlyNumbers(KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}
		
		// Solo recibir numeros en un TextBox y una coma (decimal 0,0), mediante evento KeyPress
		public void NumDecim(object sender, KeyPressEventArgs e)
        {
            
        }
		
		// Realizar cálculos básicos con TextBox/Variable
		void Calcular()
		{
			string Operacion;
			int/decimal [Variable 1], [Variable 2], [Variable 3];
			/*
			Int32 = Números enteros de -2.147.483.648 a 2.147.483.647 (9 cifras 000.000.000)
			Int64 = Números enteros de -9.223.372.036.854.775.808 a 9.223.372.036.854.775.807 (18 cifras 000.000.000.000.000.000)
			*/
			[Variable 1] = Convert.ToInt64/ToDecimal([TextBox1]);
			[Variable 2] = Convert.ToInt64/ToDecimal([TextBox2]);
			// Suma
			[Variable 3] = [Variable 1] + [Variable 2];
			// Resta
			[Variable 3] = [Variable 1] - [Variable 2];
			// Multiplicación
			[Variable 3] = [Variable 1] * [Variable 2];
			// División
			[Variable 3] = [Variable 1] / [Variable 2];
			// Mostrar resultado
			[TextBox3] = [Variable 3].ToString();
		}
		
		// Capturar datos de un DataGridView
		void CellE()
		{
			try
			{
				// Mostrar datos texto en un Label/TextBox/Variable
				[Label/TextBox] = [DataGridView].CurrentRow.Cells[Columna].Value.ToString();
				// Mostrar datos bit/boolean en un CheckBox
				object [Variable] = [DataGridView].CurrentRow.Cells[Columna].Value;
				[CheckBox].Checked = Convert.ToBoolean([Variable]);
				// Mostrar datos blob/longblob/varbinary en un PictureBox
				var [Variable 1] = (Byte[])[DataGridView].CurrentRow.Cells[Columna].Value;
				var [Variable 2] = new MemoryStream([Variable 1);
				[PictureBox].Image = Image.FromStream([Variable 2]);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		// Cambiar el ancho/nombre de las columnas de un DataGridView
		void CellZ()
		{
			try
			{
				// Ancho de columna
				[DataGridView].Columns[Columna].Width = [Value];
				// Texto/cabecera de columna
				[DataGridView].Columns[Columna].HeaderText = [Text];
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		// Cambiar los colores de las filas de un DataGridView
		void ColorsDGV()
		{
			[DataGridView].RowsDefaultCellStyle.BackColor = Color.[Color];
			[DataGridView].AlternatingRowsDefaultCellStyle.BackColor = Color.[Color];
		}
		
		void ClearDGV()
		{
			[DataGridView].Columns.Clear();
		}
		
		// No permitir modificación de datos en un ComboBox
		void PropCBx()
		{
			[ComboBox].DropDownStyle = ComboBoxStyle.DropDownList;
			[ComboBox].FlatStyle = FlatStyle.Popup;
		}
		
		// Borrar los items de un ComboBox cuando no están rellenados por medio de un DataSet
		void ClearCBx()
		{
			[ComboBox].Items.Clear();
            [ComboBox].ResetText();
		}
		
		// Maximizar ventana con FormBorderStyle: None sin cubrir toda la pantalla
		void MaxRestForm()
		{
			
		}
		
		// Captar valores de fecha (yyyy/mm/dd) y hora (hh:mm:ss) de un DateTimePicker
		string Fecha, Hora;
		void MyDateTime()
		{
			Fecha = ;
			Hora = ;
		}
		
		// Configurar un OpenFileDialog en un para cargar una imagen en un PictureBox
		void OFile()
		{
			OpenFileDialog [Variable] = new OpenFileDialog
			{
                Filter = "Imagenes JPG|*.jpg|Imagenes JPEG|*.jpeg|Imagenes PNG|*.png",
                Title = "Seleccionar imagen",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if ([Variable].ShowDialog() == DialogResult.OK)
            {
                [PictureBox].Image = Image.FromFile([Variable].FileName);
            }
		}
		
		// Abrir formularios para una unica pantalla
		private void AbrirForm(object FormA)
        {
            // Vaciar panel en caso de tener otro form abierto
			if ([Panel].Controls.Count > 0)
            {
                [Panel].Controls.RemoveAt(0);
            }
            Form [Variable] = FormA as Form;
            [Variable].TopLevel = false;
            [Variable].Dock = DockStyle.Fill;
            [Variable].BackColor = BackColor;
            [Panel].Controls.Add([Variable]);
            [Panel].Tag = [Variable];
            [Variable].Show();
        }
		
		// Mostrar respectivo Form llamando 'AbrirForm'
		void Abrir()
		{
			AbrirForm(new [Form]());
		}
	}
}