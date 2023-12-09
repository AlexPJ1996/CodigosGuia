'Librerías
Imports System.Data.SqlClient   'SQL Server
Imports System.Data.OleDb       'Access (2003/2007-2013)
Imports System.Data.SQLite      'SQLite
Imports Devart.Data.SQLite      'SQLite/ADO.NET Devart
Imports MySQL.Data.MySqlClient  'MySQL

Public Class CRUD/ConectarDB
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
	'Provider=Microsoft.ACE.OLEDB.12.0; Data Source=(Ubicación)\[Database].accdb
	'Provider=Microsoft.ACE.OLEDB.12.0; Data Source=(Ubicación)\[Database].accdb; Jet OLEDB:Database Password=[Password]
	'
	'SQLite
	'Data Source=(Ubicación)\[Database].db; Version=3
	'
	'MySQL
	'Server=[Server];Database=[DatabaseB];Uid=[User];Pwd=[Password]
	Dim Cadena As String = "ConecctionString"
    Protected Conectar As New SqlConnection(Cadena) '
    Public EsCon As Boolean
    Public EMess As String
	
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
                EMess = ex.Message
                If MessageBox.Show(EMess, "¿Reintentar conexión?", MessageBoxButtons.YesNo) = vbNo Then
                    End
                End If
            End Try
        End While
    End Sub

    'Proceso para consultas SQL: SELECT, mostrar datos en un DataGridView
	Sub Consulta(ByVal Tabla As DataGridView, ByVal SQL As String)
        Try
            Dim DA As New SqlDataAdapter(SQL, Conectar)
            Dim DT As New DataTable
            DA.Fill(DT)
            Tabla.DataSource = DT
        Catch ex As Exception
            EMess = ex.Message
            MessageBox.Show(EMess)
        End Try
    End Sub

    'Proceso para consultas SQL: SELECT, rellenar un ComboBox
	Function Rellenar(ByVal SQL As String)
        Dim DT As New DataTable
        Try
            Dim DA As New SqlDataAdapter(SQL, Conectar)
            DA.Fill(DT)
        Catch ex As Exception
            EMess = ex.Message
            MessageBox.Show(EMess)
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
            EMess = ex.Message
            MessageBox.Show(EMess)
        End Try
        Conectar.Close()
    End Sub
End Class

'Procesos para consultas SQL en Forms
Public Class [Forms]
    'Variable para llamar los Procesos de la clase 'CRUD/ConectarDB'
	Dim CDB As New CRUD
	'Probar conexión	
	Sub ConTest()	    
		Obj.Conexion()
	End Sub
	
	'Variables que almacenan las respectivas consultas SQL
	Dim SQL As String
	'Proceso para consultas SELECT en DataGridView
	Sub Mostrar()
	    SQL = "SELECT * FROM [Tabla]"
		CDB.Consulta([DataGridView], SQL)
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
    Sub RellCate()
        SQL = "SELECT [Columna] FROM [Tabla] ORDER BY [Columna] ASC/DESC"
        Try
            Dim DT As New DataTable
            DT = CDB.Rellenar(SQL)
            [ComboBox].DataSource = DT
            [ComboBox].DisplayMember = "[Columna]"
            [ComboBox].SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
	
	'Proceso para consultas INSERT
	Private Sub Agregar()
		SQL = "INSERT INTO [Tabla] ([Columna1]), [ColumnaN]) SELECT " & [Dato1] & ", ..." & [DatoN] & ""		
		CDB.Operaciones([DataGridView], SQL)
	End Sub
	'Usar '' para los datos de String: ", " & [Dato1] & "', "
	
	'Proceso para consultas UPDATE
	Private Sub Actualizar()
		SQL = "UPDATE [Tabla] SET [Columna1]=" & [Dato1] & ", [ColumnaN]=" & [DatoN] & " WHERE ([Criterio] = " & [Criterio] & ")"
		CDB.Operaciones([DataGridView], SQL)
	End Sub
	
	'Proceso para consultas DELETE
	Private Sub Eliminar()
		SQL = "DELETE FROM [Tabla] WHERE ([Criterio] = " & [Criterio] & ")"
		CDB.Operaciones([DataGridView], SQL)
	End Sub
	
	'Proceso para consultas TRUNCATE TABLE
	Private Sub Truncar()
		SQL = "TRUNCATE TABLE [Tabla]"
		CDB.Operaciones([DataGridView], SQL)
	End Sub

	'Proceso para consultas TRUNCATE TABLE en SQLite
	Private Sub TruncarSQLite()
		SQL = "DELETE FROM [Tabla]"
		CDB.Operaciones([DataGridView], SQL)
		SQL = "UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME=[Tabla]"
		CDB.Operaciones([DataGridView], SQL)
	End Sub
End Class