Public Class [Forms]
	'Proceso para solo recibir numeros en un TextBox, mediante el evento KeyPress
	Sub OnlyNumbers(ByRef e As System.Windows.Forms.KeyPressEventArgs)
		e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)
    End Sub
	
	'Proceso para mostrar datos de un DataGridView en Label/TextBox
	Sub CellE()
        Try
            [Label/TextBox] = [DataGridView].CurrentRow.Cells([#Columna]).Value
			'Formato para mostrar en un PictureBox una imagen guardada en base de datos
			[PictureBox].Image = [DataGridView].CurrentRow.Cells([#Columna]).FormattedValue
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
	
	'Proceso para cambiar el ancho/nombre de las columnas
	Sub CellZ()
        Try
           [DataGridView].Columns([#Columna]).Width = [Value]
           [DataGridView].Columns([#Columna]).HeaderText = [Text]
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
	
	'Proceso para cambiar los colores de las filas de un DataGridView
    Sub ColorsDGV()
        [DataGridView].RowsDefaultCellStyle.BackColor = Color.[Color]
        [DataGridView].AlternatingRowsDefaultCellStyle.BackColor = Color.[Color]
    End Sub
	
	'Proceso para no permitir modificación de datos en un ComboBox
	Sub PropCB()
        [ComboBox].DropDownStyle = ComboBoxStyle.DropDownList
        [ComboBox].FlatStyle = FlatStyle.Popup
    End Sub
	
	'Proceso para maximizar ventana con FormBorderStyle: None sin cubrir toda la pantalla
    Dim MaxForm As Boolean
    Dim FAnc As String = Width
    Dim FAlt As String = Height
    Sub MaxRestForm()
        If MaxForm <> True Then
            Me.Location = Screen.PrimaryScreen.WorkingArea.Location
            Me.Size = Screen.PrimaryScreen.WorkingArea.Size
            MaxForm = True
        Else
            Width = FAnc
            Height = FAlt
            CenterToScreen()
            MaxForm = False
        End If
    End Sub
	
	'Proceso para captar valores de fecha (yyyy/mm/dd) y hora (hh:mm:ss) de un DateTimePicker
	Dim Fecha As String
	Dim Hora As String
    Sub MyDateTime()
        Fecha = DateAndTime.Year([DateTimePicker].Value) & "/" & DateAndTime.Month([DateTimePicker].Value) & "/" & DateAndTime.Day([DateTimePicker].Value)
        Hora = DateAndTime.Hour([DateTimePicker].Value) & ":" & DateAndTime.Minute([DateTimePicker].Value) & ":" & DateAndTime.Second([DateTimePicker].Value)
    End Sub
	
	'Proceso para configurar un OpenFileDialog en un para cargar una imagen en un PictureBox
	Private Sub [Button]_Click(sender As Object, e As EventArgs) Handles [Button].Click
        Dim OpenFile As New OpenFileDialog
        OpenFile.Filter = "Imagenes JPG|*.jpg|Imagenes JPEG|*.jpeg|Imagenes PNG|*.png"
        OpenFile.Title = "Seleccionar imagen"
        OpenFile.FilterIndex = 1
        OpenFile.RestoreDirectory = True
        If OpenFile.ShowDialog() = DialogResult.OK Then
            [PictureBox].Image = Image.FromFile(OpenFile.FileName)
        End If
    End Sub
	
	'Proceso para limpiar un DataGridView
	Sub ClearDGV()
		[DataGridView].Columns.Clear()
	End Sub
	
	'Proceso para mostrar mensajes multilinea
	Sub MenMulin()
        'MsgBox()
		Dim [Variable] As String = [Linea 1] & vbCrLf & [Linea 2] & vbCrLf & [Linea N]
        MsgBox([Variable])
		
		MsgBox([Linea 1] & vbCrLf & [Linea 2] & vbCrLf & [Linea N])
		
		'MessageBox.Show()
		MessageBox.Show([Variable])	
		If MessageBox.Show([Variable], "", MessageBoxButtons.OKCancel) = vbOK Then
		    
        End If
		
		MessageBox.Show([Linea 1] & vbCrLf & [Linea 2] & vbCrLf & [Linea N])		
		If MessageBox.Show([Linea 1] & vbCrLf & [Linea 2] & vbCrLf & [Linea N], "", MessageBoxButtons.OKCancel) = vbOK Then
		    
        End If
    End Sub
End Class

'Configuración para exportar datos mostrados en un DataGridView a libro de Excel
Imports Microsoft.Office.Interop         'Posible remover en ciertos casos
Imports Microsoft.Office.Interop.Excel
Imports System.IO
Imports System.Diagnostics.Process       'Posible remover en ciertos casos
Module MExportE

    Sub Export(ByVal DGV As DataGridView)
        Dim ExcelApp As New Application With {.Visible = True}
        Dim WorkBook As Workbook = ExcelApp.Workbooks.Add()
        Dim WorkSheet As Worksheet = CType(WorkBook.Sheets(1), Worksheet)
        Dim ColumnCount As Integer = DGV.Columns.Count
        For i As Integer = 0 To ColumnCount - 1
            WorkSheet.Cells(1, i + 1) = DGV.Columns(i).HeaderText
        Next
        Dim RowCount As Integer = DGV.Rows.Count
        For i As Integer = 0 To RowCount - 1
            For j As Integer = 0 To ColumnCount - 1
                WorkSheet.Cells(i + 2, j + 1) = DGV.Rows(i).Cells(j).Value
            Next
        Next
        Dim TempFile As String = Path.GetTempFileName() & ".xls"
        WorkBook.SaveAs(TempFile)
        Process.Start(TempFile)
    End Sub
End Module

'Configuración para permitir arrastrar ventana
Imports System.Runtime.InteropServices
Public Class [Form]
    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture()

    End Sub

    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(ByVal hWnd As System.IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)

    End Sub

    Private Sub [Control]_MouseMove(sender As Object, e As MouseEventArgs) Handles [Control].MouseMove
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub
End Class