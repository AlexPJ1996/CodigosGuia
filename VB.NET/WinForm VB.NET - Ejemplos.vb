Public Class Ejemplos
    
	'Procedimiento para solo recibir numeros en un TextBox, mediante el evento KeyPress
	Sub MyKeyPress(ByRef e As System.Windows.Forms.KeyPressEventArgs)
		e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)
    End Sub
	
	'Procedimiento para mostrar datos de un DataGridView en Label/TextBox
	Sub CellE()
        Try
            [Label/TextBox] = [DataGridView].CurrentRow.Cells([Columna]).Value
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
	
	'Procedimiento para cambiar el ancho/nombre de las columnas
	Sub CellZ()
        Try
           [DataGridView].Columns([Columna]).Width = [Value]
           [DataGridView].Columns([Columna]).HeaderText = [Text]
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
	
	'Procedimiento para mostrar texto de un Label/TextBox con formato moneda
	'Configurar a un evento del tipo Click para evitar errores en ejecución
	Sub Moneda()
	    [Label/TextBox] = FormatCurrency(([Label/TextBox]), "$ 0")
		[Label/TextBox] = Format([Label/TextBox], "$ 0")
	End Sub
	
	'Procedimiento para no permitir modificación de datos en un ComboBox
	Sub PropCB()
        [ComboBox].DropDownStyle = ComboBoxStyle.DropDownList
        [ComboBox].FlatStyle = FlatStyle.Popup
    End Sub

    'Procedimiento para maximizar ventana con FormBorderStyle: None sin cubrir toda la pantalla
    Dim MaxForm As Boolean
    Dim FAnc As String = Width
    Dim FAlt As String = Height
    Private Sub MaxRestForm()
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
End Class

'Configuración para permitir arrastrar ventana
Imports System.Runtime.InteropServices
Public Class
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