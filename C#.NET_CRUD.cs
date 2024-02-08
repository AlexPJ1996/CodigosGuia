using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Librerías
using System.Data;             // Usar Clase DataTable
using System.IO;               // Usar Clase MemoryStream
using System.Windows.Forms;    // Usar opciones/componentes de WinForms
using System.Data.SqlClient;   // SQL Server
using System.Data.OleDb;       // Access (2003/2007-2013)
using System.Data.SQLite;      // SQLite
using MySqlConnector;          // MySQL

namespace [Proyecto]
{
    class CRUD
    {
		/*
		ConecctionString: (https://www.connectionstrings.com/)
		la cadena de conexión o ConecctionString deberá estar entre las comillas
		(Directorio) será cambiada por |DataDirectory|\ si es una base de datos local
		
		SQL Server LocalDB:
		Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\[Database].mdf; Integrated Security=True
		
		Access
		2003
		Provider=Microsoft.Jet.OLEDB.4.0; Data Source=(Ubicación)\[Database].mdb
		Provider=Microsoft.Jet.OLEDB.4.0; Data Source=(Ubicación)\[Database].mdb; Jet OLEDB:Database Password=[Password]
		2007-2013
		Provider=Microsoft.ACE.OLEDB.12.0; Data Source=(Ubicación)\[Database].acCRUD
		Provider=Microsoft.ACE.OLEDB.12.0; Data Source=(Ubicación)\[Database].acCRUD; Jet OLEDB:Database Password=[Password]
		
		SQLite
		Data Source=(Ubicación)\[Database].db; Version=3
		
		MySQL
		Server=[ServerAddress]; Database=[Database]; Uid=[User]; Pwd=[Password];
		Server=[ServerAddress]; Port=[#Port000]; Database=[Database]; Uid=[User]; Pwd=[Password];
		*/
		
		// C# no admite la opción de usar variables para almacenar ConnectionString con el metodo actual
		private SqlConnection Conectar = new SqlConnection(@"");
		/*
		.Net 5.0 en adelante
		private SqlConnection Conectar = new(@"");
		*/
        public bool EsCon, EsLec;
		
		// Iniciar y probar conexión con base de datos
        public void Conexion()
        {
            EsCon = false;
            while (EsCon == false)
            {
                try
                {
                    Conectar.Open();
                    EsCon = true;
                    Conectar.Close();
                }
                catch (Exception ex)
                {
                    EsCon = false;
                    if (MessageBox.Show(string.Concat(ex.Message + Environment.NewLine + "" + Environment.NewLine + "Desea reintentar la conexión?"), "Error", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }
		
		/*
        Consultas SQL: SELECT
        Utilizable para hacer login con usuario y contraseña almacenada en base de datos 
        */
        public void Leer(string SQL)
        {
            Conectar.Open();
            try
            {
                SqlCommand CMD = new SqlCommand(SQL, Conectar);
				// SqlCommand CMD = new(SQL, Conectar);
                SqlDataReader DR = CMD.ExecuteReader();
                if (DR.HasRows)
                {
                    EsLec = true;
                }
                else
                {
                    EsLec = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Conectar.Close();
        }
		
		// Consultas SQL: SELECT, mostrar datos en un DataGridView
        public void Consulta(DataGridView Tabla, string SQL)
        {
            try
            {
                SqlDataAdapter DA = new SqlDataAdapter(SQL, Conectar);
				// SqlDataAdapter DA = new(SQL, Conectar);
                DataTable DT = new DataTable();
				// DataTable DT = new();
                DA.Fill(DT);
                Tabla.DataSource = DT;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
		
		// Consultas SQL: SELECT, rellenar un ComboBox
        public DataTable Rellenar(string SQL)
        {
            DataTable DT = new DataTable();
			// DataTable DT = new();
            try
            {
                SqlDataAdapter DA = new SqlDataAdapter(SQL, Conectar);
				// SqlDataAdapter DA = new(SQL, Conectar);
                DA.Fill(DT);
            }
            catch (Exception ex)
            {
                Conectar.Close();
                MessageBox.Show(ex.Message);
            }
            return DT;
        }
		
		// Consultas SQL: INSERT, UPDATE, DELETE y TRUNCATE TABLE
        public void Operaciones(DataGridView Tabla, string SQL)
        {
            Conectar.Open();
            try
            {
                SqlCommand CMD = new SqlCommand(SQL, Conectar);
				// SqlCommand CMD = new(SQL, Conectar);
                CMD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Conectar.Close();
        }
		
		// Consultas SQL: INSERT, UPDATE y DELETE de imágenes en base de datos
        public void WorkImgs(String SQL, PictureBox Imagen)
        {
            MemoryStream MS = new MemoryStream();
			// MemoryStream MS = new();
            Imagen.Image.Save(MS, Imagen.Image.RawFormat);
            byte[] Imagenes = MS.GetBuffer();
            SqlCommand CMD = new SqlCommand(SQL, Conectar);
			// SqlCommand CMD = new(SQL, Conectar);
            CMD.Parameters.AddWithValue("@Imagen", Imagenes);
            try
            {
                Conectar.Open();
                CMD.ExecuteNonQuery();
                Conectar.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Conectar.Close();
            }
        }
    }
}

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
	public partial class [Form] : Form
	{
		// Variable para acceder a procesos y variables de la clase CRUD
		CRUD CDB = new CRUD();
		/*
		.Net 5.0 en adelante
		CRUD CDB = new();
		*/
		// Variable que almacenan las respectivas consultas SQL
		string SQL;
		
		public [Form]()
        {
            InitializeComponent();
        }
		
		// Probar conexión
		void Probar()
		{
			CDB.Conexion();
		}
		
		// Consulta SQL: CREATE TABLE para tablas en caso de que estas no existan
		void CreTable()
		{
			SQL = "CREATE TABLE IF NOT EXISTS [Tabla] (([Columna 1] [TipoDato](Longitud) [NULL/NOT NULL], [Columna N] [TipoDato](Longitud) [NULL/NOT NULL], ...);";
			Operaciones([DataGridView], SQL);
		}
		
		// Consulta SQL: SELECT en sin cargar datos en DataGridView
		void ReadRegs()
        {
            SQL = "SELECT * FROM [Tabla]";
			CDB.Leer(SQL);
        }
		
		// Consulta SQL: SELECT para cargar datos DataGridView
		void LoadRegs()
		{
			SQL = "SELECT * FROM [Tabla]";
			CDB.Consulta([DataGridView], SQL);
		}
		
		/*
		Consultas SQL: SELECT
		Usar CAST([Columna] AS VARCHAR([Longitud])) para busquedas =/LIKE con valores numéricos
		*                | SELECT * FROM [Tabla]
		[Columna]        | SELECT [Columna] FROM [Tabla]
		=                | SELECT * FROM [Tabla] WHERE ([Columna] = '" & [Dato] & "')"
		LIKE             | SELECT * FROM [Tabla] WHERE ([Columna] LIKE '%" & [Dato] & "%')"
		ASC/DESC         | SELECT * FROM [Tabla] ORDER BY [Columna] ASC/DESC
		COUNT()          | SELECT COUNT(*) FROM [Tabla]
		SUM()          | SELECT SUM([Columna]) FROM [Tabla]
		MAX()/MIN()      | SELECT MAX([Columna]) FROM [Tabla]
		*/
		
		// Rellenar ComboBox
		void RelleCBx()
		{
			SQL = "SELECT [Columna] FROM [Tabla] ORDER BY [Columna] ASC/DESC";
			try
            {
                DataTable DT = new DataTable();
				// DataTable DT = new();
                DT = CDB.Rellenar(SQL);
                [ComboBox].DataSource = DT;
                [ComboBox].DisplayMember = "[Columna]";
                [ComboBox].SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
		}
		
		// Consultas SQL: INSERT, UPDATE, DELETE, TRUNCATE TABLE
		void InUpDeTru()
		{
			// DataGridView
			// Guardar 
			SQL = "INSERT INTO [Tabla] ([Columna 1], [Columna 2],... [Columna N]) SELECT " + [Dato 1] + ", " + [Dato 2] + ",... " + [Dato N];
			// Actualizar
			SQL = "UPDATE [Tabla] SET [Columna 1] = " + [Dato 1] + ", [Columna 2] = " + [Dato 2] + ",... Columna N = " + [Dato N] + " WHERE ([Criterio] = " + [Criterio] + ")";
			// Eliminar
			SQL = "DELETE FROM [Tabla] WHERE ([Criterio]=" + [Criterio] + ")";
			// Vaciar tablas
			SQL = "TRUNCATE TABLE [Tabla]";
			CDB.Operaciones([DataGridView], SQL);
			
			// PictureBox
			// Guardar
			SQL = "INSERT INTO [Tabla] ([Columna 1], [Columna 2],... [Columna N]) VALUES (" + [Dato 1] + ", " + [Dato 2] + ",... " + [Dato N] + ", @Imagen)";
			// Actualizar
			SQL = "UPDATE [Tabla] SET [Columna 1] = " + [Dato 1] + ", [Columna 2] = " + [Dato 2] + ",... Columna N = " + [Dato N] + " [Columna] = @Imagen  WHERE ([Criterio] = " + [Criterio] + ")";
			// Eliminar
			SQL = "DELETE FROM [Tabla] WHERE ([Criterio]=" + [Criterio] + ")";
			WorkImgs(SQL, [PictureBox]);
		}
		
		// Consulta SQL: TRUNCATE TABLE en SQLite
		void TruncarSQLite()
		{
			SQL = "DROP TABLE [Tabla]";
			CDB.Operaciones([DataGridView], SQL);
			SQL = "CREATE TABLE [Tabla] ([Columna 1] [TipoDato](Longitud) [NULL/NOT NULL], [Columna N] [TipoDato](Longitud) [NULL/NOT NULL], ...);";
			CDB.Operaciones([DataGridView], SQL);
		}
	}
}