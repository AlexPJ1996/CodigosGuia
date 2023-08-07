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
End Class