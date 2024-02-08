'Librerías
Imports System.Data.SqlClient   'SQL Server
Imports System.Data.OleDb       'Access (2003/2007-2013)
Imports System.Data.SQLite      'SQLite
Imports MySqlConnector          'MySqlConnector Nuget Package
Imports System.IO               'Usar MemoryStream

Module CRUD
    'ConecctionString: (https://www.connectionstrings.com/)
	'la cadena de conexión o ConecctionString deberá estar entre las comillas
	'(Directorio) será cambiada por |DataDirectory|\ si es una base de datos local
	'
	'SQL Server LocalDB:
	'Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\[Database].mdf; Integrated Security=True
	'
	'Access
	'2003
	'Provider=Microsoft.Jet.OLEDB.4.0; Data Source=(Ubicación)\[Database].mdb
	'Provider=Microsoft.Jet.OLEDB.4.0; Data Source=(Ubicación)\[Database].mdb; Jet OLEDB:Database Password=[Password]
	'2007-2013
	'Provider=Microsoft.ACE.OLEDB.12.0; Data Source=(Ubicación)\[Database].acCRUD
	'Provider=Microsoft.ACE.OLEDB.12.0; Data Source=(Ubicación)\[Database].acCRUD; Jet OLEDB:Database Password=[Password]
	'
	'SQLite
	'Data Source=(Ubicación)\[Database].db; Version=3
	'
	'MySQL
	'Server=[ServerAddress]; Database=[Database]; Uid=[User]; Pwd=[Password];
	'Server=[ServerAddress]; Port=[#Port000]; Database=[Database]; Uid=[User]; Pwd=[Password];
	Private Cadena As String = "ConnectionString"
    Private Conectar As New SqlConnection(Cadena)
    Public EsCon, EsLec As Boolean
	
    'Iniciar y probar conexión con base de datos
    Sub Conexion()
        EsCon = False
        While EsCon = False
            Try
                Conectar.Open()
                EsCon = True
                Conectar.Close()
            Catch ex As Exception
                EsCon = False
                If MessageBox.Show(ex.Message & vbCrLf & "" & vbCrLf & "¿Desea reintentar la conexión?", "Error", MessageBoxButtons.OKCancel) = vbCancel Then
                    End
                End If
            End Try
        End While
    End Sub
	
	'Proceso para hacer consultas de datos SELECT
	'Utilizable para hacer login con usuario y contraseña almacenada en base de datos
    Sub Leer(ByVal SQL As String)
        Conectar.Open()
        Try
            Dim CMD As New MySqlCommand(SQL, Conectar)
            Dim DR As MySqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows Then
                EsLec = True
            Else
                EsLec = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Conectar.Close()
    End Sub
	
    'Proceso para consultas SQL: SELECT, mostrar datos en un DataGridView
	Sub Consulta(ByVal Tabla As DataGridView, ByVal SQL As String)
        Try
            Dim DA As New SqlDataAdapter(SQL, Conectar)
            Dim DT As New DataTable
            DA.Fill(DT)
            Tabla.DataSource = DT
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
	
    'Proceso para consultas SQL: SELECT, rellenar un ComboBox
	Function Rellenar(ByVal SQL As String)
        Dim DT As New DataTable
        Try
            Dim DA As New SqlDataAdapter(SQL, Conectar)
            DA.Fill(DT)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return DT
    End Function
	
    'Proceso para consultas SQL: INSERT, UPDATE, DELETE y TRUNCATE TABLE
	Sub Operaciones(ByVal Tabla As DataGridView, ByVal SQL As String)
        Conectar.Open()
        Try
            Dim CMD As New SqlCommand(SQL, Conectar)
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Conectar.Close()
    End Sub
	
	'Proceso para Consultas SQL: INSERT, UPDATE de imágenes en base de datos
	Sub WorkImgs(ByVal SQL As String, ByVal Imagen As PictureBox)
        Dim MS As New MemoryStream
        Imagen.Image.Save(MS, Imagen.Image.RawFormat)
        Dim Imagenes() As Byte = MS.GetBuffer
        Dim CMD As New SqlCommand(SQL, Conectar)
        CMD.Parameters.AddWithValue("@Imagen", Imagenes)
        Try
            Conectar.Open()
            CMD.ExecuteNonQuery()
            Conectar.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
			Conectar.Close()
        End Try
    End Sub
End Module

'Procesos para consultas SQL en Forms
Public Class [Forms]
    
	'Probar conexión
	Sub ConTest()	    
		Conexion()
	End Sub
	
	'Crear tablas en caso de que estas no existan
	Sub CreTable()
        SQL = "CREATE TABLE IF NOT EXISTS [Tabla] (([Columna1] [TipoDato](Longitud) [NULL/NOT NULL], [ColumnaN] [TipoDato](Longitud) [NULL/NOT NULL], ...);"
        Operaciones([DataGridView], SQL)
    End Sub
	
	'Variables que almacenan las respectivas consultas SQL
	Dim SQL As String
	'Proceso para consultas SELECT en DataGridView
	Sub Mostrar()
	    SQL = "SELECT * FROM [Tabla]"
		Consulta([DataGridView], SQL)
	End Sub
	'Consultas SQL: SELECT
	'Usar CAST([Columna] AS VARCHAR) para busquedas =/LIKE con valores numéricos
	'*                | SELECT * FROM [Tabla]
	'[Columna]        | SELECT [Columna] FROM [Tabla]
	'=                | SELECT * FROM [Tabla] WHERE ([Columna] = '" & [Dato] & "')"
	'LIKE             | SELECT * FROM [Tabla] WHERE ([Columna] LIKE '%" & [Dato] & "%')"
	'ASC/DESC         | SELECT * FROM [Tabla] ORDER BY [Columna] ASC/DESC
	'COUNT()          | SELECT COUNT(*) FROM [Tabla]
	'COUNT()          | SELECT COUNT([Columna]) FROM [Tabla]
	'MAX()/MIN()      | SELECT MAX([Columna]) FROM [Tabla]
	
	'Proceso para rellenar ComboBox
    Sub RelleCBx()
        SQL = "SELECT [Columna] FROM [Tabla] ORDER BY [Columna] ASC/DESC"
        Try
            Dim DT As New DataTable
            DT = Rellenar(SQL)
            [ComboBox].DataSource = DT
            [ComboBox].DisplayMember = "[Columna]"
            [ComboBox].SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
	
	'Proceso para consultas INSERT
	Private Sub Agregar()
		'Consulta para guardar información en base de datos (ordinarios)
		SQL = "INSERT INTO [Tabla] ([Columna1]), [ColumnaN]) SELECT " & [Dato1] & ", ..." & [DatoN] & ""
		Operaciones([DataGridView], SQL)
		'Consulta para guardar información en base de datos (imágenes)
		SQL = "INSERT INTO [Tabla] ([Columna1]), [ColumnaN]) VALUES (" & [Dato1] & ", ..." & [DatoN] & "', @Imagen)"
		WorkImgs(SQL, [PictureBox])
	End Sub
	'Usar '' para los datos de String: ", " & [Dato1] & "', "
	
	'Proceso para consultas UPDATE
	Private Sub Actualizar()
		'Consulta para actualizar información en base de datos (ordinarios)
		SQL = "UPDATE [Tabla] SET [Columna1]=" & [Dato1] & ", [ColumnaN]=" & [DatoN] & " WHERE ([Criterio] = " & [Criterio] & ")"
		Operaciones([DataGridView], SQL)
		'Consulta para actualizar información en base de datos (imágenes)
		SQL = "UPDATE [Tabla] SET [Columna1]=" & [Dato1] & ", [ColumnaN]=" & [DatoN] & " [Columna]= @Imagen WHERE ([Criterio] = " & [Criterio] & ")"
		WorkImgsSavImg(SQL, [PictureBox])
	End Sub
	
	'Proceso para consultas DELETE
	Private Sub Eliminar()
		SQL = "DELETE FROM [Tabla] WHERE ([Criterio] = " & [Criterio] & ")"
		Operaciones([DataGridView], SQL)
		'Consulta para eliminar información en base de datos (imágenes)
		WorkImgs(SQL, [PictureBox])
	End Sub
	
	'Proceso para consultas TRUNCATE TABLE
	Private Sub Truncar()
		SQL = "TRUNCATE TABLE [Tabla]"
		Operaciones([DataGridView], SQL)
	End Sub

	'Proceso para consultas TRUNCATE TABLE en SQLite
	Private Sub TruncarSQLite()
		SQL = "DROP TABLE [Tabla]"
		Operaciones([DataGridView], SQL)
		SQL = "CREATE TABLE [Tabla] ([Columna1] [TipoDato](Longitud) [NULL/NOT NULL], [ColumnaN] [TipoDato](Longitud) [NULL/NOT NULL], ...);"
		Operaciones([DataGridView], SQL)
	End Sub
End Class