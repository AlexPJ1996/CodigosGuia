'Librerias para conectar con bases de datos:
Imports System.Data.SqlClient   'SQL Server
Imports System.Data.OleDb       'Access (2003/2007-2013)
Imports System.Data.SQLite      'SQLite
Imports MySQL.Data.MySqlClient  'MySQL
'Cambiar "Module/End Module" por "Public Class/End Class"
Public Class CRUD
    '----------------------------------------------------------------------------------------------------
	'Cadena de conexión con base de datos	
	'--la cadena de conexión deberá estar entre las comillas
	'ConecctionString: (https://www.connectionstrings.com/)	
	'----------------------------------------------------------------------------------------------------
	'SQL Server LocalDB
	'-Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\[DB].mdf; Integrated Security=True
	'----------------------------------------------------------------------------------------------------
	'Access 2003
	'-Provider=Microsoft.Jet.OLEDB.4.0; Data Source=(Ubicación)\[DB].mdb
	'-Provider=Microsoft.Jet.OLEDB.4.0; Data Source=(Ubicación)\[DB].mdb; Jet OLEDB:Database Password=[PWD]
	'Access 2007-2013
	'-Provider=Microsoft.ACE.OLEDB.12.0; Data Source=(Ubicación)\[DB].accdb
	'-Provider=Microsoft.ACE.OLEDB.12.0; Data Source=(Ubicación)\[DB].accdb; Jet OLEDB:Database Password=[PWD]
	'--(Ubicación) será cambiada por |DataDirectory|\ si es una base de datos local
	'----------------------------------------------------------------------------------------------------
	'SQLite
	'-Data Source=(Ubicación)\[DB].db; Version=3
	'--(Ubicación) será cambiada por |DataDirectory|\ si es una base de datos local
	'----------------------------------------------------------------------------------------------------
	'MySQL
	'-Server=[Servidor];Database=[DB];Uid=[Usuario];Pwd=[Contraseña]
	'----------------------------------------------------------------------------------------------------
	Dim Cadena As String = ""
	Dim Conectar As New SqlConnection(Cadena)
	Private Con As New SqlConnection(Cadena)
	'---------------------------------------------------------------------------------------------------- 
	'-SqlConnection    - SQL Server
	'-OleDbConnection  - Access (2003/2007-2013)
	'-SQLiteConnection - SQLite
	'-MySQLConnection  - MySQL
	'----------------------------------------------------------------------------------------------------    
	Public EsCon As String
	Public EMess As String
	
	'Probar conexión con base de datos
	Sub Conexion()
        EsCon = "Conectado: No"
        'Instruccion While para intentar establecer conexión con la base de datos
		While EsCon = "Conectado: No"		
            'Instruccion Try para capturar errores
			Try
                'Abrir conexion
				Conectar.Open()
                EsCon = "Conectado: Si"
                MessageBox.Show(EsCon)
				'Cerrar conexion
                Conectar.Close()
            Catch ex As Exception
                EsCon = "Conectado: No"
                EMess = ex.Message
                MessageBox.Show(EMess)
            End Try
        End While
    End Sub
	
	'Crear procedimiento mostrar datos en un DataGridView mediante consultas SELECT
	'Indicar que pida 2 parametros para ejecutarse correctamente (Tabla, SQL)
    Sub Consulta(ByVal Tabla As DataGridView, ByVal SQL As String)
        Try
            'Objeto DateAdapter, pasar los dos parametros (Instruccion, conexión)
            Dim DA As New SqlDataAdapter(SQL, Conectar)
			'----------------------------------------------------------------------------------------------------
			'-SqlDataAdapter    - SQL Server
			'-OleDbDataAdapter  - Access (2003/2007-2013)
			'-MySQLDataAdapter  - MySQL
			'-SQLiteDataAdapter - SQLite
			'----------------------------------------------------------------------------------------------------
            'Crear objeto DataTable que recibe la informacion del DataAdapter
            Dim DT As New DataTable
            'Pasar la informacion del DataAdapter al DataTable mediante la propiedad Fill
            DA.Fill(DT)
            'Mostrar los datos en el DataGridView
            Tabla.DataSource = DT
        Catch ex As Exception
            EMess = ex.Message
			MessageBox.Show(EMess)
        End Try
    End Sub
	
	'Procedimiento para rellenar ComboBox con datos de una tabla	
	Function Rellenar(ByVal SQL As String)
            'Objeto DateAdapter, pasar los dos parametros (Instruccion, conexión)
			Dim DT As New DataTable
        Try
            'Crear objeto DataTable que recibe la informacion del DataAdapter
			Dim DA As New SqlDataAdapter(SQL, Con)
			'----------------------------------------------------------------------------------------------------
			'-SqlDataAdapter    - SQL Server
			'-OleDbDataAdapter  - Access (2003/2007-2013)
			'-MySQLDataAdapter  - MySQL
			'-SQLiteDataAdapter - SQLite
			'----------------------------------------------------------------------------------------------------
            'Mostrar los datos en el DataTable
			DA.Fill(DT)			
        Catch ex As Exception
            EMess = ex.Message
			MessageBox.Show(EMess)
        End Try
		'Retornar resultado obtenido
        Return DT
    End Function

    'Crear procedimiento para consultas INSERT, UPDATE y DELETE
	'Indicar que pida 2 parametros para ejecutarse correctamente (Tabla, SQL)
    Sub Operaciones(ByVal Tabla As DataGridView, ByVal SQL As String)        
        Conectar.Open()        
        Try
            'Crear objeto de tipo Command que almacenara nuestras instrucciones
            'Necesita 2 parametros (Instruccion, conexion)
            Dim CMD As New SqlCommand(SQL, Conectar)
			'----------------------------------------------------------------------------------------------------
			'-SqlCommand    - SQL Server
			'-OleDbCommand  - Access (2003/2007-2013)
			'-MySQLCommand  - MySQL
			'-SQLiteCommand - SQLite
			'----------------------------------------------------------------------------------------------------
            'Ejecutar la instruccion mediante la propiedad ExecuteNonQuery del command
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            EMess = ex.Message
			MessageBox.Show(EMess)
        End Try
        Conectar.Close()
    End Sub	
End Class

'En adelante, todo será úbicado en el código de los respectivos formularios que se desean configurar
Public Class [Forms]
    'Variable para llamar los procedimientos Conexion(), Consulta() y Operaciones() del modulo/clase CRUD
	Dim Obj As New CRUD
	
	'----------------------------------------------------------------------------------------------------		
	'Se 'Crea la variable que guardar la consulta SQL
	'-Dim SQL As String = ""
	'--La consulta deberá ir dentro de las comillas
	'Mediante la variable global "Obj" se Accede al procedimientos, y se pasan los 2 parametros ([DataGridView], SQL)
	'-Obj.Conexion()                       - Acceder a Conexion(), y probar conexión con base de datos
	'-Obj.Consulta([DataGridView], SQL)    - Acceder a Consulta(), y realizar consultas SELECT
	'-Obj.Operaciones([DataGridView], SQL) - Acceder a Operaciones(), y realizar consultas Insert, UPDATE y DELETE
	'----------------------------------------------------------------------------------------------------
	Private Sub ConTest()	    
		Obj.Conexion()
	End Sub
	
	Private Sub Mostrar()		
		Dim SQL As String = "SELECT * FROM [Tabla]"        
        Obj.Consulta([DataGridView], SQL)
		'----------------------------------------------------------------------------------------------------
		'-SELECT (*)      - SELECT * FROM [Tabla]
		'-SELECT (=)      - SELECT * FROM [Tabla] WHERE ([Columna] = " + [Dato] + ")"
		'-SELECT LIKE     - SELECT * FROM [Tabla] WHERE ([Columna] LIKE '%" + [Dato] + "%')"
		'-SELECT ASC/DESC - SELECT * FROM [Tabla] ORDER BY [Columna] ASC/DESC
		'----------------------------------------------------------------------------------------------------
	End Sub	
	'Seran necesarios '' para los datos de tipo texto (... SELECT '" & [Dato1] & "', "...)
	Private Sub Agregar()
		Dim SQL As String = "INSERT INTO [Tabla] ([Columna1]), [Columna2], ...[ColumnaN]) SELECT " & [Dato1] & ", " & [Dato2] & ", ..." & [DatoN] & ""		
		Obj.Operaciones([DataGridView], SQL)
	End Sub
	
	Private Sub Actualizar()
		Dim SQL As String = "UPDATE [Tabla] SET [Columna1]=" & [Dato1] & ", [Columna2]=" & [Dato2] & ", ...[ColumnaN]=" & [DatoN] & " WHERE ([Criterio]=" & [Criterio] & ")"
		Obj.Operaciones([DataGridView], SQL)
	End Sub
		
	Private Sub Eliminar()
		Dim SQL As String = "DELETE FROM [Tabla] WHERE ([Criterio] = " & [Criterio] & ")"
		Obj.Operaciones([DataGridView], SQL)
	End Sub
	
	'Rellenar ComboBox con datos almacenados en una tabla
	Sub RellenarCB()
	    'Crear variable que guardará la consulta SQL
        Dim SQL As String = "SELECT [Columna] FROM [Tabla]"
        'Crear DataTable que recibirá los datos de la consulta
        Dim DT As New DataTable
        'Asignar objeto a DataTable y llamar función a la que se le asignó la consulta SQL
        DT = OBJ.Rellenar(SQL)
        'Asignar origen de datos del ComboBox con su propiedad (DataSource) con el DataTable que almacenó los valores obtenidos por el procedimiento
        [ComboBox].DataSource = DT
        'Indicar al ComboBox la columna que mostrará
        [ComboBox].DisplayMember = "[Columna]"
        'Indicamos muestre el primer valor de la columna
        [ComboBox].SelectedIndex = 0
	End Sub
End Class